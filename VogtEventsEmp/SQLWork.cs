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
            // True or false to continue running
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

                    // Prepare to read the DB with the SQLcmd
                    var sqlCheck = sqlCmd.ExecuteScalar();

                    if (sqlCheck != null)
                    {
                        // Display showing that the credentials are correct
                        Console.WriteLine("Login information was correct! Proceeding...");

                        // Stop running and proceed
                        run = false;

                    }
                    else
                    {
                        // Display an error that the guests credentials don't match in the DB
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("\nIncorrect!\n");
                        Console.ResetColor();

                        // Continue running
                        run = true;

                    }
                }
                catch (Exception e)
                {
                    // Display an exception message
                    Console.Clear();

                    // Error with the credentials
                    Console.WriteLine(e.ToString());

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

            // Have a counter for the number of users added
            int count = 0;

            try
            {
                // Open th connection
                sqlConn.Open();

                // Insert into DB
                string sqlInsert = "INSERT INTO dbo.Employee_Table(emp_number, emp_name, date_added, emp_type, emp_password) VALUES(@emp_number, @emp_name, @date_added, @emp_type, @emp_password)";

                foreach (var personnel in sortedPersonnel)
                {
                    if (personnel.Value.Item2 == 'E')
                    {
                        // Prepare the insert statement
                        SqlCommand sqlCmd = new SqlCommand(sqlInsert, sqlConn);

                        // Insert personnel properties
                        sqlCmd.Parameters.AddWithValue("@emp_number", personnel.Key.ToString());
                        sqlCmd.Parameters.AddWithValue("@emp_name", personnel.Value.Item1);
                        sqlCmd.Parameters.AddWithValue("@date_added", date.ToShortDateString());
                        sqlCmd.Parameters.AddWithValue("@emp_type", personnel.Value.Item2);
                        sqlCmd.Parameters.AddWithValue("@emp_password", personnel.Value.Item3);

                        sqlCmd.ExecuteNonQuery();

                        count += 1;

                    }
                }

                // Notify the admin how many employees were added
                Console.WriteLine("\n" + count + " employees were added to the DB!\n");
            }
            catch (SqlException sqlE)
            {
                // Display custom message for taken PK
                if (sqlE.Number == 2627)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("\nEmployee number is already taken: " + sqlE.Message + "\n");
                    Console.ResetColor();

                }
            }
            catch (Exception e)
            {
                // General exception
                Console.WriteLine(e.ToString());

            }
            finally
            {
                // Close DB
                sqlConn.Close();

            }

        }
        #endregion

        #region SQLINSERT
        /// <summary>
        /// Method that inserts an admin object into a seperate table
        /// </summary>
        /// <param name="admin">The admin object to pass in</param>
        public static void SqlInsertAdmin(Admin admin)
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
                string sqlInsert = "INSERT INTO dbo.Admin_Table(admin_number, admin_name, date_added, emp_type, admin_password) VALUES(@admin_number, @admin_name, @date_added, @emp_type, @admin_password)";

                // Prepare the insert statement
                SqlCommand sqlCmd = new SqlCommand(sqlInsert, sqlConn);

                // Insert personnel properties
                sqlCmd.Parameters.AddWithValue("@admin_number", admin.Number);
                sqlCmd.Parameters.AddWithValue("@admin_name", admin.Name);
                sqlCmd.Parameters.AddWithValue("@date_added", date.ToShortDateString());
                sqlCmd.Parameters.AddWithValue("@emp_type", 'A');
                sqlCmd.Parameters.AddWithValue("@admin_password", admin.Password);

                sqlCmd.ExecuteNonQuery();

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
                    Program.AddInformation();

                }
                else
                {
                    Console.WriteLine("Another error occured!");
                }
            }
            catch (Exception e)
            {
                // General exception
                Console.WriteLine(e.ToString());

            }
            finally
            {
                // Close DB
                sqlConn.Close();

            }

        }
        #endregion

        /*
        #region
        /// <summary>
        /// A method that queries the DB to see all employees in the DB
        /// </summary>
        /// <returns>All employees in the db</returns>
        public static bool SQLSelectAllEmployees(List<Employee<DateTime>> employeeList)
        {
            // True or false to continue running
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
                string sqlInsert = "SELECT emp_number, emp_name FROM dbo.Employee_Table";

                try
                {
                    foreach (var employees in employeeList)
                    {
                        // Prepare a select statement
                        SqlCommand sqlCmd = new SqlCommand(sqlInsert, sqlConn);

                        // Admin properties to query
                        sqlCmd.Parameters.AddWithValue("@emp_number", employees.Number);
                        sqlCmd.Parameters.AddWithValue("@emp_password", employees.Name);

                        // Prepare to read the DB with the SQLcmd
                        var sqlCheck = sqlCmd.ExecuteScalar();

                        if (sqlCheck != null)
                        {
                            // Display showing that the credentials are correct
                            Console.WriteLine("Login information was correct! Proceeding...");

                            // Stop running and proceed
                            run = false;

                        }
                        else
                        {
                            // Display an error that the guests credentials don't match in the DB
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("\nIncorrect!\n");
                            Console.ResetColor();

                            // Continue running
                            run = true;

                        }
                    }
                }
                catch (Exception e)
                {
                    // Display an exception message
                    Console.Clear();

                    // Error with the credentials
                    Console.WriteLine(e.ToString());

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
                // Might be redundant
                sqlConn.Close();

            }

            return run;

        }
        #endregion
    */
    }
}
