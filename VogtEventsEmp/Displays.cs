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

        #region InitialDisplayForProgram
        /// <summary>
        /// Method to display when the program starts
        /// </summary>
        public static int InitialDisplayForProgram()
        {
            int choice = default;

            // Standard system messages
            DisplayHeader();
            DisplayGreeting();
            choice = DisplayForAdmin();

            return choice;

        }
        #endregion

        #region DisplayForAdmin
        /// <summary>
        /// Display the initial choice for the admin
        /// </summary>
        /// <returns>The selection made</returns>
        public static int DisplayForAdmin()
        {
            int choice = default;

            // Ask the admin if they'd like to login or sign up
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Sign up\n");
            Console.Write("SELECTION: ");
            choice = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine(" ");

            return choice;

        }
        #endregion

        #region AdmnDisplayForSignUp
        /// <summary>
        /// Display the admin entry
        /// </summary>
        public static void AdminDisplayForSignUp()
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
