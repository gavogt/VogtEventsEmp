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
            var employeeList = new List<Employee<int>>();
            DisplayHeader();
            DisplayGreeting();

            string userName = default;
            Console.WriteLine("What is the user's name?");
            userName = Console.ReadLine();
            Console.WriteLine("");

            Action<string> displayUserInfo = ShowUserGreeting;
            displayUserInfo(userName);

            var emp = new Employee<int>();
            emp.Name = "Gabbins";
            emp.Number = 13114;
            emp.HireDate = 2017;

            var empTwo = new Employee<int>();
            empTwo.Name = "Captain Marrow";
            empTwo.Number = 1337;
            empTwo.HireDate = 2018;

            employeeList.Add(emp);
            employeeList.Add(empTwo);

            emp.DisplayEmployeeInfo(emp.Name, emp.Number, emp.HireDate);
            empTwo.DisplayEmployeeInfo(empTwo.Name, empTwo.Number, empTwo.HireDate);
            //emp.DisplayEmployeeInfo(name: "Gabbins", number: 13114, hiredate: 2017);

            // For each snippet
            foreach (var employee in employeeList)
            {
                // Cw
                Console.WriteLine("---------");
                Console.WriteLine(employee.Name);
                Console.WriteLine(employee.Number);
                Console.WriteLine(employee.HireDate);

            }

        }

        public static void ShowUserGreeting(string userName)
        {
            Console.WriteLine($"Greetins {userName} please select options\n");

        }

        public static void DisplayHeader() => Console.WriteLine("**************EMPLOYEE SOLUTION**************");
        public static void DisplayGreeting() => Console.WriteLine("Welcome to the employee solution!\n");

    }
}
