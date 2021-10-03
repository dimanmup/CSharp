using System;
using System.Threading;

namespace Thread_Mutex_WaitOneTimeout
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

            //Thread 3 >> Ожидание Mutex в течение 2с.
            //Thread 3 >> Получен Mutex.
            //1
            //Thread 4 >> Ожидание Mutex в течение 2с.
            //2
            //3
            //4
            //Thread 4 >> Не дождался освобождения Mutex.
            //5
            //6
            //7
            //8
            //9
            //10
            //Thread 3 >> Освобожден Mutex.
        }

        static void Inc(int dx)
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} >> Ожидание Mutex в течение 2с.");

            if (!SharedRes.Mutex.WaitOne(TimeSpan.FromSeconds(2)))
            {
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} >> Не дождался освобождения Mutex.");
                return;
            }

            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} >> Получен Mutex.");

            for (int i = 0; i < 10; i++)
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
