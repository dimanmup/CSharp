using System.IO;

namespace Stream_CopyTo
{
    class Program
    {
        static void Main(string[] args)
        {
            string path1 = "AZ1.txt";
            string path2 = "AZ2.txt";
            CreateTestFile(path1);

            using (StreamWriter sw = new StreamWriter(path2))
            using (StreamReader sr = new StreamReader(path1))
            {
                sr.BaseStream.CopyTo(sw.BaseStream);
            }
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
