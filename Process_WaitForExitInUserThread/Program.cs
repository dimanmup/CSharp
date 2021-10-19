using System;
using System.Diagnostics;
using System.Threading;

namespace Process_WaitForExitInUserThread
{
    class Program
    {
        static void Main()
        {
            Thread th = new Thread(() =>
            {
                int i = 0;
                while (true)
                {
                    Console.Write('*');
                    Thread.Sleep(300);

                    if (i++ == 10)
                    {
                        string timerPath = @"E:\VS\CSharp\Process_Toy_Timer\bin\Debug\Process_Toy_Timer.exe";
                        Process timer = new Process();
                        ProcessStartInfo timerProcessInfo = new ProcessStartInfo(timerPath, "5 yellow");
                        timer.StartInfo = timerProcessInfo;
                        timer.Start();
                        Stopwatch sw = new Stopwatch();
                        sw.Start();
                        timer.WaitForExit(10000);
                        sw.Stop();
                        Console.Write(sw.Elapsed.TotalSeconds);
                    }
                }
            });
            th.Start();

            while (true)
            {
                Console.Write(".");
                Thread.Sleep(300);
            }

            //.*.*.*.*.*.*.*.*.*.*.*..................5,1275662*.*.*.*.*.*.*.*.*.*.*.*.*.
        }
    }
}
