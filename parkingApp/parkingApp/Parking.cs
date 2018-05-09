using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parkingApp
{
    public sealed class Parking
    {
        private List<Car> _cars;
        private List<Transaction> _transaction;
        private int _parkingSpace;
        private int _fine;
        private Dictionary<string, int> _parkingPrice;

        private int Balance { get; set; }
        private static readonly Lazy<Parking> lazy = new Lazy<Parking>(() => new Parking());

        private Parking()
        {
            _cars = new List<Car>();
            _transaction = new List<Transaction>();
            _parkingSpace = Settings.ParkingSpace;
            _fine = Settings.Fine;
            _parkingPrice = Settings.ParkingPrice;
        }

        public static Parking GetInstance()
        {
            return lazy.Value;
        }

        public void ParkTheCar(Car car)
        {
            if (!(car == null) && _cars.Count() <= _parkingSpace)
            {
                _cars.Add(car);
            }
        }

        public void PickUpTheCar(string carId)
        {
            Car car = _cars.FirstOrDefault(c => c.Id == carId);
            if(car.Balance<0)
            {
                while(car.Balance >)
            }

        }

        public void ShowParkingSpace()
        {
            int freeParkingSpace = _parkingSpace - _cars.Count();
            Console.WriteLine(string.Format("Available number of space {0}, busy spaces {1}.", freeParkingSpace, _cars.Count()));
        }

        public void Withdraw()
        {
            int amount = 0;
            if (_cars != null)
            {
                foreach (var car in _cars)
                {
                    amount = _parkingPrice[car.CarType.ToString()];
                    if (car.Balance < 0)
                    {
                        amount *= _fine;
                    }
                    car.Balance -= amount;
                    _transaction.Add(new Transaction { CarId = car.Id, TransactionTime = DateTime.Now, WriteOffs = _parkingPrice[car.CarType.ToString()] });
                }
            }
        }
    }
}
