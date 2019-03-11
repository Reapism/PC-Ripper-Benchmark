using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace PC_Ripper_Benchmark {
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
                MessageBox.Show("Password do not match. Please try again.");
            }

            if (!(errorMessage == null)) {
                MessageBox.Show($"{errorMessage} field(s) missing value");
            }
            #endregion
            #region Regular Expression Checks

            if (!util.RegexUtilities.IsValidEmail(this.emailTextBox.Text)) {
                MessageBox.Show("Invalid email!");
                this.emailTextBox.Focus();
            }


            if (!util.RegexUtilities.IsValidPhoneNumber(this.phoneTextBox.Text)) {
                MessageBox.Show("Invalid phone number!");
                this.phoneTextBox.Focus();
            }

            if (!util.RegexUtilities.IsValidPassword(this.userPasswordBox.Password)) {
                MessageBox.Show("Invalid password!");
                this.userPasswordBox.Focus();

            }

            if (this.userPasswordBox.Password != this.confirmUserPasswordBox.Password) {
                lblConfirmPassword.Content = "Passwords don't match";
                lblConfirmPassword.Visibility = Visibility.Visible;
                this.confirmUserPasswordBox.Focus();
            }

            if (!util.RegexUtilities.IsValidName(this.firstNameTextBox.Text)) {
                MessageBox.Show("Invalid characters in first name!");
                this.firstNameTextBox.Focus();
            }

            if (!util.RegexUtilities.IsValidName(this.lastNameTextBox.Text)) {
                MessageBox.Show("Invalid characters in last name!");
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
                util.Encryption passwordEncryption = new util.Encryption();

                MessageBox.Show("Account Created!");
                util.UserData newUser = new util.UserData {
                    //Encrypt user data and set to newUser object
                    FirstName = passwordEncryption.EncryptText(this.firstNameTextBox.Text),
                    LastName = passwordEncryption.EncryptText(this.lastNameTextBox.Text),
                    Email = passwordEncryption.EncryptText(this.emailTextBox.Text),
                    PhoneNumber = passwordEncryption.EncryptText(this.phoneTextBox.Text)
                };
                newUser.Password = passwordEncryption.EncryptText(newUser.Password);
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
                lblPasswordsMatch.Content = "Passwords match";
                lblPasswordsMatch.Visibility = Visibility.Visible;
                lblPasswordsMatch.Background = Brushes.Green;
            
                //Confirm password box turns green
                this.confirmUserPasswordBox.BorderThickness = new Thickness(3.0);
                this.confirmUserPasswordBox.BorderBrush = Brushes.Green;
            }
            else {

                //If the password does not match, set passwords match label to visible;
                lblPasswordsMatch.Content = "Passwords don't match";
                lblPasswordsMatch.Visibility = Visibility.Visible;
                lblPasswordsMatch.Background = Brushes.Red;


                //Confirm password box turns red
                this.confirmUserPasswordBox.BorderThickness = new Thickness(3.0);
                this.confirmUserPasswordBox.BorderBrush = Brushes.Red;
            }
        }
        #endregion

        #region KeyDown Events
        private void FirstNameTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Down)
            {
                lastNameTextBox.Focus();
            }
        }


        private void LastNameTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Down)
            {
                emailTextBox.Focus();
                e.Handled = true;
            }           
        }


        private void EmailTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Down)
            {
                phoneTextBox.Focus();
                e.Handled = true;

            }
        }

        private void PhoneTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Down)
            {
                userPasswordBox.Focus();
                e.Handled = true;
            }
        }

        private void UserPasswordBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Down)
            {
                confirmUserPasswordBox.Focus();
                e.Handled = true;
            }
        }

        private void ConfirmUserPasswordBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Down)
            {
                createAccountSubmitButton.Focus();
                e.Handled = true;
            }
        }
        #endregion
        #region KeyUp Events
        private void LastNameTextBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Up)
            {
                firstNameTextBox.Focus();
                e.Handled = true;
            }
        }

        private void EmailTextBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Up)
            {
                lastNameTextBox.Focus();
                e.Handled = true;
            }
        }

        private void PhoneTextBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Up)
            {
                emailTextBox.Focus();
                e.Handled = true;
            }
        }
             
        private void UserPasswordBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Up)
            {
                phoneTextBox.Focus();
                e.Handled = true;
            }
        }    
        
        private void ConfirmUserPasswordBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Up)
            {
                userPasswordBox.Focus();
                e.Handled = true;
            }
        }

        private void CreateAccountSubmitButton_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                confirmUserPasswordBox.Focus();
                e.Handled = true;
            }
        }
        #endregion
    }
}
