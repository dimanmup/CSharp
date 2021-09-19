using System;
using System.IO;
using System.Threading;

namespace Stream_StreamReader_2Threads
{
    class Program
    {
        static Random rnd = new Random();

        static void Main()
        {
            Thread reading1 = new Thread(Readind);
            Thread reading2 = new Thread(Readind);

            reading1.Start();
            reading2.Start();

            reading1.Join();
            reading2.Join();

            Console.ReadKey();
        }

        static void Readind()
        {
            StreamReader sr = new StreamReader(@"C:\Users\Diman\Desktop\numbers.txt");

            int b = sr.Read();
            while (b != -1)
            {
                Thread.Sleep(rnd.Next(0, 1000));
                Console.Write((char)b);
                b = sr.Read();
            }
        }
    }
}
