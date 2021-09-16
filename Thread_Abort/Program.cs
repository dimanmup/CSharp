using System;
using System.Threading;

namespace Thread_Abort
{
    class Program
    {
        static Thread aborted = new Thread(Aborted);
        static Thread aborter = new Thread(Aborter);

        static void Main()
        {
            aborted.Start();
            aborter.Start();
            aborted.Join();

            Console.Write($"aborted (from thread {Thread.CurrentThread.ManagedThreadId}).");
            Console.ReadKey();

            //started (from thread 3).*********aborted (from thread 1).
        }

        static void Aborted()
        {
            Console.Write($"started (from thread {Thread.CurrentThread.ManagedThreadId}).");

            while (true)
            {
                Thread.Sleep(200);
                Console.Write('*');
            }
        }

        static void Aborter()
        {
            Thread.Sleep(TimeSpan.FromSeconds(2));
            aborted.Abort();
        }
    }
}
