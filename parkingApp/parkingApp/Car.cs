using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parkingApp
{
    public class Car
    {
        public string Id { get; set; }
        public CarType CarType { get; set; }
        public int Balance { get; set; }


        public void ReplenishAccount()
        {
            string inputString = null;
            int amount = 0;

            while (amount <= 0)
            {
                Console.Write("Enter amount to replenish (enter \"x\" to cancel): ");
                inputString = Console.ReadLine();
                if(inputString.ToLower() == "x")
                {
                    break;
                }
                if (!int.TryParse(inputString, out amount) || amount <= 0)
                {
                    Console.WriteLine("Error! Invalid input. The text you enter must be an integer value greater than 0");
                }
                this.Balance += amount;
            }
        }
    }
}
