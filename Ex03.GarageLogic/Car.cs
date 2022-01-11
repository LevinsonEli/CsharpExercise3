using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private eNumOfDoors m_NumOfDoors;
        private eCarColor m_Color;

        public eCarColor Color
        {
            get
            {
                return m_Color;
            }

            private set
            {
                if (Enum.IsDefined(typeof(eCarColor), value))
                {
                    m_Color = value;
                }
                else
                {
                    throw new FormatException(string.Format("Incorrect value {0} of type {1}", value, typeof(eCarColor).ToString()));
                }
            }
        }

        public eNumOfDoors NumOfDoors
        {
            get
            {
                return m_NumOfDoors;
            }

            private set
            {
                if (Enum.IsDefined(typeof(eNumOfDoors), value))
                {
                    m_NumOfDoors = value;
                }
                else
                {
                    throw new FormatException(string.Format("Incorrect value {0} of type {1}", value, typeof(eNumOfDoors).ToString()));
                }
            }
        }

        public Car(
            string i_LicenseNumber,
            string i_OwnerName,
            string i_OwnerPhoneNumber,
            string i_VehicleModel,
            List<Wheel> i_Wheels,
            float i_PercentageOfEnergyLeft,
            eCarColor i_CarColor,
            eNumOfDoors i_NumOfDoors)
            : base(
                i_LicenseNumber,
                i_OwnerName,
                i_OwnerPhoneNumber,
                i_VehicleModel,
                i_Wheels,
                i_PercentageOfEnergyLeft)
        {
            Color = i_CarColor;
            m_NumOfDoors = i_NumOfDoors;
        }
    }
}
