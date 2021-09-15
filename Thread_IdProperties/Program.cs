using System;
using System.Threading;

namespace Thread_IdProperties
{
    class Program
    {
        static void Main()
        {
            Thread.CurrentThread.Name = "Main";
            WriteInfo(Thread.CurrentThread);

            Thread th = new Thread(MyThread);
            th.Start();
            Thread.Sleep(100);

            WriteInfo(th);

            Console.ReadKey();

            //id: 1
            //name: Main

            //id: 3
            //name: My Thread
        }

        static void WriteInfo(Thread th)
        {
            Console.WriteLine("id: " + th.ManagedThreadId);
            Console.WriteLine("name: " + th.Name);
            Console.WriteLine();
        }

        static void MyThread()
        {
            Thread.CurrentThread.Name = "My Thread";
        }
    }
}
