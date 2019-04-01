using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace PC_Ripper_Benchmark.window {
    /// <summary>
    /// The <see cref="CreateAccountWindow"/> class.
    /// <para></para>
    /// Contains all methods and properties about 
    /// the Create Account Window.
    /// <para>Author: David Hartglass (c), all rights reserved.</para>
    /// </summary>
    public partial class CreateAccountWindow : Window {

        //Global window settings class object
        function.WindowSettings settings = new function.WindowSettings();

        Popup codePopup = new Popup();
        TextBlock popupContent = new TextBlock();
        function.WindowSettings windowSettings = new function.WindowSettings();

        /// <summary>
        /// Default constructor in <see cref="CreateAccountWindow"/>.
        /// <para>Creates a window of type CreateWindow
        /// and calls CenterWindowOnScreen to center the window</para>
        /// </summary>
        /// 
        public CreateAccountWindow() {
            InitializeComponent();
            this.settings.CenterWindowOnScreen(this.windowCreateAccount);
            this.firstNameTextBox.Focus();
        }

        /// <summary>
        /// Event handler for button click in <see cref="CreateAccountWindow"/>.
        /// <para>When Submit is clicked, the text fields check to make sure valid data is entered
        /// using regular expressions in <see cref="util.RegexUtilities"/></para>
        /// </summary>
        /// 
        private void CreateAccountSubmitButton_Click(object sender, RoutedEventArgs e) {
            #region TextField Error Checking
            String errorMessage = null;
            if (string.IsNullOrWhiteSpace(this.firstNameTextBox.Text)) {
                errorMessage += " \"First Name\" ";
            }

            if (string.IsNullOrWhiteSpace(this.lastNameTextBox.Text)) {
                errorMessage += " \"Last Name\" ";
            }

            if (string.IsNullOrWhiteSpace(this.emailTextBox.Text)) {
                errorMessage += " \"Email\" ";
            }

            if (string.IsNullOrWhiteSpace(this.userPasswordBox.Password)) {
                errorMessage += " \"Password\" ";
            }

            if (string.IsNullOrWhiteSpace(this.confirmUserPasswordBox.Password)) {
                errorMessage += " \"Password\" ";
            } else if (this.confirmUserPasswordBox.Password != this.userPasswordBox.Password) {
                MessageBox.Show("Password do not match. Please try again.", "Invalid Password", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (!(errorMessage == null)) {
                MessageBox.Show($"{errorMessage} field(s) missing value", "Invalid Fields", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            #endregion
            #region Regular Expression Checks

            if (!util.RegexUtilities.IsValidEmail(this.emailTextBox.Text)) {
                this.emailTextBox.Focus();
            }


            if (!util.RegexUtilities.IsValidPhoneNumber(this.phoneTextBox.Text)) {
                this.phoneTextBox.Focus();
            }

            if (!util.RegexUtilities.IsValidPassword(this.userPasswordBox.Password)) {
                this.userPasswordBox.Focus();

            }

            if (this.userPasswordBox.Password != this.confirmUserPasswordBox.Password) {
                this.lblConfirmPassword.Visibility = Visibility.Visible;
                this.confirmUserPasswordBox.Focus();
            }

            if (!util.RegexUtilities.IsValidName(this.firstNameTextBox.Text)) {
                this.firstNameTextBox.Focus();
            }

            if (!util.RegexUtilities.IsValidName(this.lastNameTextBox.Text)) {
                this.lastNameTextBox.Focus();
            }

            #endregion
            #region Create a user
            //If all the data is valid...
            if (util.RegexUtilities.IsValidPhoneNumber(this.phoneTextBox.Text) &&
               util.RegexUtilities.IsValidName(this.firstNameTextBox.Text) &&
               util.RegexUtilities.IsValidName(this.lastNameTextBox.Text) &&
               util.RegexUtilities.IsValidEmail(this.emailTextBox.Text) &&
               util.RegexUtilities.IsValidPhoneNumber(this.phoneTextBox.Text) &&
               util.RegexUtilities.IsValidPassword(this.userPasswordBox.Password) &&
               this.userPasswordBox.Password == this.confirmUserPasswordBox.Password) {

                //New instance of encryption class
                util.Encryption encrypter = new util.Encryption();

                util.UserData newUser = new util.UserData {
                    //Encrypt user data and set to newUser object
                    FirstName = this.firstNameTextBox.Text,
                    LastName = this.lastNameTextBox.Text,
                    Email = encrypter.EncryptText(this.emailTextBox.Text),
                    PhoneNumber = encrypter.EncryptText(this.phoneTextBox.Text),
                    Password = encrypter.EncryptText(this.userPasswordBox.Password)
                };

                //SQL Connection String
                SqlConnectionStringBuilder connectionString = new SqlConnectionStringBuilder {
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

                //Open database connection and send that data to the database hashed.
                database.DatabaseConnection dbConnection = new database.DatabaseConnection(connectionString.ConnectionString);

                if (dbConnection.AddUserToDatabase(dbConnection.Connection, newUser)) {
                    LoginWindow loginWindow = new LoginWindow();
                    this.windowSettings.TransitionScreen(loginWindow, this);
                }
            }
            #endregion
        }

        #region Border Color First Name
        private void FirstNameTextBox_GotFocus(object sender, RoutedEventArgs e) {
            if (util.RegexUtilities.IsValidName(this.firstNameTextBox.Text)) {
                this.firstNameTextBox.BorderThickness = new Thickness(3.0);
                this.firstNameTextBox.BorderBrush = Brushes.Green;
            } else {
                this.firstNameTextBox.BorderThickness = new Thickness(3.0);
                this.firstNameTextBox.BorderBrush = Brushes.Red;
            }
        }

        private void FirstNameTextBox_LostFocus(object sender, RoutedEventArgs e) {
            if (util.RegexUtilities.IsValidName(this.firstNameTextBox.Text)) {
                this.firstNameTextBox.BorderThickness = new Thickness(3.0);
                this.firstNameTextBox.BorderBrush = Brushes.Green;
            } else {
                this.firstNameTextBox.BorderThickness = new Thickness(3.0);
                this.firstNameTextBox.BorderBrush = Brushes.Red;
            }
        }

        private void FirstNameTextBox_TextChanged(object sender, TextChangedEventArgs e) {
            if (util.RegexUtilities.IsValidName(this.firstNameTextBox.Text)) {
                this.firstNameTextBox.BorderThickness = new Thickness(3.0);
                this.firstNameTextBox.BorderBrush = Brushes.Green;
            } else {
                this.firstNameTextBox.BorderThickness = new Thickness(3.0);
                this.firstNameTextBox.BorderBrush = Brushes.Red;
            }
        }
        #endregion
        #region Border Color Last Name
        private void LastNameTextBox_GotFocus(object sender, RoutedEventArgs e) {
            if (util.RegexUtilities.IsValidName(this.lastNameTextBox.Text)) {
                this.lastNameTextBox.BorderThickness = new Thickness(3.0);
                this.lastNameTextBox.BorderBrush = Brushes.Green;
            } else {
                this.lastNameTextBox.BorderThickness = new Thickness(3.0);
                this.lastNameTextBox.BorderBrush = Brushes.Red;
            }
        }

        private void LastNameTextBox_LostFocus(object sender, RoutedEventArgs e) {
            if (util.RegexUtilities.IsValidName(this.lastNameTextBox.Text)) {
                this.lastNameTextBox.BorderThickness = new Thickness(3.0);
                this.lastNameTextBox.BorderBrush = Brushes.Green;
            } else {
                this.lastNameTextBox.BorderThickness = new Thickness(3.0);
                this.lastNameTextBox.BorderBrush = Brushes.Red;
            }
        }

        private void LastNameTextBox_TextChanged(object sender, TextChangedEventArgs e) {
            if (util.RegexUtilities.IsValidName(this.lastNameTextBox.Text)) {
                this.lastNameTextBox.BorderThickness = new Thickness(3.0);
                this.lastNameTextBox.BorderBrush = Brushes.Green;
            } else {
                this.lastNameTextBox.BorderThickness = new Thickness(3.0);
                this.lastNameTextBox.BorderBrush = Brushes.Red;
            }
        }
        #endregion
        #region Border Color Email
        private void EmailTextBox_LostFocus(object sender, RoutedEventArgs e) {
            if (util.RegexUtilities.IsValidEmail(this.emailTextBox.Text)) {
                this.emailTextBox.BorderThickness = new Thickness(3.0);
                this.emailTextBox.BorderBrush = Brushes.Green;
            } else {
                this.emailTextBox.BorderThickness = new Thickness(3.0);
                this.emailTextBox.BorderBrush = Brushes.Red;
            }
        }

        private void EmailTextBox_GotFocus(object sender, RoutedEventArgs e) {
            if (util.RegexUtilities.IsValidEmail(this.emailTextBox.Text)) {
                this.emailTextBox.BorderThickness = new Thickness(3.0);
                this.emailTextBox.BorderBrush = Brushes.Green;
            } else {
                this.emailTextBox.BorderThickness = new Thickness(3.0);
                this.emailTextBox.BorderBrush = Brushes.Red;
            }
        }

        private void EmailTextBox_TextChanged(object sender, TextChangedEventArgs e) {
            if (util.RegexUtilities.IsValidEmail(this.emailTextBox.Text)) {
                this.emailTextBox.BorderThickness = new Thickness(3.0);
                this.emailTextBox.BorderBrush = Brushes.Green;
            } else {
                this.emailTextBox.BorderThickness = new Thickness(3.0);
                this.emailTextBox.BorderBrush = Brushes.Red;
            }
        }
        #endregion
        #region Border Color Phone
        private void PhoneTextBox_TextChanged(object sender, TextChangedEventArgs e) {
            if (util.RegexUtilities.IsValidPhoneNumber(this.phoneTextBox.Text)) {
                this.phoneTextBox.BorderThickness = new Thickness(3.0);
                this.phoneTextBox.BorderBrush = Brushes.Green;
            } else {
                this.phoneTextBox.BorderThickness = new Thickness(3.0);
                this.phoneTextBox.BorderBrush = Brushes.Red;
            }
        }

        private void PhoneTextBox_LostFocus(object sender, RoutedEventArgs e) {
            if (util.RegexUtilities.IsValidPhoneNumber(this.phoneTextBox.Text)) {
                this.phoneTextBox.BorderThickness = new Thickness(3.0);
                this.phoneTextBox.BorderBrush = Brushes.Green;
            } else {
                this.phoneTextBox.BorderThickness = new Thickness(3.0);
                this.phoneTextBox.BorderBrush = Brushes.Red;
            }
        }

        private void PhoneTextBox_GotFocus(object sender, RoutedEventArgs e) {
            if (util.RegexUtilities.IsValidPhoneNumber(this.phoneTextBox.Text)) {
                this.phoneTextBox.BorderThickness = new Thickness(3.0);
                this.phoneTextBox.BorderBrush = Brushes.Green;
            } else {
                this.phoneTextBox.BorderThickness = new Thickness(3.0);
                this.phoneTextBox.BorderBrush = Brushes.Red;
            }
        }
        #endregion
        #region Border Color Password
        private void UserPasswordBox_GotFocus(object sender, RoutedEventArgs e) {
            //Sets the popup values, background property,
            this.popupContent.FontSize = 10;
            this.codePopup.PlacementTarget = this.userPasswordBox;
            this.popupContent.Text = "Password must contain: \n-One uppercase letter" +
                "\n-One special character\n-One number";

            this.popupContent.Background = Brushes.PeachPuff;
            this.popupContent.Foreground = Brushes.Red;
            this.codePopup.Child = this.popupContent;

            this.codePopup.IsOpen = true;

            if (util.RegexUtilities.IsValidPassword(this.userPasswordBox.Password)) {
                this.userPasswordBox.BorderThickness = new Thickness(3.0);
                this.userPasswordBox.BorderBrush = Brushes.Green;
                this.codePopup.IsOpen = false;
            } else {
                this.userPasswordBox.BorderThickness = new Thickness(3.0);
                this.userPasswordBox.BorderBrush = Brushes.Red;
            }
        }

        private void UserPasswordBox_LostFocus(object sender, RoutedEventArgs e) {
            this.codePopup.IsOpen = false;

            if (util.RegexUtilities.IsValidPassword(this.userPasswordBox.Password)) {
                this.userPasswordBox.BorderThickness = new Thickness(3.0);
                this.userPasswordBox.BorderBrush = Brushes.Green;
            } else {
                this.userPasswordBox.BorderThickness = new Thickness(3.0);
                this.userPasswordBox.BorderBrush = Brushes.Red;
            }
        }

        private void UserPasswordBox_PasswordChanged(object sender, RoutedEventArgs e) {
            if (util.RegexUtilities.IsValidPassword(this.userPasswordBox.Password)) {
                this.codePopup.IsOpen = false;
                this.userPasswordBox.BorderThickness = new Thickness(3.0);
                this.userPasswordBox.BorderBrush = Brushes.Green;
            } else {
                this.userPasswordBox.BorderThickness = new Thickness(3.0);
                this.userPasswordBox.BorderBrush = Brushes.Red;
            }

        }
        #endregion
        #region Border Color Confirm Password
        private void ConfirmUserPasswordBox_PasswordChanged(object sender, RoutedEventArgs e) {
            if (this.userPasswordBox.Password == this.confirmUserPasswordBox.Password) {

                //If the password matches, set passwords match label to green border and visible
                this.lblPasswordsMatch.Content = "Passwords match";
                this.lblPasswordsMatch.Visibility = Visibility.Visible;
                this.lblPasswordsMatch.Background = Brushes.Green;

                //Confirm password box turns green
                this.confirmUserPasswordBox.BorderThickness = new Thickness(3.0);
                this.confirmUserPasswordBox.BorderBrush = Brushes.Green;
            } else {

                //If the password does not match, set passwords match label to visible;
                this.lblPasswordsMatch.Content = "Passwords don't match";
                this.lblPasswordsMatch.Visibility = Visibility.Visible;
                this.lblPasswordsMatch.Background = Brushes.Red;


                //Confirm password box turns red
                this.confirmUserPasswordBox.BorderThickness = new Thickness(3.0);
                this.confirmUserPasswordBox.BorderBrush = Brushes.Red;
            }
        }
        #endregion

        #region KeyUp Events
        private void FirstNameTextBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e) {
            if (e.Key == System.Windows.Input.Key.Down) {
                this.lastNameTextBox.Focus();
                e.Handled = true;
            } else if (e.Key == System.Windows.Input.Key.Enter) {
                this.lastNameTextBox.Focus();
                e.Handled = true;
            }
        }

        private void LastNameTextBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e) {
            if (e.Key == System.Windows.Input.Key.Up) {
                this.firstNameTextBox.Focus();
                e.Handled = true;
            } else if (e.Key == System.Windows.Input.Key.Enter) {
                this.emailTextBox.Focus();
                e.Handled = true;
            } else if (e.Key == System.Windows.Input.Key.Down) {
                this.emailTextBox.Focus();
                e.Handled = true;
            }
        }

        private void EmailTextBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e) {
            if (e.Key == System.Windows.Input.Key.Up) {
                this.lastNameTextBox.Focus();
                e.Handled = true;
            } else if (e.Key == System.Windows.Input.Key.Enter) {
                this.phoneTextBox.Focus();
                e.Handled = true;
            } else if (e.Key == System.Windows.Input.Key.Down) {
                this.phoneTextBox.Focus();
                e.Handled = true;
            }
        }

        private void PhoneTextBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e) {
            if (e.Key == System.Windows.Input.Key.Up) {
                this.emailTextBox.Focus();
                e.Handled = true;
            } else if (e.Key == System.Windows.Input.Key.Enter) {
                this.userPasswordBox.Focus();
                e.Handled = true;
            } else if (e.Key == System.Windows.Input.Key.Down) {
                this.userPasswordBox.Focus();
                e.Handled = true;
            }
        }

        private void UserPasswordBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e) {
            if (e.Key == System.Windows.Input.Key.Up) {
                this.phoneTextBox.Focus();
                e.Handled = true;
            } else if (e.Key == System.Windows.Input.Key.Enter) {
                this.confirmUserPasswordBox.Focus();
                e.Handled = true;
            } else if (e.Key == System.Windows.Input.Key.Down) {
                this.confirmUserPasswordBox.Focus();
                e.Handled = true;
            }
        }

        private void ConfirmUserPasswordBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e) {
            if (e.Key == System.Windows.Input.Key.Up) {
                this.userPasswordBox.Focus();
                e.Handled = true;
            } else if (e.Key == System.Windows.Input.Key.Enter) {
                this.securityQuestionComboBox.Focus();
                e.Handled = true;
            } else if (e.Key == System.Windows.Input.Key.Down) {
                this.securityQuestionComboBox.Focus();
                e.Handled = true;
            }
        }

        private void SecurityQuestionComboBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e) {
            if (e.Key == System.Windows.Input.Key.Up) {
                this.confirmUserPasswordBox.Focus();
                e.Handled = true;
            } else if (e.Key == System.Windows.Input.Key.Enter) {
                this.securityQuestionTextBox.Focus();
                e.Handled = true;
            } else if (e.Key == System.Windows.Input.Key.Down) {
                this.securityQuestionTextBox.Focus();
                e.Handled = true;
            }
        }

        private void SecurityQuestionTextBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e) {
            if (e.Key == System.Windows.Input.Key.Up) {
                this.securityQuestionComboBox.Focus();
                e.Handled = true;
            } else if (e.Key == System.Windows.Input.Key.Enter) {
                this.createAccountSubmitButton.Focus();
                e.Handled = true;
            } else if (e.Key == System.Windows.Input.Key.Down) {
                this.createAccountSubmitButton.Focus();
                e.Handled = true;
            }
        }

        private void CreateAccountSubmitButton_KeyUp(object sender, System.Windows.Input.KeyEventArgs e) {
            if (e.Key == System.Windows.Input.Key.Up) {
                this.securityQuestionTextBox.Focus();
                e.Handled = true;
            } else if (e.Key == System.Windows.Input.Key.Enter) {
                this.loginButton.Focus();
                e.Handled = true;
            } else if (e.Key == System.Windows.Input.Key.Down) {
                this.loginButton.Focus();
                e.Handled = true;
            }
        }

        private void LoginButton_KeyUp(object sender, System.Windows.Input.KeyEventArgs e) {
            if (e.Key == System.Windows.Input.Key.Up) {
                this.createAccountSubmitButton.Focus();
                e.Handled = true;
            } else if (e.Key == System.Windows.Input.Key.Enter) {
                this.loginButton.Focus();
                e.Handled = true;
            }
        }
        #endregion

        private void LoginButton_Click(object sender, RoutedEventArgs e) {
            Window window = new LoginWindow();
            this.windowSettings.TransitionScreen(window, this);
        }
    }
}
