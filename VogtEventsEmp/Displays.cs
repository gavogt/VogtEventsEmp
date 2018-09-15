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
        public static void DisplaySortedDictionary(SortedDictionary<int, Tuple<string, char>> sortedPersonnel)
        {
            foreach (var personnel in sortedPersonnel)
            {
                // Have to make unique
                Console.WriteLine("--------- Sorted Dictionary ---------");
                Console.WriteLine($"Primary key: {personnel.Key.ToString()} Personnel name: {personnel.Value} ");
            }

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

        #region Lambdas
        /// <summary>
        /// Lambda for display
        /// </summary>
        public static void DisplayHeader() => Console.WriteLine("**************EMPLOYEE SOLUTION**************");
        public static void DisplayGreeting() => Console.WriteLine("Welcome to the employee solution!\n");
        #endregion

    }
}
