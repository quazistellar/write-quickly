using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextQuick
{
    public static class Leaderboard
    {
        private static List<User> users = new List<User>();

        public static void AddUser(User user)
        {
            LoadLeaders(); 
            users.Add(user);
            SaveLeaders(); 
        }


        public static void LoadLeaders()
        {
            string desktop_road = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(desktop_road, "Таблица рекордов.json");

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                users = JsonConvert.DeserializeObject<List<User>>(json);
            }
        }

        public static void ShowLeaders()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine(" >>> Таблица рекордов <<<");
            Console.WriteLine("------------------------------------------------------------------------");
            Console.WriteLine("    Имя пользователя\tСимволы в минуту\tСимволы в секунду ");
            Console.ResetColor();

            foreach (User user in users)
            {
                Console.WriteLine($"         {user.name.PadRight(15)}       {user.symb_min}{user.symb_sec.PadLeft(26)}");
            }
        }

        public static void SaveLeaders()
        {
            string for_json = JsonConvert.SerializeObject(users, Formatting.Indented);
            string desktop_road = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            File.WriteAllText(Path.Combine(desktop_road, "Таблица рекордов.json"), for_json);
        }
    }
}
