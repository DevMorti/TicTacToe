using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class ComputerGame : Game, IGame
    {
        new public Field[] Fields { get; set; }
        new public IComputer Computer { get; set; }
        public IComputer Computer2 { get; set; }

        public ComputerGame()
        {
            Random random = new Random();
            Fields = Field.CreateNewFields();
            Fields[random.Next(Fields.Length)].Assignment = Assignment.Computer;
            byte chosenfield;
            do
            {
                chosenfield = (byte)random.Next(Fields.Length);
                if (Field.IsFreeField(Fields, chosenfield))
                {
                    Fields[chosenfield].Assignment = Assignment.User;
                }
                
            } while (!Field.IsFreeField(Fields, chosenfield)) ;
                Computer = new DifficultComputer(Fields,Assignment.Computer,Assignment.User);
            Computer2 = new DifficultComputer(Fields,Assignment.User,Assignment.Computer);
        }

        public override void GameLoop()
        {
            while (true)
            {
                SyncGameInConsole(Fields);
                Fields[Computer.GetNextFieldID() - 1].Assignment = Assignment.Computer;
                Console.WriteLine();
                Console.WriteLine("Tippe eine beliebige Taste, um das Spiel fortzuführen.");
                Console.ReadKey();

                if (Field.GetWinner(Fields) != Assignment.Free)
                {
                    SyncGameInConsole(Fields);
                    Console.ReadKey();
                    IfThereIsAWinner(Fields);
                    break;
                }

                else if (Field.CountFieldsOf(Fields, Assignment.Free) == 0)
                {
                    SyncGameInConsole(Fields);
                    Console.ReadKey();
                    IfFieldsAreCovered();
                    break;
                }

                SyncGameInConsole(Fields);
                Fields[Computer2.GetNextFieldID() - 1].Assignment = Assignment.User;
                Console.WriteLine();
                Console.WriteLine("Tippe eine beliebige Taste, um das Spiel fortzuführen.");
                Console.ReadKey();


                if (Field.GetWinner(Fields) != Assignment.Free)
                {
                    SyncGameInConsole(Fields);
                    Console.ReadKey();
                    IfThereIsAWinner(Fields );
                    break;
                }

                else if (Field.CountFieldsOf(Fields, Assignment.Free) == 0)
                {
                    IfFieldsAreCovered();
                    break;
                }
            }
        }

        public override void IfThereIsAWinner(Field[] fields)
        {
            Assignment winner = Field.GetWinner(Fields);
            if (winner == Assignment.User)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("x hat gewonnen!");
                Console.ResetColor();
                Console.ReadKey();
            }
            else if (winner == Assignment.Computer)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("o hat gewonnen!");
                Console.ResetColor();
                Console.ReadKey();
            }
        }

    }
}
