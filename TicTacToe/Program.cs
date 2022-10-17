using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MainMenu();
        }
        public static void MainMenu()
        {
            bool end = false;
            while (true)
            {
                Console.WriteLine("### Tic Tac Toe ###");
                Console.WriteLine("###################");
                Console.WriteLine();
                Console.WriteLine("Wähle eine Option aus: ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("[1] Einfaches Spiel starten");
                Console.WriteLine("[2] Schweres Spiel starten");
                Console.WriteLine("[3] Computer Arena");
                Console.WriteLine("[4] Beenden");
                Console.ResetColor();
                Console.Write("Auswahl: ");
                byte option;
                try
                {
                    option = Convert.ToByte(Console.ReadLine());
                }
                catch
                {
                    break;
                }
                switch (option)
                {
                        case 1:
                            EasyGame easyGame = new EasyGame();
                            easyGame.GameLoop();
                        break;
                        case 2:
                            DifficultGame difficultGame = new DifficultGame();
                            difficultGame.GameLoop();
                            break;
                        case 3:
                            ComputerGame computerGame = new ComputerGame();
                            computerGame.GameLoop();
                            break;
                        case 4:
                            end = true;
                            break;
                }
                if (end)
                {
                    break;
                }
                Console.Clear();
            }
            if (!end)
            {
                Console.Clear();
                MainMenu();
            }
        }
    }
}
