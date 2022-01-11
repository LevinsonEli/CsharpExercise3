using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        public float MinValue { get; private set; }

        public float MaxValue { get; private set; }

        public float CurrentValue { get; private set; }

        public ValueOutOfRangeException(Exception i_InnerException, float i_MinValue, float i_MaxValue, float i_CurrentValue)
            : base(
                  string.Format(
                "Value {0} is out of range. The value must be between {1} and {2}",
                i_CurrentValue,
                i_MinValue,
                i_MaxValue), 
                  i_InnerException)
        {
        }
    }
}
