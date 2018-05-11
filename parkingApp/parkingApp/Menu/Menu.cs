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
                        return "DysplayParkingSpace";
                    case "2":
                        return "DysplayParkingBalance";
                    case "3":
                        return "DysplayParkingBalanceInTheLastMinute";
                    case "4":
                        return "DysplayTransactionInLastMinute";
                    case "5":
                        return "Back";
                    default:
                        throw new ArgumentException();
                }
            }
            catch (ArgumentException)
            {
                WriteMessage("Entered value " + _inputString + " does not match any particles which were proposed");
                return "";
            }
        }

        public string ParkingPickUpTheCarMenu()
        {
            Console.Write(_textLiteParkingPickUpTheCarMenu);
            return "";
        }

        public void DysplayTransactionInLastMinute(Parking parking)
        {
            string transactionList = "";
            int count = 1;
            if (parking.Transactions.Count > 0)
            {
                int lastTransaction = parking.Transactions.Count();
                foreach (Transaction transaction in parking.Transactions)
                {
                    transactionList += string.Format("{0} withdraw from car with id {1}: {2}g.",
                        transaction.TransactionTime,
                        transaction.CarId,
                        transaction.WriteOffs);
                    if (count != lastTransaction)
                    {
                        transactionList += "\n";
                    }
                    count++;
                }
            }

            WriteMessage(transactionList);
        }

        public void DysplayParkingSpace(Parking parking)
        {
            int busyPlace;
            int freePlace;
            parking.GetParkingSpace(out busyPlace, out freePlace);
            WriteMessage(string.Format("Available number of places {0}, busy places {1}.", freePlace, busyPlace));
        }

        public void DysplayParkingBalance(Parking parking)
        {
            WriteMessage(string.Format("The current parking balance is {0}.", parking.GetBalance()));
        }

        public void DysplayParkingBalanceInTheLastMinute(Parking parking)
        {
            WriteMessage(string.Format("The current parking balance in the last minute is {0}.", parking.GetBalanceInTheLastMinute()));
        }

        private void WriteMessage(string message)
        {
            Console.WriteLine(new string('-', 50));
            Console.WriteLine(message);
            Console.WriteLine(new string('-', 50));
            Delay();
        }

        private void Delay()
        {
            Console.Write("Press any button to continue...");
            Console.ReadKey();
        }

    }
}
