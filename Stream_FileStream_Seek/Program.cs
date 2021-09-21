using System;
using System.IO;

namespace Stream_FileStream_Seek
{
    class Program
    {
        static void Main()
        {
            string path = "AZ.txt";
            CreateTestFile(path);

            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
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
