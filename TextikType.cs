using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Xml.Linq;

namespace TextQuick
{
    internal class TextikType
    {
        private static string name;
        private static int totalSymbols = 0;
        private static bool test = true;
        public static void menu_start()
        {
            test = true;

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine(" >>> ТЕСТ НА СКОРОПЕЧАТАНИЕ <<< ");
            Console.ResetColor();
            Console.WriteLine("Введите своё имя для таблицы рекордов: ");
            name = Console.ReadLine();
            Console.Clear();
            begin_test();

        }



        public static string text = "В самые ранние моменты Большого взрыва вся энергия и пространство были сжаты до нулевого объёма и бесконечной плотности. Астрофизики называют это сингулярностью. В начальном состоянии не существовало времени и пространства в классическом понимании. Но около 13,8 млрд лет назад произошло стремительное расширение: в секунды из сингулярности Вселенная увеличилась в миллионы раз. Из-за расширения она стала менее плотной и остыла, а материя получила возможность формироваться.";

        private static void begin_test()
        {

            string[] textArray = { text };

            Console.WriteLine(text);
            Console.WriteLine("----------------------------------------");

            Console.WriteLine("Как только будете готовы, нажмите Enter");

            ConsoleKeyInfo keyInfo = Console.ReadKey();
            ConsoleKeyInfo key;

            Stopwatch timer = new Stopwatch();
            if (keyInfo.Key == ConsoleKey.Enter)
            {
                Thread timerThread = new Thread(_ =>
                {
                    for (int i = 60; i >= 0; i--)
                    {
                        timer.Start();
                        Console.SetCursorPosition(0, 20);
                        Console.WriteLine("Оставшееся время: 0:" + i.ToString("D2"));
                        Thread.Sleep(1000);
                    }

                    Console.SetCursorPosition(0, 21);
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Время вышло!");
                    Console.WriteLine("Нажмите Enter, чтобы увидеть таблицу рекордов");
                    Console.ResetColor();
                    test = false;

                });

                timerThread.Start();
                var currentPos = 0;
                int top = 0;
                int storona = 0;


                while (test && currentPos < textArray[0].Length)
                {

                    if (storona >= Console.WindowWidth)
                    {
                        storona = 0;
                        top++;
                    }

                    Console.SetCursorPosition(storona, top);

                    key = Console.ReadKey(true);

                    if (key.KeyChar == textArray[0][currentPos])
                    {
                        totalSymbols++;
                        Console.SetCursorPosition(storona, top);

                        if (currentPos == textArray[0].Length)
                        {
                            Console.SetCursorPosition(currentPos + 1, 0);
                        }

                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.Write(textArray[0][currentPos]);
                        Console.ResetColor();
                        currentPos++;
                        storona++;
                    }
                }
                timerThread.Abort();
                timer.Stop();
            }

            double sec = timer.Elapsed.TotalSeconds;
            double symb_in_min = totalSymbols;
            double symb_in_sec = (totalSymbols / sec);
            string sym_sec = symb_in_sec.ToString("F2");

            User user = new User
            {
                name = name,
                symb_min = symb_in_min,
                symb_sec = sym_sec
            };

            Leaderboard.AddUser(user);
            Leaderboard.SaveLeaders();
            Console.SetCursorPosition(0, 22);
            Leaderboard.ShowLeaders();
            Console.WriteLine();

            Console.WriteLine("Хотите пройти тест ещё раз? (нажмите Z, иначе - что-то другое, чтобы выйти из программы)");

            key = Console.ReadKey();

            if (key.Key == ConsoleKey.Z)
            {
                totalSymbols = 0;
                Console.Clear();
                timer.Stop();
                Program.Main();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("----------------------------------");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Вы завершили работу с программой!");
                Console.ResetColor();
                Console.WriteLine("----------------------------------");
                Environment.Exit(0);
            }
        }
    }
}