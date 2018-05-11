using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace parkingApp
{
    public sealed class Parking
    {
        private static readonly Lazy<Parking> lazy = new Lazy<Parking>(() => new Parking());
        private List<Car> _cars;
        private List<Transaction> _transactions;
        private int _parkingSpace;
        private int _fine;
        private Dictionary<CarType, int> _parkingPrice;
        private int _timeout;
        private Timer _timer;

        private int Balance { get; set; }
        public List<Transaction> Transactions { get { return _transactions; } }

        private Parking()
        {
            _cars = new List<Car>();
            _transactions = new List<Transaction>();
            _parkingSpace = Settings.ParkingSpace;
            _fine = Settings.Fine;
            _parkingPrice = Settings.ParkingPrice;
            _timeout = Settings.Timeout;
            _timer = new Timer();
            _timer.Interval = _timeout;
            _timer.Enabled = true;
            _timer.Elapsed += Withdraw;
        }

        public static Parking GetInstance()
        {
            return lazy.Value;
        }

        public int GetBalance()
        {
            return Balance;
        }

        public int GetBalanceInTheLastMinute()
        {
            int sum = _transactions.Sum(tr => tr.WriteOffs);
            return sum;
        }

        public bool CheckForFreePlace()
        {
            return _cars != null?true:_cars.Count() < _parkingSpace;
        }

        public void ParkTheCar(Car car)
        {
            if (!(car == null) && _cars.Count() <= _parkingSpace)
            {
                _cars.Add(car);
            }
        }

        public void PickUpTheCar()
        {
            Car outgoingСar = FindCar();
            bool canseledAction = false;

            if (outgoingСar == null)
            {
                return;
            }
            else
            {
                while (outgoingСar.Balance < 0)
                {
                    canseledAction = LoanPayment(outgoingСar);
                    if (canseledAction)
                    {
                        break;
                    }
                }
            }
            _cars.Remove(outgoingСar);
        }

        private Car FindCar()
        {
            string inputString = string.Empty;
            Car outgoingСar = null;
            do
            {
                Console.Write("Enter your id (enter \"x\" to cancel): ");
                inputString = Console.ReadLine().ToLower();
                if (inputString == "x")
                {
                    break;
                }
                else
                {
                    outgoingСar = _cars.FirstOrDefault(c => c.Id == inputString);
                    if (outgoingСar == null)
                    {
                        Console.WriteLine(string.Format("Car with id {0} not found, please check the correctness of the input id.", inputString));
                    }
                }
            } while (outgoingСar == null);

            return outgoingСar;
        }

        private bool LoanPayment(Car outgoingСar)
        {
            bool cansalesAction = false;
            int requiredAmount = outgoingСar.Balance * -1;
            Console.WriteLine(string.Format("In your account {0}, to pick up the car replenish the account not less than {1}",
            outgoingСar.Balance, requiredAmount));
            cansalesAction = outgoingСar.ReplenishAccount();
            return cansalesAction;
        }

        public void GetParkingSpace(out int busyPosition, out int freePosition)
        {
            busyPosition = _cars.Count();
            freePosition = _parkingSpace - _cars.Count();
        }

        private void Withdraw(object sender, ElapsedEventArgs e)
        {
            int amount = 0;
            if (_cars != null)
            {
                foreach (var car in _cars)
                {
                    amount = _parkingPrice[car.CarType];
                    if (car.Balance < 0)
                    {
                        amount *= _fine;
                    }
                    car.Balance -= amount;
                    _transactions.Add(new Transaction { CarId = car.Id, 
                        TransactionTime = DateTime.Now, 
                        WriteOffs = _parkingPrice[car.CarType] });
                }
            }
        }

    }
}
