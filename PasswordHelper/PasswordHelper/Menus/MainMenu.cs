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
                new Option("Удалить приложение", () => DeleteApp(Program.apps)),
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

            if(int.TryParse(Console.ReadLine(), out int indexSelectedOption) && indexSelectedOption >= 1 && indexSelectedOption <= Options.Count)
            {
                Options[indexSelectedOption - 1].Action();
            }
            else
            {
                Console.WriteLine("Данной опции не существует.");
                Console.ReadKey();
            }
        }

        private void CopyLogin(List<App> apps)
        {
            while(true)
            {
                OutputApps(apps, "Введите номер приложения, логин которого нужно скопировать.");

                if (int.TryParse(Console.ReadLine(), out int indexSelectedOption) && indexSelectedOption >= 1 && indexSelectedOption <= apps.Count)
                {
                    Clipboard.SetText(apps[indexSelectedOption - 1].Login);
                    break;
                }
                else
                {
                    Console.WriteLine("Данной опции не существует.");
                    Console.ReadKey();
                }
            }
        }

        private void CopyPassword(List<App> apps)
        {
            while (true)
            {
                OutputApps(apps, "Введите номер приложения, пароль которого нужно скопировать.");

                if (int.TryParse(Console.ReadLine(), out int indexSelectedOption) && indexSelectedOption >= 1 && indexSelectedOption <= apps.Count)
                {
                    Clipboard.SetText(apps[indexSelectedOption - 1].Password);
                    break;
                }
                else
                {
                    Console.WriteLine("Данной опции не существует.");
                    Console.ReadKey();
                }
            }
        }

        private void ChangeLogin(List<App> apps)
        {
            while(true)
            {
                OutputApps(apps, "Введите номер приложения, логин которого нужно изменить.");

                if (int.TryParse(Console.ReadLine(), out int indexSelectedOption) && indexSelectedOption >= 1 && indexSelectedOption <= apps.Count)
                {
                    Console.WriteLine($"Введите новый логин для {apps[indexSelectedOption - 1].AppName}.");
                    string newLogin = Console.ReadLine();

                    apps[indexSelectedOption - 1] = new App(apps[indexSelectedOption - 1].AppName, newLogin, apps[indexSelectedOption - 1].Password);

                    Clipboard.SetText(apps[indexSelectedOption - 1].Login);
                    App.Save(Program.saveFileName, apps);

                    break;
                }
                else
                {
                    Console.WriteLine("Данной опции не существует.");
                    Console.ReadKey();
                }
            }
        }

        private void ChangePassword(List<App> apps)
        {
            while (true)
            {
                OutputApps(apps, "Введите номер приложения, пароль которого нужно изменить.");

                if (int.TryParse(Console.ReadLine(), out int indexSelectedOption) && indexSelectedOption >= 1 && indexSelectedOption <= apps.Count)
                {
                    Console.WriteLine($"Введите новый пароль для {apps[indexSelectedOption - 1].AppName}.");
                    string newPassword = Console.ReadLine();

                    apps[indexSelectedOption - 1] = new App(apps[indexSelectedOption - 1].AppName, apps[indexSelectedOption - 1].Login, newPassword);

                    Clipboard.SetText(apps[indexSelectedOption - 1].Password);
                    App.Save(Program.saveFileName, apps);

                    break;
                }
                else
                {
                    Console.WriteLine("Данной опции не существует.");
                    Console.ReadKey();
                }
            }
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

        private void DeleteApp(List<App> apps)
        {
            while (true)
            {
                OutputApps(apps, "Введите номер приложения, которое нужно удалить.");

                if (int.TryParse(Console.ReadLine(), out int indexSelectedOption) && indexSelectedOption >= 1 && indexSelectedOption <= apps.Count)
                {
                    Console.Clear();
                    Console.WriteLine($"Вы уверены, что хотите удалить логин и пароль для {apps[indexSelectedOption - 1].AppName}?");
                    Console.WriteLine($"1. Да.");
                    Console.WriteLine($"2. Нет.");

                    int indexApp = indexSelectedOption - 1;

                    if (int.TryParse(Console.ReadLine(), out indexSelectedOption) && indexSelectedOption == 1)
                    {
                        apps.RemoveAt(indexApp);
                        App.Save(Program.saveFileName, apps);
                    }
                    else if (indexSelectedOption == 2)
                    {
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Данной опции не существует.");
                    Console.ReadKey();
                }
            }
        }

        private void ImportApps(List<App> apps)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text documents (.txt)|*.txt";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                while(true)
                {
                    Console.Clear();
                    Console.WriteLine("При импорте другого файла, исходный файл будет удалён, вы дейсвительно хотите импортировать новый файл?");
                    Console.WriteLine("1. Да");
                    Console.WriteLine("2. Нет");

                    if (int.TryParse(Console.ReadLine(), out int indexSelectedOption) && indexSelectedOption == 1)
                    {
                        StreamReader sr = new StreamReader(openFileDialog.FileName);
                        StreamWriter sw = new StreamWriter(new FileStream(Program.saveFileName, FileMode.OpenOrCreate));

                        sw.Write(sr.ReadToEnd());
                        sr.Close();
                        sw.Close();

                        Program.apps = App.Load(Program.saveFileName);

                        break;
                    }
                    else if (indexSelectedOption == 2) break;
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
