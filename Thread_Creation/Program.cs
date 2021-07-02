using System;
using System.Threading;

namespace Thread_Creation
{
    /// <summary>
    /// Дочерний поток th переживает основной поток main.
    /// </summary>
    class Program
    {
        static void Main()
        {
            Console.WriteLine("main start");

            Thread th = new Thread(thEp);
            th.Start();

            int i = 0;
            while (i++ < 30)
            {
                Console.Write('.');
                Thread.Sleep(200);
            }

            Console.WriteLine("\nmain stop");
        }

        static void thEp()
        {
            Console.WriteLine("\nthEp start");

            while (true)
            {
                Console.Write('*');
                Thread.Sleep(500);
            }
        }

        //main start
        //.
        //thEp start
        //*..*..*...*..*...*..*...*..*...*..*...*..*
        //main stop
        //**************
    }
}
