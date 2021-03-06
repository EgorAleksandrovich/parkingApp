﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace parkingApp
{
    public static class Settings
    {
        private static int _parkingSpace = 100;
        private static int _fine = 2;
        private static int _timeout = 3000;
        private static int _timeoutOneMinute = 60000;
        private static string _logFilePath = Path.Combine(Environment.CurrentDirectory, @"Transaction.log");
        private static Dictionary<CarType, int> _parkingPrice = new Dictionary<CarType, int>
        {
            {CarType.Truck, 5},
            {CarType.Passenger, 3},
            {CarType.Bus, 2},
            {CarType.Motorcycle, 1}
        };

        public static Dictionary<CarType, int> ParkingPrice { get { return _parkingPrice; } }
        public static int ParkingSpace { get { return _parkingSpace; } }
        public static int Fine { get { return _fine; } }
        public static int Timeout { get { return _timeout; } }
        public static int TimeoutOneMinute { get { return _timeoutOneMinute; } }
        public static string LogFilePath { get { return _logFilePath; } }
    }
}
