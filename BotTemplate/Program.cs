using System;
using System.Windows.Forms;
using BotTemplate.Forms;
using BotTemplate.Interact;
using System.Runtime.InteropServices;
using Utilities;
using BotTemplate.Helper;

namespace BotTemplate
{
    static class Program
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ExceptionLogger logger = new ExceptionLogger();
            logger.AddLogger(new TextFileLogger());
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new mainForm());
        }

        static void OnProcessExit(object sender, EventArgs e)
        {
            if (Inject.isHookApplied)
            {
                try
                {
                    BmWrapper.memory.WriteUInt(0x00C7B2A4, 0x0F110B73);
                    Calls.StopRunning();
                    Inject.Restore();
                }
                catch { }
            }
            Application.Exit();
        }
    }
}
