using System;
using System.IO;

namespace Stream_SreamReaderOnSreamReader
{
    class Program
    {
        static void Main()
        {
            string path = "AZ.txt";
            CreateTestFile(path);

            using (StreamReader sr1 = new StreamReader(path)) // Закрывается последним.
            using (StreamReader sr2 = new StreamReader(sr1.BaseStream)) // Закрывается первым.
            {
                while (sr2.Peek() != -1)
                {
                    Console.Write((char)sr2.Read());
                }
            }

            Console.ReadKey();
            //ABCDEFGHIJKLMNOPQRSTUVWXYZ
        }

        static void CreateTestFile(string path)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                for (int i = 'A'; i <= 'Z'; i++)
                {
                    sw.Write((char)i);
                }
            }
        }
    }
}
