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
        private string _textSetCarType;
        private string _inputString;
        private bool _successfulInput;

        public Menu()
        {
            _successfulInput = false;
            _menuMessageDictionary = new Dictionary<string, string>();
            _menuMessageDictionary = Messages.MenuMessagesDictionary;
            _textLineStartMenu = _menuMessageDictionary["StartMenu"];
            _textLineParkingInfoMenu = _menuMessageDictionary["ParkingInfoMenu"]; ;
            _textLiteParkingPickUpTheCarMenu = _menuMessageDictionary["ParkingPickUpTheCarMenu"];
            _textGreeting = _menuMessageDictionary["Greeting"];
            _textSetCarType = _menuMessageDictionary["CarType"];
            Greeting();
        }
        private void Greeting()
        {
            Console.WriteLine(_textGreeting);
        }

        public void StartMenu(Parking parking)
        {
            Console.Write(_textLineStartMenu);
            while (_successfulInput == false & _inputString != "5")
            try
            {
                _inputString = Console.ReadLine();
                switch (_inputString)
                {
                    case "1":
                        ParkingInfoMenu(parking);
                        _successfulInput = true;
                        break;
                    case "2":
                        ParkingPickUpTheCarMenu();
                        _successfulInput = true;
                        break;
                    case "3":
                        break;
                    default:
                        throw new ArgumentException();
                }
            }
            catch (ArgumentException)
            {
                WriteMessage("Entered value " + _inputString + " does not match any particles which were proposed");
            }
        }

        public void ParkingInfoMenu(Parking parking)
        {
            Console.Write(_textLineParkingInfoMenu);
            while (_successfulInput == false & _inputString != "5")
            {
                try
                {
                    _inputString = Console.ReadLine();
                    switch (_inputString)
                    {
                        case "1":
                            DysplayParkingSpace(parking);
                            break;
                        case "2":
                            DysplayParkingBalance(parking);
                            break;
                        case "3":
                            DysplayParkingBalanceInTheLastMinute(parking);
                            break;
                        case "4":
                            DysplayTransactionInLastMinute(parking);
                            break;
                        case "5":
                            break;
                        default:
                            throw new ArgumentException();
                    }
                }
                catch (ArgumentException)
                {
                    WriteMessage("Entered value " + _inputString + " does not match any particles which were proposed");
                }
            }
        }

        public string ParkingPickUpTheCarMenu()
        {
            Console.Write(_textLiteParkingPickUpTheCarMenu);
            try
            {
                _inputString = Console.ReadLine();
                switch (_inputString)
                {
                    case "1":
                        return "Park";
                    case "2":
                        return "PickUpTheCar";
                    case "3":
                        return "ReplenishBalance";
                    case "4":
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
            return _inputString;
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

        public void ReplenishBalance()
        {

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

        public void Park(Parking parking)
        {
            Car newCar;
            bool successfulInput = false;
            if (parking.CheckForFreePlace())
            {
                newCar = new Car();
                newCar.Id = GenerateId();
                while (successfulInput == false & _inputString != "5")
                {
                    Console.Write(_textSetCarType);
                    try
                    {
                        _inputString = Console.ReadLine();
                        switch (_inputString)
                        {
                            case "1":
                                newCar.CarType = CarType.Passenger;
                                successfulInput = true;
                                break;
                            case "2":
                                newCar.CarType = CarType.Bus;
                                successfulInput = true;
                                break;
                            case "3":
                                newCar.CarType = CarType.Motorcycle;
                                successfulInput = true;
                                break;
                            case "4":
                                newCar.CarType = CarType.Truck;
                                successfulInput = true;
                                break;
                            case "5":
                                break;
                            default:
                                throw new ArgumentException();
                        }
                    }
                    catch (ArgumentException)
                    {
                        WriteMessage("Entered value " + _inputString + " does not match any particles which were proposed");
                    }
                }
            }
        }

        private string GenerateId()
        {
            string id;
            Console.Write("Enter your name:");
            id = Console.ReadLine();
            id += Guid.NewGuid().ToString().GetHashCode().ToString("x");
            return id;
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
