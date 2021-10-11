using Microsoft.Data.Sqlite;
using System.Data;
using System.IO;

namespace Stream_FileFromBlob_Sqlite
{
    public static class FileSqliteAdapter
    {
        public static void SaveBlob(
            this SqliteConnection connection,
            string tableName,
            string blobColumnName,
            string fileNameColumnName,
            long id,
            string destinationFolder
            )
        {
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            string path = null;
            using (SqliteCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = $"SELECT {fileNameColumnName} FROM {tableName} WHERE Id = {id};";

                string fileName = (string)cmd.ExecuteScalar();
                path = Path.Combine(destinationFolder, fileName);
            }

            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            using (SqliteBlob sb = new SqliteBlob(connection, tableName, blobColumnName, id))
            {
                sb.CopyTo(fs);
            }
        }
    }

    class Program
    {
        static void Main()
        {
            using (SqliteConnection connection = new SqliteConnection(@"data source = E:\test.db"))
            {
                connection.SaveBlob("files", "Data", "Name", 37, @"C:\Users\Diman\Desktop");
            }
        }
    }
}
