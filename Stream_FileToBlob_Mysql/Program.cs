using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.IO;

namespace Stream_FileToBlob_Mysql
{
    public static class FileMysqlAdapter
    {
        public static bool CopyToBlob(
            this FileStream source,
            MySqlConnection connection,
            string tableName,
            string blobColumnName,
            string fileNameColumnName,
            string fileSizeColumnName,
            out string errorMessage
            )
        {
            Console.WriteLine($"Загрузка файла {source.Name}...");

            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            // Файлы могут загружаться только из пути в ситемной переменной secure_file_priv.
            string uploadingFolder;
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = "SELECT @@global.secure_file_priv;";
                uploadingFolder = (string)cmd.ExecuteScalar();
            }

            // Копирование в папку secure_file_priv.
            string fileName = Path.GetFileName(source.Name);
            string uploadingPath = Path.Combine(uploadingFolder, fileName);
            File.Copy(source.Name, uploadingPath, true);

            string uploadingPathForQuery = uploadingPath.Replace('\\', '/');
            bool uploadingAvailable = false;

            using (MySqlCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = $"SELECT load_file(\"{uploadingPathForQuery}\") is null;";
                uploadingAvailable = (long)cmd.ExecuteScalar() != 1;
            }

            if (!uploadingAvailable)
            {
                errorMessage = $"Файл \"{source.Name}\" невозможно загрузить.";

                using (MySqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT @@global.max_allowed_packet;";
                    long maxAllowedPacket = (long)(ulong)cmd.ExecuteScalar();

                    if (source.Length > maxAllowedPacket)
                    {
                        errorMessage += $"\nРазмер файла ({source.Length}) превышает допустимый ({maxAllowedPacket}).";
                    }
                }

                File.Delete(uploadingPath);
                return false;
            }

            using (MySqlCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = $"INSERT INTO {tableName} ({fileNameColumnName}, {fileSizeColumnName}, {blobColumnName}) VALUES ('{fileName}', {source.Length}, LOAD_FILE('{uploadingPathForQuery}'));";
                cmd.ExecuteNonQuery();
            }

            File.Delete(uploadingPath);
            ColorConsole.WriteLineSuccess($"Файл \"{source.Name}\" загружен.");
            errorMessage = null;
            return true;
        }
    }

    class Program
    {
        static void Main()
        {
            string connectionString = $"Data Source=localhost;Port=3306;Database=test;User Id=root;Password=Dm24119320***";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string error;

                using (FileStream fs = new FileStream(@"E:\Docs\.NET\Рихтер (2013).pdf", FileMode.Open))
                {
                    if (!fs.CopyToBlob(connection, "files", "Data", "Name", "Size", out error))
                    {
                        ColorConsole.WriteLineError(error);
                    }
                }

                Console.WriteLine();

                using (FileStream fs = new FileStream(@"E:\Docs\Kafka\Cloudera, Apache Kafka Guide.pdf", FileMode.Open))
                {
                    if (!fs.CopyToBlob(connection, "files", "Data", "Name", "Size", out error))
                    {
                        ColorConsole.WriteLineError(error);
                    }
                }

                Console.ReadKey();

                //Загрузка файла E:\Docs\.NET\Рихтер (2013).pdf...
                //Файл "E:\Docs\.NET\Рихтер (2013).pdf" невозможно загрузить.
                //Размер файла (6569667) превышает допустимый (4194304).

                //Загрузка файла E:\Docs\Kafka\Cloudera, Apache Kafka Guide.pdf...
                //Файл "E:\Docs\Kafka\Cloudera, Apache Kafka Guide.pdf" загружен.
            }
        }
    }

    class ColorConsole
    {
        public static void WriteLineSuccess(string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void WriteLineError(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
