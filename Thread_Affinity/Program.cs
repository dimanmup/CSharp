using System;
using System.Diagnostics;
using System.Threading;

namespace Thread_Affinity
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.Sleep(TimeSpan.FromSeconds(15));

            // off(CPU3) | on(CPU2) | on(CPU1) | off(CPU0) = 0110 = 6(dec)
            Process.GetCurrentProcess().ProcessorAffinity = (IntPtr)6;

            while (true);
        }
    }
}
