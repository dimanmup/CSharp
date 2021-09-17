using System;
using System.Threading;

namespace Thread_SuspendResume
{
    class Program
    {
        static int i = 0;
        static int max = 3;
        static int timeout = 100;
        static Thread writer1 = new Thread(Writer1);
        static Thread writer2 = new Thread(Writer2);

        static void Main()
        {
            writer1.Start();
            writer2.Start();

            while (writer1.ThreadState != ThreadState.Suspended) ;
            writer1.Resume();
        }

        static void Writer1()
        {
            SuspendAndResume(writer2, '*', timeout);
        }

        static void Writer2()
        {
            SuspendAndResume(writer1, '+', timeout);
        }

        static void SuspendAndResume(Thread resumed, char symbol, int timeout)
        {
            Thread.CurrentThread.Suspend();

            while (true)
            {
                Thread.Sleep(timeout);
                Console.Write(symbol);

                if (++i == max)
                {
                    i = 0;
                    resumed.Resume();
                    Thread.CurrentThread.Suspend();
                }
            }
        }
    }
}
