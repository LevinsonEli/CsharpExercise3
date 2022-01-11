using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private readonly string r_MancufacturerName;
        private readonly float r_MaxPressure;
        private float m_CurrentPressure;

        public string ManufacturerName
        {
            get
            {
                return r_MancufacturerName;
            }
        }

        public float MaxPressure
        {
            get
            {
                return r_MaxPressure;
            }
        }

        public float CurrentPressure
        {
            get
            {
                return m_CurrentPressure;
            }

            private set
            {
                if (value < 0 || value > r_MaxPressure)
                {
                    throw new ValueOutOfRangeException(null, 0, r_MaxPressure, value);
                }
                else
                {
                    m_CurrentPressure = value;
                }
            }
        }

        public Wheel(string i_ManufacturerName, float i_MaxPressure, float i_CurrentPressure)
        {
            if (i_CurrentPressure > i_MaxPressure)
            {
                throw new ValueOutOfRangeException(null, 0, i_MaxPressure, i_CurrentPressure);
            }
            else
            {
                r_MancufacturerName = i_ManufacturerName;
                r_MaxPressure = i_MaxPressure;
                m_CurrentPressure = i_CurrentPressure;
            }
        }

        public void Inflate(float i_Amount)
        {
            if ((i_Amount + CurrentPressure) > MaxPressure)
            {
                throw new ValueOutOfRangeException(null, 0, MaxPressure - CurrentPressure, i_Amount);
            }
            else
            {
                CurrentPressure += i_Amount;
            }
        }

        public void InflateToMax()
        {
           CurrentPressure = r_MaxPressure;
        }
    }
}
