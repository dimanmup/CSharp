using System;
using System.Threading;

namespace Thread_Monitor_IsEntered
{
    class Program
    {
        static void Main()
        {
            Thread th = new Thread(() => 
            {
                Console.WriteLine(Monitor.IsEntered(typeof(Program))); // false

                lock (typeof(Program))
                {
                    Console.WriteLine(Monitor.IsEntered(typeof(Program))); // true
                }

                Console.WriteLine(Monitor.IsEntered(typeof(Program))); // false
            });

            th.Start();
            th.Join();
            Console.ReadKey();
        }
    }
}
