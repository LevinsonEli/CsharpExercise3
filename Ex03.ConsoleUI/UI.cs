using System;
using System.Collections.Generic;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class UI
    {
        private enum eMenu
        {
            AddNewVehicle = 1,
            DisplayListOfLicenseNumbers,
            ChangeVehicleStatus,
            PumpTheWheels,
            Refuel,
            Charge,
            DisplayFullInfo,
            Exit = 'Q'
        }

        public static void StartUI()
        {
            try
            {
                List<string> menuList = new List<string>();

                setMenu(menuList);
                handleMenu(menuList);
            }
            catch (FormatException formatEx)
            {
                Console.WriteLine(string.Format("Format exception was occured: {0}", formatEx.Message));
            }
            catch (ArgumentException argumentEx)
            {
                Console.WriteLine(string.Format("Argument exception was occured: {0}", argumentEx.Message));
            }
            catch (Ex03.GarageLogic.ValueOutOfRangeException outOfRangeEx)
            {
                Console.WriteLine(string.Format("ValueOutOfRange exception was occured: {0}", outOfRangeEx.Message));
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Exception was occured: {0}", ex.Message));
            }
        }

        private static void setMenu(List<string> i_MenuList)
        {
            i_MenuList.Add(string.Format("Add new vehicle. "));
            i_MenuList.Add(string.Format("Display list of license numbers. "));
            i_MenuList.Add(string.Format("Change vehicle status. "));
            i_MenuList.Add(string.Format("Pump the wheels in a vehicle. "));
            i_MenuList.Add(string.Format("Refuel a vehicle. "));
            i_MenuList.Add(string.Format("Chagre a vehicle. "));
            i_MenuList.Add(string.Format("Display full info about a vehicle. "));
            i_MenuList.Add(string.Format("Exit. "));
        }

        private static void handleMenu(List<string> i_MenuList)
        {
            eMenu menuChoice;
            Array menuValues = Enum.GetValues(typeof(eMenu));
            int i;

            do
            {
                for (i = 0; i < i_MenuList.Count - 1; i++)
                {
                    Console.WriteLine("{0}. {1}", (int)menuValues.GetValue(i), i_MenuList[i]);
                }

                Console.WriteLine("{0}. {1}", (char)(int)menuValues.GetValue(i), i_MenuList[i]);
                string inputStr = Console.ReadLine();
                int menuChoiceInt;

                if (inputStr.CompareTo("q") == 0 || inputStr.CompareTo("Q") == 0)
                {
                    menuChoice = eMenu.Exit;
                }
                else
                {
                    int.TryParse(inputStr, out menuChoiceInt);
                    menuChoice = (eMenu)menuChoiceInt;
                }

                switch (menuChoice)
                {
                    case eMenu.AddNewVehicle:
                        getVehicleFromConsoleAndAddVehicleToGarage();
                        break;
                    case eMenu.DisplayListOfLicenseNumbers:
                        displayLicenseNumberList();
                        break;
                    case eMenu.ChangeVehicleStatus:
                        changeVehicleStatus();
                        break;
                    case eMenu.PumpTheWheels:
                        pumpMaxTheWheels();
                        break;
                    case eMenu.Refuel:
                        refuelTheFuelVehicle();
                        break;
                    case eMenu.Charge:
                        chargeTheElectricVehicle();
                        break;
                    case eMenu.DisplayFullInfo:
                        displayFullVehicleInfo();
                        break;
                    case eMenu.Exit:
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }
            }
            while (menuChoice != eMenu.Exit);
        }

        private static void getVehicleFromConsoleAndAddVehicleToGarage()
        {
            string licenseNumber = getConsoleString(string.Format("Enter the license number: "));

            if (Ex03.GarageLogic.GarageSystem.IndexOf(licenseNumber) != -1)
            {
                Console.WriteLine(string.Format("Vehicle with such a license number is already in garage. "));
            }
            else
            {
                string ownerName, ownerPhoneNumber, vehicleModel;
                eEnergySourceType energySourceType;
                eVehicleType vehicleType;
                float perecentageOfEnergyLeft;

                getConsoleOwner(out ownerName, out ownerPhoneNumber);
                vehicleType = (eVehicleType)(int)getFloatInRangeFromConsole(
                    string.Format(
                        "Enter the vehicle's type: {0}1 - motorcycle, 2 - car, 3 - truck",
                        Environment.NewLine),
                    "Vehicle Type",
                    1,
                    3);
                vehicleModel = getConsoleString(string.Format("Enter the model of the vehicle: "));

                if (vehicleType != eVehicleType.Truck)
                {
                    energySourceType = (eEnergySourceType)(int)getFloatInRangeFromConsole(
                        string.Format(
                            "If the vehicle is electric enter 1,{0}If the vehicle if fuel enter 2",
                            Environment.NewLine),
                        string.Format("Electric/Fuel"),
                        1,
                        2);
                }
                else
                {
                    energySourceType = eEnergySourceType.Fuel;
                }

                getConsoleCurrentAmountOfEnergy(vehicleType, energySourceType, out perecentageOfEnergyLeft);
                getUniqueInfoForVehicleAndAddItToGarage(
                    vehicleType,
                    licenseNumber,
                    ownerName,
                    ownerPhoneNumber,
                    vehicleModel,
                    energySourceType,
                    perecentageOfEnergyLeft);
            }
        }

        private static void getUniqueInfoForVehicleAndAddItToGarage(
            eVehicleType i_VehicleType,
            string i_LicenseNumber,
            string i_OwnerName,
            string i_OwnerPhoneNumber,
            string i_VehicleModel,
            eEnergySourceType i_EnergySourceType,
            float i_PercentageOfEnergyLeft)
        {
            switch (i_VehicleType)
            {
                case eVehicleType.Truck:
                    getUniqueInfoForTruckAndAddItToGarage(
                        i_LicenseNumber,
                        i_OwnerName,
                        i_OwnerPhoneNumber,
                        i_VehicleModel,
                        i_EnergySourceType,
                        i_PercentageOfEnergyLeft);
                    break;
                case eVehicleType.Car:
                    getUniqueInfoForCarAndAddItToGarage(
                        i_LicenseNumber,
                        i_OwnerName,
                        i_OwnerPhoneNumber,
                        i_VehicleModel,
                        i_EnergySourceType,
                        i_PercentageOfEnergyLeft);
                    break;
                case eVehicleType.Motorcycle:
                    getUniqueInfoForMotorcycleAndAddItToGarage(
                        i_LicenseNumber,
                        i_OwnerName,
                        i_OwnerPhoneNumber,
                        i_VehicleModel,
                        i_EnergySourceType,
                        i_PercentageOfEnergyLeft);
                    break;
                default:
                    throw new ValueOutOfRangeException(null, (int)eVehicleType.Motorcycle, (int)eVehicleType.Truck, (int)i_VehicleType);
            }
        }

        private static List<Wheel> getWheelsList(int i_NumOfWheels, float i_MaxWheelPressure)
        {
            List<Wheel> wheelsList = new List<Wheel>();

            for (int i = 0; i < i_NumOfWheels; i++)
            {
                float currentWheelPressure = getFloatInRangeFromConsole(
                    string.Format("Enter current {0}'s wheel pressure: ", i + 1),
                    string.Format("Current wheel pressure "),
                    0,
                    i_MaxWheelPressure);
                string wheelManufacturer = getConsoleString(string.Format("Enter the manufacturer of the {0}'s wheel: ", i + 1));
                wheelsList.Add(new Wheel(wheelManufacturer, i_MaxWheelPressure, currentWheelPressure));
            }

            return wheelsList;
        }

        private static void getUniqueInfoForTruckAndAddItToGarage(
                        string i_LicenseNumber,
                        string i_OwnerName,
                        string i_OwnerPhoneNumber,
                        string i_VehicleModel,
                        eEnergySourceType i_EnergySourceType,
                        float i_PercentageOfEnergyLeft)
        {
            int hazardousInputInt;

            int.TryParse(
                getConsoleString(string.Format(
                    "Does the vehicle carry hazardous materials?{0}If yes - enter 1",
                    Environment.NewLine)),
                    out hazardousInputInt);

            bool doesCarryHazardousMaterials = hazardousInputInt == 1;
            float trunkCapacity = getPositiveFloatFromConsole(
                string.Format("Enter the capacity of trunk: "),
                string.Format("Trunk capacity "));
            List<Wheel> wheelsList = getWheelsList((int)eNumOfWheels.Truck, (float)eMaxWheelPressure.Truck);

            Ex03.GarageLogic.GarageSystem.AddVehicle(
                i_LicenseNumber,
                i_OwnerName,
                i_OwnerPhoneNumber,
                i_VehicleModel,
                wheelsList,
                i_EnergySourceType,
                i_PercentageOfEnergyLeft,
                doesCarryHazardousMaterials,
                trunkCapacity);
        }

        private static void getUniqueInfoForCarAndAddItToGarage(
                        string i_LicenseNumber,
                        string i_OwnerName,
                        string i_OwnerPhoneNumber,
                        string i_VehicleModel,
                        eEnergySourceType i_EnergySourceType,
                        float i_PercentageOfEnergyLeft)
        {
            eCarColor carColor = (eCarColor)(int)getFloatInRangeFromConsole(
                        string.Format("Enter the color of the car: {0}1 - red, 2 - white, 3 - black, 4 - silver", Environment.NewLine),
                        string.Format("Car color "),
                        1,
                        4);
            eNumOfDoors numberOfDoors = (eNumOfDoors)(int)getFloatInRangeFromConsole(
                string.Format("Enter number of doors (2, 3, 4, 5): "),
                string.Format("Number of doors "),
                2,
                5);
            List<Wheel> wheelsList = getWheelsList((int)eNumOfWheels.Car, (float)eMaxWheelPressure.Car);
            Ex03.GarageLogic.GarageSystem.AddVehicle(
                i_LicenseNumber,
                i_OwnerName,
                i_OwnerPhoneNumber,
                i_VehicleModel,
                wheelsList,
                i_EnergySourceType,
                i_PercentageOfEnergyLeft,
                carColor,
                numberOfDoors);
        }

        private static void getUniqueInfoForMotorcycleAndAddItToGarage(
                        string i_LicenseNumber,
                        string i_OwnerName,
                        string i_OwnerPhoneNumber,
                        string i_VehicleModel,
                        eEnergySourceType i_EnergySourceType,
                        float i_PercentageOfEnergyLeft)
        {
            eLicenseType licenseType = (eLicenseType)(int)getFloatInRangeFromConsole(
                        string.Format(
                            "Enter the license type: {0}1 - A, 2 - A1, 3 - AA, 4 - B",
                            Environment.NewLine),
                        string.Format("License type "),
                        1,
                        4);
            int engineCapacity = (int)getPositiveFloatFromConsole(
                string.Format("Enter the engine capacity: "),
                string.Format("Engine capacity"));
            List<Wheel> wheelsList = getWheelsList((int)eNumOfWheels.Motorcycle, (float)eMaxWheelPressure.Motorcycle);

            Ex03.GarageLogic.GarageSystem.AddVehicle(
                i_LicenseNumber,
                i_OwnerName,
                i_OwnerPhoneNumber,
                i_VehicleModel,
                wheelsList,
                i_EnergySourceType,
                i_PercentageOfEnergyLeft,
                licenseType,
                engineCapacity);
        }

        private static string getConsoleString(string i_Msg)
        {
            string strInput;

            Console.WriteLine(i_Msg);
            strInput = Console.ReadLine();

            return strInput;
        }

        private static float getFloatInRangeFromConsole(
            string i_InputMsg,
            string i_TypeName,
            float i_MinValue,
            float i_MaxValue)
        {
            string numOutOfRangeStr = null;
            float numInputed;

            do
            {
                if (!float.TryParse(getConsoleString(string.Format(i_InputMsg, Environment.NewLine)), out numInputed))
                {
                    numOutOfRangeStr = string.Format("Can't parse succsesfully. ");
                }
                else
                {
                    numOutOfRangeStr = Ex03.GarageLogic.Validate.OutOfRange(numInputed, i_MinValue, i_MaxValue);

                    if (numOutOfRangeStr != null)
                    {
                        Console.WriteLine(string.Format("{0}: {1}", i_TypeName, numOutOfRangeStr));
                    }
                }
            }
            while (numOutOfRangeStr != null);

            return numInputed;
        }

        private static float getPositiveFloatFromConsole(string i_InputMsg, string i_TypeName)
        {
            string errorMsg = string.Format("{0} must be positive. ", i_TypeName);
            float numInputed;
            bool succeedToParse = false;

            do
            {
                if (!float.TryParse(
                    getConsoleString(string.Format(
                        i_InputMsg,
                        Environment.NewLine)),
                    out numInputed))
                {
                    Console.WriteLine(string.Format("Enter only numbers. "));
                    succeedToParse = false;
                }
                else
                {
                    succeedToParse = true;
                    if (numInputed < 0)
                    {
                        Console.WriteLine(errorMsg);
                    }
                }
            }
            while (numInputed < 0 || !succeedToParse);

            return numInputed;
        }

        private static void getConsoleOwner(out string o_OwnerName, out string o_OwnerPhoneNumber)
        {
            string errorInput = null;

            o_OwnerName = getConsoleString(string.Format("Enter the owner's name: "));
            do
            {
                o_OwnerPhoneNumber = getConsoleString(string.Format("Enter the owner's phone number: "));
                errorInput = Ex03.GarageLogic.Validate.PhoneNumber(o_OwnerPhoneNumber);
                Console.Write(errorInput == null ? string.Empty : string.Format("{0}{1}", errorInput, Environment.NewLine));
            }
            while (errorInput != null);
        }

        private static void getConsoleWheel(
            eVehicleType i_VehicleType,
            out string o_WheelManufacturer,
            out float o_CurrentWheelPressure)
        {
            int maxWheelPressue;

            o_WheelManufacturer = getConsoleString(string.Format("Enter the manufacturer of wheels: "));
            if (i_VehicleType == eVehicleType.Motorcycle)
            {
                maxWheelPressue = (int)eMaxWheelPressure.Motorcycle;
            }
            else if (i_VehicleType == eVehicleType.Car)
            {
                maxWheelPressue = (int)eMaxWheelPressure.Car;
            }
            else if (i_VehicleType == eVehicleType.Truck)
            {
                maxWheelPressue = (int)eMaxWheelPressure.Truck;
            }
            else
            {
                throw new ValueOutOfRangeException(null, (int)eVehicleType.Motorcycle, (int)eVehicleType.Truck, (int)i_VehicleType);
            }

            o_CurrentWheelPressure = getFloatInRangeFromConsole(
                    string.Format("Enter the current wheel pressure: "),
                    "Wheel pressure ",
                    0,
                    maxWheelPressue);
        }

        private static void getConsoleCurrentAmountOfEnergy(
            eVehicleType i_VehicleType,
            eEnergySourceType i_EnergySourceType,
            out float o_PercentageOfEnergyLeft)
        {
            if (i_VehicleType == eVehicleType.Truck && i_EnergySourceType == eEnergySourceType.Electric)
            {
                throw new ArgumentException(string.Format("Truck can not be electrical in this garage. "));
            }
            else
            {
                float maxAmountOfEnergy, currentAmountOfEnergy;
                string inputMsg, valueTypeMsg;

                if (i_EnergySourceType == eEnergySourceType.Electric)
                {
                    inputMsg = string.Format("Enter the current battery time (in hours): ");
                    valueTypeMsg = string.Format("Battery time ");

                    if (i_VehicleType == eVehicleType.Motorcycle)
                    {
                        maxAmountOfEnergy = MaxAmountOfEnergy.ElectricMotorcycle;
                    }
                    else if (i_VehicleType == eVehicleType.Car)
                    {
                        maxAmountOfEnergy = MaxAmountOfEnergy.ElectricCar;
                    }
                    else
                    {
                        throw new ValueOutOfRangeException(null, (int)eVehicleType.Motorcycle, (int)eVehicleType.Truck, (int)i_VehicleType);
                    }
                }
                else if (i_EnergySourceType == eEnergySourceType.Fuel)
                {
                    inputMsg = string.Format("Enter the current amount of fuel: ");
                    valueTypeMsg = string.Format("Amount of fuel ");

                    if (i_VehicleType == eVehicleType.Motorcycle)
                    {
                        maxAmountOfEnergy = MaxAmountOfEnergy.FuelMotorcycle;
                    }
                    else if (i_VehicleType == eVehicleType.Car)
                    {
                        maxAmountOfEnergy = MaxAmountOfEnergy.FuelCar;
                    }
                    else if (i_VehicleType == eVehicleType.Truck)
                    {
                        maxAmountOfEnergy = MaxAmountOfEnergy.Truck;
                    }
                    else
                    {
                        throw new ValueOutOfRangeException(null, (int)eVehicleType.Motorcycle, (int)eVehicleType.Truck, (int)i_VehicleType);
                    }
                }
                else
                {
                    throw new ValueOutOfRangeException(null, (int)eEnergySourceType.Electric, (int)eEnergySourceType.Fuel, (int)i_EnergySourceType);
                }

                currentAmountOfEnergy = getFloatInRangeFromConsole(
                    inputMsg,
                    valueTypeMsg,
                    0,
                    maxAmountOfEnergy);

                // Calculating currentAmountOfEnergy as precentage of maxAmountOfEnergy.
                o_PercentageOfEnergyLeft = currentAmountOfEnergy * 100 / maxAmountOfEnergy;
            }
        }

        private static void changeVehicleStatus()
        {
            string licenseNumber = getConsoleString(string.Format("Enter the license number: "));
            int indexOfLicenseNumber = Ex03.GarageLogic.GarageSystem.IndexOf(licenseNumber);

            if (indexOfLicenseNumber != -1)
            {
                eVehicleStatus newStatus = (eVehicleStatus)(int)getFloatInRangeFromConsole(
                    string.Format("Choose the new status: {0}1 - In repair, 2 - Repaired, 3 - Paid", Environment.NewLine),
                    string.Format("Vehicle status "),
                    1,
                    3);

                Ex03.GarageLogic.GarageSystem.ChangeVehicleStatus(indexOfLicenseNumber, newStatus);
            }
            else
            {
                Console.WriteLine(string.Format("No such a license number. "));
            }
        }

        private static void displayLicenseNumberList()
        {
            List<string> licenseNumberList = Ex03.GarageLogic.GarageSystem.GetLicenseNumberList();

            Console.WriteLine(string.Format("The list of license numbers: "));
            foreach (string licenseNumber in licenseNumberList)
            {
                Console.WriteLine(licenseNumber);
            }

            eVehicleStatus filter = (eVehicleStatus)(int)getFloatInRangeFromConsole(
                string.Format(
                    "To filter by status of the vehicle enter: {0}1 - In repair, 2 - Repaired, 3 - Paid{0}Enter 0 - to return to the main menu",
                    Environment.NewLine),
                string.Format("Filter "),
                0,
                3);
            if (filter != 0)
            {
                licenseNumberList = Ex03.GarageLogic.GarageSystem.GetLicenseNumberList(filter);
                Console.WriteLine(string.Format("The list of license numbers filtered by {0}: ", filter.ToString()));
                foreach (string licenseNumber in licenseNumberList)
                {
                    Console.WriteLine(licenseNumber);
                }
            }
        }

        private static void pumpMaxTheWheels()
        {
            string licenseNumber = getConsoleString(string.Format("Enter the license number: "));

            if (Ex03.GarageLogic.GarageSystem.IndexOf(licenseNumber) == -1)
            {
                Console.WriteLine(string.Format("No such a license number in garage. "));
            }
            else
            {
                Ex03.GarageLogic.GarageSystem.PumpMaxTheWheels(licenseNumber);
            }
        }

        private static void refuelTheFuelVehicle()
        {
            string licenseNumber = getConsoleString(string.Format("Enter the license number: "));
            int indexOfLicenseNumber = Ex03.GarageLogic.GarageSystem.IndexOf(licenseNumber);

            if (indexOfLicenseNumber != -1)
            {
                if (!Ex03.GarageLogic.GarageSystem.DoesRunByFuel(indexOfLicenseNumber))
                {
                    Console.WriteLine(string.Format("Vehicle with license number {0} is not fuel. ", licenseNumber));
                }
                else
                {
                    eFuelType typeOfFuel = (eFuelType)(int)getFloatInRangeFromConsole(
                        string.Format("Enter the type of fuel: {0}1 - Soler, 2 - Octan95, 3 - Octan96, 4 - Octan98", Environment.NewLine),
                        string.Format("Type of fuel "),
                        1,
                        4);
                    if (typeOfFuel != Ex03.GarageLogic.GarageSystem.GetFuelType(indexOfLicenseNumber))
                    {
                        Console.WriteLine(string.Format("Incorrect fuel type. "));
                    }
                    else
                    {
                        float amount, maxAmountOfFuelThatCanBeLoaded,
                            percentageOfEnergyLeft = Ex03.GarageLogic.GarageSystem.GetVehicle(indexOfLicenseNumber).PercentageOfEnergyLeft;

                        if (Ex03.GarageLogic.GarageSystem.GetVehicleType(indexOfLicenseNumber) == eVehicleType.Motorcycle)
                        {
                            maxAmountOfFuelThatCanBeLoaded = MaxAmountOfEnergy.FuelMotorcycle * (1 - (percentageOfEnergyLeft / 100));
                        }
                        else if (Ex03.GarageLogic.GarageSystem.GetVehicleType(indexOfLicenseNumber) == eVehicleType.Car)
                        {
                            maxAmountOfFuelThatCanBeLoaded = MaxAmountOfEnergy.FuelCar * (1 - (percentageOfEnergyLeft / 100));
                        }
                        else
                        {
                            maxAmountOfFuelThatCanBeLoaded = MaxAmountOfEnergy.Truck * (1 - (percentageOfEnergyLeft / 100));
                        }

                        amount = getFloatInRangeFromConsole(
                            string.Format("Enter the amount to refuel: "),
                            string.Format("Amount of fuel "),
                            0,
                            maxAmountOfFuelThatCanBeLoaded);
                        Ex03.GarageLogic.GarageSystem.RefuelTheFuelVehicle(indexOfLicenseNumber, typeOfFuel, amount);
                    }
                }
            }
            else
            {
                Console.WriteLine(string.Format("No vehicle with such a license number in garage. "));
            }
        }

        private static void chargeTheElectricVehicle()
        {
            string licenseNumber = getConsoleString(string.Format("Enter the license number: "));
            int indexOfLicenseNumber = Ex03.GarageLogic.GarageSystem.IndexOf(licenseNumber);

            if (indexOfLicenseNumber != -1)
            {
                if (!Ex03.GarageLogic.GarageSystem.DoesRunByElectricity(indexOfLicenseNumber))
                {
                    Console.WriteLine(string.Format("Vehicle with license number {0} is not electrical. ", licenseNumber));
                }
                else
                {
                    float numOfMinutes, maxNumOfMinutesToCharge,
                        percentageOfEnergyLeft = Ex03.GarageLogic.GarageSystem.GetVehicle(indexOfLicenseNumber).PercentageOfEnergyLeft;

                    if (Ex03.GarageLogic.GarageSystem.GetVehicleType(indexOfLicenseNumber) == eVehicleType.Motorcycle)
                    {
                        maxNumOfMinutesToCharge = MaxAmountOfEnergy.ElectricMotorcycle * (1 - (percentageOfEnergyLeft / 100)) * 60;
                    }
                    else
                    {
                        maxNumOfMinutesToCharge = MaxAmountOfEnergy.ElectricCar * (1 - (percentageOfEnergyLeft / 100)) * 60;
                    }

                    numOfMinutes = getFloatInRangeFromConsole(
                        string.Format("Enter number of minutes to charge: "),
                        string.Format("Num of minutes "),
                        0,
                        maxNumOfMinutesToCharge);
                    Ex03.GarageLogic.GarageSystem.ChargeTheElectricVehicle(indexOfLicenseNumber, numOfMinutes);
                }
            }
            else
            {
                Console.WriteLine(string.Format("No vehicle with such a license number in garage. "));
            }
        }

        private static void printVehicleCommonInfo(Vehicle i_Vehicle)
        {
            Console.WriteLine(string.Format("License number: {0}", i_Vehicle.LicenseNumber));
            Console.WriteLine(string.Format("Status: {0}", i_Vehicle.Status));
            Console.WriteLine(string.Format("Owner name: {0}", i_Vehicle.OwnerName));
            Console.WriteLine(string.Format("Owner's phone number: {0}", i_Vehicle.OwnerPhoneNumber));
            Console.WriteLine(string.Format("Model: {0}", i_Vehicle.ModelName));

            List<Wheel> wheelsList = i_Vehicle.Wheels;

            for (int i = 0; i < wheelsList.Count; i++)
            {
                Console.WriteLine(string.Format(
                    "Wheel number {0}: Manufacturer - {1}, Current pressure - {2}",
                    i + 1,
                    wheelsList[i].ManufacturerName,
                    wheelsList[i].CurrentPressure));
            }

            if (i_Vehicle is IElectric)
            {
                Console.WriteLine(string.Format("The vehicle has {0: 0.00} % battery", i_Vehicle.PercentageOfEnergyLeft));
            }
            else
            {
                Console.WriteLine(string.Format("The vehicle has {0: 0.00} % fuel tank", i_Vehicle.PercentageOfEnergyLeft));
                Console.WriteLine(string.Format("The vehicle runs by {0} fuel type", (i_Vehicle as IFuel).FuelType.ToString()));
            }
        }

        private static void printVehicleUniqueInfo(Vehicle i_Vehicle)
        {
            if (i_Vehicle is Truck)
            {
                Console.WriteLine(
                    string.Format(
                        "Does carry hazardous materials: {0}",
                        (i_Vehicle as Truck).DoesCarryHazardousMaterials.ToString()));
                Console.WriteLine(string.Format("Trunk capactity: {0}", (i_Vehicle as Truck).TrunkCapacity));
            }
            else if (i_Vehicle is Car)
            {
                Console.WriteLine(string.Format("Color: {0}", (i_Vehicle as Car).Color.ToString()));
                Console.WriteLine(string.Format("Number of doors: {0}", (int)(i_Vehicle as Car).NumOfDoors));
            }
            else if (i_Vehicle is Motorcycle)
            {
                Console.WriteLine(string.Format("License type: {0}", (i_Vehicle as Motorcycle).LicenseType.ToString()));
                Console.WriteLine(string.Format("Engine capacity: {0}", (i_Vehicle as Motorcycle).EngineCapacity));
            }
            else
            {
                throw new ArgumentException("Unknown vehicle type. ");
            }
        }

        private static void displayFullVehicleInfo()
        {
            string licenseNumber = getConsoleString(string.Format("Enter the license number: "));
            int indexOfLicenseNumber = Ex03.GarageLogic.GarageSystem.IndexOf(licenseNumber);

            if (indexOfLicenseNumber != -1)
            {
                Vehicle vehicleToDisplay = Ex03.GarageLogic.GarageSystem.GetVehicle(indexOfLicenseNumber);

                printVehicleCommonInfo(vehicleToDisplay);
                printVehicleUniqueInfo(vehicleToDisplay);
            }
            else
            {
                Console.WriteLine(string.Format("No vehicle with such a license number in garage. "));
            }
        }
    }
}
