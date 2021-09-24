using System;
using System.Threading;

namespace Mutex_2Thread
{
    class Program
    {
        static void Main()
        {
            Thread th1 = new Thread(SingletonMethod);
            Thread th2 = new Thread(SingletonMethod);
            Thread th3 = new Thread(SingletonMethod);

            th1.Start();

            while (th1.ThreadState != ThreadState.Running) ;

            th2.Start();

            th1.Join();
            th2.Join();

            Console.ReadKey();
        }

        public static void SingletonMethod()
        {
            Console.Write($"THREAD {Thread.CurrentThread.ManagedThreadId}: ");
            using (Mutex mutex = new Mutex(false, "my singleton method"))
            {
                bool isAnotherInstanceOpen = !mutex.WaitOne(TimeSpan.Zero);
                if (isAnotherInstanceOpen)
                {
                    Console.WriteLine("An instance of this entity already exists.");
                    return;
                }

                Console.WriteLine("Hello World!");
                Thread.Sleep(TimeSpan.FromSeconds(3));
            }
        }
    }
}
