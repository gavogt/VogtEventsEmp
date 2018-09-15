using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace VogtEventsEmp
{
    class SQLWork
    {
        #region
        /// <summary>
        /// A method that queries the DB to see if the admin can log in successfully
        /// </summary>
        /// <returns>A bool of wether or not the password was correct</returns>
        public static bool SQLPasswordMatch()
        {
            // True or false if the passwords matched
            bool passwordsMatch = default;

            return passwordsMatch;

        }
        #endregion

        #region SQLConnect
        /// <summary>
        /// String builder to connect to the DB
        /// </summary>
        /// <returns>A string with the local DB credentials</returns>
        public static SqlConnectionStringBuilder SQLConnect()
        {
            // Build a string for connection details
            SqlConnectionStringBuilder sqlString = new SqlConnectionStringBuilder();
            sqlString.DataSource = "DESKTOP-DP8TQ6A\\VOGTSS";
            sqlString.InitialCatalog = "employee_db";
            sqlString.IntegratedSecurity = true;

            return sqlString;

        }
        #endregion

        #region SQLINSERT
        /// <summary>
        /// A method that takes all personnel and inserts it into a DB
        /// </summary>
        /// <param name="sortedPersonnel">All combined workers in a sorted dictionary</param>
        public static void SQLInsert(SortedDictionary<int, Tuple<string, char, string>> sortedPersonnel)
        {
            SqlConnectionStringBuilder sqlString;
            // New DateTime should be swapped with added information
            DateTime date = DateTime.Now;

            sqlString = SQLConnect();

            // Declare and initialize a new connection
            SqlConnection sqlConn = new SqlConnection(sqlString.ToString());

            try
            {
                sqlConn.Open();

                string sqlInsert = "INSERT INTO dbo.Employee_Table(emp_number, emp_name, date_added, emp_type, emp_password) VALUES(@emp_number, @emp_name, @date_added, @emp_type, @emp_password)";

                foreach (var personnel in sortedPersonnel)
                {
                    // Prepare the insert statement
                    SqlCommand sqlCmd = new SqlCommand(sqlInsert, sqlConn);

                    sqlCmd.Parameters.AddWithValue("@emp_number", personnel.Key.ToString());
                    sqlCmd.Parameters.AddWithValue("@emp_name", personnel.Value.Item1);
                    sqlCmd.Parameters.AddWithValue("@date_added", date.ToShortDateString());
                    sqlCmd.Parameters.AddWithValue("@emp_type", personnel.Value.Item2);
                    sqlCmd.Parameters.AddWithValue("@emp_password", personnel.Value.Item3);

                    sqlCmd.ExecuteNonQuery();

                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error! Contact your admin!");

            }
            finally
            {
                sqlConn.Close();

            }

        }
        #endregion
    }
}
