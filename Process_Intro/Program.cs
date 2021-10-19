using System;
using System.Diagnostics;
using System.Threading;

namespace Process_Intro
{
    class Program
    {
        static void Main()
        {
            Console.Title = "Process Intro App";
            AddMyThreads(3);

            // Инкапсуляция текущего процесса.
            Process p = Process.GetCurrentProcess();
 
            string processorAffinity_2 = Convert.ToString(p.ProcessorAffinity.ToInt32(), 2);

            Console.WriteLine($"Id: {p.Id}");
            Console.WriteLine($"ProcessName: {p.ProcessName}");
            Console.WriteLine($"MainModule: {p.MainModule}");
            Console.WriteLine($"MainWindowTitle: {p.MainWindowTitle}");
            Console.WriteLine($"ProcessorAffinity_10: {p.ProcessorAffinity}");
            Console.WriteLine($"ProcessorAffinity_2: {processorAffinity_2}");

            Console.WriteLine("Process threads:");
            for (int i = 1; i <= p.Threads.Count; i++)
            {
                var priority = p.Threads[i - 1].PriorityLevel;

                if (priority == ThreadPriorityLevel.Lowest)
                    Console.ForegroundColor = ConsoleColor.Yellow;
                else
                    Console.ForegroundColor = ConsoleColor.Gray;

                Console.WriteLine($" Thread {i} priority: {priority}");
            }

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.ReadKey();

            //My thread 1 priority: Lowest
            //My thread 2 priority: Lowest
            //My thread 3 priority: Lowest
            //Id: 10056
            //ProcessName: Process_Intro
            //MainModule: System.Diagnostics.ProcessModule (Process_Intro.exe)
            //MainWindowTitle: Process Intro App
            //ProcessorAffinity_10: 15
            //ProcessorAffinity_2: 1111
            //Process threads:
            // Thread 1 priority: Normal
            // Thread 2 priority: Normal
            // Thread 3 priority: Normal
            // Thread 4 priority: Normal
            // Thread 5 priority: Normal
            // Thread 6 priority: Normal
            // Thread 7 priority: Highest
            // Thread 8 priority: Normal
            // Thread 9 priority: Lowest
            // Thread 10 priority: Lowest
            // Thread 11 priority: Lowest
        }

        static void AddMyThreads(int number)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            for (int i = 1; i <= number; i++)
            {
                Thread th = new Thread(() => { while (true) ; });
                th.IsBackground = true;
                th.Priority = ThreadPriority.Lowest;

                th.Start();
                Console.WriteLine($"My thread {i} priority: {th.Priority}");
            }

            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
