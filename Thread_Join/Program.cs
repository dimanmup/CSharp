using System;
using System.Threading;

namespace Thread_Join
{
    class Program
    {
        static Thread th1 = new Thread(Th1);
        static Thread th2 = new Thread(Th2);

        static void Main()
        {
            th1.Start();
            th2.Start();

            th2.Join();
            For('.');

            Console.ReadKey();
            //*****+++++.....
        }

        static void For(char symbol)
        {
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(100);
                Console.Write(symbol);
            }
        }

        static void Th1()
        {
            For('*');
        }

        static void Th2()
        {
            th1.Join();
            For('+');
        }
    }
}
