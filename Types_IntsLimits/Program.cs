using System;

namespace Types_IntsLimits
{
    class Program
    {
        static void Main()
        {
            sbyte sbyteMin = sbyte.MinValue;
            sbyte sbyteMax = sbyte.MaxValue;
            byte byteMin = byte.MinValue;
            byte byteMax = byte.MaxValue;
            short shortMin = short.MinValue;
            short shortMax = short.MaxValue;
            ushort ushortMin = ushort.MinValue;
            ushort ushortMax = ushort.MaxValue;
            int intMin = int.MinValue;
            int intMax = int.MaxValue;
            uint uintMin = uint.MinValue;
            uint uintMax = uint.MaxValue;
            long longMin = long.MinValue;
            long longMax = long.MaxValue;
            ulong ulongMin = ulong.MinValue;
            ulong ulongMax = ulong.MaxValue;

            Console.WriteLine("sbyte: " + sbyteMin + " - " + sbyteMax);
            Console.WriteLine("byte: " + byteMin + " - " + byteMax);
            Console.WriteLine("short: " + shortMin + " - " + shortMax);
            Console.WriteLine("ushort: " + ushortMin + " - " + ushortMax);
            Console.WriteLine("int: " + intMin + " - " + intMax);
            Console.WriteLine("uint: " + uintMin + " - " + uintMax);
            Console.WriteLine("long: " + longMin + " - " + longMax);
            Console.WriteLine("ulong: " + ulongMin + " - " + ulongMax);

            /*
            sbyte: -128 - 127
            byte: 0 - 255
            short: -32768 - 32767
            ushort: 0 - 65535
            int: -2147483648 - 2147483647
            uint: 0 - 4294967295
            long: -9223372036854775808 - 9223372036854775807
            ulong: 0 - 18446744073709551615
            */

            Console.ReadKey();
        }
    }
}