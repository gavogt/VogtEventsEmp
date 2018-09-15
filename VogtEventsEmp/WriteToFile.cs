using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace VogtEventsEmp
{
    class WriteToFile
    {
        #region WriteToPasswordFile
        /// <summary>
        /// Takes the admin object and saves the password to a file
        /// </summary>
        /// <param name="admin">The admin to pass in</param>
        /// <param name="encryptedPassword">Encrypted password</param>
        public static void WriteToPasswordFile(Admin admin, string hashedPassword) // Setup before SQL. No testing done
        {
            // Open new stream
            StreamWriter File = new StreamWriter(@"C:\WorkLists\PasswordList.txt");

            File.WriteLine($"Admin # {admin.Number} Password: {hashedPassword}");

            Console.WriteLine("Password has been updated");

            File.Close();

        }
        #endregion

        #region WriteSortedDictionaryToFile
        /// <summary>
        /// Method that takes a sorted dictionary and writes the PK and Values to it
        /// </summary>
        /// <param name="sortedDictionary">The sorted dictionary to pass in</param>
        public static void WriteSortedDictionaryToFile(SortedDictionary<int, Tuple<string, char>> sortedDictionary)
        {
            // Open new stream
            StreamWriter File = new StreamWriter(@"C:\WorkLists\SortedDictionaryList.txt");

            foreach (var personnel in sortedDictionary)
            {
                File.WriteLine($"PK: {personnel.Key} Value: {personnel.Value} ");

            }

            File.Close();

        }
        #endregion

        #region WriteAdminToFile
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
            File.WriteLine($"{admin.Name} {admin.Number} " + accessed.ToLocalTime());

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
                File.WriteLine($"{employee.Name} {employee.Number} {employee.HireDate.ToShortDateString()} ");
            }

            File.Close();

        }
        #endregion
    }
}
