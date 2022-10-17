using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    abstract class Computer : IComputer
    {
        public Field[] Fields { get; set; }
        public Assignment MyAssignment { get; set; }
        public Assignment RivalAssignment { get; set; }

        public Computer(Field[] fields, Assignment myassignment, Assignment rivalassignment)
        {
            Fields = fields;
            MyAssignment = myassignment;
            RivalAssignment = rivalassignment;
        }
        public abstract byte GetNextFieldID();
    }
}
