using System;
using System.Threading;

namespace Thread_IsAlive
{
    class Program
    {
        static void Main()
        {
            Thread th = new Thread(MyThread);

            Console.WriteLine(th.IsAlive); // False

            th.Start();
            Thread.Sleep(100);

            Console.WriteLine(th.IsAlive); // True

            Thread.Sleep(300);

            Console.WriteLine(th.IsAlive); // False

            Console.ReadKey();
        }
        static void MyThread()
        {
            Thread.Sleep(200);  
        }
    }
}
