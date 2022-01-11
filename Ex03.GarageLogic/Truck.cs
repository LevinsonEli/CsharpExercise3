using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle, IFuel
    {
        private bool m_DoesCarryHazardousMaterials;
        private float m_TrunkCapacity;
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
        
        public bool DoesCarryHazardousMaterials
        {
            get
            {
                return m_DoesCarryHazardousMaterials;
            }

            private set
            {
                m_DoesCarryHazardousMaterials = value;
            }
        }

        public float TrunkCapacity
        {
            get
            {
                return m_TrunkCapacity;
            }

            set
            {
                if (value < 0)
                {
                    throw new Exception(string.Format("Truck's trunk capacity can't be a negative number. "));
                }
                else
                {
                    m_TrunkCapacity = value;
                }
            }
        }

        public Truck(
            string i_LicenseNumber,
            string i_OwnerName,
            string i_OwnerPhoneNumber,
            string i_VehicleModel,
            List<Wheel> i_Wheels,
            float i_PercentageOfEnergyLeft,
            bool i_DoesCarryHazardousMaterials,
            float i_TrunkCapacity)
            : base(
                i_LicenseNumber,
                i_OwnerName,
                i_OwnerPhoneNumber,
                i_VehicleModel,
                i_Wheels,
                i_PercentageOfEnergyLeft)
        {
            DoesCarryHazardousMaterials = i_DoesCarryHazardousMaterials;
            TrunkCapacity = i_TrunkCapacity;

            MaxAmountOfFuel = MaxAmountOfEnergy.Truck;
            CurrentAmountOfFuel = i_PercentageOfEnergyLeft / 100 * MaxAmountOfFuel;
            FuelType = eFuelType.Soler;
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
