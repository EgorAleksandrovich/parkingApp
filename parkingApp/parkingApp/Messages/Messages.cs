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
            {"Greeting","Hello! Welcom to our parking \"Сar under guard\"!"},
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
                                        "\n\t\t 3 'Replenish balance'" +
                                        "\n\t\t 4 'Back'"+"\n>"},
            {"CarType", GetCarTypeMessage()}
        };
        public static Dictionary<string, string> MenuMessagesDictionary { get { return _dictionaryMessages; } }

        static private string GetCarTypeMessage()
        {
            string result = "(Car type) Enter number of car type :";
            int count = 1;
            foreach (CarType carType in Enum.GetValues(typeof(CarType)))
            {
                result += "\n\t\t " + count +" "+ carType;
                count++;
            }
            result += "\n>";
            return result;
        }
    }
}
