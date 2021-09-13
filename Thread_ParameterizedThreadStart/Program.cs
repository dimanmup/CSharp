using System;
using System.Threading;

namespace Thread_ParameterizedThreadStart
{
    class Program
    {
        static void Main()
        {
            ParameterizedThreadStart thArg = new ParameterizedThreadStart(thEp);
            Thread th = new Thread(thArg);
            th.Start('@');
        }

        static void thEp(object @char)
        {
            while (true)
            {
                Console.Write((char)@char);
                Thread.Sleep(300);
            }
        }

        //@@@@@@@@@
    }
}
