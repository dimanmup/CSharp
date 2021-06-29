using System;

namespace Types_Object
{
    class Program
    {
        static void Main()
        {
            // ToString()
            // Вывод текстового представления в текущей культуре (ru-RU).
            object x = 1.5;
            Console.WriteLine(x); // 1,5 <= ru-RU
            Console.WriteLine(x.ToString()); // 1,5 <= ru-RU
            Console.WriteLine();

            // GetType()
            // Получение .Net-имени типа.
            Console.WriteLine(x.GetType().Name); // Double
            Console.WriteLine(1.GetType().Name); // Int32
            Console.WriteLine();

            object m = 1;
            object n = 1;

            // Equals()
            // Сравнение значений.
            // True
            Console.WriteLine(m.Equals(n));
            Console.WriteLine(m.Equals(1));
            Console.WriteLine(1.Equals(1));
            Console.WriteLine(1.Equals(m));
            Console.WriteLine(object.Equals(m, n));
            Console.WriteLine(object.Equals(m, 1));
            Console.WriteLine(object.Equals(1, 1));
            Console.WriteLine(object.Equals(1, m));
            Console.WriteLine(object.ReferenceEquals(m, m));
            Console.WriteLine(object.ReferenceEquals(null, null)); // !
            Console.WriteLine();

            // ReferenceEquals()
            // Сравнение ссылок.
            // False
            Console.WriteLine(object.ReferenceEquals(m, n));
            Console.WriteLine(object.ReferenceEquals(m, 1));
            Console.WriteLine(object.ReferenceEquals(1, 1)); // !
            Console.WriteLine(object.ReferenceEquals(1, m));
            Console.WriteLine(object.ReferenceEquals(m, null));
            Console.WriteLine();

            Console.ReadKey();
        }
    }
}
