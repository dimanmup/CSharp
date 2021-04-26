using System;

namespace Types_RealsNaN
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine(double.NaN + 1); // NaN
            Console.WriteLine(double.NaN + double.Epsilon); // NaN
            Console.WriteLine(double.NaN + double.PositiveInfinity); // NaN
            Console.WriteLine(double.NaN - double.NaN); // NaN
            Console.WriteLine(double.NaN * 0); // NaN
            Console.WriteLine(double.PositiveInfinity * 0); // NaN
            
            Console.WriteLine(double.NaN > 0); // false
            Console.WriteLine(double.NaN < 0); // false
            Console.WriteLine(double.NaN == double.NaN); // false
            Console.WriteLine(double.NaN.Equals(double.NaN)); // true
            Console.WriteLine(double.IsNaN(double.NaN)); // true

            Console.ReadKey();
        }
    }
}
