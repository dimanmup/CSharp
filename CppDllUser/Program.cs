using System;
using System.Runtime.InteropServices;

namespace CppDllUser
{
    class Program
    {
        public class MyClass
        {
            [DllImport(@"E:\VS\CSharp\x64\Debug\CppDll.dll", EntryPoint = "get_1")]
            public static extern int Get1();

        }

        static void Main(string[] args)
        {
            Console.WriteLine(MyClass.Get1());
            Console.ReadKey();
        }
    }
}
