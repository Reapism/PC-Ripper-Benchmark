using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace PC_Ripper_Benchmark.database {

    /// <summary>
    /// The <see cref="DatabaseConnection"/> class.
    /// <para> </para>
    /// Represents all the functions for 
    /// testing the CPU component. Includes single
    /// and multithreaded testing using various
    /// common data structures.
    /// <para>Author: David Hartglass
    /// all rights reserved.</para>
    /// </summary>
    public class DatabaseConnection {

        /// <summary>
        /// A <see cref="DatabaseConnection"/> instance
        /// used to create a database connection.
        /// All user data must be passed as parameters. 
        /// </summary>
        /// 

        public SqlConnection connection { get; set; }

        /// <summary>
        /// Default constructor. Takes a connection string to open the connection to the database
        /// </summary>
        public DatabaseConnection(string connectionString) {
            connection = new SqlConnection();
            connection.ConnectionString = connectionString;
            connection.Open();
        }

        /// <summary>
        /// Member function that adds a user to the database.
        /// Takes a connection, and the required fields to
        /// insert into the database using a stored
        /// procedure
        /// </summary>
        public void addUserToDatabase(SqlConnection connection, string firstName,
            string lastName, string phoneNumber, string email, string password)
        {           
            SqlCommand addUser = new SqlCommand("UserAdd", connection);
            addUser.CommandType = CommandType.StoredProcedure;

            addUser.Parameters.AddWithValue("@FirstName", firstName.Trim());
            addUser.Parameters.AddWithValue("@LastName", lastName.Trim());
            addUser.Parameters.AddWithValue("@PhoneNumber", phoneNumber.Trim());
            addUser.Parameters.AddWithValue("@Email", email.Trim());
            addUser.Parameters.AddWithValue("@Password", password.Trim());

            addUser.ExecuteNonQuery();
            MessageBox.Show("Registration Successful");
        }
    }
}
