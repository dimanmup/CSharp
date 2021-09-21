using System;
using System.IO;

namespace Stream_FileStrem_FileShare
{
    class Program
    {
        static void Main()
        {
            string path = "text.txt";
            CreateTestFile(path);

            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                using (StreamReader sr = new StreamReader(path))
                {
                    Console.WriteLine(sr.ReadToEnd());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            //ABCDEFGHIJKLMNOPQRSTUVWXYZ

            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                using (StreamReader sr = new StreamReader(path))
                {
                    Console.WriteLine(sr.ReadToEnd());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            //ABCDEFGHIJKLMNOPQRSTUVWXYZ

            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None))
                using (StreamReader sr = new StreamReader(path))
                {
                    Console.WriteLine(sr.ReadToEnd());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            //Процесс не может получить доступ к файлу
            //"E:\VS\CSharp\Console_Draft\bin\Debug\text.txt",
            //так как этот файл используется другим процессом.

            Console.ReadKey();
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
