using System;
using System.IO;

namespace Stream_SreamReader
{
    class Program
    {
        static void Main()
        {
            StreamReader sr = new StreamReader(@"C:\Users\Diman\Desktop\numbers.txt");

            int b = sr.Read();
            while (b != -1)
            {
                Console.Write((char)b);
                b = sr.Read();
            }

            Console.ReadKey();
        }
    }
}
