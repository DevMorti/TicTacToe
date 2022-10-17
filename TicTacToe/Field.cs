using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class Field
    {
        public byte FieldID { get; set; }
        public Assignment Assignment { get; set; } = Assignment.Free;

        public static byte[,] winningNumbers = new byte[,]
        {
            {
                1,
                4,
                7,
                1,
                2,
                3,
                1,
                3,
            },
            {
                2,
                5,
                8,
                4,
                5,
                6,
                5,
                5,
            },
            {
                3,
                6,
                9,
                7,
                8,
                9,
                9,
                7,
            }
        };

        public Field(byte fieldID, Assignment assignment)
        {
            FieldID = fieldID;
            Assignment = assignment;
        }

        public static Field[] CreateNewFields()
        {
            Field[] fields = new Field[9];
            for (byte i = 0; i < 9; i++)
            {
                fields[i] = new Field((byte)(i + 1), Assignment.Free);
            }
            return fields;
        }

        public static void ShowTicTacToe(Field[] fields)
        {
            foreach(Field field in fields)
            {
               if(field.FieldID == 4 || field.FieldID == 7)
                {
                    Console.WriteLine();
                }
                switch (field.Assignment)
                {
                    case Assignment.Free:
                        Console.Write(field.FieldID + " ");
                        break;
                    case Assignment.Computer:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("o ");
                        break;
                    case Assignment.User:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("x ");
                        break;
                }
                Console.ResetColor();
            }
        }

        public static Assignment GetWinner(Field[] fields)
        {
            byte[] fieldsofuser = GetFieldsOf(fields, Assignment.User);
            byte[] fieldsofcomputer = GetFieldsOf(fields, Assignment.Computer);

            if (MemberIsWinner(fieldsofuser,3))
            {
                return Assignment.User;
            }
            else if (MemberIsWinner(fieldsofcomputer,3))
            {
                return Assignment.Computer;
            }
            else
            {
                return Assignment.Free;
            }
        }

        public static bool MemberIsWinner(byte[] fieldsOfMember,byte neededFieldsToWin)
        {
            bool memberIsWinner = false;
            int fieldsNextToEachOther = 0;

            for (byte i = 0; i < winningNumbers.GetLength(1); i++)
            {
                for (byte j = 0; j < winningNumbers.GetLength(0); j++)
                {
                    foreach (byte k in fieldsOfMember)
                    {
                        if (k == winningNumbers[j, i])
                        {
                            fieldsNextToEachOther++;
                        }
                    }
                }
                if (fieldsNextToEachOther >= neededFieldsToWin)
                {
                    memberIsWinner = true;
                }
                else
                {
                    fieldsNextToEachOther = 0;
                }
            }

            return memberIsWinner;
        }

        public static byte[] GetFieldsOf(Field[] fields, Assignment assignment)
        {
            byte countedfields = CountFieldsOf(fields, assignment);
            byte[] fieldsOfAssignment = new byte[countedfields];

            byte nextpointinarray = 0;
            for(byte i = 0; i < countedfields; i++)
            {
                for(byte j = nextpointinarray; true; j++)
                {
                    if (fields[j].Assignment == assignment)
                    {
                        fieldsOfAssignment[i] = fields[j].FieldID;
                        nextpointinarray = (byte)(j + 1);
                        break;
                    }
                }
            }

            return fieldsOfAssignment;
        }

        public static byte CountFieldsOf(Field[] fields, Assignment assignment)
        {
            byte countedFieldsOfAssignment = 0;
            foreach (Field field in fields)
            {
                if (field.Assignment == assignment)
                {
                    countedFieldsOfAssignment++;
                }
            }

            return countedFieldsOfAssignment;
        }

        public static bool IsFreeField(Field[] fields, byte chosenfield)
        {
            byte[] freefields = GetFieldsOf(fields, Assignment.Free);
            foreach (byte freefield in freefields)
            {
                if (freefield == chosenfield)
                {
                    return true;
                }
            }
            return false;
        }

        public static byte GetDiagonalOf(byte cornerfield) 
        {
            switch (cornerfield)
            {
                case 1: return 9;
                case 3: return 7;
                case 7: return 3;
                case 9: return 1;
                default: return 0;
            }
        }

        public static bool IsCornerField(byte field)
        {
            if(field == 1 || field == 3 || field == 7 || field == 9)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    enum Assignment
    {
        Computer,
        User,
        Free
    }
}
