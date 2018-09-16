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
        public static bool SQLPasswordMatch(Admin admin)
        {
            // True or false if the passwords matched
            bool passwordsMatch = default;

            // New DateTime should be swapped with added information
            DateTime date = DateTime.Now;

            // SQLStringConnect variable
            SqlConnectionStringBuilder sqlString = SQLString();

            // SQLConnect variable
            SqlConnection sqlConn = new SqlConnection();

            // Assign the sqlconnection to the sqlconnection method
            sqlConn = SqlConn(sqlString);

            try
            {
                sqlConn.Open();

                string sqlInsert = $"SELECT emp_number FROM dbo.Employee_Table WHERE emp_number = @emp_number AND password = @emp_password";

                try
                {
                    SqlCommand sqlCmd = new SqlCommand(sqlInsert, sqlConn);
                    sqlCmd.Parameters.AddWithValue("@emp_number", admin.Number);
                    sqlCmd.Parameters.AddWithValue("@emp_password", admin.Password);

                    sqlCmd.ExecuteNonQuery();
                }
                catch
                {
                    Console.WriteLine("Your login information is incorrect!");
                }
                finally
                {
                    sqlConn.Close();
                }
                /*
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
                */
            }
            catch (Exception)
            {
                Console.WriteLine("Error! Contact your DB admin!");

            }
            finally
            {
                sqlConn.Close();

            }

            return passwordsMatch;

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
