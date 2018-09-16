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
        public static bool SQLPasswordMatch(Guest guest)
        {
            // True or false if the passwords matched
            bool run = true;

            // SQLStringConnect variable
            SqlConnectionStringBuilder sqlString = SQLString();

            // SQLConnect variable
            SqlConnection sqlConn = new SqlConnection();

            // Assign the sqlconnection to the sqlconnection method
            sqlConn = SqlConn(sqlString);

            try
            {
                // Open a new connection
                sqlConn.Open();

                // Select from the DB
                string sqlInsert = "SELECT emp_number FROM dbo.Employee_Table WHERE emp_number = @emp_number AND emp_password = @emp_password";

                try
                {
                    // Prepare a select statement
                    SqlCommand sqlCmd = new SqlCommand(sqlInsert, sqlConn);

                    // Admin properties to query
                    sqlCmd.Parameters.AddWithValue("@emp_number", guest.Number);
                    sqlCmd.Parameters.AddWithValue("@emp_password", guest.Password);

                    sqlCmd.ExecuteNonQuery();

                    // If they exist display
                    Console.WriteLine("Login information was correct! Proceeding...");

                }
                catch (Exception)
                {
                    // Error with the credentials
                    Console.WriteLine("Your login information is incorrect!");

                    // Passwords don't match
                    run = true;

                }
                finally
                {
                    // Close the connection
                    sqlConn.Close();


                }
            }
            catch
            {
                // SELECT statement is off
                Console.WriteLine("Error with the query!");

            }
            finally
            {
                // Might be redunant
                sqlConn.Close();

            }

            return run;

        }
        #endregion

        #region SQLString
        /// <summary>
        /// String builder to connect to the DB
        /// </summary>
        /// <returns>A string with the local DB credentials</returns>
        public static SqlConnectionStringBuilder SQLString()
        {
            // Build a string for connection details
            SqlConnectionStringBuilder sqlString = new SqlConnectionStringBuilder();
            sqlString.DataSource = "DESKTOP-DP8TQ6A\\VOGTSS";
            sqlString.InitialCatalog = "employee_db";
            sqlString.IntegratedSecurity = true;

            return sqlString;

        }
        #endregion

        /// <summary>
        /// A method that makes a new sql connection based on a passed in sql string object
        /// </summary>
        /// <param name="sqlString">Connection string builder to pass n</param>
        /// <returns>The SQL connection</returns>
        public static SqlConnection SqlConn(SqlConnectionStringBuilder sqlString)
        {
            // Declare and initialize a new connection
            SqlConnection sqlConn = new SqlConnection(sqlString.ToString());

            return sqlConn;

        }

        #region SQLINSERT
        /// <summary>
        /// A method that takes all personnel and inserts it into a DB
        /// </summary>
        /// <param name="sortedPersonnel">All combined workers in a sorted dictionary</param>
        public static void SQLInsert(SortedDictionary<int, Tuple<string, char, string>> sortedPersonnel)
        {
            // Variable for a new database connection string
            SqlConnectionStringBuilder sqlString;

            // Declare and initialize the sql connection
            SqlConnection sqlConn = new SqlConnection();

            // New DateTime should be swapped with added information
            DateTime date = DateTime.Now;

            // Assign the string builder to the string building method
            sqlString = SQLString();

            // Assign the sqlconnection to the sqlconnection method
            sqlConn = SqlConn(sqlString);

            try
            {
                // Open th connection
                sqlConn.Open();

                // Insert into DB
                string sqlInsert = "INSERT INTO dbo.Employee_Table(emp_number, emp_name, date_added, emp_type, emp_password) VALUES(@emp_number, @emp_name, @date_added, @emp_type, @emp_password)";

                foreach (var personnel in sortedPersonnel)
                {
                    // Prepare the insert statement
                    SqlCommand sqlCmd = new SqlCommand(sqlInsert, sqlConn);

                    // Add variables
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
                // Error inserting into the DB
                Console.WriteLine("Error! Contact your DB admin!");

            }
            finally
            {
                sqlConn.Close();

            }

        }
        #endregion

    }
}
