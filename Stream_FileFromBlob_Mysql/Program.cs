using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.IO;
using System.Linq;

namespace Stream_FileFromBlob_Mysql
{
    public static class FileSqliteAdapter
    {
        public static void SaveBlob(
            this MySqlConnection connection,
            string tableName,
            string blobColumnName,
            string fileNameColumnName,
            string fileSizeColumnName,
            long id,
            string destinationFolder
            )
        {
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            string path = null;
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = $"SELECT {fileNameColumnName} FROM {tableName} WHERE Id = {id};";

                string fileName = (string)cmd.ExecuteScalar();
                path = Path.Combine(destinationFolder, fileName);
            }

            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = $"SELECT {blobColumnName}, {fileSizeColumnName} FROM {tableName} WHERE Id = {id};";

                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    if (!dr.HasRows)
                    {
                        return;
                    }

                    dr.Read();

                    int fileSize = dr.GetInt32(dr.GetOrdinal(fileSizeColumnName));
                    int containerSize = 4096;
                    byte[] container = new byte[containerSize];
                    long bufferOffset = 0;
                    int bufferSize = 0;
                    
                    while (bufferOffset < fileSize)
                    {
                        bufferSize = (int)dr.GetBytes(0, bufferOffset, container, 0, container.Length);
                        fs.Write(container.Take(bufferSize).ToArray(), 0, bufferSize);
                        bufferOffset += bufferSize;

                        double prc = Math.Round((double)bufferOffset / fileSize * 100, 2);
                        Console.WriteLine($"{prc}%");
                    }
                }
            }
        }
    }

    class Program
    {
        static void Main()
        {
            string connectionString = $"Data Source=localhost;Port=3306;Database=test;User Id=root;Password=Dm24119320***";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.SaveBlob("files", "Data", "Name", "Size", 17, @"C:\Users\Diman\Desktop");
            }

            Console.ReadKey();
        }
    }
}
