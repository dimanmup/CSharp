using System;

namespace Types_StringIntern
{
    class Program
    {
        static void Main()
        {
            string s1 = "Hello World!";
            string s2 = "Hello World!";
            string s3 = s2;

            Console.WriteLine(object.ReferenceEquals(s1, s2)); // True
            Console.WriteLine(object.ReferenceEquals(s1, s3)); // True
            Console.WriteLine(object.ReferenceEquals(s2, s3)); // True

            Console.ReadKey();
        }
    }
}
