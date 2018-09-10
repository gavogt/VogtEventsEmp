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
            string adminName = default;
            string empName = default;
            var admin = new Admin();
            var employeeList = new List<Employee<DateTime>>();
            var emp = new Employee<DateTime>();

            // Displays
            InitialDisplayForProgram();

            // Ask Admin's name
            adminName = AskAdminName();
            admin = AddAdministrator(adminName);

            // Menu
            Menu(adminName);

            // Add employee and return to list
            employeeList = AddEmployeeToList(empName);

            // Loop through Employee
            LoopThroughEmployeeList(employeeList);

            // Add Employee list to a file
            WriteEmployeeListToFile(employeeList);
            WriteAdminToFile(admin, DateTime.Now);

        }

        #region WriteUserToFile
        /// <summary>
        /// Method for writing a user list to a file
        /// </summary>
        /// <param name="admin">The Admin to pass in and append to a file</param>
        /// <param name="accessed">DateTime now when accessed</param>
        public static void WriteAdminToFile(Admin admin, DateTime accessed)
        {
            // Open new stream
            StreamWriter File = new StreamWriter(@"C:\WorkLists\AdminList.txt");

            // Write user to file
            File.Write($"{admin.Name} {admin.Number} " + accessed.ToLocalTime());

            File.Close();

        }
        #endregion

        #region WriteEmployeeListToFile
        /// <summary>
        /// Method for writing to a directory
        /// </summary>
        /// <param name="employeeList">A list to write to a directory</param>
        public static void WriteEmployeeListToFile(List<Employee<DateTime>> employeeList)
        {
            // Open new stream
            StreamWriter File = new StreamWriter(@"C:\WorkLists\EmployeeList.txt");

            // Loop through employee list
            foreach (var employee in employeeList)
            {
                File.Write($"{employee.Name} {employee.Number} {employee.HireDate.ToShortDateString()}");
            }

            File.Close();

        }
        #endregion

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
        public static void LoopThroughEmployeeList(List<Employee<DateTime>> employeeList)
        {
            // For each snippet
            foreach (var employee in employeeList)
            {
                // Cw
                Console.WriteLine("---------");
                Console.WriteLine("Employee name: " + employee?.Name);
                Console.WriteLine("Employee number: " + employee?.Number);
                Console.WriteLine("Employee hired: " + employee?.HireDate.ToShortDateString());

            }
        }
        #endregion

        #region AddEmployeeToList
        /// <summary>
        /// Method for taking the user's name and returning a list
        /// </summary>
        /// <param name="employee">User's name that's entering the data</param>
        /// <returns>An employee list</returns>
        public static List<Employee<DateTime>> AddEmployeeToList(string employee)
        {
            // Variables
            var employeeList = new List<Employee<DateTime>>();
            var emp = new Employee<DateTime>();
            bool run = true;
            int choice = default;
            string color = default;

            while (run) // Loop to add employees
            {
                // Custom message off the username delegate
                Username myAdminName = delegate (string adminName) { Console.WriteLine("Please try again " + adminName); };

                // Assign the employee to a list
                emp = AddEmployee(employee);
                employeeList.Add(emp);

                try // Try catch with a specific message and color changes
                {
                    choice = 0;

                    while (choice < 1 || choice > 2)
                    {
                        ClearConsole();
                        Console.WriteLine("What would you like to do: ");
                        DisplayOptionsForMenu();
                        Console.Write("\nSELECTION: ");
                        choice = Convert.ToInt32(Console.ReadLine());

                    }
                }
                catch (FormatException)
                {
                    ClearConsole();
                    myAdminName(employee);
                    color = "RED";
                    ChangeConsoleColor(color);

                }
                catch
                {
                    ClearConsole();
                    myAdminName(employee);
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

        #region EnterValidInformation
        /// <summary>
        /// Provide errors if the input isn't correct
        /// </summary>
        /// <param name="empVariable">Emp variable to pass in</param>
        /// <param name="message">Message of error</param>
        public static void EnterValidInformation(int empVariable, string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Please enter valid information! " + empVariable + " is incorrect. " + message);
            ConsoleColorReset();

        }
        #endregion

        #region AddUser
        /// <summary>
        /// Method for adding a user
        /// </summary>
        /// <returns>A new user object</returns>
        public static Admin AddAdministrator(string adminName)
        {
            // Initialize an admin object
            var admin = new Admin();

            // Assign passed in string to admin name
            admin.Name = adminName;

            // Color for error throws 
            string color = default;

            try // Try catch for adding an administrators's properties
            {
                Console.Write("What is your number: ");
                while (admin.Number < 100 || admin.Number > 200)
                {
                    admin.Number = Convert.ToInt32(Console.ReadLine());

                    if (admin.Number < 100 || admin.Number > 200)
                    {
                        EnterValidInformation(admin.Number, "The admin number must be between 100 and 200");

                        Console.Write("What is your number: ");
                        admin.Number = Convert.ToInt32(Console.ReadLine());
                    }
                }
            }
            catch (FormatException)
            {
                ClearConsole();
                color = "RED";
                ChangeConsoleColor(color);

            }
            catch
            {
                ClearConsole();
                color = "BLUE";
                ChangeConsoleColor(color);

            }

            ClearConsole();

            admin.DisplayAdminInfo(admin.Name, admin.Number);

            return admin;

        }
        #endregion

        #region AddEmployee
        /// <summary>
        /// Method for adding an employee
        /// </summary>
        /// <returns>A new employee object</returns>
        public static Employee<DateTime> AddEmployee(string employeeVar)
        {
            // Custom message from the username delegate
            Username myUsername = delegate (string username) { Console.WriteLine("Please try again with adding an employee " + username); };

            // Initialize an employee object with a generic for int
            var employee = new Employee<DateTime>();

            // Color for error throws 
            string color = default;

            try // Try catch for adding an employee's properties
            {
                var year = -1;
                var month = -1;
                var day = -1;

                ClearConsole();
                Console.Write("\nWhat is the employee's name: ");
                employee.Name = Console.ReadLine().ToUpper();

                Console.Write("What is the employee's number: ");
                while (employee.Number < 1000 || employee.Number > 2000)
                {
                    employee.Number = Convert.ToInt32(Console.ReadLine());

                    if (employee.Number < 1000 || employee.Number > 2000)
                    {
                        EnterValidInformation(employee.Number, "The number must be between 1000 and 2000");

                        Console.Write("What is the employee's number: ");
                        employee.Number = Convert.ToInt32(Console.ReadLine());
                    }
                }
                while (year < 1935 || year > 2018)
                {
                    Console.Write("What year was the employee hired: ");
                    year = Convert.ToInt32(Console.ReadLine());

                    if (year < 1935 || year > 2018)
                    {
                        EnterValidInformation(year, "Year must be after 1935 and before 2018!");

                        Console.Write("What year was the employee hired: ");
                        year = Convert.ToInt32(Console.ReadLine());
                    }
                }
                while (month < 1 || month > 12)
                {
                    Console.Write("What month was the employee hired: ");
                    month = Convert.ToInt32(Console.ReadLine());

                    if (month < 1 || month > 12)
                    {
                        EnterValidInformation(month, "Month must be between 1 and 12!");

                        Console.Write("What month was the employee hired: ");
                        month = Convert.ToInt32(Console.ReadLine());

                    }
                }
                while (day < 1 || day > 31)
                {
                    Console.Write("What day was the employee hired: ");
                    day = Convert.ToInt32(Console.ReadLine());

                    if (day < 1 || day > 12)
                    {
                        EnterValidInformation(month, "Day must be between 1 and 31!");

                        Console.Write("What day was the employee hired: ");
                        day = Convert.ToInt32(Console.ReadLine());

                    }
                }

                employee.HireDate = new DateTime(year, month, day);

            }
            catch (FormatException)
            {
                ClearConsole();
                color = "RED";
                ChangeConsoleColor(color);
                myUsername(employeeVar);

            }
            catch
            {
                ClearConsole();
                color = "BLUE";
                ChangeConsoleColor(color);
                myUsername(employeeVar);

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
                    Console.WriteLine("What is your selection: ");
                    DisplayOptionsForMenu();
                    Console.Write("\nSELECTION: ");
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
        /// Display a greeting for the admin
        /// </summary>
        /// <param name="adminName"></param>
        public static void ShowGreetingToUser(string adminName)
        {
            ClearConsole();

            // Standard message for a greeting
            Console.WriteLine($"Greetings {adminName}! Please enter your admin number, and then select one of the proceeding options. \n");
            Speak(adminName);

        }
        #endregion

        #region AskAdminName
        /// <summary>
        /// Ask for the admin's name
        /// </summary>
        public static string AskAdminName()
        {
            // Variables
            string adminName = default;

            // Assign the user
            Console.WriteLine("What your name?");
            adminName = Console.ReadLine().ToUpper();
            Console.WriteLine("");

            // Display a standard greeting message
            Action<string> displayAdminInfo = ShowGreetingToUser;
            displayAdminInfo(adminName);

            return adminName;

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
        /// <param name="adminName">The name to pass in</param>
        public static void DisplayUsername(string adminName)
        {
            Console.WriteLine("Try again " + adminName);

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
        /// <param name="adminName">Name of the user to pass in</param>
        public static void Speak(string admin)
        {
            SpeechSynthesizer speaker = new SpeechSynthesizer();
            speaker.Speak($"Greetings {admin}! Please enter your admin number, and then select one of the proceeding options. \n");

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
