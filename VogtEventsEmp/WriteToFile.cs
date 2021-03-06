﻿using System;
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
        public static void WriteToPasswordFile(Admin admin, string hashedPassword)
        {
            // Open new stream
            StreamWriter st = File.AppendText(@"C:\WorkLists\PasswordList.txt");

            // Write the admin's number with a secure hash
            st.WriteLine($"Admin # {admin.Number} Password: {hashedPassword}");

            // Display that the password list has been updated
            Console.WriteLine("\nPassword has been written to the DB!");

            // Close stream
            st.Close();

        }
        #endregion

        #region WriteSortedDictionaryToFile
        /// <summary>
        /// Method that takes a sorted dictionary and writes the PK and Values to it
        /// </summary>
        /// <param name="sortedDictionary">The sorted dictionary to pass in</param>
        public static void WriteSortedDictionaryToFile(SortedDictionary<int, Tuple<string, char, string>> sortedDictionary)
        {
            // Open new stream
            StreamWriter st = File.AppendText(@"C:\WorkLists\SortedDictionaryList.txt");

            // Loop trough personnel for key and value pairs
            foreach (var personnel in sortedDictionary)
            {
                st.WriteLine($"PK: {personnel.Key} Value: {personnel.Value} ");

            }

            // Close stream
            st.Close();

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
            StreamWriter st = File.AppendText(@"C:\WorkLists\AdminList.txt");

            // Write user to file
            st.WriteLine($"{admin.Name} {admin.Number} " + accessed.ToLocalTime());

            // Close stream
            st.Close();

        }
        #endregion

        #region WriteBruteForceAttemptsToFile
        /// <summary>
        /// Method for writing attempted bruteforce list to a file as a simple dectection system
        /// </summary>
        /// <param name="guest">Guest that gets passed in</param>
        /// <param name="occured">DateTime when attempted bruteforce happened</param>
        public static void WriteBruteForceAttemptsToFile(Guest guest, DateTime occured)
        {
            occured = DateTime.Now;

            StreamWriter st = File.AppendText(@"C:\WorkLists\BruteForceList.txt");

            // Write brute force to file
            st.WriteLine($"\nGuest number: {guest.Number} at {occured.ToLocalTime()}");

            // Close stream
            st.Close();

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
            StreamWriter st = File.AppendText(@"C:\WorkLists\EmployeeList.txt");

            // Loop through employee list
            foreach (var employee in employeeList)
            {
                st.WriteLine($"{employee.Name} {employee.Number} {employee.HireDate.ToShortDateString()} ");
            }

            // Close stream
            st.Close();

        }
        #endregion

    }
}
