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
        public delegate void DisplayColor(string color);

        static void Main(string[] args)
        {
            // Variables  
            var employeeList = new List<Employee<int>>();
            var emp = new Employee<int>();
            bool run = true;
            int choice = default;
            string color = default;

            // Displays
            DisplayForSystem();
            AskUserName();

            // Loop to add employees
            while (run)
            {
                emp = EmpAdd();

                employeeList.Add(emp);

                try
                {
                    Console.WriteLine("Would you like to add another employee?");
                    choice = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException e)
                {
                    color = "RED";
                    ChangeConsoleColor(color);

                }
                catch
                {
                    color = "BLUE";
                    ChangeConsoleColor(color);
                }

                if (choice == 2)
                {
                    run = false;
                }
            }

            // For each snippet
            foreach (var employee in employeeList)
            {
                // Cw
                Console.WriteLine("here");
                Console.WriteLine("---------");
                Console.WriteLine(employee.Name);
                Console.WriteLine(employee.Number);
                Console.WriteLine(employee.HireDate);

            }
        }

        /// <summary>
        /// Method for adding an employee
        /// </summary>
        /// <returns>A new employee object</returns>
        public static Employee<int> EmpAdd()
        {
            var employee = new Employee<int>();
            string color = default;

            try
            {
                Console.WriteLine("What is the employee's name?");
                employee.Name = Console.ReadLine();
                Console.WriteLine("What is the employee's number?");
                employee.Number = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("What is the employee's hire date?");
                employee.HireDate = Convert.ToInt32(Console.ReadLine());

            }
            catch (FormatException e)
            {
                color = "RED";
                ChangeConsoleColor(color);

            }
            catch
            {
                color = "BLUE";
                ChangeConsoleColor(color);

            }

            employee.DisplayEmployeeInfo(employee.Name, employee.Number, employee.HireDate);

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
        /// Display Red console text
        /// </summary>
        public static void ChangeConsoleColor(string color)
        {
            if (color == "RED")
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Error with format!");

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("The light is broken inside but I still work");

            }

            ConsoleReset();
        }

        /// <summary>
        /// Return the console color to normal
        /// </summary>
        public static void ConsoleReset()
        {
            Console.ResetColor();

        }

        /// <summary>
        /// Lambda for display
        /// </summary>
        public static void DisplayHeader() => Console.WriteLine("**************EMPLOYEE SOLUTION**************");
        public static void DisplayGreeting() => Console.WriteLine("Welcome to the employee solution!\n");
    }
}
