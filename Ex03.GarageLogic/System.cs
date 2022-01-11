using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public static class GarageSystem
    {
        private static readonly List<Vehicle> s_Vehicles = new List<Vehicle>();

        public static void AddVehicle(
            string i_LicenseNumber,
            string i_OwnerName,
            string i_OwnerPhoneNumber,
            string i_VehicleModel,
            List<Wheel> i_Wheels,
            eEnergySourceType i_EnergySourceType,
            float i_PerecentageOfEnergyLeft,
            bool i_DoesCarryHazardousMaterials,
            float i_TrunkCapacity)
        {
            if (!IsEmpty() && IndexOf(i_LicenseNumber) != -1)
            {
                throw new ArgumentException(string.Format("Vehicle with such a license number is already in the list. "));
            }
            else
            {
                s_Vehicles.Add(new Truck(
                    i_LicenseNumber,
                    i_OwnerName,
                    i_OwnerPhoneNumber,
                    i_VehicleModel,
                    i_Wheels,
                    i_PerecentageOfEnergyLeft,
                    i_DoesCarryHazardousMaterials,
                    i_TrunkCapacity));
            }
        }

        public static void AddVehicle(
            string i_LicenseNumber,
            string i_OwnerName,
            string i_OwnerPhoneNumber,
            string i_VehicleModel,
            List<Wheel> i_Wheels,
            eEnergySourceType i_EnergySourceType,
            float i_PerecentageOfEnergyLeft,
            eCarColor i_CarColor,
            eNumOfDoors i_NumberOfDoors)
        {
            if (!IsEmpty() && IndexOf(i_LicenseNumber) != -1)
            {
                throw new ArgumentException(string.Format("Vehicle with such a license number is already in the list. "));
            }
            else
            {
                if (i_EnergySourceType == eEnergySourceType.Electric)
                {
                    s_Vehicles.Add(new ElectricCar(
                        i_LicenseNumber,
                        i_OwnerName,
                        i_OwnerPhoneNumber,
                        i_VehicleModel,
                        i_Wheels,
                        i_PerecentageOfEnergyLeft,
                        i_CarColor,
                        i_NumberOfDoors));
                }
                else if (i_EnergySourceType == eEnergySourceType.Fuel)
                {
                    s_Vehicles.Add(new FuelCar(
                        i_LicenseNumber,
                        i_OwnerName,
                        i_OwnerPhoneNumber,
                        i_VehicleModel,
                        i_Wheels,
                        i_PerecentageOfEnergyLeft,
                        i_CarColor,
                        i_NumberOfDoors));
                }
                else
                {
                    throw new FormatException(string.Format("Incorrect value {0} of type {1}", i_EnergySourceType, typeof(eEnergySourceType).ToString()));
                }
            }
        }

        public static void AddVehicle(
            string i_LicenseNumber,
            string i_OwnerName,
            string i_OwnerPhoneNumber,
            string i_VehicleModel,
            List<Wheel> i_Wheels,
            eEnergySourceType i_EnergySourceType,
            float i_PerecentageOfEnergyLeft,
            eLicenseType i_LicenseType,
            int i_EngineCapacity)
        {
            if (!IsEmpty() && IndexOf(i_LicenseNumber) != -1)
            {
                throw new ArgumentException(string.Format("Vehicle with such a license number is already in the list. "));
            }
            else
            {
                if (i_EnergySourceType == eEnergySourceType.Electric)
                {
                    s_Vehicles.Add(new ElectricMotorcycle(
                        i_LicenseNumber,
                        i_OwnerName,
                        i_OwnerPhoneNumber,
                        i_VehicleModel,
                        i_Wheels,
                        i_PerecentageOfEnergyLeft,
                        i_LicenseType,
                        i_EngineCapacity));
                }
                else if (i_EnergySourceType == eEnergySourceType.Fuel)
                {
                    s_Vehicles.Add(new FuelMotorcycle(
                        i_LicenseNumber,
                        i_OwnerName,
                        i_OwnerPhoneNumber,
                        i_VehicleModel,
                        i_Wheels,
                        i_PerecentageOfEnergyLeft,
                        i_LicenseType,
                        i_EngineCapacity));
                }
                else
                {
                    throw new FormatException(string.Format("Incorrect value {0} of type {1}", i_EnergySourceType, typeof(eEnergySourceType).ToString()));
                }
            }
        }

        public static bool IsEmpty()
        {
            return s_Vehicles.Count == 0;
        }

        public static int IndexOf(string i_LicenseNumber)
        {
            int indexOfLicenseNumber = -1;

            for (int i = 0; i < s_Vehicles.Count; i++)
            {
                if (s_Vehicles[i].LicenseNumber.CompareTo(i_LicenseNumber) == 0)
                {
                    indexOfLicenseNumber = i;
                    break;
                }
            }

            return indexOfLicenseNumber;
        }

        public static void ChangeVehicleStatus(int i_IndexOfLicenseNumber, eVehicleStatus i_NewStatus)
        {
            if (i_IndexOfLicenseNumber < 0 || i_IndexOfLicenseNumber >= s_Vehicles.Count)
            {
                throw new ValueOutOfRangeException(null, 0, s_Vehicles.Count - 1, i_IndexOfLicenseNumber);
            }
            else
            {
                s_Vehicles[i_IndexOfLicenseNumber].Status = i_NewStatus;
            }
        }

        public static List<string> GetLicenseNumberList()
        {
            List<string> licenseNumberList = new List<string>(s_Vehicles.Count);

            foreach (Vehicle vehicle in s_Vehicles)
            {
                licenseNumberList.Add(vehicle.LicenseNumber);
            }

            return licenseNumberList;
        }

        public static List<string> GetLicenseNumberList(eVehicleStatus i_FilterStatus)
        {
            List<string> licenseNumberList = new List<string>();

            if (i_FilterStatus < eVehicleStatus.InRepair || i_FilterStatus > eVehicleStatus.Paid)
            {
                throw new ValueOutOfRangeException(
                    null, 
                    (int)eVehicleStatus.InRepair, 
                    (int)eVehicleStatus.Paid, 
                    (int)i_FilterStatus);
            }
            else
            {
                foreach (Vehicle vehicle in s_Vehicles)
                {
                    if (vehicle.Status == i_FilterStatus)
                    {
                        licenseNumberList.Add(vehicle.LicenseNumber);
                    }
                }
            }

            return licenseNumberList;
        }

        public static void PumpMaxTheWheels(string i_LicenseNumber)
        {
            if (IndexOf(i_LicenseNumber) == -1)
            {
                throw new ArgumentException(string.Format("No vehicle with such a license number in the list."));
            }
            else
            {
                foreach (Vehicle vehicle in s_Vehicles)
                {
                    if (vehicle.LicenseNumber == i_LicenseNumber)
                    {
                        List<Wheel> vehicleWheels = vehicle.Wheels;

                        foreach (Wheel wheel in vehicleWheels)
                        {
                            wheel.Inflate(wheel.MaxPressure - wheel.CurrentPressure);
                        }

                        break;
                    }
                }
            }
        }

        public static bool DoesRunByFuel(int i_IndexOfLicenseNumber)
        {
            if (i_IndexOfLicenseNumber >= 0 && i_IndexOfLicenseNumber < s_Vehicles.Count)
            {
                return s_Vehicles[i_IndexOfLicenseNumber] is IFuel;
            }
            else
            {
                throw new ValueOutOfRangeException(null, 0, s_Vehicles.Count - 1, i_IndexOfLicenseNumber);
            }
        }

        public static bool DoesRunByElectricity(int i_IndexOfLicenseNumber)
        {
            if (i_IndexOfLicenseNumber >= 0 && i_IndexOfLicenseNumber < s_Vehicles.Count)
            {
                return s_Vehicles[i_IndexOfLicenseNumber] is IElectric;
            }
            else
            {
                throw new ValueOutOfRangeException(null, 0, s_Vehicles.Count - 1, i_IndexOfLicenseNumber);
            }
        }
        
        public static eVehicleType GetVehicleType(int i_IndexOfLicenseNumber)
        {
            if (i_IndexOfLicenseNumber >= 0 && i_IndexOfLicenseNumber < s_Vehicles.Count)
            {
                if (s_Vehicles[i_IndexOfLicenseNumber] is Motorcycle)
                {
                    return eVehicleType.Motorcycle;
                }
                else if (s_Vehicles[i_IndexOfLicenseNumber] is Car)
                {
                    return eVehicleType.Car;
                }
                else
                {
                    return eVehicleType.Truck;
                }
            }
            else
            {
                throw new ValueOutOfRangeException(null, 0, s_Vehicles.Count - 1, i_IndexOfLicenseNumber);
            }
        }

        public static eFuelType GetFuelType(int i_IndexOfLicenseNumber)
        {
            if (i_IndexOfLicenseNumber >= 0 && i_IndexOfLicenseNumber < s_Vehicles.Count)
            {
                if (s_Vehicles[i_IndexOfLicenseNumber] is IFuel)
                {
                    return (s_Vehicles[i_IndexOfLicenseNumber] as IFuel).FuelType;
                }
                else
                {
                    throw new ArgumentException(
                        "Vehicle with license number {0} doesn't run by fuel. ", 
                        s_Vehicles[i_IndexOfLicenseNumber].LicenseNumber);
                }
            }
            else
            {
                throw new ValueOutOfRangeException(null, 0, s_Vehicles.Count - 1, i_IndexOfLicenseNumber);
            }
        }

        public static void RefuelTheFuelVehicle(int i_IndexOfLicenseNumber, eFuelType i_TypeOfFuel, float i_Amount)
        {
            if (i_IndexOfLicenseNumber >= 0 && i_IndexOfLicenseNumber < s_Vehicles.Count)
            {
                if (s_Vehicles[i_IndexOfLicenseNumber] is IFuel)
                {
                    float maxAmountOfFuelThatCanBeFueled = (s_Vehicles[i_IndexOfLicenseNumber] as IFuel).MaxAmountOfFuel -
                        (s_Vehicles[i_IndexOfLicenseNumber] as IFuel).CurrentAmountOfFuel;

                    if (i_Amount <= maxAmountOfFuelThatCanBeFueled)
                    {
                        (s_Vehicles[i_IndexOfLicenseNumber] as IFuel).FuelTheTank(i_TypeOfFuel, i_Amount);
                    }
                    else
                    {
                        throw new ValueOutOfRangeException(null, 0, maxAmountOfFuelThatCanBeFueled, i_Amount);
                    }
                }
                else
                {
                    throw new ArgumentException(string.Format(
                        "Vehicle with license number {0} is not fuel.", 
                        s_Vehicles[i_IndexOfLicenseNumber].LicenseNumber));
                }
            }
            else
            {
                throw new ValueOutOfRangeException(null, 0, s_Vehicles.Count - 1, i_IndexOfLicenseNumber);
            }
        }

        public static void ChargeTheElectricVehicle(int i_IndexOfLicenseNumber, float i_AmountOfMinutes)
        {
            if (i_IndexOfLicenseNumber >= 0 && i_IndexOfLicenseNumber < s_Vehicles.Count)
            {
                if (s_Vehicles[i_IndexOfLicenseNumber] is IElectric)
                {
                    float maxMinutesThatCanBeCharged = ((s_Vehicles[i_IndexOfLicenseNumber] as IElectric).MaxBatteryTime -
                        (s_Vehicles[i_IndexOfLicenseNumber] as IElectric).CurrentBatteryTime) * 60;

                    if (i_AmountOfMinutes <= maxMinutesThatCanBeCharged)
                    {
                        (s_Vehicles[i_IndexOfLicenseNumber] as IElectric).ChargeTheBattery(i_AmountOfMinutes);
                    }
                    else
                    {
                        throw new ValueOutOfRangeException(null, 0, maxMinutesThatCanBeCharged, i_AmountOfMinutes);
                    }
                }
                else
                {
                    throw new ArgumentException(string.Format(
                        "Vehicle with license number {0} is not electrical. ", 
                        s_Vehicles[i_IndexOfLicenseNumber].LicenseNumber));
                }
            }
            else
            {
                throw new ValueOutOfRangeException(null, 0, s_Vehicles.Count - 1, i_IndexOfLicenseNumber);
            }
        }
        
        public static Vehicle GetVehicle(int i_IndexOfLicenseNumber)
        {
            if (i_IndexOfLicenseNumber >= 0 && i_IndexOfLicenseNumber < s_Vehicles.Count)
            {
                return s_Vehicles[i_IndexOfLicenseNumber];
            }
            else
            {
                throw new ValueOutOfRangeException(null, 0, s_Vehicles.Count - 1, i_IndexOfLicenseNumber);
            }
        }
    }
}
