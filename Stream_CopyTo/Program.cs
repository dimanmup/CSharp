using System.IO;

namespace Stream_CopyTo
{
    class Program
    {
        static void Main()
        {
            string azPath = "AZ.txt";
            string azCopy1Path = "AZ copy 1.txt";
            string azCopy2Path = "AZ copy 2.txt";

            CreateTestFile(azPath);

            using (FileStream fsw = new FileStream(azCopy2Path, FileMode.Create, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(azCopy1Path))

            using (FileStream fsr = new FileStream(azPath, FileMode.Open, FileAccess.Read))
            using (StreamReader sr = new StreamReader(azPath))
            
            {
                sr.BaseStream.CopyTo(fsw);
                fsr.CopyTo(sw.BaseStream);
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
