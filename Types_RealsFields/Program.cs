using System;

namespace Types_RealsFields
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("float.Epsilon: " + float.Epsilon);
            Console.WriteLine("float.MaxValue: " + float.MaxValue);
            Console.WriteLine("float.MinValue: " + float.MinValue);
            Console.WriteLine("float.NaN: " + float.NaN);
            Console.WriteLine("float.NegativeInfinity: " + float.NegativeInfinity);
            Console.WriteLine("float.PositiveInfinity: " + float.PositiveInfinity);
            Console.WriteLine();
            Console.WriteLine("double.Epsilon: " + double.Epsilon);
            Console.WriteLine("double.MaxValue: " + double.MaxValue);
            Console.WriteLine("double.MinValue: " + double.MinValue);
            Console.WriteLine("double.NaN: " + double.NaN);
            Console.WriteLine("double.NegativeInfinity: " + double.NegativeInfinity);
            Console.WriteLine("double.PositiveInfinity: " + double.PositiveInfinity);
            Console.WriteLine();
            Console.WriteLine("decimal.MaxValue: " + decimal.MaxValue);
            Console.WriteLine("decimal.MinValue: " + decimal.MinValue);
            Console.WriteLine("decimal.One: " + decimal.One);
            Console.WriteLine("decimal.MinusOne: " + decimal.MinusOne);
            Console.WriteLine("decimal.Zero: " + decimal.Zero);
            /*
            float.Epsilon: 1,401298E-45
            float.MaxValue: 3,402823E+38
            float.MinValue: -3,402823E+38
            float.NaN: не число
            float.NegativeInfinity: -?
            float.PositiveInfinity: ?

            double.Epsilon: 4,94065645841247E-324
            double.MaxValue: 1,79769313486232E+308
            double.MinValue: -1,79769313486232E+308
            double.NaN: не число
            double.NegativeInfinity: -?
            double.PositiveInfinity: ?

            decimal.MaxValue: 79228162514264337593543950335
            decimal.MinValue: -79228162514264337593543950335
            decimal.One: 1
            decimal.MinusOne: -1
            decimal.Zero: 0
            */
            Console.ReadKey();
        }
    }
}
