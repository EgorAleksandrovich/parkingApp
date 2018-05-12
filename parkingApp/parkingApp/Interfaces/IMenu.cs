﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parkingApp
{
    public interface IMenu
    {
        void StartMenu();
        void ParkingInfoMenu();
        void ParkingPickUpTheCarMenu();
        void PickUpTheCar();
        void ReplanishBalance();
        void FindCar();
        void DysplayTransactionInLastMinute();
        void DysplayParkingSpace();
        void DysplayParkingBalance();
        void DysplayParkingBalanceInTheLastMinute();
        void Park();
    }
}
