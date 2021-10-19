using System;
using System.Threading;

namespace Process_Toy_Timer
{
    /// <summary>
    /// .NET Framework 3.5 для Windows 7.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), args[1], true);

            int secondsNumber = int.Parse(args[0]);

            do
            {
                Console.WriteLine($"Осталось {secondsNumber} секунд.");

                Thread.Sleep(1000);
                secondsNumber--;
            } 
            while (secondsNumber > 0);
        }
    }
}
