using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace VogtEventsEmp
{
    class Program
    {
        static void Main(string[] args)
        {
            DisplayHeader();
            DisplayGreeting();

        }

        public static void ShowUserGreeting(string userName)
        {
            Console.WriteLine($"Greetins {userName} please select options");

        }

        public static void DisplayHeader() => Console.WriteLine("**************EMPLOYEE SOLUTION**************");
        public static void DisplayGreeting() => Console.WriteLine("Welcome to the employee solution!");

    }
}
