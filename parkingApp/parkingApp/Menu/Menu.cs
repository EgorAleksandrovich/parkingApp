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

        public Menu()
        {
            _menuMessageDictionary = new Dictionary<string, string>();            
            _menuMessageDictionary = Messages.MenuMessagesDictionary;
            _textLineStartMenu = _menuMessageDictionary["StartMenu"];
            _textLineParkingInfoMenu = _menuMessageDictionary["ParkingInfoMenu"]; ;
            _textLiteParkingPickUpTheCarMenu = _menuMessageDictionary["ParkingPickUpTheCarMenu"];
            Greeting();
        }
        private void Greeting()
        {
            Console.WriteLine("Hello! Welcom to our parking \"Сar under guard\"!");
        }

        public void StartMenu()
        {
            Console.Write(_textLineStartMenu);
            string inputString = Console.ReadLine();
        }

        public void ParkingInfoMenu()
        {
            Console.Write(_textLineParkingInfoMenu);
        }

        public void ParkingPickUpTheCarMenu()
        {
            Console.Write(_textLiteParkingPickUpTheCarMenu);
        }
    }
}
