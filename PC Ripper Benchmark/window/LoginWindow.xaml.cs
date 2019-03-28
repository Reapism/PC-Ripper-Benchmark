using PC_Ripper_Benchmark.function;
using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;

namespace PC_Ripper_Benchmark.window {

    /// <summary>
    /// Interaction logic for <see cref="LoginWindow"/>
    /// Inherits from Window base class to have properties of a windows
    /// <para>Author: David Hartglass</para>
    /// </summary>

    public partial class LoginWindow : Window {

        private WindowSettings settings = new WindowSettings();

        /// <summary>
        /// Default constructor for <see cref="LoginWindow"/>.
        /// </summary>

        public LoginWindow() {
            InitializeComponent();
            //Change the progressbar visibilty to not show on screen
            this.database_progressbar.Opacity = 0;

            //Create new instance of the window settings class
            this.settings.CenterWindowOnScreen(this.windowLogin);
        }

        #region Event Handlers
        private void LoginButton_Click(object sender, RoutedEventArgs e) {

            SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder {
                DataSource = "tcp:bcsproject.database.windows.net,1433",
                UserID = "Konrad100",
                Password = "Coolguy100",
                PersistSecurityInfo = false,
                InitialCatalog = "CPURipper",
                MultipleActiveResultSets = false,
                Encrypt = true,
                TrustServerCertificate = false,
                ConnectTimeout = 30
            };

            SqlConnection connection = new SqlConnection(stringBuilder.ConnectionString);
            database.DatabaseConnection newConnection = new database.DatabaseConnection(connection.ConnectionString);


            try {
                if (!newConnection.CheckAccountExists(connection, this.emailTextBox.Text, this.passwordTextBox.Password)) {
                    Console.WriteLine("Good credentials");
                } else {
                    Close();
                }

            } catch (SqlException a) {
                MessageBox.Show(a.Errors.ToString());
                connection.Close();
                throw;
            }
        }

        /// <summary>
        /// Event handler for signUpButton in <see cref="LoginWindow"/>.
        /// <para>When signUpButton is clicked,the window changes to a window of type <see cref="CreateAccountWindow"/></para>
        /// </summary>

        private void SignUpButton_Click(object sender, RoutedEventArgs e) {
            CreateAccountWindow createAccountWindow = new CreateAccountWindow();

            this.settings.TransitionScreen(createAccountWindow, this);
        }
        #endregion

        private void BtnTemp_Click(object sender, RoutedEventArgs e) {
            new MainWindow().Show();
        }

        private void PasswordTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e) {
            if (e.Key == Key.Enter) { LoginButton_Click(sender, e); }
        }

        private void LoginButton_MouseRightButtonDown(object sender, MouseButtonEventArgs e) {
            // DEBUG PURPOSES.
            new MainWindow().Show();
            Close();
        }
    }
}