using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public static class Validate
    {
        public static string PhoneNumber(string i_PhoneNumber)
        {
            string outError = null;

            if (i_PhoneNumber != null)
            {
                for (int i = 0; i < i_PhoneNumber.Length; i++)
                {
                    if (!(i_PhoneNumber[i] >= '0' && i_PhoneNumber[i] <= '9') &&
                        i_PhoneNumber[i] != '(' &&
                        i_PhoneNumber[i] != ')' &&
                        i_PhoneNumber[i] != '-' && 
                        i_PhoneNumber[i] != '+' && 
                        i_PhoneNumber[i] != '*')
                    {
                        outError = string.Format("Phone number can consist only of numbers and symbols: +-()*");
                        break;
                    }
                }
            }

            return outError;
        }

        public static string OutOfRange(float i_CurrentValue, float i_MinValue, float i_MaxValue)
        {
            string outError = null;

            if (i_CurrentValue < i_MinValue || i_CurrentValue > i_MaxValue)
            {
                outError = string.Format("Value is out of range. Possible values are from {0} to {1}. ", i_MinValue, i_MaxValue);
            }

            return outError;
        }
    }
}
