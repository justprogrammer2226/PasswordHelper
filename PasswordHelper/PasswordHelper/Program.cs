using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordHelper
{
    class Program
    {
        private static List<App> apps = new List<App>();

        [STAThread]
        static void Main(string[] args)
        {
            apps.Add(new App("Telegram", "login", "password"));

            Menu startMenu = new Menu(new List<Option>()
            {
                new Option("Скопировать логин", () =>
                {
                    if (apps.Count != 0)
                    {
                        Console.Clear();
                        Console.WriteLine("Введите номер приложения, логин которого нужно скопировать.");

                        for (int i = 0; i < apps.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {apps[i].AppName}");
                        }

                        int option = int.Parse(Console.ReadLine());

                        Clipboard.SetText(apps[option - 1].Login);
                    }
                }),
                new Option("Скопировать пароль", () =>
                {
                    if (apps.Count != 0)
                    {
                        Console.Clear();
                        Console.WriteLine("Введите номер приложения, пароль которого нужно скопировать.");

                        for (int i = 0; i < apps.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {apps[i].AppName}");
                        }

                        int option = int.Parse(Console.ReadLine());

                        Clipboard.SetText(apps[option - 1].Password);
                    }
                }),
                new Option("Изменить логин", () =>
                {
                    if (apps.Count != 0)
                    {
                        Console.Clear();
                        Console.WriteLine("Введите номер приложения, логин которого нужно изменить.");

                        for (int i = 0; i < apps.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {apps[i].AppName}");
                        }

                        int option = int.Parse(Console.ReadLine());
                        Console.WriteLine($"Введите новый логин для {apps[option - 1].AppName}.");
                        string newLogin = Console.ReadLine();

                        apps[option - 1] = new App(apps[option - 1].AppName, newLogin, apps[option - 1].Password);

                        Clipboard.SetText(apps[option - 1].Login);
                    }
                }),
                new Option("Изменить пароль", () =>
                {
                    if (apps.Count != 0)
                    {
                        Console.Clear();
                        Console.WriteLine("Введите номер приложения, пароль которого нужно изменить.");

                        for (int i = 0; i < apps.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {apps[i].AppName}");
                        }

                        int option = int.Parse(Console.ReadLine());
                        Console.WriteLine($"Введите новый пароль для {apps[option - 1].AppName}.");
                        string newPassword = Console.ReadLine();

                        apps[option - 1] = new App(apps[option - 1].AppName, apps[option - 1].Login, newPassword);

                        Clipboard.SetText(apps[option - 1].Password);
                    }
                }),
                new Option("Добавить логин и пароль", () => Environment.Exit(0)),
                new Option("Выход", () => Environment.Exit(0))

            });

            startMenu.Show();
        }
    }
}
