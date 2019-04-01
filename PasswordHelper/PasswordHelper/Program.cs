using System;
using System.Collections.Generic;
using System.IO;
using PasswordHelper.Menus;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.ComponentModel;
using System.Threading;

namespace PasswordHelper
{
    internal class Program
    {
        private static BackgroundWorker worker = new BackgroundWorker();

        public static List<App> apps = new List<App>();
        public static readonly string saveFileName = "apps.txt";

        private const int SW_RESTORE = 9;

        private const int VK_ALT = 0x12;
        private const int VK_J = 0x4A;

        [STAThread]
        static void Main(string[] args)
        {
            if (File.Exists(saveFileName)) apps = App.Load(saveFileName);
            else
            {
                File.Create(saveFileName);
                File.SetAttributes(saveFileName, FileAttributes.Hidden);
                apps.Add(new App("Telegram", "login", "password"));
            }

            worker.DoWork += CheckHotKeyPress;
            worker.RunWorkerAsync();

            IMenu mainMenu = new MainMenu();

            while (true) mainMenu.Show();
        }

        private static void CheckHotKeyPress(object sender, DoWorkEventArgs e)
        {
            var processes = Process.GetProcessesByName("PasswordHelper");

            while (true)
            {
                if (GetAsyncKeyState(VK_ALT) == short.MinValue && GetAsyncKeyState(VK_J) == short.MinValue)
                {
                    if (processes.Length > 0)
                    {
                        ShowWindow(processes[0].MainWindowHandle, SW_RESTORE);
                        SetForegroundWindow(processes[0].MainWindowHandle);
                    }
                }
                Thread.Sleep(40);
            }
        }

        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(int vKey);

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    }
}
