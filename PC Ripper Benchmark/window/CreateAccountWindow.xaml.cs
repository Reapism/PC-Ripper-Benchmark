using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.Windows.Media.Animation;

namespace PC_Ripper_Benchmark
{
    /// <summary>
    /// The <see cref="CreateAccountWindow"/> class.
    /// <para></para>
    /// Contains all methods and properties about 
    /// the Create Account Window.
    /// <para>Author: David Hartglass (c), all rights reserved.</para>
    /// </summary>
    public partial class CreateAccountWindow : Window
    {
        //Global window settings class object
        function.WindowSettings settings = new function.WindowSettings();

        /// <summary>
        /// Default constructor in <see cref="CreateAccountWindow"/>.
        /// <para>Creates a window of type CreateWindow
        /// and calls CenterWindowOnScreen to center the window</para>
        /// </summary>
        public CreateAccountWindow()
        {
            InitializeComponent();       
            settings.CenterWindowOnScreen(this.windowCreateAccount);
        }
                
        /// <summary>
        /// Event handler for button click in <see cref="CreateAccountWindow"/>.
        /// <para>When Submit is clicked, the text fields check to make sure valid data is entered
        /// using regular expressions in <see cref="util.RegexUtilities"/></para>
        /// </summary>
        /// 

        private void CreateAccountSubmitButton_Click(object sender, RoutedEventArgs e)
        {
            #region TextField Error Checking
            String errorMessage = null;
            if (string.IsNullOrWhiteSpace(firstNameTextBox.Text))
            {
                errorMessage += " \"First Name\" ";
            }

            if (string.IsNullOrWhiteSpace(lastNameTextBox.Text))
            {
                errorMessage += " \"Last Name\" ";
            }

            if (string.IsNullOrWhiteSpace(emailTextBox.Text))
            {
                errorMessage += " \"Email\" ";
            }

            if (string.IsNullOrWhiteSpace(userPasswordBox.Password))
            {
                errorMessage += " \"Password\" ";
            }

            if (string.IsNullOrWhiteSpace(confirmUserPasswordBox.Password))
            {
                errorMessage += " \"Password\" ";
            }
            else if (confirmUserPasswordBox.Password != userPasswordBox.Password)
            {
                MessageBox.Show("Password do not match. Please try again.");
            }

            if (!(errorMessage == null))
            {
                MessageBox.Show($"{errorMessage} field(s) missing value");
            }
            #endregion

            #region Regular Expression Checks

            if (!util.RegexUtilities.IsValidEmail(emailTextBox.Text))
            {
                MessageBox.Show("Invalid email!");
            }

            if (!util.RegexUtilities.isValidPhoneNumber(phoneTextBox.Text))
            {
                MessageBox.Show("Invalid phone number!");
            }

            if (!util.RegexUtilities.IsValidPassword(userPasswordBox.Password))
            {
                MessageBox.Show("Invalid password!");
            }

            if (userPasswordBox.Password != confirmUserPasswordBox.Password)
            {
                MessageBox.Show("Passwords do not match!");
            }

            if (!util.RegexUtilities.IsValidName(firstNameTextBox.Text))
            {
                MessageBox.Show("Invalid characters in first name!");           
            }

            if (!util.RegexUtilities.IsValidName(lastNameTextBox.Text))
            {
                MessageBox.Show("Invalid characters in last name!");
            }

            if (util.RegexUtilities.isValidPhoneNumber(phoneTextBox.Text) &&
               util.RegexUtilities.IsValidName(firstNameTextBox.Text) &&
               util.RegexUtilities.IsValidName(lastNameTextBox.Text) &&
               util.RegexUtilities.IsValidEmail(emailTextBox.Text) &&
               util.RegexUtilities.isValidPhoneNumber(phoneTextBox.Text) &&
               util.RegexUtilities.IsValidPassword(userPasswordBox.Password) &&
               userPasswordBox.Password == confirmUserPasswordBox.Password)
            {
                MessageBox.Show("Account Created!");
                util.UserData newUser = new util.UserData();
                newUser.FirstName = firstNameTextBox.Text;
                newUser.LastName = lastNameTextBox.Text;
                newUser.Email = emailTextBox.Text;
                newUser.PhoneNumber = phoneTextBox.Text;
                newUser.Password = userPasswordBox.Password;

                //byte[] passwordProtected = userPasswordBox.Password;
            }
            #endregion

        }
    }
}
