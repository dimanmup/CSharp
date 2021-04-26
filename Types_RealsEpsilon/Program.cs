using System;

namespace Types_RealsEpsilon
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine(double.Epsilon == double.Epsilon + double.Epsilon); // false
            Console.WriteLine(1 == 1 + double.Epsilon); // true
            Console.WriteLine(1 == 1 - double.Epsilon); // true
            Console.WriteLine(0 == 0 + double.Epsilon); // false
            Console.WriteLine(0 == 0 - double.Epsilon); // false
            Console.WriteLine(1 - 1 == double.Epsilon); // false
            Console.ReadKey();
        }
    }
}
