using Ionic.Zip;
using Ionic.Zlib;
using Microsoft.Data.Sqlite;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Stream_FileToBlobAsZip_Sqlite
{
    public static class FileSqliteAdapter
    {
        public static void CopyToBlobWithCompression(
            this FileStream source,
            SqliteConnection connection,
            string tableName,
            string blobColumnName,
            string fileNameColumnName,
            string fileName,
            string fileSizeColumnName,
            CompressionLevel compressionLevel = CompressionLevel.BestCompression,
            CompressionMethod compressionMethod = CompressionMethod.Deflate
            )
        {
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            string zipName = fileName + ".zip";
            long id;

            // Длина BLOB не изменяется, поэтому задается при инициализации.
            // Инициализировать нужно с длиной zip, иначе лишние нули в конце BLOB испортят архив.

            using (FileStream fs = new FileStream(zipName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read))
            {
                using (ZipOutputStream zo = new ZipOutputStream(fs, true))
                {
                    zo.PutNextEntry(fileName);
                    zo.CompressionLevel = compressionLevel;
                    zo.CompressionMethod = compressionMethod;
                    source.CopyTo(zo);
                }

                long zipSize = fs.Length;
                fs.Seek(0, SeekOrigin.Begin);

                // Инициализация BLOB с длиной архива.
                using (SqliteCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"INSERT INTO {tableName} ({blobColumnName}, {fileNameColumnName}, {fileSizeColumnName}) VALUES (zeroblob({source.Length}), \"{zipName}\", {zipSize});";

                    // Чтобы вернуть Id для текущего соединения.
                    cmd.CommandText += "SELECT Last_Insert_Rowid();";

                    id = (long)cmd.ExecuteScalar();
                }

                using (SqliteBlob sb = new SqliteBlob(connection, tableName, blobColumnName, id))
                {
                    fs.CopyTo(sb);
                }
            }

            File.Delete(zipName);
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
                    fs.CopyToBlobWithCompression(connection, "files", "Data", "Name", name, "Size");
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
