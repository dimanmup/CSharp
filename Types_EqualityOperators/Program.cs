using System;

namespace Types_EqualityOperators
{
    class Program
    {
        static void Main()
        {
            int i = 1;
            int j = 1;
            object I = 1;
            object J = 1;

            // True
            Console.WriteLine(i == j);
            Console.WriteLine(i == 1);
            Console.WriteLine(1 == 1);
            Console.WriteLine(i != 2);
            Console.WriteLine(1 != 2);
            Console.WriteLine(I != J);
            Console.WriteLine(null == null);
            Console.WriteLine();

            // False
            Console.WriteLine(I == J);
            Console.WriteLine(i != j);
            Console.WriteLine(i != 1);

            // Error
            // Не может сравнивать ссылку и значение.
            //Console.WriteLine(I == i);
            //Console.WriteLine(I == 1);

            Console.ReadKey();
        }
    }
}
