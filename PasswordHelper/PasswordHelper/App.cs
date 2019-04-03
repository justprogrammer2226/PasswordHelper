using System.Collections.Generic;
using System.IO;

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

        /// <summary> Сохраняет список приложений по указаному путю. </summary>
        /// <param name="path"> Путь к файлу, куда нужно сохранить список приложений. </param>
        /// <param name="apps"> Список приложений, которые нужно сохранить. </param>
        public static void Save(string path, List<App> apps)
        {
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
            StreamWriter writer = new StreamWriter(fs);

            for(int i = 0; i < apps.Count; i++)
                writer.WriteLine($"{apps[i].AppName}-{apps[i].Login}-{apps[i].Password}");

            writer.Close();
            fs.Close();
        }

        /// <summary> Загружает список приложений, по указаному путю. </summary>
        /// <param name="path"> Путь к файлу, откуда нужно загрузить список приложений. </param>
        /// <returns> Список приложений, загруженых с файла. </returns>
        public static List<App> Load(string path)
        {
            List<App> apps = new List<App>();

            FileStream fs = new FileStream(path, FileMode.Open);
            StreamReader reader = new StreamReader(fs);

            while(!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] infoAboutApp = line.Split('-');

                apps.Add(new App(infoAboutApp[0], infoAboutApp[1], infoAboutApp[2]));
            }

            reader.Close();
            fs.Close();

            return apps;
        }
    }
}
