using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    interface IGame
    {
        Field[] Fields { get; set; }
        IComputer Computer { get; set; }

        void GameLoop();
    }
}
