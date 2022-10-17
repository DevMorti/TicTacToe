using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{

    class EasyComputer : Computer, IComputer
    {
        public EasyComputer(Field[] fields, Assignment myassignment, Assignment rivalassignment) : base(fields, myassignment, rivalassignment) { }
        public override byte GetNextFieldID()
        {
            Random random = new Random();
            byte[] freeFields = Field.GetFieldsOf(Fields,Assignment.Free);
            return freeFields[random.Next(1,freeFields.Length)];
        }
    }
}
