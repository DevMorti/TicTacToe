using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    abstract class Game : IGame
    {
        public Field[] Fields { get; set; }
        public IComputer Computer { get; set; }

        public static void SyncGameInConsole(Field[] fields)
        {
            Console.Clear();
            Console.WriteLine("Willkommen in der Arena!");
            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.Red;
            Console.Write(" ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" Computer");

            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Write(" ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(" Spieler");
            Console.ResetColor();

            Console.WriteLine();
            Field.ShowTicTacToe(fields);
        }

        public static void AskUserForField(Field[] fields)
        {
            try
            {
                Console.Write("Wähle ein Feld aus: ");
                byte chosenfield = Convert.ToByte(Console.ReadLine());
                while (Field.IsFreeField(fields, chosenfield) != true)
                {
                    SyncGameInConsole(fields);
                    Console.WriteLine();
                    Console.Write("Wähle ein Feld aus: ");
                    chosenfield = Convert.ToByte(Console.ReadLine());
                }
                fields[chosenfield - 1].Assignment = Assignment.User;
            }
            catch
            {
                AskUserForField(fields);
            }
        }

        public static void IfFieldsAreCovered()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Unentschieden!");
            Console.WriteLine("Viel Glück beim nächsten Mal!");
            Console.ResetColor();
            Console.ReadKey();
        }

        public virtual void IfThereIsAWinner(Field[] fields)
        {
            Assignment winner = Field.GetWinner(fields);
            if (winner == Assignment.User)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Du hast gewonnen!");
                Console.WriteLine("Herzlichen Glückwunsch!");
                Console.ResetColor();
                Console.ReadKey();
            }
            else if (winner == Assignment.Computer)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Du hast verloren!");
                Console.WriteLine("Viel Glück beim nächsten Mal!");
                Console.ResetColor();
                Console.ReadKey();
            }
        }

        public abstract void GameLoop();
    }
}
