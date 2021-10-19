using System;
using System.Diagnostics;
using System.Security;

namespace Process_StartDomain
{
    /// <summary>
    /// .NET Framework 3.5 для Windows 7.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = args[0];
            string domain = args[1];
            string userName = args[2];
            string password = args[3];
            string arguments = args[4] + " " + args[5];

            Console.WriteLine();
            Console.WriteLine($"File Path: {filePath}");
            Console.WriteLine($"Domain: {domain}");
            Console.WriteLine($"User Name: {userName}");
            Console.WriteLine($"Password: {password}");
            Console.WriteLine($"Arguments: {arguments}");

            SecureString securePassword = new SecureString();
            for (int i = 0; i < password.Length; i++)
            {
                securePassword.AppendChar(password[i]);
            }

            Process p = new Process();
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = filePath;
            psi.Arguments = arguments;
            psi.Domain = domain;
            psi.UserName = userName;
            psi.Password = securePassword;
            psi.UseShellExecute = false;
            p.StartInfo = psi;

            try
            {
                p.Start();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Успех");

                p.WaitForExit();
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
        }
    }
}
