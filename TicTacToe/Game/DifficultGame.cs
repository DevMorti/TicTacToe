using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class DifficultGame : Game, IGame
    {
        new public Field[] Fields { get; set; }
        new public IComputer Computer { get; set; }

        public DifficultGame()
        {
            Fields = Field.CreateNewFields();
            Computer = new DifficultComputer(Fields,Assignment.Computer,Assignment.User);
        }
        public override void GameLoop()
        {
            while (true)
            {
                SyncGameInConsole(Fields);
                Console.WriteLine();
                AskUserForField(Fields);

                if (Field.GetWinner(Fields) != Assignment.Free)
                {
                    IfThereIsAWinner(Fields);
                    break;
                }

                else if (Field.CountFieldsOf(Fields, Assignment.Free) == 0)
                {
                    IfFieldsAreCovered();
                    break;
                }

                SyncGameInConsole(Fields);
                Fields[Computer.GetNextFieldID() - 1].Assignment = Assignment.Computer;
                System.Threading.Thread.Sleep(1000);


                if (Field.GetWinner(Fields) != Assignment.Free)
                {
                    SyncGameInConsole(Fields);
                    Console.ReadKey();
                    IfThereIsAWinner(Fields);
                    break;
                }

                else if (Field.CountFieldsOf(Fields, Assignment.Free) == 0)
                {
                    IfFieldsAreCovered();
                    break;
                }
            }
        }
    }
}
