using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Speech.Synthesis;
using System.IO;
using System.Threading;
using System.Security.Cryptography;
using System.Data.SqlClient;

namespace VogtEventsEmp
{
    class Program
    {
        // Delegate for the username
        public delegate void Username(string name);

        static void Main(string[] args)
        {
            // Variables
            Guest guest = new Guest();
            int choice = default;

            // Displays
            choice = Displays.InitialDisplayForProgram();

            if (choice == 1)
            {
                // Variables for brute force and running while true
                int bruteForce = 0;
                bool run = true;
                DateTime date = new DateTime();

                while (run == true)
                {
                    // Check the guest's credentials
                    guest = PasswordLogin();

                    // Run a query with the information
                    run = SQLWork.SQLPasswordMatch(guest);

                    // Count login attempts
                    bruteForce += 1;

                    if (bruteForce >= 3)
                    {
                        // Write the bruteforce attempts to a file
                        WriteToFile.WriteBruteForceAttemptsToFile(guest, date);

                        // Display the brute force message
                        BruteForceMessage();

                    }
                }
            }
            if (choice == 2)
            {
                AddInformation();
            }
        }

        public static void AddInformation()
        {
            // Variables  
            string adminName = default;
            string empName = default;
            byte[] encryptedPassword = default;
            var admin = new Admin();
            var adminList = new List<Admin>();
            var emp = new Employee<DateTime>();
            var employeeList = new List<Employee<DateTime>>();
            var guest = new Guest();
            var hashedPassword = String.Empty;
            var password = String.Empty;
            var sortedDictionary = new SortedDictionary<int, Tuple<string, char, string>>();

            // Ask Admin's name
            adminName = AskAdminName();

            // Checking how I would save a byte to SQL ?
            encryptedPassword = AskPassword();

            // AES to SHA512
            hashedPassword = SHA512Crypto.Hash(encryptedPassword);

            admin = AddAdministrator(adminName, hashedPassword);

            // Add admin to a list
            adminList.Add(admin);

            // Menu
            Menu(adminName);

            // Add employee and return to list
            employeeList = AddEmployeeToList(empName);

            // Loop through Employee
            LoopThroughEmployeeList(employeeList);

            // Add Employee list to a file
            WriteToFile.WriteEmployeeListToFile(employeeList);
            WriteToFile.WriteAdminToFile(admin, DateTime.Now);

            // Assign a sorted dictionary from emp list and adminlist
            sortedDictionary = AllPersonnel(employeeList, adminList);

            // Display the sorted dictionary on console
            Displays.DisplaySortedDictionary(sortedDictionary);

            // Write the sorted dictionary to a file
            WriteToFile.WriteSortedDictionaryToFile(sortedDictionary);

            // Write password to File
            WriteToFile.WriteToPasswordFile(admin, hashedPassword);

            // DB Insert
            SQLWork.SQLInsert(sortedDictionary);

        }

        #region PasswordsMatch 
        /// <summary>
        /// A method lets the user know if his or her password matches the DB
        /// </summary>
        /// <param name="match">a hashed string to pass in</param>
        /// <returns>True or false</returns>
        public static bool PasswordsMatch(string match)
        {
            bool doTheyMatch = default;

            return doTheyMatch;

        }
        #endregion

        #region PasswordLogin
        /// <summary>
        /// Password portion for the AskPassword method
        /// </summary>
        /// <returns>A temporary guest class</returns>
        public static Guest PasswordLogin()
        {
            // Declare and initalize a bool for tp 
            bool run = false;

            // New guest object
            Guest guest = new Guest();

            // Ask for admin number
            Console.Write("Please enter your admin number: ");
            run = int.TryParse(Console.ReadLine(), out int number) && number >= 100 && number <= 200;

            while (!run)
            {
                // Provide a custom message if the number isn't correct
                EnterValidInformation(number, "The admin number must be between 100 and 200");
                Console.Write("Please try again: ");
                run = int.TryParse(Console.ReadLine(), out number) && number >= 100 && number <= 200;

            }

            // Ask for admin password
            Console.Write("Please enter your password: ");
            guest.Password = Console.ReadLine();

            // Return guest object
            return guest;

        }
        #endregion

        #region Password
        /// <summary>
        /// Password portion for the AskPassword method
        /// </summary>
        /// <returns>A string the user entered in</returns>
        public static string Password()
        {
            string password = String.Empty;

            Console.Write("Please enter your password: ");
            password = Console.ReadLine();

            return password;

        }
        #endregion

        #region AskPassword
        /// <summary>
        /// Ask the admin what his or her password is
        /// </summary>
        /// <param name="password">A string for password to pass in</param>
        /// <returns>The password to be encrypted</returns>
        public static byte[] AskPassword()
        {
            // Variables
            byte[] encryptedPassword = default;
            SHA256 strongSHA = default;
            string password = String.Empty;

            // New SHA256 object
            strongSHA = SHA256.Create();

            // CW for the password
            password = Password();

            // AES
            Aes aes = Aes.Create();

            // Encrypt
            encryptedPassword = AESC.EncryptByAES(password, aes.Key, aes.IV);

            return encryptedPassword;

        }
        #endregion

        #region AllPersonnel
        /// <summary>
        /// Method that takes an employee list of date time, and a list of admin in order to add them to a list
        /// </summary>
        /// <param name="empList">An list of type employee datetime to pass in</param>
        /// <param name="adminList">An a list of type admin</param>
        /// <returns></returns>
        public static SortedDictionary<int, Tuple<string, char, string>> AllPersonnel(List<Employee<DateTime>> empList, List<Admin> adminList)
        {
            // A new sorted dictionary
            SortedDictionary<int, Tuple<string, char, string>> mySortedDictionary = new SortedDictionary<int, Tuple<string, char, string>>();

            // Loop through emp list
            foreach (var personnel in empList)
            {
                mySortedDictionary.Add(personnel.Number, Tuple.Create(personnel.Name, 'E', "No Pass"));

            }

            // Loop through admin list
            foreach (var personnel in adminList)
            {
                mySortedDictionary.Add(personnel.Number, Tuple.Create(personnel.Name, 'A', personnel.Password));

            }

            return mySortedDictionary;

        }
        #endregion

        #region LoopThroughEmployeeList
        public static void LoopThroughEmployeeList(List<Employee<DateTime>> employeeList)
        {
            // For each snippet
            foreach (var employee in employeeList)
            {
                // Cw
                Console.WriteLine("--------- Employee List ---------");
                Console.WriteLine("Employee name: " + employee?.Name);
                Console.WriteLine("Employee number: " + employee?.Number);
                Console.WriteLine("Employee hired: " + employee?.HireDate.ToShortDateString());

            }
        }
        #endregion

        #region AddEmployeeToList
        /// <summary>
        /// Method for taking the employee's name and returning a list
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
                        Displays.DisplayOptionsForMenu();
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
        /// <param name="empVariable">Employee input variable to pass in</param>
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
        /// <returns>A new admin object</returns>
        public static Admin AddAdministrator(string adminName, string hashedPassword)
        {
            // Initialize an admin object
            var admin = new Admin();

            // Assign passed in string to admin name
            admin.Name = adminName;

            // Assigned passed in hash to admin's password property
            admin.Password = hashedPassword;

            // Color for error throws 
            string color = default;

            try // Try catch for adding an administrator's properties
            {

                admin.Number = AdminNumber();

                try
                {
                    SQLWork.SqlInsertAdmin(admin);

                }
                catch (SqlException sqlE)
                {
                    // Display custom message for taken PK
                    if (sqlE.Number == 2627)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("\nAdmin number is already taken: " + sqlE.Message + ". Please hit any key and try again. \n");
                        Console.ResetColor();
                        Console.ReadKey();
                        Console.Clear();
                        AddAdministrator(adminName, hashedPassword);

                    }
                    else
                    {
                        Console.WriteLine("Another error occured!");
                    }
                }
            }
            catch (FormatException e)
            {
                ClearConsole();
                Console.WriteLine(e.ToString());
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
                int year;
                int month;
                int day;

                ClearConsole();
                Console.Write("\nWhat is the employee's name: ");
                employee.Name = Console.ReadLine().ToUpper();

                employee.Number = EmployeeNumber();
                year = EmployeeYearHired();
                month = EmployeeMonthHired();
                day = EmployeeDayHired();

                employee.HireDate = new DateTime(year, month, day);

                // Display emp information off an event
                employee.DisplayEmployeeInfo(employee.Name, employee.Number, employee.HireDate);

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

            return employee;

        }
        #endregion

        #region AdminNumber
        /// <summary>
        /// Method that assigns the number to an admin property
        /// </summary>
        /// <returns>The number that the admin has</returns>
        public static int AdminNumber()
        {
            bool run = false;

            Console.Write("What is your Admin number: ");
            run = int.TryParse(Console.ReadLine(), out int number) && number >= 100 && number <= 200;

            while (!run)
            {
                // Provide a custom message if the number isn't correct
                EnterValidInformation(number, "The admin number must be between 100 and 200");
                Console.Write("Please try again: ");
                run = int.TryParse(Console.ReadLine(), out number) && number >= 100 && number <= 200;

            }

            return number;

        }
        #endregion

        #region EmployeDayHired
        /// <summary>
        /// Method that assigns what day an employee was hired
        /// </summary>
        /// <returns>The day the employee was hired</returns>
        public static int EmployeeDayHired()
        {
            bool run = false;

            Console.Write("What day was the employee hired: ");
            run = int.TryParse(Console.ReadLine(), out int day) && day >= 1 && day <= 31;

            while (!run)
            {
                // Provide a custom message if the day isn't correct
                EnterValidInformation(day, "Day must be between 1 and 31!");
                Console.Write("Please try agan: ");
                run = int.TryParse(Console.ReadLine(), out day) && day >= 1 && day <= 31;

            }

            return day;

        }
        #endregion

        #region EmployeeMonthHired
        /// <summary>
        /// Method that assigns what month an employee was hired
        /// </summary>
        /// <returns>Returns what month an employee was hired</returns>
        public static int EmployeeMonthHired()
        {
            bool run = false;

            Console.Write("What month was the employee hired: ");
            run = int.TryParse(Console.ReadLine(), out int month) && month >= 1 && month <= 12;

            while (!run)
            {
                // Provide a custom message if the month isn't correct
                EnterValidInformation(month, "Month must be between 1 and 12!");
                Console.Write("Please try agan: ");
                run = int.TryParse(Console.ReadLine(), out month) && month >= 1 && month <= 12;

            }

            return month;

        }
        #endregion

        #region EmployeeYearHired
        /// <summary>
        /// A method that assigns what year an employee was hired
        /// </summary>
        /// <returns>Returns what year an employee was hired</returns>
        public static int EmployeeYearHired()
        {
            bool run = false;

            Console.Write("What year was the employee hired: ");
            run = int.TryParse(Console.ReadLine(), out int year) && year >= 1935 && year <= 2018;

            while (!run)
            {
                // Provide a custom message if the year isn't correct
                EnterValidInformation(year, "Year must be after 1935 and before 2018!");
                Console.Write("Please try agan: ");
                run = int.TryParse(Console.ReadLine(), out year) && year >= 1935 && year <= 2018;

            }

            return year;

        }
        #endregion

        #region EmployeeNumber
        /// <summary>
        /// Method that returns the number for an employee object
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>Returns what number the employee has</returns>
        public static int EmployeeNumber()
        {
            bool run = false;

            Console.Write("What is the employee's number: ");
            run = int.TryParse(Console.ReadLine(), out int number) && number >= 1000 && number <= 2000;

            while (!run)
            {
                // Provide a custom message if the number isn't correct
                EnterValidInformation(number, "The number must be between 1000 and 2000");
                Console.Write("Please try again: ");
                run = int.TryParse(Console.ReadLine(), out number) && number >= 1000 && number <= 2000;

            }

            return number;

        }
        #endregion

        #region Menu
        /// <summary>
        /// Method for displaying the menu to a user
        /// </summary>
        /// <param name="admin">The admin that needs the menu</param>
        /// <returns>The selected menu choie</returns>
        public static int Menu(string admin)
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
                    // Display menu with selection
                    Console.WriteLine("What is your selection: ");
                    Displays.DisplayOptionsForMenu();
                    Console.Write("\nSELECTION: ");
                    choice = Convert.ToInt32(Console.ReadLine());
                }
                // Send choice to a switch satement
                Choice(choice);

            }
            catch
            {
                // Display an error the username
                ClearConsole();
                usernameDefault(admin);
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
            // Clear Console
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
            Console.Write("Please enter your name: ");
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

        #region DisplayUsername
        /// <summary>
        /// Method for displaying the admin's name in an error message
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

        #region BruteForceMessage
        /// <summary>
        /// A method for when a brute force is detected
        /// </summary>
        public static void BruteForceMessage()
        {
            // Clear the console
            ClearConsole();

            // Display a red warning messae
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("\nBrute force detected! Exiting...\n");
            SpeakBruteForce();

            // Reset the color
            Console.ResetColor();

            // Exit the system
            Environment.Exit(0);

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
        /// Have an announcer for the admin
        /// </summary>
        /// <param name="adminName">Name of the admin to pass in</param>
        public static void Speak(string admin)
        {
            SpeechSynthesizer speaker = new SpeechSynthesizer();
            speaker.Speak($"Greetings {admin}! Please enter your admin number, and then select one of the proceeding options. \n");

        }
        #endregion

        #region SpeakBruteForce
        /// <summary>
        /// Have an announcer for bruteforce error
        /// </summary>
        public static void SpeakBruteForce()
        {
            // declare and initialize a new speaker
            SpeechSynthesizer speaker = new SpeechSynthesizer();

            // Have a lady announce brute force detected
            speaker.SelectVoiceByHints(VoiceGender.Female);

            // Speak
            speaker.Speak("Potential brute force detected...");

        }
        #endregion

    }
}
