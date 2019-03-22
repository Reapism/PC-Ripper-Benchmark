using PC_Ripper_Benchmark.exception;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace PC_Ripper_Benchmark.database {

    /// <summary>
    /// The <see cref="DatabaseConnection"/> class.
    /// <para> </para>
    /// Represents functions for connecting to the
    /// online Azure DB.
    /// <para>Author: David Hartglass
    /// all rights reserved.</para>
    /// </summary>

    public class DatabaseConnection {

        /// <summary>
        /// A <see cref="DatabaseConnection"/> instance
        /// used to create a database connection.
        /// All user data must be passed as parameters. 
        /// </summary>

        public SqlConnection Connection { get; set; }

        /// <summary>
        /// Default constructor. Sets the connection string.
        /// </summary>

        public DatabaseConnection(string connectionString) {
            this.Connection = new SqlConnection {
                ConnectionString = connectionString
            };
        }

        /// <summary>
        /// Opens the SQL database connection.
        /// </summary>
        /// <exception cref="RipperDatabaseException"></exception>

        public void Open() {
            try {
                this.Connection.Open();
            } catch {
                throw new RipperDatabaseException("Failed to open connection.");
            }
        }

        /// <summary>
        /// Member function that adds a user to the database.
        /// Takes a connection, and the required fields to
        /// insert into the database using a stored
        /// procedure
        /// </summary>

        public void AddUserToDatabase(SqlConnection connection, util.UserData user) {
            try {
                SqlCommand addUser = new SqlCommand("UserAdd", connection) {
                    CommandType = CommandType.StoredProcedure
                };

                addUser.Parameters.AddWithValue("@FirstName", user.FirstName.Trim());
                addUser.Parameters.AddWithValue("@LastName", user.LastName.Trim());
                addUser.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber.Trim());
                addUser.Parameters.AddWithValue("@Email", user.Email.Trim());
                addUser.Parameters.AddWithValue("@Password", user.Password.Trim());

                addUser.ExecuteNonQuery();
                MessageBox.Show("Registration Successful");
            } catch (SqlException) {
                MessageBox.Show("An account with that email already exists!");
            }
        }

        /// <summary>
        /// Member function that checks if an 
        /// email exists in the database.
        /// The stored procedure will return 
        /// a 0 or 1 to determine if it exists.
        /// </summary>

        public void CheckAccountExists(SqlConnection connection, string email, string password) {
            util.Encryption encrypter = new util.Encryption();

            SqlCommand checkAccount = new SqlCommand("SELECT * FROM Customer where Email=@Email and Password=@Password", connection);
            email = encrypter.EncryptText(email);
            password = encrypter.EncryptText(password);

            checkAccount.Parameters.AddWithValue("@Email", email);
            checkAccount.Parameters.AddWithValue("@Password", password);

            connection.Open(); 
            checkAccount.ExecuteNonQuery();

            SqlDataAdapter adapter = new SqlDataAdapter(checkAccount);
            DataSet ds = new DataSet();
            adapter.Fill(ds);

            int count = ds.Tables[0].Rows.Count;

            if (count == 1) {
                window.MainWindow mainWindow = new window.MainWindow();
                mainWindow.Show();
                connection.Close();
            } else {
                MessageBox.Show("Invalid Login");
                connection.Close();
            }
        }
    }
}
