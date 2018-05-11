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
            string previousUserChoice = null;
            string currentUsersChoice = "StartMenu";
            string newUserChoice = "";
            
            while (currentUsersChoice != "Exit")
            {
                newUserChoice = Program.CallMethod(currentUsersChoice, menu);
                if (newUserChoice == currentUsersChoice)
                {
                    currentUsersChoice = previousUserChoice;
                }
                else
                {
                    if (previousUserChoice == null)
                    {
                        previousUserChoice = newUserChoice;
                    }
                    currentUsersChoice = newUserChoice;
                }
            }
            Console.ReadKey();
        }
        static string CallMethod(string methodName, Menu menu)
        {
            string result = methodName;
            Type thisType = menu.GetType();
            if (thisType != null)
            {
                MethodInfo methodInfo = thisType.GetMethod(methodName);

                if (methodInfo != null)
                {
                    ParameterInfo[] parameters = methodInfo.GetParameters();
                    if (parameters.Length == 0)
                    {
                        result = methodInfo.Invoke(menu, null).ToString();
                    }
                    else
                    {
                        object[] parametersArray = new object[] { _parking };
                        object obj = methodInfo.Invoke(menu, parametersArray);
                        if (obj != null)
                        {
                            result = obj.ToString();
                        }
                    }
                }
            }
            return result;
        }
    }
}
