using PC_Ripper_Benchmark.exception;
using PC_Ripper_Benchmark.function;
using PC_Ripper_Benchmark.util;
using PC_Ripper_Benchmark.window;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using static PC_Ripper_Benchmark.util.UserData;

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
        /// A <see cref="SqlConnection"/> instance
        /// used to create a database connection.
        /// All user data must be passed as parameters. 
        /// </summary>

        public SqlConnection Connection { get; set; }

        /// <summary>
        /// Default constructor. Sets the connection string.
        /// </summary>

        public DatabaseConnection(string connectionString) {
            if (connectionString == "" || connectionString == null)
            {
                MessageBox.Show("Empty connection string!", "Null Connection", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                this.Connection = new SqlConnection
                {
                    ConnectionString = connectionString
                };
            }       
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
        /// Gets the connection string from <see langword="App.config"/>.
        /// </summary>
        /// <returns></returns>

        public static string GetConnectionString() => ConfigurationManager.ConnectionStrings["Connection_String"].ConnectionString;

        /// <summary>
        /// Member function that adds a user to the database.
        /// Takes a connection, and the required fields to
        /// insert into the database using a stored
        /// procedure
        /// </summary>

        public bool AddUserToDatabase(SqlConnection connection, UserData user) {

            try {
                if (SystemSettings.IsInternetAvailable() == true && connection.ConnectionString != "" && connection.ConnectionString != null) {
                    SqlCommand addUser = new SqlCommand("UserAdd", connection) {
                        CommandType = CommandType.StoredProcedure
                    };

                    connection.Open();
                    addUser.Parameters.AddWithValue("@FirstName", user.FirstName.Trim());
                    addUser.Parameters.AddWithValue("@LastName", user.LastName.Trim());
                    addUser.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber.Trim());
                    addUser.Parameters.AddWithValue("@Email", user.Email.Trim());
                    addUser.Parameters.AddWithValue("@Password", user.Password.Trim());
                    addUser.Parameters.AddWithValue("@SecurityQuestion", user.SecurityQuestion.Trim());
                    addUser.Parameters.AddWithValue("@SecurityQuestionAnswer", user.SecurityQuestionAnswer.Trim());
                    addUser.Parameters.AddWithValue("@IsLight",user.IsLight);
                    addUser.Parameters.AddWithValue("@UserSkill", (int)user.IsAdvanced);
                    addUser.Parameters.AddWithValue("@TypeOfUser", (int)user.UserType);

                    addUser.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("Registration Successful");

                    return true;
                } else {
                    return false;
                }

            } catch (SqlException e) {
                MessageBox.Show("An account with that email already exists!", "Existing Account", MessageBoxButton.OK, MessageBoxImage.Warning);
                connection.Close();
                return false;
            }
        }

        /// <summary>
        /// Populates a <see cref="UserData"/> using email
        /// as the key.
        /// </summary>
        /// <param name="conn">The <see cref="SqlConnection"/> to connect with.</param>
        /// <param name="userData">A <see cref="UserData"/> to populate and return.</param>
        /// <param name="email">The email used to find the particular user.</param>
        /// <returns></returns>

        public bool GetUserData(SqlConnection conn, out UserData userData, string email) {
           userData = new UserData();

            try {
                SqlCommand cmd = new SqlCommand("SELECT * FROM [USER] WHERE Email=@Email", conn);
                cmd.Parameters.AddWithValue("@Email", email);
                
                cmd.ExecuteScalar();

                using (SqlDataReader reader = cmd.ExecuteReader()) {
                    if (reader.Read()) {

                        userData.FirstName = reader.GetString(0);
                        userData.LastName = reader.GetString(1);
                        userData.PhoneNumber = reader.GetString(2);
                        userData.Email = reader.GetString(3);
                        userData.Password = reader.GetString(4);
                        userData.SecurityQuestion = reader.GetString(5);
                        userData.SecurityQuestionAnswer = reader.GetString(6);

                        if (int.TryParse(reader.GetString(7), out int i)) {
                            bool b = (i == 1) ? true : false;
                            userData.IsLight = b;
                        }

                        UserSkill skill = (UserSkill)int.Parse(reader.GetString(8));
                        TypeOfUser type = (TypeOfUser)int.Parse(reader.GetString(9));

                        userData.IsAdvanced = skill;
                        userData.UserType = type;

                        // if it reads another comma.
                        if (reader.Read()) {
                            return false;
                        }
                    }
                }
            } catch {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Member function that checks if an 
        /// email exists in the database.
        /// The stored procedure will return 
        /// a 0 or 1 to determine if it exists.
        /// </summary>

        public bool CheckAccountExists(SqlConnection connection, string email, string password) {
            try {
                if (SystemSettings.IsInternetAvailable() == true) {
                    connection.Open();

                    Encryption encrypter = new Encryption();

                    SqlCommand checkAccount = new SqlCommand("SELECT * FROM [USER] WHERE Email=@Email AND Password=@Password", connection);
                    email = encrypter.EncryptText(email.ToUpper().Trim());
                    password = encrypter.EncryptText(password.ToUpper().Trim());

                    checkAccount.Parameters.AddWithValue("@Email", email);
                    checkAccount.Parameters.AddWithValue("@Password", password);
                    checkAccount.ExecuteNonQuery();

                    SqlDataAdapter adapter = new SqlDataAdapter(checkAccount);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    

                    int count = ds.Tables[0].Rows.Count;

                    if (count == 1) {
                        MainWindow mainWindow;
                        if (GetUserData(connection, out UserData u, email)) {
                            // returns the user with that email.
                            mainWindow = new MainWindow(u);
                        } else {
                            // returns a null user.
                            mainWindow = new MainWindow(GetNullUser());
                        }                            

                        mainWindow.Show();
                        connection.Close();
                        return true;
                    } else {
                        connection.Close();
                        return false;
                    }
                } else {
                    MessageBox.Show("You are not connected to the internet!");
                    connection.Close();
                    return false;
                }

            } catch (Exception e) {
                MessageBox.Show($"Oh no. A RipperDatabaseException occured. {e.ToString()}");
                connection.Close();
                return false;
            }
        }

        /// <summary>
        /// Member function that checks if an 
        /// email exists in the database.
        /// The stored procedure will return 
        /// a 0 or 1 to determine if it exists.
        /// </summary>

        public bool CheckEmailExists(SqlConnection connection, string email) {
            try {
                if (SystemSettings.IsInternetAvailable() == true) {
                    connection.Open();

                    Encryption encrypter = new Encryption();

                    SqlCommand checkAccount = new SqlCommand("SELECT * FROM [USER] where Email=@Email", connection);
                    email = encrypter.EncryptText(email.ToUpper().Trim());

                    checkAccount.Parameters.AddWithValue("@Email", email);
                    checkAccount.ExecuteNonQuery();

                    SqlDataAdapter adapter = new SqlDataAdapter(checkAccount);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    int count = ds.Tables[0].Rows.Count;

                    if (count == 1) {
                        connection.Close();
                        return true;
                    } else {
                        MessageBox.Show("An account with that email does not exist", "Invalid Email", MessageBoxButton.OK, MessageBoxImage.Error);
                        connection.Close();
                        return false;
                    }
                } else {
                    MessageBox.Show("You are not connected to the internet!");
                    connection.Close();
                    return false;
                }

            } catch (Exception e) {
                MessageBox.Show($"Oh no. A RipperDatabaseException occured. {e.ToString()}");
                connection.Close();
                return false;
            }
        }
    }
}
