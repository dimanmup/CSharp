using System;

namespace Stream_Console
{
    class Program
    {
        static void Main()
        {
            string s = Console.In.ReadLine(); // Ввести "123".
            Console.Out.WriteLine(s); // Вывод "123".
            Console.ReadKey();
        }
    }
}
