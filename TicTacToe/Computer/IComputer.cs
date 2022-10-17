using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    internal interface IComputer
    {
        Assignment MyAssignment { get; set; }
        Assignment RivalAssignment { get; set; }
        byte GetNextFieldID();
    }
}
