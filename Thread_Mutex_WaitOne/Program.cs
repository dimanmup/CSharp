using System;
using System.Threading;

namespace Thread_Mutex_WaitOne
{
    class SharedRes
    {
        public static int Count = 0;
        public static Mutex Mutex = new Mutex();
    }

    class Program
    {
        static void Main()
        {
            Thread incTh = new Thread(() => Inc(1));
            Thread decTh = new Thread(() => Inc(-1));

            incTh.Start();
            Thread.Sleep(10);
            decTh.Start();

            incTh.Join();
            decTh.Join();
            Console.ReadKey();

            //Thread 3 >> Бесконечное ожидание Mutex.
            //Thread 3 >> Получен Mutex.
            //1
            //Thread 4 >> Бесконечное ожидание Mutex.
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
