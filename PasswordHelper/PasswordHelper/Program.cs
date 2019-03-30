using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasswordHelper.Menus;

namespace PasswordHelper
{
    class Program
    {
        public static List<App> apps = new List<App>();
        public static readonly string saveFileName = "apps.txt";

        [STAThread]
        static void Main(string[] args)
        {
            if (File.Exists(saveFileName)) apps = App.Load(saveFileName);
            else apps.Add(new App("Telegram", "login", "password"));

            IMenu mainMenu = new MainMenu();

            while (true) mainMenu.Show();
        }
    }
}
