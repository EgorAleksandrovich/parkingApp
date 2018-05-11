using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parkingApp
{
    public interface IMenu
    {
        void StartMenu(Parking parking);
        void ParkingInfoMenu(Parking parking);
        void ParkingPickUpTheCarMenu(Parking parking);
    }
}
