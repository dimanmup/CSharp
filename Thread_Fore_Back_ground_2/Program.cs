using System;
using System.Threading;

namespace Thread_Fore_Back_ground_2
{
    class Program
    {
        static void Main()
        {
            Thread fg = new Thread(foregroundThread);
           
            Thread bg = new Thread(backgroundThread);
            bg.IsBackground = true;

            fg.Start();
            bg.Start();            
        }

        static void foregroundThread()
        {
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
