using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parkingApp
{
    public class Car
    {
        private int _balance = 0;

        public string Id { get; set; }
        public CarType CarType { get; set; }
        public int Balance
        {
            get
            {
                return _balance;
            }
            set
            {
                if (value > 0)
                {
                    _balance = value;
                }
            }
        }


        public void ReplenishAccount()
        {
            string inputString = null;
            int amount = 0;

            while (amount <= 0)
            {
                Console.Write("Введите сумму для пополнения:");
                inputString = Console.ReadLine();
                if (!int.TryParse(inputString, out amount) || amount <= 0)
                {
                    Console.WriteLine("Ошибка! Некорректный ввод. Введите числовое значение больше 0!");
                }
                this.Balance += amount;
            }
        }
    }
}
