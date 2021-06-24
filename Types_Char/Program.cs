using System;

namespace Types_Char
{
    class Program
    {
        static void Main()
        {
            Console.Write('1');
            Console.Write('\n');
            Console.Write('2');
            Console.Write('\n');
            Console.ReadKey();
            //1
            //2
            //_

            Console.WriteLine(1);
            Console.WriteLine(2);
            Console.ReadKey();
            //1
            //2
            //_

            Console.Write('1');
            Console.Write('\r');
            Console.Write('2');
            Console.ReadKey();
            //2_

            Console.WriteLine();
            Console.WriteLine('a' + 1); // Автоматическое приведение к типу 1.
            Console.WriteLine((char)('a' + 1));
            Console.WriteLine('a' < 'b');
            Console.ReadKey();
            //'b'
            //True
        }
    }
}
