using System;
using System.Threading;

namespace Thread_Semaphore_2Owners
{
    class Program
    {
        static Semaphore sem = new Semaphore(2, 2);

        static void Main()
        {
            Thread[] ths = new Thread[5];

            for (int i = 0; i < ths.Length; i++)
            {
                string thIndicator = i.ToString();

                ths[i] = new Thread(() =>
                {
                    sem.WaitOne();

                    for (int j = 0; j < 10; j++)
                    {
                        Console.Write(thIndicator);
                        Thread.Sleep(100);
                    }

                    sem.Release();
                });

                ths[i].Start();
            }

            for (int i = 0; i < ths.Length; i++)
            {
                ths[i].Join();
            }

            Console.ReadKey();
            //01010101010101010101232323322323323232324444444444
        }
    }
}
