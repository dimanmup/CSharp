using System;

namespace Types_RealsMethodsIs
{
    class Program
    {
        static void Main()
        {
            // double
            Console.WriteLine(double.IsNaN(double.NaN)); // true
            Console.WriteLine(double.IsInfinity(double.PositiveInfinity)); // true
            Console.WriteLine(double.IsInfinity(double.NegativeInfinity)); // true
            Console.WriteLine(double.IsPositiveInfinity(double.PositiveInfinity)); // true
            Console.WriteLine(double.IsNegativeInfinity(double.NegativeInfinity)); // true

            // double, float
            Console.WriteLine(double.IsInfinity(float.PositiveInfinity)); // true
            Console.WriteLine(float.IsInfinity((float)double.PositiveInfinity)); // true
            Console.WriteLine(double.PositiveInfinity == float.PositiveInfinity); // true
            Console.WriteLine(double.IsNaN(float.NaN)); // true
            Console.WriteLine(float.IsNaN((float)double.NaN)); // true
            Console.WriteLine(double.NaN.Equals(float.NaN)); // true

            Console.ReadKey();
        }
    }
}
