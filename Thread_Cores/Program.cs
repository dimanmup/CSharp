using System;
using System.Linq;
using System.Threading;

namespace Thread_Cores
{
    class Counter
    {
        static bool stop;
        string name;

        public Counter(string name)
        {
            this.name = name;
        }

        public void Run()
        {
            long i;
            for (i = 0; !stop && i < 1_000_000_000; i++);
            stop = true;
            Console.WriteLine("Поток {0,12}, i={1}", name, i);
        }
    }


    class Program
    {
        static void Main()
        {
            Thread[] ths = Enum.GetValues(typeof(ThreadPriority))
                .Cast<ThreadPriority>()
                .OrderByDescending(x => x)
                .Select(x =>
                {
                    Counter counter = new Counter(x.ToString());
                    Thread th = new Thread(counter.Run);
                    th.Priority = x;

                    return th;
                })
                .ToArray();

            foreach (Thread th in ths)
            {
                th.Start();                
            }

            // Lowest
            ths.Last().Join();
            Console.ReadKey();
        }
    }
}
