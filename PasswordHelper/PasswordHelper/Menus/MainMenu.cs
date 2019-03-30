using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

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
                new Option("Импорт приложений", () => ImportApps(Program.apps)),
                new Option("Экспорт приложений", () => ExportApps(Program.apps)),
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
            OutputApps(apps, "Введите номер приложения, логин которого нужно скопировать.");

            int option = int.Parse(Console.ReadLine());

            Clipboard.SetText(apps[option - 1].Login);
        }

        private void CopyPassword(List<App> apps)
        {
            OutputApps(apps, "Введите номер приложения, пароль которого нужно скопировать.");

            int option = int.Parse(Console.ReadLine());

            Clipboard.SetText(apps[option - 1].Password);
        }

        private void ChangeLogin(List<App> apps)
        {
            OutputApps(apps, "Введите номер приложения, логин которого нужно изменить.");

            int option = int.Parse(Console.ReadLine());
            Console.WriteLine($"Введите новый логин для {apps[option - 1].AppName}.");
            string newLogin = Console.ReadLine();

            apps[option - 1] = new App(apps[option - 1].AppName, newLogin, apps[option - 1].Password);

            Clipboard.SetText(apps[option - 1].Login);
            App.Save(Program.saveFileName, apps);
        }

        private void ChangePassword(List<App> apps)
        {
            OutputApps(apps, "Введите номер приложения, пароль которого нужно изменить.");

            int option = int.Parse(Console.ReadLine());
            Console.WriteLine($"Введите новый пароль для {apps[option - 1].AppName}.");
            string newPassword = Console.ReadLine();

            apps[option - 1] = new App(apps[option - 1].AppName, apps[option - 1].Login, newPassword);

            Clipboard.SetText(apps[option - 1].Password);
            App.Save(Program.saveFileName, apps);
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
            App.Save(Program.saveFileName, apps);
        }

        private void ImportApps(List<App> apps)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text documents (.txt)|*.txt";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                int option;
                while(true)
                {
                    Console.Clear();
                    Console.WriteLine("При импорте другого файла, исходный файл будет удалён, вы дейсвительно хотите импортировать новый файл?");
                    Console.WriteLine("1. Да");
                    Console.WriteLine("2. Нет");

                    option = int.Parse(Console.ReadLine());

                    if (option == 1)
                    {
                        StreamReader sr = new StreamReader(openFileDialog.FileName);
                        StreamWriter sw = new StreamWriter(new FileStream(Program.saveFileName, FileMode.OpenOrCreate));

                        sw.Write(sr.ReadToEnd());
                        sr.Close();
                        sw.Close();

                        break;
                    }
                    else if (option == 2) break;

                    Program.apps = App.Load(Program.saveFileName);
                }
            }
        }

        private void ExportApps(List<App> apps)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text documents (.txt)|*.txt";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
                App.Save(openFileDialog.FileName, apps);
        }

        private void OutputApps(List<App> apps, string title = null)
        {
            Console.Clear();
            if(title != null) Console.WriteLine(title);

            for (int i = 0; i < apps.Count; i++)
                Console.WriteLine($"{i + 1}. {apps[i].AppName}");
        }
    }
}
