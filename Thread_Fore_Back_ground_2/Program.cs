using System;
using System.Threading;

namespace Thread_Fore_Back_ground_2
{
    class Program
    {
        static void Main()
        {
            Thread fg = new Thread(ForegroundThread);
           
            Thread bg = new Thread(BackgroundThread);
            bg.IsBackground = true;

            fg.Start();
            bg.Start();            
        }

        static void ForegroundThread()
        {
            for (int i = 5; i >= 0; i--)
            {
                Thread.Sleep(500);
                Console.Write(i);
            }
        }

        static void BackgroundThread()
        {
            while (true)
            {
                Console.Write('*');
                Thread.Sleep(200);
            }
        }
    }
}
