using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VogtEventsEmp
{
    class Program
    {
        static void Main(string[] args)
        {
            DisplayHeader();
            DisplayGreeting();

            Employee emp = new Employee();



        }

        class Employee
        {

            private DisplayEmployeeInformation DisplayInformation;

        }

        class DisplayEmployeeInformation
        {

        }

        public static void ShowUserGreeting(string userName)
        {
            Console.WriteLine($"Greetins {userName} please select options");

        }

        public static void DisplayHeader() => Console.WriteLine("**************EMPLOYEE SOLUTION**************");
        public static void DisplayGreeting() => Console.WriteLine("Welcome to the employee solution!");

    }
}
