using System;
using System.Runtime.InteropServices;

namespace CppDllUser_Intro
{
    class Program
    {
        public class Math32
        {
            [DllImport(@"..\..\..\..\Debug\math1.dll", EntryPoint = "sum")]
            private static extern int Sum(int x, int y);

            public static bool Sum(int x, int y, out int? result, out Exception e)
            {
                e = null;
                result = null;

                try
                {
                    result = Sum(x, y);
                    return true;
                }
                catch (Exception ex)
                {
                    e = ex;
                    return false;
                }
            }
        }

        public class Math64
        {
            [DllImport(@"..\..\..\..\x64\Debug\math1.dll", EntryPoint = "sum")]
            private static extern int Sum(int x, int y);

            public static bool Sum(int x, int y, out int? result, out Exception e)
            {
                e = null;
                result = null;

                try
                {
                    result = Sum(x, y);
                    return true;
                }
                catch (Exception ex)
                {
                    e = ex;
                    return false;
                }
            }
        }

        static void Main()
        {
            int? sum;
            Exception e;

            Console.Write("[Math32] -> ");
            if (Math32.Sum(1, 2, out sum, out e))
            {
                Console.WriteLine("sum: " + sum);
            }
            else
            {
                Console.WriteLine(e.Message);
            }

            Console.Write("[Math64] -> ");
            if (Math64.Sum(1, 2, out sum, out e))
            {
                Console.WriteLine("sum: " + sum);
            }
            else
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadKey();
            //[Math32] -> Была сделана попытка загрузить программу, имеющую неверный формат. (0x8007000B)
            //[Math64] -> sum: 3
        }
    }
}
