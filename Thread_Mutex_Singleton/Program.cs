using System;
using System.Threading;

namespace Thread_Mutex_Singleton
{
    class Program
    {
        const string mutextName = "my singleton method";
        static Mutex MyMutex;

        static void Main()
        {
            Thread th1 = new Thread(SingletonMethod);
            Thread th2 = new Thread(SingletonMethod);

            th1.Start();
            Thread.Sleep(100);
            th2.Start();

            th1.Join();
            th2.Join();

            Console.ReadKey();

            //Thread 5 >> Не нашел Mutex.
            //Thread 5 >> Создал Mutex.
            //Thread 5 >> Овладел Mutex.
            //Thread 6 >> Нашел Mutex.
            //Thread 6 >> Не овладел Mutex.
            //Thread 6 >> ЭКЗЕМПЛЯР УЖЕ ЗАПУЩЕН!
            //Thread 5 >> Освободил Mutex.
        }

        public static void SingletonMethod()
        {
            if (Mutex.TryOpenExisting(mutextName, out MyMutex))
            {
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} >> Нашел Mutex.");

                if (!MyMutex.WaitOne(TimeSpan.Zero))
                {
                    Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} >> Не овладел Mutex.");

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} >> ЭКЗЕМПЛЯР УЖЕ ЗАПУЩЕН!");
                    Console.ForegroundColor = ConsoleColor.Gray;

                    return;
                }
            }
            else
            {
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} >> Не нашел Mutex.");

                MyMutex = new Mutex(true, mutextName);

                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} >> Создал Mutex.");
            }

            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} >> Овладел Mutex.");

            Thread.Sleep(TimeSpan.FromSeconds(1));

            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} >> Освободил Mutex.");
        }
    }
}
