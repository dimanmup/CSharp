using System;
using System.Threading;

namespace Thread_Monitor_Wait2
{
    class Program
    {
        static void Main()
        {
            for (int i = 0; i < 2; i++)
            {
                string threadName = "T" + i;

                Thread th = new Thread(() =>
                {
                    lock (typeof(Program))
                    {
                        for (int j = 0; j < 10; j++)
                        {
                            Console.WriteLine($"{threadName } >> {j}");

                            if (j == 5)
                            {
                                Console.WriteLine($"{threadName } >> waited.\n");
                                Monitor.Wait(typeof(Program));
                            }
                        }
                    }
                });

                th.Start();
            }

            //T0 >> 0
            //T0 >> 1
            //T0 >> 2
            //T0 >> 3
            //T0 >> 4
            //T0 >> 5
            //T0 >> waited.

            //T1 >> 0
            //T1 >> 1
            //T1 >> 2
            //T1 >> 3
            //T1 >> 4
            //T1 >> 5
            //T1 >> waited.
        }
    }
}
