using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Vehicle
    {
        private string m_LicenseNumber;
        private string m_ModelName;
        private string m_OwnerName;
        private string m_OwnerPhoneNumber;
        private List<Wheel> m_Wheels;
        private eVehicleStatus m_Status;
        private float m_PercentageOfEnergyLeft;

        public float PercentageOfEnergyLeft
        {
            get
            {
                return m_PercentageOfEnergyLeft;
            }

            protected set
            {
                if (value < 0 || value > 100)
                {
                    throw new ValueOutOfRangeException(null, 0, 100, value);
                }
                else
                {
                    m_PercentageOfEnergyLeft = value;
                }
            }
        }
        
        public string LicenseNumber
        {
            get
            {
                return m_LicenseNumber;
            }

            private set
            {
                m_LicenseNumber = value;
            }
        }

        public string OwnerName
        {
            get
            {
                return m_OwnerName;
            }

            private set
            {
                m_OwnerName = value;
            }
        }

        public string OwnerPhoneNumber
        {
            get
            {
                return m_OwnerPhoneNumber;
            }

            private set
            {
                string phoneNumberErrorInput = Validate.PhoneNumber(value);

                if (phoneNumberErrorInput != null)
                {
                    throw new FormatException(phoneNumberErrorInput);
                }
                else
                {
                    m_OwnerPhoneNumber = value;
                }
            }
        }

        public string ModelName
        {
            get
            {
                return m_ModelName;
            }

            private set
            {
                m_ModelName = value;
            }
        }

        public List<Wheel> Wheels
        {
            get
            {
                return m_Wheels;
            }

            private set
            {
                if (value == null)
                {
                    throw new Exception(string.Format("Can't create Vehicle with an empty wheel list. "));
                }
                else
                {
                    m_Wheels = new List<Wheel>();
                    foreach (Wheel wheel in value)
                    {
                        m_Wheels.Add(new Wheel(wheel.ManufacturerName, wheel.MaxPressure, wheel.CurrentPressure));
                    }
                }
            }
        }
        
        public eVehicleStatus Status
        {
            get
            {
                return m_Status;
            }

            set
            {
                m_Status = value;
            }
        }

        public Vehicle(
            string i_LicenseNumber,
            string i_OwnerName,
            string i_OwnerPhoneNumber,
            string i_VehicleModel,
            List<Wheel> i_Wheels,
            float i_PercentageOfEnergyLeft)
        {
            LicenseNumber = i_LicenseNumber;
            OwnerName = i_OwnerName;
            OwnerPhoneNumber = i_OwnerPhoneNumber;
            ModelName = i_VehicleModel;
            Wheels = i_Wheels;
            PercentageOfEnergyLeft = i_PercentageOfEnergyLeft;
            Status = eVehicleStatus.InRepair;
        }
    }
}
