using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ElectricCar : Car, IElectric
    {
        private float m_MaxBatteryTime;
        private float m_CurrentBatteryTime;

        public float MaxBatteryTime
        {
            get
            {
                return m_MaxBatteryTime;
            }

            private set
            {
                if (value <= 0)
                {
                    throw new Exception(string.Format("Maximum battery time can't be a negative number or zero."));
                }
                else
                {
                    m_MaxBatteryTime = value;
                }
            }
        }

        public float CurrentBatteryTime
        {
            get
            {
                return m_CurrentBatteryTime;
            }

            private set
            {
                if (value < 0)
                {
                    throw new Exception(string.Format("Current battery time can't be a negative number."));
                }
                else if (value > MaxBatteryTime)
                {
                    throw new ValueOutOfRangeException(null, 0, MaxBatteryTime - m_CurrentBatteryTime, value);
                }
                else
                {
                    m_CurrentBatteryTime = value;
                    PercentageOfEnergyLeft = CurrentBatteryTime / MaxBatteryTime * 100;
                }
            }
        }

        public ElectricCar(
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
                i_PercentageOfEnergyLeft,
                i_CarColor,
                i_NumOfDoors)
        {
            MaxBatteryTime = MaxAmountOfEnergy.ElectricCar;
            CurrentBatteryTime = MaxBatteryTime / 100 * i_PercentageOfEnergyLeft;
        }

        public void ChargeTheBattery(float i_numOfMinutesToCharge)
        {
            if ((CurrentBatteryTime + (i_numOfMinutesToCharge / 60)) > MaxBatteryTime)
            {
                throw new ValueOutOfRangeException(null, 0, (MaxBatteryTime - CurrentBatteryTime) * 60, i_numOfMinutesToCharge);
            }
            else
            {
                CurrentBatteryTime += i_numOfMinutesToCharge / 60;
            }
        }
    }
}
