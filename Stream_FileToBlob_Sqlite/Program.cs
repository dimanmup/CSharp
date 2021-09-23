using Microsoft.Data.Sqlite;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Stream_FileToBlob_Sqlite
{
    public static class FileSqliteAdapter
    {
        public static void CopyToBlob(
            this FileStream source,
            SqliteConnection connection,
            string tableName,
            string blobColumnName,
            string fileNameColumnName,
            string fileName,
            string fileSizeColumnName
            )
        {
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            long id;

            using (SqliteCommand cmd = connection.CreateCommand())
            {
                // Длина BLOB не изменяется,
                // поэтому задается при инициализации.
                cmd.CommandText = $"INSERT INTO {tableName} ({blobColumnName}, {fileNameColumnName}, {fileSizeColumnName}) VALUES (zeroblob({source.Length}), \"{fileName}\", {source.Length});";

                // Чтобы вернуть Id для текущего соединения.
                cmd.CommandText += "SELECT Last_Insert_Rowid();";

                id = (long)cmd.ExecuteScalar();
            }

            using (SqliteBlob sb = new SqliteBlob(connection, tableName, blobColumnName, id))
            {
                source.CopyTo(sb);
            }
        }

        public static void Truncate(
            this SqliteConnection connection,
            string tableName)
        {
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            using (SqliteCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = $"DELETE FROM {tableName};";
                cmd.ExecuteNonQuery();
            }
        }
    }

    class Program
    {
        static void Main()
        {
            using (SqliteConnection connection = new SqliteConnection(@"data source = E:\test.db"))
            {
                Console.WriteLine("Загрузка...");
                Stopwatch sw = new Stopwatch();
                sw.Start();

                using (FileStream fs = new FileStream(@"E:\Docs\.NET\Троелсен (2018).pdf", FileMode.Open))
                {
                    string name = fs.Name.Split('\\').Last();
                    fs.CopyToBlob(connection, "files", "Data", "Name", name, "Size");
                }

                sw.Stop();
                Console.WriteLine($"Успех, время: {sw.Elapsed.TotalSeconds} секунд.");
                Console.WriteLine("После нажатия любой клавиши таблица очистится.");
                Console.ReadKey();

                connection.Truncate("files");
            }
        }
    }
}
