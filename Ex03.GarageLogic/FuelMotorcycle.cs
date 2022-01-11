using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class FuelMotorcycle : Motorcycle, IFuel
    {
        private float m_MaxAmountOfFuel;
        private eFuelType m_FuelType;
        private float m_CurrentAmountOfFuel;

        public float MaxAmountOfFuel
        {
            get
            {
                return m_MaxAmountOfFuel;
            }

            private set
            {
                if (value <= 0)
                {
                    throw new Exception(string.Format("Maximum amount of fuel in tank can't be a negative number or zero."));
                }
                else
                {
                    m_MaxAmountOfFuel = value;
                }
            }
        }

        public eFuelType FuelType
        {
            get
            {
                return m_FuelType;
            }

            private set
            {
                if (Enum.IsDefined(typeof(eFuelType), value))
                {
                    m_FuelType = value;
                }
                else
                {
                    throw new FormatException(string.Format("Incorrect value {0} of type {1}", value, typeof(eFuelType).ToString()));
                }
            }
        }

        public float CurrentAmountOfFuel
        {
            get
            {
                return m_CurrentAmountOfFuel;
            }

            private set
            {
                if (value < 0)
                {
                    throw new Exception(string.Format("Current amount of fuel in tank can't be a negative number."));
                }
                else if (value > MaxAmountOfFuel)
                {
                    throw new ValueOutOfRangeException(null, 0, MaxAmountOfFuel, value);
                }
                else
                {
                    m_CurrentAmountOfFuel = value;
                    PercentageOfEnergyLeft = CurrentAmountOfFuel / MaxAmountOfFuel * 100;
                }
            }
        }

        public FuelMotorcycle(
            string i_LicenseNumber,
            string i_OwnerName,
            string i_OwnerPhoneNumber,
            string i_VehicleModel,
            List<Wheel> i_Wheels,
            float i_PercentageOfEnergyLeft,
            eLicenseType i_LicenseType,
            int i_EngineCapacity)
            : base(
                i_LicenseNumber,
                i_OwnerName,
                i_OwnerPhoneNumber,
                i_VehicleModel,
                i_Wheels,
                i_PercentageOfEnergyLeft,
                i_LicenseType,
                i_EngineCapacity)
        {
            MaxAmountOfFuel = MaxAmountOfEnergy.FuelMotorcycle;
            FuelType = eFuelType.Octan95;
            CurrentAmountOfFuel = MaxAmountOfFuel / 100 * i_PercentageOfEnergyLeft;
        }

        public void FuelTheTank(eFuelType i_FuelType, float i_AmoutOfFuelToLoad)
        {
            if (Enum.IsDefined(typeof(eFuelType), i_FuelType))
            {
                if (i_AmoutOfFuelToLoad + CurrentAmountOfFuel <= MaxAmountOfFuel)
                {
                    CurrentAmountOfFuel += i_AmoutOfFuelToLoad;
                }
                else
                {
                    throw new ValueOutOfRangeException(null, 0, MaxAmountOfFuel - CurrentAmountOfFuel, i_AmoutOfFuelToLoad);
                }
            }
            else
            {
                throw new FormatException(string.Format("Incorrect value {0} of type {1}", i_FuelType, typeof(eFuelType).ToString()));
            }
        }
    }
}
