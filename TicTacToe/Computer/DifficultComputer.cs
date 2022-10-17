using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class DifficultComputer : Computer, IComputer
    {
        public DifficultComputer(Field[] fields, Assignment myassignment, Assignment rivalassignment) : base(fields, myassignment, rivalassignment) { }

        public override byte GetNextFieldID()
        {
            Random random = new Random();
            if (FieldToWinExists(MyAssignment))
            {
                return GetFieldToWin(MyAssignment);
            }
            else if (FieldToWinExists(RivalAssignment))
            {
                return GetFieldToWin(RivalAssignment);
            }
            if (Field.CountFieldsOf(Fields, MyAssignment) == 0)
            {
                if (Field.IsFreeField(Fields, 5))
                {
                    return 5;
                }
                else if (Field.IsFreeField(Fields, 1))
                {
                    return 1;
                }
                else if (Field.IsFreeField(Fields, 3))
                {
                    return 3;
                }
                else if (Field.IsFreeField(Fields, 7))
                {
                    return 7;
                }
                else if (Field.IsFreeField(Fields, 9))
                {
                    return 9;
                }
                else
                {
                    return Field.GetFieldsOf(Fields,Assignment.Free)[random.Next(1, Field.GetFieldsOf(Fields,Assignment.Free).Length)];
                }
            }
            List<byte> fieldsToWin = GetFieldsToWin(MyAssignment);
            List<byte> temp_fieldsToWin = new List<byte>();
            if (ProblematicFieldsExist(RivalAssignment))
            {
                foreach (byte field in fieldsToWin)
                {
                    if (!Field.IsCornerField(field))
                    {
                        temp_fieldsToWin.Add(field);
                    }
                }
                return SortFieldsToWin(temp_fieldsToWin);
            }

            else
            {
                return SortFieldsToWin(GetFieldsToWin(MyAssignment));
            }
        }

        private List<byte> GetFieldsToWin(Assignment assignment)
        {
            //Anpassungen für Lists
            sbyte fieldsNextToEachOther = 0;
            List<byte> fieldIDs = new List<byte>();
            for (byte i = 0; i < Field.winningNumbers.GetLength(1); i++)
            {
                for (byte j = 0; j < Field.winningNumbers.GetLength(0); j++)
                {
                    if (Field.GetFieldsOf(Fields, assignment).Contains(Field.winningNumbers[j, i]))
                    {
                        fieldsNextToEachOther++;
                    }
                    else if (!Field.IsFreeField(Fields, Field.winningNumbers[j, i]))
                    {
                        fieldsNextToEachOther = 0;
                        break;
                    }
                }

                if (fieldsNextToEachOther >= 1)
                {
                    for (byte l = 0; l < Field.winningNumbers.GetLength(0); l++)
                    {
                        if (Field.IsFreeField(Fields, Field.winningNumbers[l, i]) && !fieldIDs.Contains(Field.winningNumbers[l, i]))
                        {
                            fieldIDs.Add(Field.winningNumbers[l, i]);
                        }
                    }
                }

                fieldsNextToEachOther = 0;
            }
            if (fieldIDs.Count != 0)
            {
                return fieldIDs;
            }
            else
            {
                Random random = new Random();
                List<byte> randomlist = new List<byte>()
                {
                    Field.GetFieldsOf(Fields,Assignment.Free)[random.Next(0, Field.GetFieldsOf(Fields,Assignment.Free).Length)],
                    Field.GetFieldsOf(Fields,Assignment.Free)[random.Next(0, Field.GetFieldsOf(Fields,Assignment.Free).Length)],
                    Field.GetFieldsOf(Fields,Assignment.Free)[random.Next(0, Field.GetFieldsOf(Fields,Assignment.Free).Length)]
                };
                return randomlist;
            }
        }

        private bool FieldToWinExists(Assignment assignment)
        {
            byte fieldsNextToEachOther = 0;
            bool fieldToWinExists = false;
            for (byte i = 0; i < Field.winningNumbers.GetLength(1); i++)
            {
                for (byte j = 0; j < Field.winningNumbers.GetLength(0); j++)
                {
                    foreach (byte k in Field.GetFieldsOf(Fields, assignment))
                    {
                        if (k == Field.winningNumbers[j, i])
                        {
                            fieldsNextToEachOther++;
                        }
                    }
                }
                if (fieldsNextToEachOther == 2)
                {
                    for (byte l = 0; l < Field.winningNumbers.GetLength(0); l++)
                    {
                        if (Field.IsFreeField(Fields, Field.winningNumbers[l, i]))
                        {
                            fieldToWinExists = true;
                        }
                    }
                }
                fieldsNextToEachOther = 0;
            }
            return fieldToWinExists;
        }

        private byte SortFieldsToWin(List<byte> fieldsToWin)
        {
            Random random = new Random();
            List<byte> fieldsToWinUser = GetFieldsToWin(RivalAssignment);
            List<byte> temp_fieldsToWin = new List<byte>();
            byte finalField = 0;
            bool foundfinalField = false;
            foreach (byte fieldToWinUser in fieldsToWinUser)
            {
                if (fieldsToWin.Contains(fieldToWinUser))
                {
                    temp_fieldsToWin.Add(fieldToWinUser);
                }
            }
            if (temp_fieldsToWin.Count > 0)
            {
                fieldsToWin.Clear();
                foreach (byte fieldToWin in temp_fieldsToWin)
                {
                    fieldsToWin.Add(fieldToWin);
                }
            }
            temp_fieldsToWin.Clear();
            //Fehler bei foreach Schleife: Referenz temp_ von fieldsToWin darf nicht währenddessen verändert werden
            for (int i = fieldsToWin.Count - 1; i >= 0; i--)
            {
                byte fieldToWin = fieldsToWin[i];
                if ((fieldToWin == 1 || fieldToWin == 3 || fieldToWin == 7 || fieldToWin == 9))
                {
                    temp_fieldsToWin.Add(fieldToWin);
                }
                else if (fieldToWin == 5)
                {
                    finalField = fieldToWin;
                    foundfinalField = true;
                    break;
                }
            }

            if (foundfinalField)
            {
                return finalField;
            }

            else if (temp_fieldsToWin.Count == 1)
            {
                return temp_fieldsToWin[0];
            }

            else if (temp_fieldsToWin.Count > 0)
            {
                return temp_fieldsToWin[random.Next(0, temp_fieldsToWin.Count)];
            }
            else if (temp_fieldsToWin.Count == 0 && fieldsToWin.Count > 0)
            {
                return fieldsToWin[random.Next(0, fieldsToWin.Count)];
            }
            else
            {
                return Field.GetFieldsOf(Fields,Assignment.Free)[random.Next(0, Field.GetFieldsOf(Fields,Assignment.Free).Length)];
            }
        }

        private byte GetFieldToWin(Assignment assignment)
        {
            byte fieldsNextToEachOther = 0;
            byte fieldID = SortFieldsToWin(GetFieldsToWin(assignment));

            for (byte i = 0; i < Field.winningNumbers.GetLength(1); i++)
            {
                for (byte j = 0; j < Field.winningNumbers.GetLength(0); j++)
                {
                    foreach (byte k in Field.GetFieldsOf(Fields, assignment))
                    {
                        if (k == Field.winningNumbers[j, i])
                        {
                            fieldsNextToEachOther++;
                        }
                    }
                }
                if (fieldsNextToEachOther == 2)
                {
                    for (byte l = 0; l < Field.winningNumbers.GetLength(0); l++)
                    {
                        if (Field.IsFreeField(Fields, Field.winningNumbers[l, i]))
                        {
                            fieldID = Field.winningNumbers[l, i];
                        }
                    }
                }
                fieldsNextToEachOther = 0;
            }
            return fieldID;
        }

        private bool ProblematicFieldsExist(Assignment assignment)
        {
            bool fieldsExist = false;
            for (byte i = 0; i <= 9; i++)
            {
                if (Field.IsCornerField(i)
                    && Field.GetFieldsOf(Fields, assignment).Contains(i)
                    && Field.GetFieldsOf(Fields, assignment).Contains(Field.GetDiagonalOf(i)))
                {
                    fieldsExist = true;
                }
            }
            return fieldsExist;
        }
    }
}
