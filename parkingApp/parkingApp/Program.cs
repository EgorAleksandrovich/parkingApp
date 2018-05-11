using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace parkingApp
{
    class Program
    {
        static Parking _parking;
        static void Main(string[] args)
        {
            _parking = Parking.GetInstance();
            Menu menu = new Menu();
            string inputString = "";
            while(inputString != "Exit")
            {
                inputString = menu.StartMenu();
                if(inputString != null && inputString != "Exit")
                {
                    while(inputString != "Back")
                    {
                        Type thisType = menu.GetType();
                        MethodInfo theMethod = thisType.GetMethod(inputString);
                        theMethod.Invoke(menu, null);
                    }
                }
            }            
            Console.ReadKey();
        }
    }
}
