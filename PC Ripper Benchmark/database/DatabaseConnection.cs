using PC_Ripper_Benchmark.exception;
using PC_Ripper_Benchmark.function;
using PC_Ripper_Benchmark.util;
using PC_Ripper_Benchmark.window;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using static PC_Ripper_Benchmark.function.RipperTypes;
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
            if (connectionString == "" || connectionString == null) {
                MessageBox.Show("Empty connection string!", "Null Connection",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
            } else {
                this.Connection = new SqlConnection {
                    ConnectionString = connectionString
                };
            }
        }

        /// <summary>
        /// Opens the SQL database connection.
        /// </summary>
        /// <exception cref="RipperScoreException"></exception>

        public void Open() {
            try {
                this.Connection.Open();
            } catch {
                throw new RipperScoreException("Failed to open connection.");
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

        public bool AddUserToDatabase(UserData user) {

            try {
                if (SystemSettings.IsInternetAvailable() == true && this.Connection.ConnectionString != "" && this.Connection.ConnectionString != null) {
                    SqlCommand addUser = new SqlCommand("UserAdd", this.Connection) {
                        CommandType = CommandType.StoredProcedure
                    };

                    //this.Connection.Open();
                    addUser.Parameters.AddWithValue("@FirstName", user.FirstName.Trim());
                    addUser.Parameters.AddWithValue("@LastName", user.LastName.Trim());
                    addUser.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber.Trim());
                    addUser.Parameters.AddWithValue("@Email", user.Email.Trim());
                    addUser.Parameters.AddWithValue("@Password", user.Password.Trim());
                    addUser.Parameters.AddWithValue("@SecurityQuestion", user.SecurityQuestion.Trim());
                    addUser.Parameters.AddWithValue("@SecurityQuestionAnswer", user.SecurityQuestionAnswer.Trim());
                    addUser.Parameters.AddWithValue("@IsLight", user.IsLight);
                    addUser.Parameters.AddWithValue("@UserSkill", (int)user.IsAdvanced);
                    addUser.Parameters.AddWithValue("@TypeOfUser", (int)user.UserType);

                    addUser.ExecuteNonQuery();
                    this.Connection.Close();
                    MessageBox.Show("Registration Successful");

                    return true;
                } else {
                    return false;
                }

            } catch {
                MessageBox.Show("An account with that email already exists!", "Existing Account", MessageBoxButton.OK, MessageBoxImage.Warning);
                this.Connection.Close();
                return false;
            }
        }

        /// <summary>
        /// Populates a <see cref="UserData"/> using email
        /// as the key.
        /// </summary>
        /// <param name="userData">A <see cref="UserData"/> to populate and return.</param>
        /// <param name="email">The email used to find the particular user.</param>
        /// <returns></returns>

        public bool GetUserData(out UserData userData, string email) {
            userData = new UserData();

            try {
                SqlCommand cmd = new SqlCommand("SELECT * FROM [USER] WHERE Email=@Email", this.Connection);
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

        public bool CheckAccountExists(string email, string password) {

            try {
                if (SystemSettings.IsInternetAvailable() == true) {
                    this.Connection.Open();

                    HashManager encrypter = new HashManager();

                    SqlCommand checkAccount = new SqlCommand("SELECT * FROM [USER] WHERE Email=@Email AND Password=@Password", this.Connection);
                    email = encrypter.HashTextSHA256(email.Trim());
                    password = encrypter.HashUniqueTextSHA256(password.Trim());

                    checkAccount.Parameters.AddWithValue("@Email", email);
                    checkAccount.Parameters.AddWithValue("@Password", password);
                    checkAccount.ExecuteNonQuery();

                    SqlDataAdapter adapter = new SqlDataAdapter(checkAccount);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);


                    int count = ds.Tables[0].Rows.Count;

                    if (count == 1) {
                        MainWindow mainWindow;

                        if (GetUserData(out UserData u, email)) {
                            // returns the user with that email.
                            u.Password = "";
                            mainWindow = new MainWindow(u);
                        } else {
                            // returns a null user.
                            mainWindow = new MainWindow(GetNullUser());
                        }

                        mainWindow.Show();
                        this.Connection.Close();
                        return true;
                    }
                }
            } catch { }

            return false;
        }

        /// <summary>
        /// Member function that checks if an 
        /// email exists in the database.
        /// The stored procedure will return 
        /// a 0 or 1 to determine if it exists.
        /// </summary>

        public bool CheckEmailExists(string email) {
            try {
                if (SystemSettings.IsInternetAvailable() == true) {
                    this.Connection.Open();

                    HashManager encrypter = new HashManager();

                    SqlCommand checkAccount = new SqlCommand("SELECT * FROM [USER] where Email=@Email", this.Connection);
                    email = encrypter.HashTextSHA256(email.ToUpper().Trim());

                    checkAccount.Parameters.AddWithValue("@Email", email);
                    checkAccount.ExecuteNonQuery();

                    SqlDataAdapter adapter = new SqlDataAdapter(checkAccount);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    int count = ds.Tables[0].Rows.Count;

                    if (count == 1) {
                        this.Connection.Close();
                        return true;

                    }
                }
            } catch { }

            this.Connection.Close();
            return false;
        }

        /// <summary>
        /// Returns the security question using the
        /// email for the query.
        /// </summary>
        /// <param name="email">The email associated with the
        /// security question.</param>
        /// <returns></returns>

        public string GetSecurityQuestion(string email) {
            //Command to get the actual answered security question
            this.Connection.Open();
            SqlCommand getSecurityQuestion = new SqlCommand("SELECT SecurityQuestion FROM [USER] where Email=@Email", this.Connection);

            //Fill the parameter of the query
            getSecurityQuestion.Parameters.AddWithValue("@Email", email);
            return (string)getSecurityQuestion.ExecuteScalar();
        }

        /// <summary>
        /// Returns the security answer using the
        /// email for the query.
        /// </summary>
        /// <param name="email">The email associated with the
        /// security question.</param>
        /// <returns></returns>

        public string GetSecurityQuestionAnswer(string email) {
            this.Connection.Open();
            SqlCommand getSecurityQuestionAnswer = new SqlCommand("SELECT SecurityQuestionAnswer FROM [USER] where Email=@Email", this.Connection);
            getSecurityQuestionAnswer.Parameters.AddWithValue("@Email", email);

            //Set the answer returned by the query to a variable for comparison
            return (string)getSecurityQuestionAnswer.ExecuteScalar();
        }

        /// <summary>
        /// Changes user settings based on the email associated with user.
        /// </summary>
        /// <param name="skill">The new <see cref="UserSkill"/>.</param>
        /// <param name="type">The new <see cref="TypeOfUser"/>.</param>
        /// <param name="email">The email associated with the
        /// account.</param>
        /// <returns></returns>

        public bool ChangeUserSettings(UserData.UserSkill skill, UserData.TypeOfUser type, string email) {
            try {
                if (SystemSettings.IsInternetAvailable() == true && this.Connection.ConnectionString != "" && this.Connection.ConnectionString != null) {
                    SqlCommand changeUserSettings = new SqlCommand("ChangeUserSettings", this.Connection) {
                        CommandType = CommandType.StoredProcedure
                    };

                    //this.Connection.Open();
                    changeUserSettings.Parameters.AddWithValue("@UserSkill", skill);
                    changeUserSettings.Parameters.AddWithValue("@TypeOfUser", type);
                    changeUserSettings.Parameters.AddWithValue("@Email", email);

                    changeUserSettings.ExecuteNonQuery();
                    this.Connection.Close();
                    MessageBox.Show("Account Settings Changed");

                    return true;
                } else {
                    return false;
                }

            } catch {
                MessageBox.Show("Error changing settings", "Error!", MessageBoxButton.OK, MessageBoxImage.Warning);
                this.Connection.Close();
                return false;
            }
        }

        /// <summary>
        /// Adds the results to the database with a particular name.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <param name="results">The results as a string.</param>
        /// <param name="nameOfTest">The name of the test to reference.</param>
        /// <returns></returns>

        public bool AddUserResults(string email, string results, string nameOfTest) {
            try {
                if (SystemSettings.IsInternetAvailable() == true && this.Connection.ConnectionString != "" && this.Connection.ConnectionString != null) {
                    SqlCommand changeUserSettings = new SqlCommand("ResultsAdd", this.Connection) {
                        CommandType = CommandType.StoredProcedure
                    };

                    //this.Connection.Open();
                    changeUserSettings.Parameters.AddWithValue("@DateCreated", DateTime.Now.ToString("MM / dd / yyyy HH: mm:ss"));
                    changeUserSettings.Parameters.AddWithValue("@ComputerName", Environment.MachineName);
                    changeUserSettings.Parameters.AddWithValue("@Email", email);
                    changeUserSettings.Parameters.AddWithValue("@Results", results);
                    changeUserSettings.Parameters.AddWithValue("@TestName", nameOfTest);

                    changeUserSettings.ExecuteNonQuery();
                    this.Connection.Close();
                    MessageBox.Show("The following results have been added to the database.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                    return true;
                } else {
                    return false;
                }

            } catch {
                MessageBox.Show($"Test results already exist in database!", "Error!", MessageBoxButton.OK, MessageBoxImage.Warning);
                this.Connection.Close();
                return false;
            }
        }
    }
}
