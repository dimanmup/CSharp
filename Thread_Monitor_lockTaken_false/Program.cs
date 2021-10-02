using System;
using System.Threading;

namespace Thread_Monitor_lockTaken_false
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

            //Thread id: 3, lock taken: True
            //Thread id: 4, lock taken: True
            //Thread id: 5, lock taken: False
            //Thread id: 6, lock taken: False
            //Thread id: 7, lock taken: False
            //ABCDEF ABCDEF

        }

        static void WriteAZ()
        {
            bool lockTaken = false;
            Monitor.TryEnter(typeof(Program), 1000, ref lockTaken);
            Console.WriteLine($"Thread id: {Thread.CurrentThread.ManagedThreadId}, lock taken: {lockTaken}");

            if (lockTaken)
            {
                for (byte i = (byte)'A'; i <= (byte)'F'; i++)
                {
                    Thread.Sleep(rnd.Next(200));
                    res += (char)i;
                }
                res += " ";
                Monitor.Exit(typeof(Program));
            }

            threadNumber--;
        }
    }
}
