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
        #region SQLINSERT
        /// <summary>
        /// A method that takes all personnel and inserts it into a DB
        /// </summary>
        /// <param name="sortedPersonnel">All combined workers in a sorted dictionary</param>
        public static void SQLInsert(SortedDictionary<int, Tuple<string, char>> sortedPersonnel)
        {
            // New DateTime should be swapped with added information
            DateTime date = DateTime.Now;

            // Build a string for connection details
            SqlConnectionStringBuilder sqlString = new SqlConnectionStringBuilder();
            sqlString.DataSource = "DESKTOP-DP8TQ6A\\VOGTSS";
            sqlString.InitialCatalog = "employee_db";
            sqlString.IntegratedSecurity = true;

            // Declare and initialize a new connection
            SqlConnection sqlConn = new SqlConnection(sqlString.ToString());

            try
            {
                sqlConn.Open();

                string sqlInsert = "INSERT INTO dbo.Employee_Table(emp_number, emp_name, date_added, emp_type) VALUES(@emp_number, @emp_name, @date_added, @emp_type)";

                foreach (var personnel in sortedPersonnel)
                {
                    // Prepare the insert statement
                    SqlCommand sqlCmd = new SqlCommand(sqlInsert, sqlConn);

                    sqlCmd.Parameters.AddWithValue("@emp_number", personnel.Key.ToString());
                    sqlCmd.Parameters.AddWithValue("@emp_name", personnel.Value.Item1);
                    sqlCmd.Parameters.AddWithValue("@date_added", date.ToShortDateString());
                    sqlCmd.Parameters.AddWithValue("@emp_type", personnel.Value.Item2);

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
