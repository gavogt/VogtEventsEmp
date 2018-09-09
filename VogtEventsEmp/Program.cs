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
            // List initialization
            var employeeList = new List<Employee<int>>();
            var emp = new Employee<int>();
            var empTwo = new Employee<int>();

            // Displays
            DisplayForSystem();
            AskUserName();

            emp = EmpAdd();


            // Add to list 
            employeeList.Add(emp);
            //employeeList.Add(empTwo);

            emp.DisplayEmployeeInfo(emp.Name, emp.Number, emp.HireDate);
            //empTwo.DisplayEmployeeInfo(empTwo.Name, empTwo.Number, empTwo.HireDate);
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

        public static Employee<int> EmpAdd()
        {

            var employee = new Employee<int>();

            string empName = default;
            int empNumber = default;
            int empHireDate = default;

            Console.WriteLine("What is the employee's name?");
            empName = Console.ReadLine();
            Console.WriteLine("What is the employee's number?");
            empNumber = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("What is the employee's hire date?");
            empHireDate = Convert.ToInt32(Console.ReadLine());

            return employee;
        }

        /// <summary>
        /// Display a greeting for the user
        /// </summary>
        /// <param name="userName"></param>
        public static void ShowUserGreeting(string userName)
        {
            Console.WriteLine($"Greetins {userName} please select options\n");

        }

        /// <summary>
        /// Ask for the user's name
        /// </summary>
        public static void AskUserName()
        {
            string userName = default;
            Console.WriteLine("What is the user's name?");
            userName = Console.ReadLine();
            Console.WriteLine("");

            Action<string> displayUserInfo = ShowUserGreeting;
            displayUserInfo(userName);

        }

        /// <summary>
        /// Method to display
        /// </summary>
        public static void DisplayForSystem()
        {
            DisplayHeader();
            DisplayGreeting();

        }

        /// <summary>
        /// Lambda for display
        /// </summary>
        public static void DisplayHeader() => Console.WriteLine("**************EMPLOYEE SOLUTION**************");
        public static void DisplayGreeting() => Console.WriteLine("Welcome to the employee solution!\n");
    }
}
