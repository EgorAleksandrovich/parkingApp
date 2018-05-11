using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parkingApp
{
    public static class Messages
    {
        private static Dictionary<string, string> _dictionaryMessages = new Dictionary<string, string>
        {
            {"StartMenu","(Start menu) Enter number of partition which you want:" +
                         "\n\t\t 1 go to 'Parking info'" +
                         "\n\t\t 2 go to 'Park/Pick up the car'" +
                         "\n\t\t 3 go to 'Exit'"+"\n>"},
            {"ParkingInfoMenu", "(Parking info menu) Enter number of action which you want to:" +
                                "\n\t\t 1 show  'Parking space'" +
                                "\n\t\t 2 show  'Parking balance(total)'" +
                                "\n\t\t 3 show  'Parking balance(in the last minute)'" +
                                "\n\t\t 4 show  'Transactions(in the last minute)'" +
                                "\n\t\t 5 go    'Back'"+"\n>"},
            {"ParkingPickUpTheCarMenu", "(Park/Pick up the car menu) Enter number of action which you want:" +
                                        "\n\t\t 1 'Park'" +
                                        "\n\t\t 2 'Pick up the car'" +
                                        "\n\t\t 3 'Back'"+"\n>"}
        };
        public static Dictionary<string, string> MenuMessagesDictionary{ get { return _dictionaryMessages; } }
    }
}
