using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using System.Threading.Tasks;

namespace PasswordHelper.Menus
{
    internal class MainMenu : IMenu
    {
        public string Title { get; }

        public List<Option> Options { get; }

        public MainMenu(string title = null)
        {
            Title = title;

            Options = new List<Option>()
            {
                new Option("Скопировать логин", () => CopyLogin(Program.apps)),
                new Option("Скопировать пароль", () => CopyPassword(Program.apps)),
                new Option("Изменить логин", () => ChangeLogin(Program.apps)),
                new Option("Изменить пароль", () => ChangePassword(Program.apps)),
                new Option("Добавить новое приложение", () => AddNewApp(Program.apps)),
                new Option("Импорт приложений", () => Environment.Exit(0)),
                new Option("Экспорт приложений", () => Environment.Exit(0)),
                new Option("Выход", () => Environment.Exit(0))
            };
        }

        public void Show()
        {
            Console.Clear();

            if (Title != null) Console.WriteLine(Title);

            for (int i = 0; i < Options.Count; i++)
                Console.WriteLine($"{i + 1}. {Options[i].Name}");

            int selectedOption = int.Parse(Console.ReadLine());

            Options[selectedOption - 1].Action();
        }

        private void CopyLogin(List<App> apps)
        {
            Console.Clear();
            Console.WriteLine("Введите номер приложения, логин которого нужно скопировать.");

            for (int i = 0; i < apps.Count; i++)
                Console.WriteLine($"{i + 1}. {apps[i].AppName}");

            int option = int.Parse(Console.ReadLine());

            Clipboard.SetText(apps[option - 1].Login);
        }

        private void CopyPassword(List<App> apps)
        {
            Console.Clear();
            Console.WriteLine("Введите номер приложения, пароль которого нужно скопировать.");

            for (int i = 0; i < apps.Count; i++)
                Console.WriteLine($"{i + 1}. {apps[i].AppName}");

            int option = int.Parse(Console.ReadLine());

            Clipboard.SetText(apps[option - 1].Password);
        }

        private void ChangeLogin(List<App> apps)
        {
            Console.Clear();
            Console.WriteLine("Введите номер приложения, логин которого нужно изменить.");

            for (int i = 0; i < apps.Count; i++)
                Console.WriteLine($"{i + 1}. {apps[i].AppName}");

            int option = int.Parse(Console.ReadLine());
            Console.WriteLine($"Введите новый логин для {apps[option - 1].AppName}.");
            string newLogin = Console.ReadLine();

            apps[option - 1] = new App(apps[option - 1].AppName, newLogin, apps[option - 1].Password);

            Clipboard.SetText(apps[option - 1].Login);
        }

        private void ChangePassword(List<App> apps)
        {
            Console.Clear();
            Console.WriteLine("Введите номер приложения, пароль которого нужно изменить.");

            for (int i = 0; i < apps.Count; i++)
                Console.WriteLine($"{i + 1}. {apps[i].AppName}");

            int option = int.Parse(Console.ReadLine());
            Console.WriteLine($"Введите новый пароль для {apps[option - 1].AppName}.");
            string newPassword = Console.ReadLine();

            apps[option - 1] = new App(apps[option - 1].AppName, apps[option - 1].Login, newPassword);

            Clipboard.SetText(apps[option - 1].Password);
        }

        private void AddNewApp(List<App> apps)
        {
            Console.Clear();

            Console.WriteLine("Введите имя приложения.");
            string appName = Console.ReadLine();

            Console.WriteLine($"Введите логин для {appName}.");
            string login = Console.ReadLine();

            Console.WriteLine($"Введите пароль для {appName}.");
            string password = Console.ReadLine();

            apps.Add(new App(appName, login, password));
        }
    }
}
