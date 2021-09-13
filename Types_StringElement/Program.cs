using System;

namespace Types_StringElement
{
    class Program
    {
        static void Main()
        {
            string s = "Hello World!";

            // Error: it is read only.
            //s[0] = "h";

            Console.WriteLine(s[0]); // H
            Console.ReadKey();
        }
    }
}
