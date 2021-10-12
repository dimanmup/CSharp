using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data;
using System.IO;
using System.Linq;

namespace Stream_FileToBlob_Oracle
{
    public static class FileOracleAdapter
    {
        public static void CopyToBlob(
            this FileStream source,
            OracleConnection connection,
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

            string idString;

            using (OracleCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = $"INSERT INTO {tableName} ({fileNameColumnName}, {fileSizeColumnName}, {blobColumnName}) VALUES ('{fileName}', {source.Length}, :P_EMPTY_BLOB) returning ID INTO :P_ID";
                OracleParameter pId = new OracleParameter("P_ID", OracleDbType.Int32, ParameterDirection.ReturnValue);
                OracleParameter pEmptyBlob = new OracleParameter("P_EMPTY_BLOB", OracleDbType.Blob, ParameterDirection.Input);
                pEmptyBlob.Value = new byte[0];
                cmd.Parameters.Add(pEmptyBlob);
                cmd.Parameters.Add(pId);
                cmd.ExecuteNonQuery();
                idString = pId.Value.ToString();
            }

            using (OracleCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = $"SELECT ID, {blobColumnName} FROM {tableName} WHERE ID = {idString}";

                using (OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (!dr.Read())
                    {
                        return;
                    }

                    using (OracleBlob blob = dr.GetOracleBlobForUpdate(dr.GetOrdinal(blobColumnName)))
                    {
                        source.CopyTo(blob);
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
                using (FileStream fs = new FileStream(@"E:\Docs\.NET\Троелсен (2018).pdf", FileMode.Open))
                {
                    string name = fs.Name.Split('\\').Last();
                    fs.CopyToBlob(connection, "FILE_STORE", "DATA", "NAME", name, "DATA_SIZE");
                }
            }
        }
    }
}
