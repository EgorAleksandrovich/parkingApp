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
        private Parking _parking;


        public Menu()
        {
            _parking = Parking.GetInstance();
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

        public void StartMenu()
        {
            bool exit = false;
            while (exit == false)
                try
                {
                    Console.Write(_textLineStartMenu);
                    _inputString = Console.ReadLine();
                    switch (_inputString)
                    {
                        case "1":
                            ParkingInfoMenu();
                            break;
                        case "2":
                            ParkingPickUpTheCarMenu();
                            break;
                        case "3":
                            exit = true;
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

        public void ParkingInfoMenu()
        {
            bool canseled = false;

            while (canseled == false)
            {
                Console.Write(_textLineParkingInfoMenu);
                try
                {
                    _inputString = Console.ReadLine();
                    switch (_inputString)
                    {
                        case "1":
                            DysplayParkingSpace();
                            break;
                        case "2":
                            DysplayParkingBalance();
                            break;
                        case "3":
                            DysplayParkingBalanceInTheLastMinute();
                            break;
                        case "4":
                            DysplayTransactionInLastMinute();
                            break;
                        case "5":
                            canseled = true;
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

        public void ParkingPickUpTheCarMenu()
        {
            bool canseled = false;
            while (canseled == false)
            {
                Console.Write(_textLiteParkingPickUpTheCarMenu);
                try
                {
                    _inputString = Console.ReadLine();
                    switch (_inputString)
                    {
                        case "1":
                            Park();
                            break;
                        case "2":
                            PickUpTheCar();
                            break;
                        case "3":
                            ReplanishBalance();
                            break;
                        case "4":
                            canseled = true;
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

        public void PickUpTheCar()
        {
            Car outgoingCar = FindCar();
            while (outgoingCar.Balance < 0 || _successfulInput == false)
            {
                int requiredAmount = outgoingCar.Balance * -1;
                Console.WriteLine(string.Format("In your account {0}, to pick up the car replenish the account not less than {1}",
                                                                                         outgoingCar.Balance, requiredAmount));

                _successfulInput = ReplanishBalance();
            }
        }

        public bool ReplanishBalance()
        {
            Car outgoingCar = FindCar();
            Console.Write("Enter amount to replenish (enter \"x\" to cancel): ");
            int amount = 0;
            while (_successfulInput == false)
            {
                try
                {
                    _inputString = Console.ReadLine();
                    if (_inputString.ToLower() == "x")
                    {
                        break;
                    }
                    else
                    {
                        if (!int.TryParse(_inputString, out amount) || amount <= 0)
                        {
                            throw new ArgumentException();
                        }
                        else
                        {
                            outgoingCar.Balance += amount;
                            _successfulInput = true;
                        }
                    }

                }
                catch (ArgumentException)
                {
                    WriteMessage("Error! Invalid input. The text you enter must be an integer value greater than 0");
                }
            }
            return _successfulInput;
        }

        public Car FindCar()
        {
            Car outgoingСar = new Car();
            try
            {
                while (_successfulInput == false)
                    Console.Write("Enter your id (enter \"x\" to cancel): ");
                _inputString = Console.ReadLine().ToLower();
                if (_inputString == "x")
                {
                    _successfulInput = true;
                }
                else
                {
                    outgoingСar = _parking.GetCar(_inputString);
                    if (outgoingСar == null)
                    {
                        throw new ArgumentException();
                    }
                }
            }
            catch (ArgumentException)
            {
                WriteMessage(string.Format("Car with id {0} not found, please check the correctness of the input id.", _inputString));
            }
            return outgoingСar;
        }

        public void DysplayTransactionInLastMinute()
        {
            string transactionList = "";
            int count = 1;
            if (_parking.Transactions.Count > 0)
            {
                int lastTransaction = _parking.Transactions.Count();
                foreach (Transaction transaction in _parking.Transactions)
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

        public void DysplayParkingSpace()
        {
            int busyPlace;
            int freePlace;
            _parking.GetParkingSpace(out busyPlace, out freePlace);
            WriteMessage(string.Format("Available number of places {0}, busy places {1}.", freePlace, busyPlace));
        }

        public void DysplayParkingBalance()
        {
            WriteMessage(string.Format("The current parking balance is {0}.", _parking.GetBalance()));
        }

        public void DysplayParkingBalanceInTheLastMinute()
        {
            WriteMessage(string.Format("The current parking balance in the last minute is {0}.", _parking.GetBalanceInTheLastMinute()));
        }

        public void Park()
        {
            Car newCar;
            bool successfulInput = false;
            if (_parking.CheckForFreePlace())
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
                        _parking.ParkTheCar(newCar);
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
