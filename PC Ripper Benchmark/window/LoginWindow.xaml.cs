using PC_Ripper_Benchmark.function;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

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
            this.loginButton.BorderThickness = new Thickness(3);
            this.signUpButton.BorderThickness = new Thickness(3);
            this.resetPasswordButton.BorderThickness = new Thickness(3);

            this.loginButton.BorderBrush = Brushes.White;
            this.signUpButton.BorderBrush = Brushes.White;
            this.resetPasswordButton.BorderBrush = Brushes.White;
            this.emailTextBox.Focus();
        }

        #region Event Handlers
        private void LoginButton_Click(object sender, RoutedEventArgs e) {
            function.SystemSettings systemSettings = new function.SystemSettings();
            if (systemSettings.IsInternetAvailable() == true) {
                if (util.RegexUtilities.IsValidEmail(this.emailTextBox.Text)) {
                    this.database_progressbar.Opacity = 100;
                    this.database_progressbar.Value = 0;

                    string connectionString = ConfigurationManager.ConnectionStrings["Connection_String"].ConnectionString;

                    SqlConnection connection = new SqlConnection(connectionString);
                    database.DatabaseConnection newConnection = new database.DatabaseConnection(connection.ConnectionString);

                    try {
                        this.database_progressbar.Value = 50;
                        if (!newConnection.CheckAccountExists(connection, this.emailTextBox.Text, this.passwordBox.Password)) {
                            Console.WriteLine("Good credentials");
                            this.database_progressbar.Value = 100;
                        } else {
                            Close();
                        }

                    } catch (SqlException a) {
                        MessageBox.Show(a.Errors.ToString());
                        connection.Close();
                        throw;
                    }
                    this.database_progressbar.Opacity = 0;
                } else {
                    MessageBox.Show("Invalid Email", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            } else {
                MessageBox.Show("No internet connection!", "No Internet", MessageBoxButton.OK, MessageBoxImage.Warning);
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

        #region KeyDown Events

        //DEBUG

        private void PasswordTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                this.loginButton.Focus();
            } else if (e.Key == Key.Down) {
                this.loginButton.Focus();
            }
        }

        private void LoginButton_MouseRightButtonDown(object sender, MouseButtonEventArgs e) {
            // DEBUG PURPOSES.
            new MainWindow().Show();
            Close();
        }

        private void ResetPasswordButton_Click(object sender, RoutedEventArgs e) {
            PasswordResetWindow passwordResetWindow = new PasswordResetWindow();

            this.settings.TransitionScreen(passwordResetWindow, this);
        }

        private void EmailTextBox_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                this.passwordBox.Focus();
            } else if (e.Key == Key.Down) {
                this.passwordBox.Focus();
            }
        }
        #endregion

        private void BtnTemp_Click(object sender, RoutedEventArgs e) {
            new MainWindow().Show();
        }

        #region Button GotFocus Methods
        private void LoginButton_GotFocus(object sender, RoutedEventArgs e) {
            this.loginButton.Foreground = Brushes.Black;
            this.loginButton.BorderBrush = Brushes.ForestGreen;
        }

        private void SignUpButton_GotFocus(object sender, RoutedEventArgs e) {
            this.signUpButton.Foreground = Brushes.Black;
            this.signUpButton.BorderBrush = Brushes.ForestGreen;
        }

        private void ResetPasswordButton_GotFocus(object sender, RoutedEventArgs e) {
            this.resetPasswordButton.Foreground = Brushes.Black;
            this.resetPasswordButton.BorderBrush = Brushes.ForestGreen;
        }
        #endregion
        #region Button LostFocus Methods
        private void LoginButton_LostFocus(object sender, RoutedEventArgs e) {
            this.loginButton.Foreground = Brushes.White;
            this.loginButton.BorderBrush = Brushes.White;
        }

        private void SignUpButton_LostFocus(object sender, RoutedEventArgs e) {
            this.signUpButton.Foreground = Brushes.White;
            this.signUpButton.BorderBrush = Brushes.White;
        }

        private void ResetPasswordButton_LostFocus(object sender, RoutedEventArgs e) {
            this.resetPasswordButton.Foreground = Brushes.White;
            this.resetPasswordButton.BorderBrush = Brushes.White;
        }
        #endregion
    }
}