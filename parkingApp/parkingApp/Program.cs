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
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            menu.StartMenu();
            Console.ReadKey();
        }
    }
}
