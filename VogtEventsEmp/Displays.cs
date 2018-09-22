using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VogtEventsEmp
{
    class Displays
    {
        #region DisplayOptionsForMenu
        /// <summary>
        /// Ask the admin if they'd like to add another employee or to Exit
        /// </summary>
        public static void DisplayOptionsForMenu()
        {
            Console.WriteLine("1. Add an employee");
            Console.WriteLine("2. Exit");
            Console.WriteLine("3. Update an employee");
            Console.WriteLine("4. Remove an employee from the DB");

        }
        #endregion

        #region DisplaySortedDictionary
        /// <summary>
        /// Runs a loop through a sorted dictionary to display keys and value
        /// </summary>
        /// <param name="sortedPersonnel">A sorted dictionary to pass in</param>
        public static void DisplaySortedDictionary(SortedDictionary<int, Tuple<string, char, string>> sortedPersonnel)
        {
            Console.WriteLine("");

            foreach (var personnel in sortedPersonnel)
            {
                // Have to make unique
                Console.WriteLine("--------- Sorted Dictionary ---------");
                Console.WriteLine($"Primary key: {personnel.Key.ToString()} Personnel name: {personnel.Value} ");
            }

            Console.ReadKey();

        }
        #endregion

        #region DisplayInitialMenuMethodForProgram
        /// <summary>
        /// Method to display when the program starts
        /// </summary>
        public static int DisplayInitialMenuMethodForProgram()
        {
            int choice = default;

            // Standard system messages
            DisplayHeader();
            DisplayGreeting();
            choice = DisplayMenuForAdmin();

            return choice;

        }
        #endregion

        #region DisplayMenuForAdmin
        /// <summary>
        /// Display the initial choice for the admin
        /// </summary>
        /// <returns>The selection made</returns>
        public static int DisplayMenuForAdmin()
        {
            int choice = default;

            // Ask the admin if they'd like to login or sign up
            MenuOptionForAdminDisplay();
            choice = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(" ");

            return choice;

        }
        #endregion

        #region MenuOptionsForAdminDisplay
        /// <summary>
        /// Method that has the options inside of its own menu
        /// </summary>
        public static void MenuOptionForAdminDisplay()
        {
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Sign up\n");
            Console.Write("SELECTION: ");

        }
        #endregion

        #region DisplayPleaseTryAgain
        /// <summary>
        /// A method that displays a please try again message
        /// </summary>
        public static void DisplayPleaseTryAgain()
        {
            Console.Write("Please try again: ");

        }
        #endregion

        #region DisplayToAdminHeaderForSignUp
        /// <summary>
        /// Display the admin entry
        /// </summary>
        public static void DisplayToAdminHeaderForSignUp()
        {
            Console.WriteLine("ADMIN ENTRY ~~~~~~~");

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
