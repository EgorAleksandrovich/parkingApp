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

        private int Balance { get; set; }
        private static readonly Lazy<Parking> lazy = new Lazy<Parking>(() => new Parking());

        private Parking()
        {
            _cars = new List<Car>();
            _transaction = new List<Transaction>();
            _parkingSpace = Settings.ParkingSpace;
            _fine = Settings.Fine;
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

        public void ShowFreeParkingSpace()
        {
            int freeParkingSpace = _parkingSpace - _cars.Count();
            Console.WriteLine(string.Format("Свободных мест на парковке: {0}", freeParkingSpace));
        }
    }
}
