using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public interface IElectric
    {
        float MaxBatteryTime { get; }

        float CurrentBatteryTime { get; }

        void ChargeTheBattery(float i_numOfMinutesToCharge);
    }
}
