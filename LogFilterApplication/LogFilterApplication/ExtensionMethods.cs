using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogFilterApplication
{
    public static class ExtensionMethods
    {
        public static string ShiftDecimalPoint(double inputNumber, int decimalToShift)
        {
            string retVal = "";

            if (decimalToShift == -1)
            {
                return inputNumber.ToString();
            }
            inputNumber /= Math.Pow(10.0, decimalToShift);
            retVal = inputNumber.ToString();

            return retVal;
        }
    }
}
