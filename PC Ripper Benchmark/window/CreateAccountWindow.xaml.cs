using System;
using System.Windows;
using System.Windows.Media.Animation;
using PC_Ripper_Benchmark.util;

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
        WindowSettings settings = new WindowSettings();

        /// <summary>
        /// Default constructor in <see cref="CreateAccountWindow"/>.
        /// <para>Creates a window of type CreateWindow
        /// and calls CenterWindowOnScreen to center the window</para>
        /// </summary>

        public CreateAccountWindow() {
            InitializeComponent();
            this.settings.CenterWindowOnScreen(this.windowCreateAccount);
        }

        /// <summary>
        /// Event handler for button click in <see cref="CreateAccountWindow"/>.
        /// <para>When Submit is clicked, the text fields check to make sure valid data is entered
        /// using regular expressions in <see cref="util.RegexUtilities"/></para>
        /// </summary>

        private void CreateAccountSubmitButton_Click(object sender, RoutedEventArgs e) {
            #region TextField Error Checking
            string errorMessage = null;
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
            }

            if (!util.RegexUtilities.IsValidPassword(this.userPasswordBox.Password)) {
                MessageBox.Show("Invalid password!");
            }

            if (!util.RegexUtilities.IsValidName(this.firstNameTextBox.Text)) {
                MessageBox.Show("Invalid characters in first name!");
            }

            if (!util.RegexUtilities.IsValidName(this.lastNameTextBox.Text)) {
                MessageBox.Show("Invalid characters in last name!");
            }
            #endregion

            LoginWindow loginWindow = new LoginWindow();
            DoubleAnimation openScreen = new DoubleAnimation();

            //settings.TransitionToLoginScreen(loginWindow, this, openScreen);
        }
    }
}
