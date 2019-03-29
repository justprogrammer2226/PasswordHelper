using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasswordHelper.Menus;

namespace PasswordHelper
{
    class Program
    {
        public static List<App> apps = new List<App>();

        [STAThread]
        static void Main(string[] args)
        {
            apps.Add(new App("Telegram", "login", "password"));

            IMenu mainMenu = new MainMenu();

            while(true) mainMenu.Show();
        }
    }
}
