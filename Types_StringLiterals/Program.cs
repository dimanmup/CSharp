using System;

namespace Types_StringLiterals
{
    class Program
    {
        static void Main()
        {
            int age = 10;

            // Concatenation.
            string s1 = "My age is " + age; 

            string s2 = "My name is \"Vasya\"";
            string s3 = "C:\\MyFolder\\MyFule.txt";

            // Verbatium string literals.
            string vs1 = @"C:\MyFolder\MyFule.txt";
            string vs2 = @"My name 
                is
                ""Vasya""
                \n";

            // String interpolations.
            string si1 = $"My age is {age}";
            string si2 = $"My age is {age, 7}.";
            string si3 = $"My age is {age, -7}.";
            string si4 = $"Set A = {{ 1, 2, 3 }}";

            Console.WriteLine(s1);
            Console.WriteLine(s2);
            Console.WriteLine(s3);
            Console.WriteLine(vs1);
            Console.WriteLine(vs2);
            Console.WriteLine(si1);
            Console.WriteLine(si2);
            Console.WriteLine(si3);
            Console.WriteLine(si4);
            //My age is 10
            //My name is "Vasya"
            //C:\MyFolder\MyFule.txt
            //C:\MyFolder\MyFule.txt
            //My name
            //                is
            //                "Vasya"
            //                \n
            //My age is 10
            //My age is      10.
            //My age is 10     .
            //Set A = { 1, 2, 3 }

            Console.ReadKey();
        }
    }
}
