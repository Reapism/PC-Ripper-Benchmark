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
        /// <summary>
        /// Default constructor in <see cref="CreateAccountWindow"/>.
        /// <para>Creates a window of type CreateWindow
        /// and calls CenterWindowOnScreen to center the window</para>
        /// </summary>
        public CreateAccountWindow()
        {
            InitializeComponent();
            CenterWindowOnScreen();
        }

        /// <summary>
        /// Method in <see cref="CreateAccountWindow"/>.
        /// <para>Centers the window on the screen</para>
        /// </summary>
        /// 

        private void CenterWindowOnScreen()
        {
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;

            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
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
               
            if (!util.RegexUtilities.isValidPassword(userPasswordBox.Password))
            {
                MessageBox.Show("Invalid password!" + userPasswordBox.Password);
            }                    
            #endregion
        }
    }
}
