using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Tor_TryConnection
{
    class Program
    {
        /// <summary>
        /// Проверяет соединение с Tor.
        /// 
        /// УСТАНОВКА.
        /// 
        /// Во "Включение или отключение компонентов Windows" включить Telnet Client.
        /// 
        /// Скопировать в папку с tor.exe файл torrc-defaults.
        /// В cmd.exe перейти в папку с tor.exe 
        /// (cd "C:\Users\Diman\Desktop\Tor Browser\Browser\TorBrowser\Tor").
        /// cmd.exe: tor.exe --hash-password "пароль" | more
        /// 
        /// Добавить в torrc-defaults 2 строки:
        /// ControlPort 9151
        /// HashedControlPassword 16:[хэш пароля]
        /// 
        /// cmd.exe: tor.exe -f .\torrc-defaults
        /// cmd.exe(2): telnet localhost 9151
        /// В cmd.exe(2) пустой экран.
        /// 
        /// После смены пароля выйти из системы.
        /// </summary>
        /// <returns>true, если соединение устанавливается.</returns>
        public static bool TryConnection(string password, out string error)
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9151);

            using (Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                try
                {
                    server.Connect(endPoint);
                }
                catch (Exception e)
                {
                    error = e.Message;
                    return false;
                }                

                // Аутентификация.
                server.Send(Encoding.ASCII.GetBytes($"AUTHENTICATE \"{password}\"" + Environment.NewLine));
                byte[] data = new byte[1024];
                int receivedDataLength = server.Receive(data);
                string receivedData = Encoding.ASCII.GetString(data, 0, receivedDataLength);
                if (!receivedData.StartsWith("250 OK"))
                {
                    error = "Аутентификация не пройдена.";
                    return false;
                }

                // Запрос новой Identity.
                server.Send(Encoding.ASCII.GetBytes("SIGNAL NEWNYM" + Environment.NewLine));
                data = new byte[1024];
                receivedDataLength = server.Receive(data);
                receivedData = Encoding.ASCII.GetString(data, 0, receivedDataLength);

                if (!receivedData.Contains("250 OK"))
                {
                    server.Shutdown(SocketShutdown.Both);
                    error = "Не удалось получить новую личность.";
                    return false;
                }
            }

            error = null;
            return true;
        }

        static void Main()
        {
            //cd "C:\Users\Diman\Desktop\Tor Browser\Browser\TorBrowser\Tor"
            //tor.exe --hash-password "Dm2411" | more
            //tor.exe -f .\torrc-defaults

            string error;
            if (!TryConnection("Dm2411", out error))
            {
                Console.WriteLine(error);
            }
            else
            {
                Console.WriteLine("Работает!");
            }

            Console.ReadKey();
        }
    }
}
