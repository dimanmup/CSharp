using System;
using System.Threading;

namespace Thread_Lock_AsyncWriting
{
    class Program
    {
        static Random rnd = new Random();
        static string res = "";
        static int threadNumber = 5;

        static void Main()
        {
            for (int i = 0; i < threadNumber; i++)
            {
                Thread writer = new Thread(WriteAZ);
                writer.Start();
            }

            while (threadNumber != 0) ;
            Console.WriteLine(res);
            Console.ReadKey();
            //ABACABAABBBDCECCCDF DEDEF EDF F EF
        }

        static void WriteAZ()
        {
            for (byte i = (byte)'A'; i <= (byte)'F'; i++)
            {
                Thread.Sleep(rnd.Next(200));
                res += (char)i;
            }

            res += " ";
            threadNumber--;
        }
    }
}
