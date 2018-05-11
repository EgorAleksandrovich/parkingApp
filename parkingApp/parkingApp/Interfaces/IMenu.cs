using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parkingApp
{
    public interface IMenu
    {
        string StartMenu();
        string ParkingInfoMenu();
        void ParkingPickUpTheCarMenu();
    }
}
