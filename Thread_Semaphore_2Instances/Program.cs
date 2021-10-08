using System;
using System.Threading;

namespace Thread_Semaphore_2Instances
{
    class Program
    {
        const string SemaphoreName = "my sem";
        static Semaphore mySemaphore;

        static void Main()
        {
            Thread[] ths = new Thread[3];

            for (int i = 0; i < ths.Length; i++)
            {
                ths[i] = new Thread(() => TwineMethod());
                ths[i].Start();
                Thread.Sleep(200);
            }

            for (int i = 0; i < ths.Length; i++)
            {
                ths[i].Join();
            }

            Console.ReadKey();
            //Thread 3 >> Не нашел семафор.
            //Thread 3 >> Создал семафор.
            //Thread 3 >> Получил разрешение.
            //Thread 4 >> Нашел семафор.
            //Thread 4 >> Получил разрешение.
            //Thread 5 >> Нашел семафор.
            //Thread 5 >> Не получил разрешение, 2 экземпляра уже запущены!
        }

        public static void TwineMethod()
        {
            bool createdNew = false;
            mySemaphore = new Semaphore(2, 2, SemaphoreName, out createdNew);

            if (createdNew)
            {
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} >> Не нашел семафор.");                
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} >> Создал семафор.");
            }
            else
            {
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} >> Нашел семафор.");
            }

            if (!mySemaphore.WaitOne(TimeSpan.Zero))
            {
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} >> Не получил разрешение, 2 экземпляра уже запущены!");
                return;
            }

            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} >> Получил разрешение.");

            Thread.Sleep(TimeSpan.FromSeconds(5));

            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} >> Вернул разрешение.");
        }
    }
}
