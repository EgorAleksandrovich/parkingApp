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

        public void PickUpTheCar(Car outgoingCar)
        {
            if(outgoingCar != null)
            {
                _cars.Remove(outgoingCar);
            }
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

        public Car GetCar(string id)
        {
            return _cars.FirstOrDefault(c => c.Id == id);
        }

    }
}
