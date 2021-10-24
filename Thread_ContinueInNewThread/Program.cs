using System;
using System.IO;
using System.Threading;

namespace Thread_ContinueInNewThread
{
    class Program
    {
        static int pregnancyDuration = 3000;
        static string folderFrom = "from";
        static string folderTo = "to";

        static void Main()
        {
            string[] fileNames = 
            {
                "xxx.txt",
                "yyy.txt",
                "zzz.txt"
            };

            foreach (string fn in fileNames)
            {
                ParentWork(fn);
            }
        }

        static bool ParentWork(string fileName)
        {
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(500);
                Console.WriteLine("parsing " + fileName);
            }

            Thread child = new Thread(() => ChildWork(fileName, pregnancyDuration));
            child.Name = "child with " + fileName;
            child.IsBackground = false;
            child.Start();
            //child.Join();

            return true;
        }

        static void ChildWork(string fileName, int pregnancyDuration)
        {
            string configFileName = Path.Combine(folderTo, fileName + ".tmp");
            string filePath = Path.Combine(folderFrom, fileName);
            string fileCopyPath = Path.Combine(folderTo, fileName);

            try
            {
                File.Copy(filePath, fileCopyPath, true);
                Print($"Скопирован \"{fileCopyPath}\"");

                if (File.Exists(configFileName))
                {
                    File.SetCreationTime(configFileName, DateTime.Now.AddDays(1));
                    File.SetLastWriteTime(configFileName, DateTime.Now.AddDays(1));
                    File.SetLastAccessTime(configFileName, DateTime.Now.AddDays(1));

                    Print($"Изменен \"{configFileName}\"");
                }
                else
                {
                    Print($"Ожидание создания \"{configFileName}\"");

                    lock (typeof(Program))
                    {
                        Monitor.Wait(typeof(Program), pregnancyDuration);
                    }

                    File.WriteAllText(configFileName, DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss:fff"));
                    Print($"Создан \"{configFileName}\"");
                }
            }
            catch (Exception e)
            {
                Print(e.Message);
            }
        }

        static void Print(string message)
        {
            Console.WriteLine($"[{Thread.CurrentThread.Name}] -> {message}");
        }
    }
}
