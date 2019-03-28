using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordHelper
{
    internal struct App
    {
        public string AppName { get; }
        public string Login { get; }
        public string Password { get; }

        public App(string appName, string login, string password)
        {
            AppName = appName;
            Login = login;
            Password = password;
        }

        //public static void Save(string path, List<App> apps)
        //{

        //}

        //public static List<App> Load(string path)
        //{

        //}
    }
}
