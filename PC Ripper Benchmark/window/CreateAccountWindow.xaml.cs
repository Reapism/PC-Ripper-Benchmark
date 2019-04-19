using PC_Ripper_Benchmark.database;
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
        private function.WindowSettings settings = new function.WindowSettings();
        private Popup codePopup = new Popup();
        private TextBlock popupContent = new TextBlock();
        private function.WindowSettings windowSettings = new function.WindowSettings();

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

            this.createAccountSubmitButton.BorderThickness = new Thickness(3);
            this.goBackButton.BorderThickness = new Thickness(3);

            this.createAccountSubmitButton.BorderBrush = Brushes.White;
            this.goBackButton.BorderBrush = Brushes.White;
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
                util.HashManager encrypter = new util.HashManager();

                util.UserData newUser = new util.UserData {
                    //Encrypt user data and set to newUser object
                    FirstName = this.firstNameTextBox.Text,
                    LastName = this.lastNameTextBox.Text,
                    Email = encrypter.HashTextSHA256(this.emailTextBox.Text),
                    PhoneNumber = encrypter.HashTextSHA256(this.phoneTextBox.Text),
                    Password = encrypter.HashUniqueTextSHA256(this.userPasswordBox.Password),
                    SecurityQuestion = this.securityQuestionComboBox.Text,
                    SecurityQuestionAnswer = encrypter.HashTextSHA256(this.securityQuestionTextBox.Text)
                };

                // SQL Connection String
                string connectionString = DatabaseConnection.GetConnectionString();


                // Open database connection and send that data to the database hashed.
                DatabaseConnection dbConnection = new DatabaseConnection(connectionString);

                // checks if account exists.
                if (dbConnection.CheckAccountExists(newUser.Email, newUser.Password)) {
                    MessageBox.Show("Account already exists with this email", "Account Exists",
                        MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    return;
                } else {
                    var questionnaire = new QuestionaireWindow(ref newUser);
                    // If you do not press finish, show the dialog again.
                    while (questionnaire.ShowDialog() == false) {
                        questionnaire = new QuestionaireWindow(ref newUser,
                            "Please press finish to confirm your account.");
                    }
                }

                try {
                    if (dbConnection.AddUserToDatabase(newUser)) {
                        LoginWindow loginWindow = new LoginWindow();
                        this.windowSettings.TransitionScreen(loginWindow, this);
                        
                    }
                } catch (SqlException) {
                    throw new Exception();
                }
            }
            #endregion
        }


        /// <summary>
        /// Event handler for GoBackButton click in <see cref="CreateAccountWindow"/>.
        /// <para>When GoBack is clicked, the window goes back to the
        /// login screen <see cref="window.CreateAccountWindow"/></para>
        /// </summary>
        /// 
        private void GoBackButton_Click(object sender, RoutedEventArgs e) {
            Window window = new LoginWindow();
            this.windowSettings.TransitionScreen(window, this);
        }

        #region Border Color First Name
        /// <summary>
        /// Event handler for FirstNameTextBox "GotFocus" event in <see cref="CreateAccountWindow"/>.
        /// <para>When the first name textbox is focused  and contains a valid/invalid name, the border color and thickness 
        /// change<see cref="window.CreateAccountWindow"/></para>
        /// </summary>
        /// 
        private void FirstNameTextBox_GotFocus(object sender, RoutedEventArgs e) {
            if (util.RegexUtilities.IsValidName(this.firstNameTextBox.Text)) {
                this.firstNameTextBox.BorderThickness = new Thickness(3.0);
                this.firstNameTextBox.BorderBrush = Brushes.Green;
            } else {
                this.firstNameTextBox.BorderThickness = new Thickness(3.0);
                this.firstNameTextBox.BorderBrush = Brushes.Red;
            }
        }

        /// <summary>
        /// Event handler for FirstNameTextBox "LostFocus" event in <see cref="CreateAccountWindow"/>.
        /// <para>When the first name textbox loses focused and contains a valid/invalid name, the border color and thickness 
        /// change<see cref="window.CreateAccountWindow"/></para>
        /// </summary>
        /// 
        private void FirstNameTextBox_LostFocus(object sender, RoutedEventArgs e) {
            if (util.RegexUtilities.IsValidName(this.firstNameTextBox.Text)) {
                this.firstNameTextBox.BorderThickness = new Thickness(3.0);
                this.firstNameTextBox.BorderBrush = Brushes.Green;
            } else {
                this.firstNameTextBox.BorderThickness = new Thickness(3.0);
                this.firstNameTextBox.BorderBrush = Brushes.Red;
            }
        }

        /// <summary>
        /// Event handler for FirstNameTextBox "TextChanged" event in <see cref="CreateAccountWindow"/>.
        /// <para>When the first name textbox changes, and contains a valid/invalid name, the border color and thickness 
        /// change<see cref="window.CreateAccountWindow"/></para>
        /// </summary>
        /// 
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
        /// <summary>
        /// Event handler for LastNameTextBox "GotFocus" event in <see cref="CreateAccountWindow"/>.
        /// <para>When the last name textbox is focused  and contains a valid/invalid name, the border color and thickness 
        /// change<see cref="window.CreateAccountWindow"/></para>
        /// </summary>
        /// 
        private void LastNameTextBox_GotFocus(object sender, RoutedEventArgs e) {
            if (util.RegexUtilities.IsValidName(this.lastNameTextBox.Text)) {
                this.lastNameTextBox.BorderThickness = new Thickness(3.0);
                this.lastNameTextBox.BorderBrush = Brushes.Green;
            } else {
                this.lastNameTextBox.BorderThickness = new Thickness(3.0);
                this.lastNameTextBox.BorderBrush = Brushes.Red;
            }
        }

        /// <summary>
        /// Event handler for LastNameTextBox "LostFocus" event in <see cref="CreateAccountWindow"/>.
        /// <para>When the last name textbox loses focus and contains a valid/invalid name, the border color and thickness 
        /// change<see cref="window.CreateAccountWindow"/></para>
        /// </summary>
        /// 
        private void LastNameTextBox_LostFocus(object sender, RoutedEventArgs e) {
            if (util.RegexUtilities.IsValidName(this.lastNameTextBox.Text)) {
                this.lastNameTextBox.BorderThickness = new Thickness(3.0);
                this.lastNameTextBox.BorderBrush = Brushes.Green;
            } else {
                this.lastNameTextBox.BorderThickness = new Thickness(3.0);
                this.lastNameTextBox.BorderBrush = Brushes.Red;
            }
        }

        /// <summary>
        /// Event handler for LastNameTextBox "TextChanged" event in <see cref="CreateAccountWindow"/>.
        /// <para>When the last name textbox changes, and contains a valid/invalid name, the border color and thickness 
        /// change<see cref="window.CreateAccountWindow"/></para>
        /// </summary>
        /// 
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
        /// <summary>
        /// Event handler for EmailTextBox "LostFocus" event in <see cref="CreateAccountWindow"/>.
        /// <para>When the first name textbox loses focus and contains a valid/invalid email, the border color and thickness 
        /// change<see cref="window.CreateAccountWindow"/></para>
        /// </summary>
        /// 
        private void EmailTextBox_LostFocus(object sender, RoutedEventArgs e) {
            if (util.RegexUtilities.IsValidEmail(this.emailTextBox.Text)) {
                this.emailTextBox.BorderThickness = new Thickness(3.0);
                this.emailTextBox.BorderBrush = Brushes.Green;
            } else {
                this.emailTextBox.BorderThickness = new Thickness(3.0);
                this.emailTextBox.BorderBrush = Brushes.Red;
            }
        }

        /// <summary>
        /// Event handler for EmailTextBox "GotFocus" event in <see cref="CreateAccountWindow"/>.
        /// <para>When the first email textbox is focused  and contains a valid/invalid email, the border color and thickness 
        /// change<see cref="window.CreateAccountWindow"/></para>
        /// </summary>
        /// 
        private void EmailTextBox_GotFocus(object sender, RoutedEventArgs e) {
            if (util.RegexUtilities.IsValidEmail(this.emailTextBox.Text)) {
                this.emailTextBox.BorderThickness = new Thickness(3.0);
                this.emailTextBox.BorderBrush = Brushes.Green;
            } else {
                this.emailTextBox.BorderThickness = new Thickness(3.0);
                this.emailTextBox.BorderBrush = Brushes.Red;
            }
        }

        /// <summary>
        /// Event handler for EmailTextBox "TextChanged" event in <see cref="CreateAccountWindow"/>.
        /// <para>When the email name textbox changes, and contains a valid/invalid email, the border color and thickness 
        /// change<see cref="window.CreateAccountWindow"/></para>
        /// </summary>
        /// 
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
        /// <summary>
        /// Event handler for PhoneTextBox "GotFocus" event in <see cref="CreateAccountWindow"/>.
        /// <para>When the phone textbox is focused  and contains a valid/invalid phone number, the border color and thickness 
        /// change<see cref="window.CreateAccountWindow"/></para>
        /// </summary>
        /// 
        private void PhoneTextBox_GotFocus(object sender, RoutedEventArgs e) {
            if (util.RegexUtilities.IsValidPhoneNumber(this.phoneTextBox.Text)) {
                this.phoneTextBox.BorderThickness = new Thickness(3.0);
                this.phoneTextBox.BorderBrush = Brushes.Green;
            } else {
                this.phoneTextBox.BorderThickness = new Thickness(3.0);
                this.phoneTextBox.BorderBrush = Brushes.Red;
            }
        }

        /// <summary>
        /// Event handler for PhoneTextBox "TextChanged" event in <see cref="CreateAccountWindow"/>.
        /// <para>When the phone textbox changes, and contains a valid/invalid phone number, the border color and thickness 
        /// change<see cref="window.CreateAccountWindow"/></para>
        /// </summary>
        /// 
        private void PhoneTextBox_TextChanged(object sender, TextChangedEventArgs e) {
            if (util.RegexUtilities.IsValidPhoneNumber(this.phoneTextBox.Text)) {
                this.phoneTextBox.BorderThickness = new Thickness(3.0);
                this.phoneTextBox.BorderBrush = Brushes.Green;
            } else {
                this.phoneTextBox.BorderThickness = new Thickness(3.0);
                this.phoneTextBox.BorderBrush = Brushes.Red;
            }
        }


        /// <summary>
        /// Event handler for PhoneTextBox "LostFocus" event in <see cref="CreateAccountWindow"/>.
        /// <para>When the phone textbox loses focus and contains a valid/invalid phone number, the border color and thickness 
        /// change<see cref="window.CreateAccountWindow"/></para>
        /// </summary>
        /// 
        private void PhoneTextBox_LostFocus(object sender, RoutedEventArgs e) {
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
        /// <summary>
        /// Event handler for UserPasswordBox "GotFocus" event in <see cref="CreateAccountWindow"/>.
        /// <para>When the password passwordBox is focused and contains a valid/invalid password, the border color and thickness 
        /// change<see cref="window.CreateAccountWindow"/></para>
        /// </summary>
        /// 
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


        /// <summary>
        /// Event handler for UserPasswordBox "LostFocus" event in <see cref="CreateAccountWindow"/>.
        /// <para>When the password passwordBox loses focus and contains a valid/invalid password, the border color and thickness 
        /// change<see cref="window.CreateAccountWindow"/></para>
        /// </summary>
        /// 
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

        // <summary>
        /// Event handler for UserPasswordBox "PasswordChanged" event in <see cref="CreateAccountWindow"/>.
        /// <para>When the password passwordBox changes, and contains a valid/invalid password, the border color and thickness 
        /// change<see cref="window.CreateAccountWindow"/></para>
        /// </summary>
        /// 
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
        // <summary>
        /// Event handler for ConfirmUserPasswordBox "PasswordChanged" event in <see cref="CreateAccountWindow"/>.
        /// <para>When the confirmUserpassword passwordBox changes, and contains a valid/invalid password, the border color and thickness 
        /// change<see cref="window.CreateAccountWindow"/></para>
        /// </summary>
        /// 
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

        private void CreateAccountSubmitButton_KeyDown(object sender, System.Windows.Input.KeyEventArgs e) {
            if (e.Key == System.Windows.Input.Key.Up) {
                this.securityQuestionTextBox.Focus();
                e.Handled = true;
            } else if (e.Key == System.Windows.Input.Key.Enter) {
                this.goBackButton.Focus();
                e.Handled = true;
            } else if (e.Key == System.Windows.Input.Key.Down) {
                this.goBackButton.Focus();
                e.Handled = true;
            }
        }

        private void GoBackButton_KeyUp(object sender, System.Windows.Input.KeyEventArgs e) {
            if (e.Key == System.Windows.Input.Key.Up) {
                this.createAccountSubmitButton.Focus();
                e.Handled = true;
            } else if (e.Key == System.Windows.Input.Key.Enter) {
                this.goBackButton.Focus();
                e.Handled = true;
            }
        }
        #endregion

        #region Got/Lost Focus Events Buttons
        private void CreateAccountSubmitButton_GotFocus(object sender, RoutedEventArgs e) {
            this.createAccountSubmitButton.BorderBrush = Brushes.Black;
            this.createAccountSubmitButton.Foreground = Brushes.Black;
        }

        private void GoBackButton_GotFocus(object sender, RoutedEventArgs e) {
            this.goBackButton.BorderBrush = Brushes.Black;
            this.goBackButton.Foreground = Brushes.Black;
        }

        private void CreateAccountSubmitButton_LostFocus(object sender, RoutedEventArgs e) {
            this.createAccountSubmitButton.BorderBrush = Brushes.White;
            this.createAccountSubmitButton.Foreground = Brushes.White;
        }

        private void GoBackButton_LostFocus(object sender, RoutedEventArgs e) {
            this.goBackButton.BorderBrush = Brushes.White;
            this.goBackButton.Foreground = Brushes.White;
        }
        #endregion
        #region Mouse Enter/Leave Events
        private void CreateAccountSubmitButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e) {
            this.createAccountSubmitButton.Foreground = Brushes.Black;
            this.createAccountSubmitButton.BorderBrush = Brushes.Black;
        }

        private void GoBackButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e) {
            this.goBackButton.Foreground = Brushes.Black;
            this.goBackButton.BorderBrush = Brushes.Black;
        }

        private void CreateAccountSubmitButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e) {
            this.goBackButton.Foreground = Brushes.White;
            this.goBackButton.BorderBrush = Brushes.White;
        }

        private void GoBackButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e) {
            this.goBackButton.Foreground = Brushes.White;
            this.goBackButton.BorderBrush = Brushes.White;
        }
        #endregion

        // TEMPORARY
        private void CreateAccountSubmitButton_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            this.firstNameTextBox.Text = "Anthony";
            this.lastNameTextBox.Text = "Jaghab";
            this.emailTextBox.Text = "reap@gmail.com";
            this.phoneTextBox.Text = "516-605-5552";
            this.userPasswordBox.Password = "Anthony1!";
            this.confirmUserPasswordBox.Password = "Anthony1!";
            this.securityQuestionComboBox.SelectedIndex = 0;
            this.securityQuestionTextBox.Text = "poggers";
        }
    }
}
