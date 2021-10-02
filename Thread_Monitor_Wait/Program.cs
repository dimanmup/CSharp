using System;
using System.Threading;

namespace Thread_Monitor_Wait
{
    class Program
    {
        static Thread th1 = new Thread(() => WriteSymbol('*', th2));
        static Thread th2 = new Thread(() => WriteSymbol('+', th1));

        static void Main()
        {
            th1.Name = "T1";
            th2.Name = "T2";

            th1.Start();

            // Чтобы запустить th2 вторым.
            Thread.Sleep(10); // (*)

            th2.Start();

            //T1 >> T2 state is WaitSleepJoin.
            //T1 >> ***
            //T1 >> T1 waited.
            //T2 >> T1 state is WaitSleepJoin.
            //T2 >> +++
            //T2 >> T2 waited.
        }

        static void WriteSymbol(char c, Thread waited)
        {
            lock (typeof(Program))
            {
                // Чтобы посмотреть статус th2 после блокировки потоком th1.
                Thread.Sleep(100); // (*)

                #region Статус другого потока

                Console.ForegroundColor = ConsoleColor.Green;

                Console.Write($"{Thread.CurrentThread.Name} >> ");
                Console.WriteLine($"{waited.Name} state is {waited.ThreadState}.");

                Console.ForegroundColor = ConsoleColor.Gray;

                #endregion

                Console.Write($"{Thread.CurrentThread.Name} >> ");

                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(1000);

                    if (i < 3)
                    {
                        Console.Write(c);                      
                        continue;
                    }

                    Console.WriteLine($"\n{Thread.CurrentThread.Name} >> {Thread.CurrentThread.Name} waited.");
                    Monitor.Wait(typeof(Program));
                }
            }
        }
    }
}
