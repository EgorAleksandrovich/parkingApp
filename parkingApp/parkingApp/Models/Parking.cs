using System;
using System.Collections.Generic;
using System.IO;
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
        private int _timeoutOneMinute;
        private Timer _timer;
        private Timer _timerOneMinute;

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

            _timeoutOneMinute = Settings.TimeoutOneMinute;
            _timerOneMinute = new Timer();
            _timerOneMinute.Interval = _timeoutOneMinute;
            _timerOneMinute.Enabled = true;
            _timerOneMinute.Elapsed += ExportTransaction;
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
            return _cars != null ? true : _cars.Count() < _parkingSpace;
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
            if (outgoingCar != null)
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
                    Balance += amount;
                    _transactions.Add(new Transaction
                    {
                        CarId = car.Id,
                        TransactionTime = DateTime.Now,
                        WriteOffs = _parkingPrice[car.CarType]
                    });
                }
            }
        }

        private void ExportTransaction(object sender, ElapsedEventArgs e)
        {
            string filePath = @"C:\Users\User\Documents\Transaction\Transaction.log";

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                if (Transactions.Count() > 0)
                {
                    writer.WriteLine("Date :" + DateTime.Now.ToString() + "" + Environment.NewLine);
                    foreach (Transaction transaction in _transactions)
                    {
                        writer.WriteLine(string.Format("{0} withdraw from car with id {1}: {2}g.",
                            transaction.TransactionTime,
                            transaction.CarId,
                            transaction.WriteOffs));
                    }
                    writer.WriteLine(Environment.NewLine + new string('-', 50) + Environment.NewLine);
                }
            }
        }

        public Car GetCar(string id)
        {
            return _cars.FirstOrDefault(c => c.Id == id);
        }
    }
}
