using System.Diagnostics;

namespace Process_Start
{
    /// <summary>
    /// Project > Process_Start Properties > Application > Output time: Windows Application
    /// </summary>
    class Program
    {
        static void Main()
        {
            string timerPath = @"E:\VS\CSharp\Process_Toy_Timer\bin\Debug\Process_Toy_Timer.exe";

            Process timer1 = new Process();
            ProcessStartInfo timerProcessInfo = new ProcessStartInfo(timerPath, "3 yellow");
            timer1.StartInfo = timerProcessInfo;
            timer1.Start();
            timer1.WaitForExit();

            Process timer2 = new Process();
            ProcessStartInfo timer2ProcessInfo = new ProcessStartInfo(timerPath, "3 green");
            timer2.StartInfo = timer2ProcessInfo;
            timer2.Start();
            timer2.WaitForExit();

            Process.Start(timerPath, "3 cyan");
        }
    }
}
