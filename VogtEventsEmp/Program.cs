using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Speech.Synthesis;
using System.IO;

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
            InitialDisplayForProgram();

            // Ask username
            user = AskUsername();
            Speak(user);

            // Menu
            Menu(user);

            // Add employee and return to list
            employeeList = AddEmployeeToList(user);

            // Loop through Employee
            LoopThroughEmployeeList(employeeList);

            // Add Employee list to a file
            WriteToFile(employeeList);

        }

        /// <summary>
        /// Method for writing to a directory
        /// </summary>
        /// <param name="employeeList">A list to write to a directory</param>
        public static void WriteToFile(List<Employee<int>> employeeList)
        {

            StreamWriter File = new StreamWriter(@"C:\EmployeeLists\EmployeeList.txt");

            foreach (var employee in employeeList)
            {
                File.Write($"{employee.Name} {employee.Number} {employee.HireDate}");
            }

            File.Close();
        }

        #region DisplayOptionsForMenu
        /// <summary>
        /// Ask the user if they'd like to add another employee or to Exit
        /// </summary>
        public static void DisplayOptionsForMenu()
        {
            Console.WriteLine("1. Add an employee");
            Console.WriteLine("2. Exit");

        }
        #endregion

        #region LoopThroughEmployeeList
        public static void LoopThroughEmployeeList(List<Employee<int>> employeeList)
        {
            // For each snippet
            foreach (var employee in employeeList)
            {
                // Cw
                Console.WriteLine("---------");
                Console.WriteLine(employee?.Name);
                Console.WriteLine(employee?.Number);
                Console.WriteLine(employee?.HireDate);

            }
        }
        #endregion

        #region AddEmployeeToList
        /// <summary>
        /// Method for taking the user's name and returning a list
        /// </summary>
        /// <param name="user">User's name that's entering the data</param>
        /// <returns>An employee list</returns>
        public static List<Employee<int>> AddEmployeeToList(string user)
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
                emp = AddEmployee(user);
                employeeList.Add(emp);

                try // Try catch with a specific message and color changes
                {
                    choice = 0;

                    while (choice < 1 || choice > 2)
                    {
                        Console.WriteLine("Would you like to add another employee?");
                        DisplayOptionsForMenu();
                        choice = Convert.ToInt32(Console.ReadLine());

                    }
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

        #region AddEmployee
        /// <summary>
        /// Method for adding an employee
        /// </summary>
        /// <returns>A new employee object</returns>
        public static Employee<int> AddEmployee(string user)
        {
            // Custom message from the username delegate
            Username myUsername = delegate (string username) { Console.WriteLine("Please try again with adding an employee " + username); };

            // Initialize an employee object with a generic for int
            var employee = new Employee<int>();

            // Color for error throws 
            string color = default;

            try // Try catch for adding an employee's properties
            {
                Console.WriteLine("\nWhat is the employee's name?");
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

        #region Menu
        /// <summary>
        /// Method for displaying the menu to a user
        /// </summary>
        /// <param name="user">The user that needs the menu</param>
        /// <returns>The selected menu choie</returns>
        public static int Menu(string user)
        {
            // Standard message off the username delegate
            Username usernameDefault = new Username(DisplayUsername);

            // Variables
            int choice = default;
            string color = default;

            try // Try catch for the menu selection
            {
                while (choice < 1 || choice > 2)
                {
                    Console.WriteLine("What is your selection?");
                    DisplayOptionsForMenu();
                    choice = Convert.ToInt32(Console.ReadLine());
                }
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

        #region ShowGreetingToUser
        /// <summary>
        /// Display a greeting for the user
        /// </summary>
        /// <param name="userName"></param>
        public static void ShowGreetingToUser(string userName)
        {
            // Standard message for a greeting
            Console.WriteLine($"Greetings {userName}! Please select one of the following options below. \n");

        }
        #endregion

        #region AskUsername
        /// <summary>
        /// Ask for the user's name
        /// </summary>
        public static string AskUsername()
        {
            // Variables
            string userName = default;

            // Assign the user
            Console.WriteLine("What is the user's name?");
            userName = Console.ReadLine().ToUpper();
            Console.WriteLine("");

            // Display a standard greeting message
            Action<string> displayUserInfo = ShowGreetingToUser;
            displayUserInfo(userName);

            return userName;

        }
        #endregion

        #region Choice
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

        #region InitialDisplayForProgram
        /// <summary>
        /// Method to display when the program starts
        /// </summary>
        public static void InitialDisplayForProgram()
        {
            // Standard system messages
            DisplayHeader();
            DisplayGreeting();

        }
        #endregion

        #region DisplayUsername
        /// <summary>
        /// Method for displaying a user name
        /// </summary>
        /// <param name="userName">The name to pass in</param>
        public static void DisplayUsername(string userName)
        {
            Console.WriteLine("Try again " + userName);

        }
        #endregion

        #region ClearConsole
        /// <summary>
        /// Method to remove the previous text
        /// </summary>
        public static void ClearConsole()
        {
            Console.Clear();

        }
        #endregion

        #region ChangeConsoleColor
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

            ConsoleColorReset();

        }
        #endregion

        #region ConsoleColorReset
        /// <summary>
        /// Return the console color to normal
        /// </summary>
        public static void ConsoleColorReset()
        {
            Console.ResetColor();

        }
        #endregion

        #region Speak
        /// <summary>
        /// Have an announcer
        /// </summary>
        /// <param name="userName">Name of the user to pass in</param>
        public static void Speak(string userName)
        {
            SpeechSynthesizer speaker = new SpeechSynthesizer();
            speaker.Speak($"Greetings {userName}! Please select one of the following options below. \n");

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
