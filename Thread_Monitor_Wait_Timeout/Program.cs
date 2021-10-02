using System;
using System.Diagnostics;
using System.Threading;

namespace Thread_Monitor_Wait_Timeout
{
    class Program
    {
        static void Timer(char mark, Stopwatch sw, int timeout)
        {
            sw.Start();

            while (sw.ElapsedMilliseconds < timeout)
            {
                continue;
            }

            Console.WriteLine($"[{mark}]");
        }

        static Thread th1 = new Thread(() => WriteSymbol('.', 2500));
        static Thread th2 = new Thread(() => WriteSymbol('*', 1000));
        static Thread th3 = new Thread(() => WriteSymbol('@', 0));

        static int maxIndex = 20;
        static int stopIndex = maxIndex / 2;

        static void Main()
        {
            th1.Start();
            Thread.Sleep(10);
            th2.Start();
            Thread.Sleep(10);
            th3.Start();

            th1.Join();
            th2.Join();
            th3.Join();
            Console.ReadKey();

            //.
            //.
            //.
            //.
            //.
            //.
            //.
            //.
            //.
            //.
            //(.)
            //*
            //*
            //*
            //*
            //*
            //*
            //*
            //*
            //*
            //*
            //(*)
            //@
            //@
            //@
            //@
            //@
            //@
            //@
            //@
            //@
            //[.] - ready queue = |th1> th3(obj)
            //[*] - ready queue = |th2,th1> th3(obj)
            //@
            //(@) - Освобождение obj.
            //[@] - ready queue = |th3,th2,th1> obj
            //. - ready queue = |th3,th2> th1(obj)
            //.
            //.
            //.
            //.
            //.
            //.
            //.
            //.
            //.
            //* - ready queue = |th3> th2(obj)
            //*
            //*
            //*
            //*
            //*
            //*
            //*
            //*
            //*
            //@ - ready queue = |> th3(obj)
            //@
            //@
            //@
            //@
            //@
            //@
            //@
            //@
            //@
        }

        static void WriteSymbol(char c, int waitTimeout)
        {
            Stopwatch sw = new Stopwatch();

            lock (typeof(Program))
            {
                for (int i = 1; i <= maxIndex; i++)
                {
                    Thread.Sleep(100);
                    Console.WriteLine(c);

                    if (i == stopIndex + 1)
                    {
                        sw.Stop();
                    }

                    if (i == stopIndex)
                    {
                        Console.WriteLine($"({c})");
                        Thread timerThread = new Thread(() => Timer(c, sw, waitTimeout));
                        timerThread.Start();

                        Monitor.Wait(typeof(Program), waitTimeout);
                    }
                }
            }
        }
    }
}
