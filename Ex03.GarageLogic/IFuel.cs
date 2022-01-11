using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public interface IFuel
    {
        float MaxAmountOfFuel { get; }

        eFuelType FuelType { get; }

        float CurrentAmountOfFuel { get; }

        void FuelTheTank(eFuelType i_FuelType, float i_AmoutOfFuelToLoad);
    }
}
