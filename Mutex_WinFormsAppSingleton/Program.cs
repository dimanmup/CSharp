using System;
using System.Threading;
using System.Windows.Forms;

namespace Mutex_WinFormsAppSingleton
{
    static class Program
    {
        public static Mutex mutex = new Mutex(false, "my singleton winform app");

        [STAThread]
        static void Main()
        {
            if (mutex.WaitOne(TimeSpan.Zero))
            {
                Application.SetHighDpiMode(HighDpiMode.SystemAware);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            else
            {
                MessageBox.Show(
                    "Экземпляр ПО уже запущен!", 
                    "Внимание!", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Warning);

                return;
            }            
        }
    }
}
