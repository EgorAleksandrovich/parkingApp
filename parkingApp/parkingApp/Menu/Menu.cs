using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parkingApp
{
    public class Menu : IMenu
    {
        public Menu()
        {
            Greeting();
        }
        private void Greeting()
        {
            Console.WriteLine("Hello! Welcom to our parking \"Сar under guard\"!");
        }

        public void StartMenu()
        {
            Console.WriteLine("(Start menu) Enter number of partition which you want:"+
                "\n\t\t 1 go to 'Parking info'"+
                "\n\t\t 2 go to 'Parking/Pick up the car'"+
                "\n\t\t 3 go to 'Exit'");
        }

        public void ParkingInfoMenu()
        {
            Console.WriteLine("(Parking info menu) Enter number of action which you want:" +
                "\n\t 1 show  'Parking space'"+
                "\n\t 2 show  'Parking balance(total)'"+
                "\n\t 3 show  'Parking balance(in the last minute)'" +
                "\n\t 4 show  'Transaction in the last minute'" +
                "\n\t 5 go to 'Back'");
        }

        public void ParkingPickUpTheCarMenu()
        {
            Console.WriteLine("(Parking/Pick up the car menu) Enter number of action which you want:" +
                "\n\t 1 show 'Parking space'" +
                "\n\t 2 show 'Parking balance(total)'" +
                "\n\t 2 show 'Parking balance(in the last minute)'" +
                "\n\t 3 show 'Transaction in the last minute'");
        }

    }
}
