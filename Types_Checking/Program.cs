using System;

namespace Types_Checking
{
    class Program
    {
        static void Main()
        {
            byte err, min, max;

            // Ошибки.
            /*
            err = (byte)(byte.MaxValue + 1);
            checked
            {
                err = (byte)(byte.MaxValue + 1);
            }
            err = checked((byte)(byte.MinValue - 1));
            */

            unchecked
            {
                min = (byte)(byte.MaxValue + 1);
            }
            max = unchecked((byte)(byte.MinValue - 1));

            Console.WriteLine(byte.MinValue);
            Console.WriteLine(min);
            Console.WriteLine("---");
            Console.WriteLine(byte.MaxValue);
            Console.WriteLine(max);

            /*
            0
            0
            ---
            255
            255 
            */

            Console.ReadKey();
        }
    }
}
