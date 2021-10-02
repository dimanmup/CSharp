using System;
using System.Threading;

namespace Thread_Monitor_Pulse
{
    class Program
    {
        static void Main(string[] args)
        {
            int threadsCount = 3;

            for (int i = 1; i <= threadsCount; i++)
            {
                int threadNumber = i;

                Thread th = new Thread(() =>
                {
                    lock (typeof(Program))
                    {
                        for (int j = 0; j < 10; j++)
                        {
                            Console.WriteLine($"T{threadNumber} >> {j}");

                            if (j == 5)
                            {
                                Console.WriteLine($"T{threadNumber} >> waited.\n");

                                if (threadNumber == threadsCount)
                                {
                                    Monitor.Pulse(typeof(Program));
                                }

                                Monitor.Wait(typeof(Program));
                            }
                        }
                    }
                });

                th.Start();

                //T1 >> 0
                //T1 >> 1
                //T1 >> 2
                //T1 >> 3
                //T1 >> 4
                //T1 >> 5
                //T1 >> waited.

                //T2 >> 0
                //T2 >> 1
                //T2 >> 2
                //T2 >> 3
                //T2 >> 4
                //T2 >> 5
                //T2 >> waited.

                //T3 >> 0
                //T3 >> 1
                //T3 >> 2
                //T3 >> 3
                //T3 >> 4
                //T3 >> 5
                //T3 >> waited.

                //T1 >> 6
                //T1 >> 7
                //T1 >> 8
                //T1 >> 9
            }
        }
    }
}
