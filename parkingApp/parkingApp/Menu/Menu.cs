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
        private Parking _parking;

        public Menu()
        {
            _parking = Parking.GetInstance();
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
            try
            {
                while (_inputString != "3")
                {
                    Console.Clear();
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
                            break;
                        default:
                            throw new ArgumentException();
                    }
                }
            }
            catch (ArgumentException)
            {
                WriteMessage("Entered value " + _inputString + " does not match any particles which were proposed");
                this.StartMenu();
            }
        }

        public void ParkingInfoMenu()
        {
            try
            {
                while (_inputString != "6")
                {
                    Console.Clear();
                    Console.Write(_textLineParkingInfoMenu);
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
                            DysplayAllTransactions();
                            break;
                        case "6":
                            break;
                        default:
                            throw new ArgumentException();
                    }
                }
            }
            catch (ArgumentException)
            {
                WriteMessage("Entered value " + _inputString + " does not match any particles which were proposed");
                this.StartMenu();

            }
        }

        public void ParkingPickUpTheCarMenu()
        {
            try
            {
                while (_inputString != "4")
                {
                    Console.Clear();
                    Console.Write(_textLiteParkingPickUpTheCarMenu);
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
                            ReplanishCarBalance();
                            break;
                        case "4":
                            break;
                        default:
                            throw new ArgumentException();
                    }
                }
            }
            catch (ArgumentException)
            {
                WriteMessage("Entered value " + _inputString + " does not match any particles which were proposed");
                this.ParkingPickUpTheCarMenu();
            }
        }

        public void PickUpTheCar()
        {
            Car outgoingCar = FindCar();
            int amount = 0;
            if (outgoingCar != null)
            {
                while (outgoingCar.Balance < 1)
                {
                    int requiredAmount = outgoingCar.Balance * -1;
                    WriteMessage(string.Format("In your account {0}, to pick up the car replenish the account not less than {1}", outgoingCar.Balance, requiredAmount));
                    amount = ReplanishCarBalance(outgoingCar);
                    if (amount == 0)
                    {
                        WriteMessage("Operation canceled!");
                        break;
                    }
                }
                if(amount != 0)
                {
                    _parking.PickUpTheCar(outgoingCar);
                    WriteMessage("The car left, goodbye!");
                }
            }
        }

        public int ReplanishCarBalance(Car car)
        {
            int amount = 0;
            Replanish(car, out amount);
            if (amount > 0)
            {
                WriteMessage("Car has been parked!");
            }
            return amount;
        }

        public int ReplanishCarBalance()
        {
            int amount = 0;
            Car car = FindCar();
            Replanish(car, out amount);
            return amount;
        }

        public void Replanish(Car car, out int amount)
        {
            amount = 0;
            try
            {
                Console.Clear();
                Console.Write("Enter amount to replenish (enter \"x\" to cancel): ");
                _inputString = Console.ReadLine();
                if (_inputString.ToLower() == "x")
                {
                    return;
                }
                else
                {
                    if (!int.TryParse(_inputString, out amount) || amount <= 0)
                    {
                        throw new ArgumentException();
                    }
                    else
                    {
                        car.Balance += amount;
                        WriteMessage(string.Format("Balance have been successful replenish for the amount of {0}!", amount));
                    }
                }

            }
            catch (ArgumentException)
            {
                WriteMessage("Error! Invalid input. The text you enter must be an integer value greater than 0");
                Replanish(car, out amount);
            }
        }

        public Car FindCar()
        {
            Car outgoingСar = new Car();
            try
            {
                Console.Clear();
                Console.Write("Enter your id (enter \"x\" to cancel): ");
                _inputString = Console.ReadLine().ToLower();
                if (_inputString == "x")
                {
                    return null;
                }
                else
                {
                    outgoingСar = _parking.GetCar(_inputString);
                    if (outgoingСar != null)
                    {
                        return outgoingСar;
                    }
                    else
                    {
                        throw new ArgumentException();
                    }
                }
            }
            catch (ArgumentException)
            {
                WriteMessage(string.Format("Car with id \"{0}\" not found, please check the correctness of the input id.", _inputString));

            }
            return null;
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
            else
            {
                transactionList = "No transction yet!";
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
            if (_parking.CheckForFreePlace())
            {
                int amount = 0;
                newCar = new Car();
                newCar.Id = GenerateId();
                SetCarType(newCar);
                amount = ReplanishCarBalance(newCar);
                if (amount > 0)
                {
                    _parking.ParkTheCar(newCar);
                }
            }
        }

        public void SetCarType(Car car)
        {
            try
            {
                Console.Clear();
                Console.Write(_textSetCarType);
                _inputString = Console.ReadLine();
                switch (_inputString)
                {
                    case "1":
                        car.CarType = CarType.Bus;
                        break;
                    case "2":
                        car.CarType = CarType.Truck;
                        break;
                    case "3":
                        car.CarType = CarType.Motorcycle;
                        break;
                    case "4":
                        car.CarType = CarType.Passenger;
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
                SetCarType(car);
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

        public void DysplayAllTransactions()
        {
            string result = "";
            string[] lines = _parking.RetriveTransactionData();
            if (lines.Length == 0)
            {
                result = "No transction yet!";
            }
            else
            {
                foreach (string line in lines)
                {
                    result += "\n" + line;
                }
            }
            WriteMessage(result);
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
