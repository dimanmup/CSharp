using System;
using System.Threading;

namespace Thread_AbortPar
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
            Console.ReadKey();

            //started (from thread 3).*********aborted (from thread 4).
        }

        static void Aborted()
        {
            try
            {
                Console.Write($"started (from thread {Thread.CurrentThread.ManagedThreadId}).");

                while (true)
                {
                    Thread.Sleep(200);
                    Console.Write('*');
                }
            }
            catch (ThreadAbortException e)
            {
                Console.Write(e.ExceptionState);
            }            
        }

        static void Aborter()
        {
            Thread.Sleep(TimeSpan.FromSeconds(2));
            aborted.Abort($"aborted (from thread {Thread.CurrentThread.ManagedThreadId}).");
        }
    }
}
