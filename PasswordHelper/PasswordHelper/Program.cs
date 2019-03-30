using System;
using System.Collections.Generic;
using System.IO;
using PasswordHelper.Menus;

namespace PasswordHelper
{
    internal class Program
    {
        public static List<App> apps = new List<App>();
        public static readonly string saveFileName = "apps.txt";

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

            IMenu mainMenu = new MainMenu();

            while (true) mainMenu.Show();
        }
    }
}
