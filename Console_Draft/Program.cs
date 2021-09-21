using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Draft
{
    class Program
    {
        static void Main()
        {
            string path = "text.txt";
            CreateTestFile(path);

            using (FileStream fs = new FileStream("text.txt", FileMode.Open, FileAccess.Read))
            {
                for (long offset = 1; offset <= fs.Length; offset++)
                {
                    fs.Seek(-offset, SeekOrigin.End);
                    Console.Write((char)fs.ReadByte());
                }
            }

            Console.ReadKey();
            //ZYXWVUTSRQPONMLKJIHGFEDCBA
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
