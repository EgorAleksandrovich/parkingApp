using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parkingApp
{
    public static class Settings
    {
        private static int _parkingSpace = 100;
        private static int _fine = 2;
        private static Dictionary<string,int> _parkingPrice = new Dictionary<string,int>
        {
            {"Truck", 5},
            {"Passenger", 3},
            {"Bus", 2},
            {"Motorcycle", 1}
        };

        public static Dictionary<string, int> ParkingPrice { get { return _parkingPrice; } }
        public static int ParkingSpace { get { return _parkingSpace; } }
        public static int Fine { get { return _fine; } }
    }
}
