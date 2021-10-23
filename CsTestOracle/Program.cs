using Oracle.ManagedDataAccess.Client;
using System;
using System.IO;

namespace CsTestOracle
{
    /// <summary>
    /// ВМ с Oracle должно быть с NAT-адаптером.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            foreach (string cs in args)
            {
                using (OracleConnection connection = new OracleConnection(cs))
                {
                    Console.WriteLine("\nCS: " + connection.ConnectionString);
                    try
                    {
                        connection.Open();

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("A connection was successfully established.");
                    }
                    catch (Exception e)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(e.Message);
                        Console.WriteLine(e.StackTrace);
                    }
                }
            }

            Console.ReadKey();
        }
    }
}
