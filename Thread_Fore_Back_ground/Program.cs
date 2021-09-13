using System;
using System.Threading;

namespace Thread_Fore_Back_ground
{
    class Program
    {
        static void Main()
        {
            Thread th = new Thread(backgroundThread);
            th.IsBackground = true;
            th.Start();

            for (int i = 5; i >= 0; i--)
            {
                Thread.Sleep(500);
                Console.Write(i);                
            }
        }

        static void backgroundThread()
        {
            while (true)
            {
                Console.Write('*');
                Thread.Sleep(200);
            }
        }
    }
}
