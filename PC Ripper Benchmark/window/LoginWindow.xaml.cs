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
            database_progressbar.Opacity = 100;
            database_progressbar.Value = 0;

            string connectionString = Properties.Settings.Default.Connection_String;


            SqlConnection connection = new SqlConnection(connectionString);
            database.DatabaseConnection newConnection = new database.DatabaseConnection(connection.ConnectionString);

            try {
                database_progressbar.Value = 50;
                if (!newConnection.CheckAccountExists(connection, this.emailTextBox.Text, this.passwordTextBox.Password)) {
                    Console.WriteLine("Good credentials");
                    database_progressbar.Value = 100;
                }
                else {
                    Close();
                }

            } catch (SqlException a) {
                MessageBox.Show(a.Errors.ToString());
                connection.Close();
                throw;
            }
            database_progressbar.Opacity = 0;
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

        private void ResetPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            PasswordResetWindow passwordResetWindow = new PasswordResetWindow();

            this.settings.TransitionScreen(passwordResetWindow, this);
        }
    }
}