using System;
using System.Threading;

namespace Thread_Mutex_initiallyOwned
{
    class SharedRes
    {
        public static int Count = 0;
        public static Mutex Mutex;
    }

    class Program
    {
        static void Main()
        {
            SharedRes.Mutex = new Mutex(true);
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} >> Создан и получен Mutex.");

            Thread incTh = new Thread(() => Inc(1));
            Thread decTh = new Thread(() => Inc(-1));

            incTh.Start();
            Thread.Sleep(10);
            decTh.Start();
            Thread.Sleep(10);

            Console.Write($"Thread {Thread.CurrentThread.ManagedThreadId} >> ");
            for (int i = 0; i < 5; i++)
            {
                Console.Write(".");
                Thread.Sleep(1000);
            }

            SharedRes.Mutex.ReleaseMutex();
            Console.WriteLine($"\nThread {Thread.CurrentThread.ManagedThreadId} >> Освобожден Mutex.");

            incTh.Join();
            decTh.Join();
            Console.ReadKey();

            //Thread 1 >> Создан и получен Mutex.
            //Thread 3 >> Бесконечное ожидание Mutex.
            //Thread 4 >> Бесконечное ожидание Mutex.
            //Thread 1 >> .....
            //Thread 1 >> Освобожден Mutex.
            //Thread 3 >> Получен Mutex.
            //1
            //2
            //3
            //4
            //5
            //Thread 3 >> Освобожден Mutex.
            //Thread 4 >> Получен Mutex.
            //4
            //3
            //2
            //1
            //0
            //Thread 4 >> Освобожден Mutex.

        }

        static void Inc(int dx)
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} >> Бесконечное ожидание Mutex.");
            SharedRes.Mutex.WaitOne();
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} >> Получен Mutex.");

            for (int i = 0; i < 5; i++)
            {
                SharedRes.Count += dx;
                Console.WriteLine(SharedRes.Count);
                Thread.Sleep(500);
            }

            SharedRes.Mutex.ReleaseMutex();
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} >> Освобожден Mutex.");
        }
    }
}
