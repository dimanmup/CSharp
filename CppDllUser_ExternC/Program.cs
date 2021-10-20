using System;
using System.Runtime.InteropServices;

namespace CppDllUser_ExternC
{
    class Program
    {
        [DllImport(@"..\..\..\..\x64\Debug\math1.dll", EntryPoint = "mult")]
        private static extern int MultCExterned(int x, int y);

        [DllImport(@"..\..\..\..\x64\Debug\math1.dll", EntryPoint = "?mult@@YAHHH@Z")]
        private static extern int MultRenamed(int x, int y);

        public static bool MultCExterned(int x, int y, out int? result, out Exception e)
        {
            e = null;
            result = null;

            try
            {
                result = MultCExterned(x, y);
                return true;
            }
            catch (Exception ex)
            {
                e = ex;
                return false;
            }
        }
        public static bool MultRenamed(int x, int y, out int? result, out Exception e)
        {
            e = null;
            result = null;

            try
            {
                result = MultRenamed(x, y);
                return true;
            }
            catch (Exception ex)
            {
                e = ex;
                return false;
            }
        }

        static void Main()
        {
            int? sum;
            Exception e;

            Console.Write("[MultCExterned] -> ");
            if (MultCExterned(2, 3, out sum, out e))
            {
                Console.WriteLine(sum);
            }
            else
            {
                Console.WriteLine(e.Message);
            }

            Console.Write("[MultRenamed] -> ");
            if (MultRenamed(2, 3, out sum, out e))
            {
                Console.WriteLine(sum);
            }
            else
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadKey();
            //[MultCExterned] -> Unable to find an entry point named 'mult' in DLL '..\..\..\..\x64\Debug\math1.dll'.
            //[MultRenamed] -> 6
        }
    }
}
