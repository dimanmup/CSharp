using System;
using System.IO;

namespace Stream_SreamReader
{
    class Program
    {
        static void Main()
        {
            string path = "AZ.txt";
            using (StreamWriter sw = new StreamWriter(path))
            {
                for (int i = 'A'; i <= 'Z'; i++)
                {
                    sw.Write((char)i);
                }
            }

            string s = "";

            using (StreamReader sr = new StreamReader(path))
            {
                int b = sr.Read();
                while (b != -1)
                {
                    s += (char)b;
                    Console.Write((char)b);
                    b = sr.Read();
                }
            }            

            Console.WriteLine(s);
            Console.ReadKey();
            //ABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZ
        }
    }
}
