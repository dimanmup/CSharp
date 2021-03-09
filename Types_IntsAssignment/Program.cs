using System;

namespace Types_IntsAssignment
{
    class Program
    {
        static void Main()
        {
            int x0;
            int x1 = 1;
            int x2 = x1 + 1, x3 = x1 + x2;

            // Синтаксическая ошибка: переменной x0 не присвоено значение.
            //Console.WriteLine("x0 : " + x0);
            
            Console.WriteLine("x1 : " + x1);
            Console.WriteLine("x2 : " + x2);
            Console.WriteLine("x3 : " + x3);

            /*
            x1 : 1
            x2 : 2
            x3 : 3
            */

            Console.ReadKey();
        }
    }
}
