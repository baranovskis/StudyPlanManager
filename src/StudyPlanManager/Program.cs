using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace StudyPlanManager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var mutex = new Mutex(false, "StudyPlanManagerInstance"))
            {
                // Check instance
                if (mutex.WaitOne(0, false))
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new CustomContext());
                }
                else
                {
                    Process.Start(CustomContext.BaseAddress);
                }

                mutex.Close();
            }
        }
    }
}
