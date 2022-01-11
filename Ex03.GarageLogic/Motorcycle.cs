using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        private eLicenseType m_LicenseType;
        private int m_EngineCapacity;

        public eLicenseType LicenseType
        {
            get
            {
                return m_LicenseType;
            }

            private set
            {
                if (Enum.IsDefined(typeof(eLicenseType), value))
                {
                    m_LicenseType = value;
                }
                else
                {
                    throw new FormatException(string.Format("Incorrect value {0} of type {1}", value, typeof(eLicenseType).ToString()));
                }
            }
        }

        public int EngineCapacity
        {
            get
            {
                return m_EngineCapacity;
            }

            private set
            {
                if (value <= 0)
                {
                    throw new Exception("Engine capacity can't be a negative number or zero. ");
                }
                else
                {
                    m_EngineCapacity = value;
                }
            }
        }

        public Motorcycle(
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
                i_PercentageOfEnergyLeft)
        {
            m_LicenseType = i_LicenseType;
            m_EngineCapacity = i_EngineCapacity;
        }
    }
}
