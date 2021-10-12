using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Data;
using System.IO;

namespace Stream_FileFromBlob_Oracle
{
    public static class FileOracleAdapter
    {
        public static void SaveBlob(
            this OracleConnection connection,
            string tableName,
            string blobColumnName,
            string fileNameColumnName,
            int idValue,
            string folder
            )
        {
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            using (OracleCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = $"SELECT ID, {blobColumnName}, {fileNameColumnName} FROM {tableName} WHERE ID = {idValue}";

                using (OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (!dr.Read())
                    {
                        return;
                    }

                    string path = Path.Combine(folder, dr.GetString(fileNameColumnName));

                    using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                    using (OracleBlob blob = dr.GetOracleBlob(dr.GetOrdinal(blobColumnName)))
                    {
                        blob.CopyTo(fs);
                    }
                }
            }
        }
    }

    class Program
    {
        static void Main()
        {
            string connectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.182.130)(PORT=1521))(CONNECT_DATA=(SID=orcl)));User Id=orcl_u;Password=orcl_u";
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.SaveBlob("FILE_STORE", "DATA", "NAME", 11, @"C:\Users\Diman\Desktop");
            }
        }
    }
}
