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
        // Delegate for the username
        public delegate void Username(string name);

        static void Main(string[] args)
        {

            // Variables  
            var employeeList = new List<Employee<int>>();
            var emp = new Employee<int>();
            string user = default;

            // Displays
            DisplayForSystem();

            // Ask username
            user = AskUserName();

            // Menu
            Menu(user);

            // Add employee and return to list
            employeeList = EmpListAdd(user);

            // Loop through Employee
            LoopThroughList(employeeList);

        }

        #region Loop through an employee list
        public static void LoopThroughList(List<Employee<int>> employeeList)
        {
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
        #endregion

        #region Add employee to a list
        /// <summary>
        /// Method for taking the user's name and returning a list
        /// </summary>
        /// <param name="user">User's name that's entering the data</param>
        /// <returns>An employee list</returns>
        public static List<Employee<int>> EmpListAdd(string user)
        {
            // Variables
            var employeeList = new List<Employee<int>>();
            var emp = new Employee<int>();
            bool run = true;
            int choice = default;
            string color = default;

            while (run) // Loop to add employees
            {
                // Custom message off the username delegate
                Username myUsername = delegate (string username) { Console.WriteLine("Please try again " + username); };

                // Assign the employee to a list
                emp = EmpAdd(user);
                employeeList.Add(emp);

                try // Try catch with a specific message and color changes
                {
                    Console.WriteLine("Would you like to add another employee?");
                    choice = Convert.ToInt32(Console.ReadLine());

                }
                catch (FormatException)
                {
                    ClearConsole();
                    myUsername(user);
                    color = "RED";
                    ChangeConsoleColor(color);

                }
                catch
                {
                    ClearConsole();
                    myUsername(user);
                    color = "BLUE";
                    ChangeConsoleColor(color);

                }
                if (choice == 2)
                {
                    run = false;

                }
            }

            return employeeList;

        }
        #endregion

        #region Add an employee with properties
        /// <summary>
        /// Method for adding an employee
        /// </summary>
        /// <returns>A new employee object</returns>
        public static Employee<int> EmpAdd(string user)
        {
            // Custom message from the username delegate
            Username myUsername = delegate (string username) { Console.WriteLine("Please try again with adding an employee " + username); };

            // Initialize an employee object with a generic for int
            var employee = new Employee<int>();

            // Color for error throws 
            string color = default;

            try // Try catch for adding an employee's properties
            {
                Console.WriteLine("What is the employee's name?");
                employee.Name = Console.ReadLine();
                Console.WriteLine("What is the employee's number?");
                employee.Number = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("What is the employee's hire date?");
                employee.HireDate = Convert.ToInt32(Console.ReadLine());

            }
            catch (FormatException)
            {
                ClearConsole();
                color = "RED";
                ChangeConsoleColor(color);
                myUsername(user);

            }
            catch
            {
                ClearConsole();
                color = "BLUE";
                ChangeConsoleColor(color);
                myUsername(user);

            }

            // Display emp information off an event
            employee.DisplayEmployeeInfo(employee.Name, employee.Number, employee.HireDate);

            return employee;

        }
        #endregion

        #region Menu for the user
        /// <summary>
        /// Method for displaying the menu to a user
        /// </summary>
        /// <param name="user">The user that needs the menu</param>
        /// <returns>The selected menu choie</returns>
        public static int Menu(string user)
        {
            // Standard message off the username delegate
            Username usernameDefault = new Username(DisplayUserName);

            // Variables
            int choice = default;
            string color = default;

            try // Try catch for the menu selection
            {
                Console.WriteLine("What is your selection?");
                Console.WriteLine("1. Add an employee");
                Console.WriteLine("2. Exit");
                choice = Convert.ToInt32(Console.ReadLine());
                Choice(choice);

            }
            catch
            {
                ClearConsole();
                usernameDefault(user);
                color = "RED";
                ChangeConsoleColor(color);

            }

            return choice;

        }
        #endregion

        #region Display a greeting for the user
        /// <summary>
        /// Display a greeting for the user
        /// </summary>
        /// <param name="userName"></param>
        public static void ShowUserGreeting(string userName)
        {
            // Standard message for a greeting
            Console.WriteLine($"Greetings {userName}! Please select one of the following options below. \n");

        }
        #endregion

        #region Ask for the user's name
        /// <summary>
        /// Ask for the user's name
        /// </summary>
        public static string AskUserName()
        {
            // Variables
            string userName = default;

            // Assign the user
            Console.WriteLine("What is the user's name?");
            userName = Console.ReadLine();
            Console.WriteLine("");

            // Display a standard greeting message
            Action<string> displayUserInfo = ShowUserGreeting;
            displayUserInfo(userName);

            return userName;

        }
        #endregion

        #region Choice for the menu
        /// <summary>
        /// Selection for options
        /// </summary>
        /// <param name="choice"></param>
        /// <returns></returns>
        public static int Choice(int choice)
        {
            switch (choice) // Loop through for the choice
            {
                case 1:
                    break;
                case 2:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Something happened!");
                    break;
            }

            return choice;

        }
        #endregion

        #region Initial Display for the system
        /// <summary>
        /// Method to display when the program starts
        /// </summary>
        public static void DisplayForSystem()
        {
            // Standard system messages
            DisplayHeader();
            DisplayGreeting();

        }
        #endregion

        #region Display the user's name (delegate)
        /// <summary>
        /// Method for displaying a user name
        /// </summary>
        /// <param name="userName">The name to pass in</param>
        public static void DisplayUserName(string userName)
        {
            Console.WriteLine("Try again " + userName);

        }
        #endregion

        #region Clear the console from prior text
        /// <summary>
        /// Method to remove the previous text
        /// </summary>
        public static void ClearConsole()
        {
            Console.Clear();

        }
        #endregion

        #region Change the console color
        /// <summary>
        /// Display Red console text
        /// </summary>
        public static void ChangeConsoleColor(string color)
        {
            if (color == "RED") // Display different colors for exceptions
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
        #endregion

        #region Reset the console Color
        /// <summary>
        /// Return the console color to normal
        /// </summary>
        public static void ConsoleReset()
        {
            Console.ResetColor();

        }
        #endregion

        #region Lambdas
        /// <summary>
        /// Lambda for display
        /// </summary>
        public static void DisplayHeader() => Console.WriteLine("**************EMPLOYEE SOLUTION**************");
        public static void DisplayGreeting() => Console.WriteLine("Welcome to the employee solution!\n");
        #endregion

    }
}
