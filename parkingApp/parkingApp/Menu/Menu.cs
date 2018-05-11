using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parkingApp
{
    public class Menu : IMenu
    {
        private Dictionary<string, string> _menuMessageDictionary;
        private string _textLineStartMenu;
        private string _textLineParkingInfoMenu;
        private string _textLiteParkingPickUpTheCarMenu;
        private string _textGreeting;
        private string _inputString;

        public Menu()
        {
            _menuMessageDictionary = new Dictionary<string, string>();
            _menuMessageDictionary = Messages.MenuMessagesDictionary;
            _textLineStartMenu = _menuMessageDictionary["StartMenu"];
            _textLineParkingInfoMenu = _menuMessageDictionary["ParkingInfoMenu"]; ;
            _textLiteParkingPickUpTheCarMenu = _menuMessageDictionary["ParkingPickUpTheCarMenu"];
            _textGreeting = _menuMessageDictionary["Greeting"];
            Greeting();
        }
        private void Greeting()
        {
            Console.WriteLine(_textGreeting);
        }

        public string StartMenu()
        {
            Console.Write(_textLineStartMenu);
            try
            {
                _inputString = Console.ReadLine();
                switch (_inputString)
                {
                    case "1":
                        return "ParkingInfoMenu";
                    case "2":
                        return "ParkingPickUpTheCarMenu";
                    case "3":
                        return "Exit";
                    default:
                        throw new ArgumentException();
                }
            }
            catch (ArgumentException)
            {
                Console.WriteLine(new string('-', 50));
                Console.WriteLine("Entered value " + _inputString + " does not match any particles which were proposed");
                Console.WriteLine(new string('-', 50));
                return "";
            }
        }

        public string ParkingInfoMenu()
        {
            Console.Write(_textLineParkingInfoMenu);
            try
            {
                _inputString = Console.ReadLine();
                switch (_inputString)
                {
                    case "1":
                        return "ShowParkingSpace";
                    case "2":
                        return "GetBalance";
                    case "3":
                        return "GetBalanceInTheLastMinute";
                    case "4":
                        return "DysplayTransaction";
                    case "5":
                        return "Exit";
                    default:
                        throw new ArgumentException();
                }
            }
            catch (ArgumentException)
            {
                Console.WriteLine(new string('-', 50));
                Console.WriteLine("Entered value " + _inputString + " does not match any particles which were proposed");
                Console.WriteLine(new string('-', 50));
                return "";
            }
        }

        public void ParkingPickUpTheCarMenu()
        {
            Console.Write(_textLiteParkingPickUpTheCarMenu);
        }

        public static void DysplayTransaction(List<Transaction> transactions)
        {
            foreach(Transaction transaction in transactions)
            {
                Console.Write(string.Format("{0} withdraw from car with id {1}: {2}", transaction.TransactionTime, transaction.CarId, transaction.WriteOffs );
            }
        }
    }
}
