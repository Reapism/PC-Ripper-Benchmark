using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PC_Ripper_Benchmark.window
{
    /// <summary>
    /// Interaction logic for PasswordResetWindow.xaml
    /// </summary>
    public partial class PasswordResetWindow : Window
    {
        int count = 3;
        util.Encryption encryption = new util.Encryption();
        Popup codePopup = new Popup();
        TextBlock popupContent = new TextBlock();
        function.SystemSettings networkConnection = new function.SystemSettings();

        string connectionString  = Properties.Settings.Default.Connection_String;

        public PasswordResetWindow()
        {
            InitializeComponent();
            lblSecurityQuestion.Opacity = 0;
            securityQuestionAnswerTextBox.Opacity = 0;
            securityQuestionAnswerTextBox.Opacity = 0;

            //newPasswordLabel & Box defaults
            newPasswordLabel.Opacity = 0;
            newPasswordLabel.Visibility = Visibility.Collapsed;
            newPasswordBox.Opacity = 0;
            newPasswordBox.Visibility = Visibility.Collapsed;

            //confirmNewPasswordBox defaults
            confirmNewPasswordBox.Opacity = 0;
            confirmNewPasswordBox.Visibility = Visibility.Collapsed;
            confirmPasswordLabel.Opacity = 0;
            confirmPasswordLabel.Visibility = Visibility.Collapsed;

            //Security answer button
            confirmSecurityAnswerButton.Opacity = 0;
            confirmSecurityAnswerButton.Visibility = Visibility.Collapsed;
            emailTextBox.Focus();

            //Done button
            doneButton.Opacity = 0;
            doneButton.Visibility = Visibility.Collapsed;

        }

        private void ConfirmEmailButton_Click(object sender, RoutedEventArgs e)
        {           
            try
            {
                if (networkConnection.IsInternetAvailable() == true)
                {
                    //Open new database connection
                    SqlConnection connection = new SqlConnection(connectionString);
                    database.DatabaseConnection newConnection = new database.DatabaseConnection(connection.ConnectionString);

                    //If the email exists in the database
                    if (newConnection.CheckEmailExists(connection, emailTextBox.Text))
                    {
                        connection.Open();

                        //Show the security question labels and fields
                        lblSecurityQuestion.Opacity = 100;
                        lblSecurityQuestion.Visibility = Visibility.Visible;
                        securityQuestionAnswerTextBox.Opacity = 100;
                        securityQuestionAnswerTextBox.Visibility = Visibility.Visible;
                        confirmSecurityAnswerButton.Visibility = Visibility.Visible;

                        //Command to get the actual answered security question
                        SqlCommand getSecurityQuestion = new SqlCommand("SELECT SecurityQuestion FROM Customer where Email=@Email", connection);
                        string email = encryption.EncryptText(emailTextBox.Text.ToUpper().Trim());
                        securityQuestionAnswerTextBox.Focus();

                        //Fill the parameter of the query
                        getSecurityQuestion.Parameters.AddWithValue("@Email", email);
                        lblSecurityQuestion.Content = (string)getSecurityQuestion.ExecuteScalar();

                        confirmEmailButton.Visibility = Visibility.Collapsed;

                        //Command to get the security question answer for comparison
                        SqlCommand getSecurityQuestionAnswer = new SqlCommand("SELECT SecurityQuestionAnswer FROM Customer where Email=@Email", connection);
                        string securityAnswer = encryption.EncryptText(securityQuestionAnswerTextBox.Text.ToUpper().Trim());

                        getSecurityQuestionAnswer.Parameters.AddWithValue("@Email", email);

                        //Set the answer returned by the query to a variable for comparison
                        string answer = (string)getSecurityQuestionAnswer.ExecuteScalar();

                        confirmSecurityAnswerButton.Opacity = 100;

                        //Make the confirmed field read only
                        emailTextBox.IsReadOnly = true;
                        emailTextBox.Background = Brushes.SlateGray;

                        connection.Close();
                    }
                }
                
            }
            catch (SqlException)
            {
                MessageBox.Show("A SQL Error was caught", "Error!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            
        }

        #region Confirm the security answer
        private void ConfirmSecurityAnswer_Click(object sender, RoutedEventArgs e)
        {
            if (networkConnection.IsInternetAvailable() == true)
            {
                //Open new database connection
                SqlConnection connection = new SqlConnection(connectionString);
                database.DatabaseConnection newConnection = new database.DatabaseConnection(connection.ConnectionString);
                connection.Open();

                //Command to get the actual answered security question
                string email = encryption.EncryptText(emailTextBox.Text.ToUpper().Trim());

                //Command to get the security question answer for comparison
                SqlCommand getSecurityQuestionAnswer = new SqlCommand("SELECT SecurityQuestionAnswer FROM Customer where Email=@Email", connection);
                string securityAnswer = encryption.EncryptText(securityQuestionAnswerTextBox.Text);

                getSecurityQuestionAnswer.Parameters.AddWithValue("@Email", email);

                //Set the answer returned by the query to a variable for comparison
                string answer = (string)getSecurityQuestionAnswer.ExecuteScalar();

                if (encryption.EncryptText(securityQuestionAnswerTextBox.Text) == answer)
                {
                    newPasswordLabel.Opacity = 100;
                    newPasswordLabel.Visibility = Visibility.Visible;
                    newPasswordBox.Opacity = 100;
                    newPasswordBox.Visibility = Visibility.Visible;
                    confirmNewPasswordBox.Opacity = 100;
                    confirmNewPasswordBox.Visibility = Visibility.Visible;
                    confirmPasswordLabel.Opacity = 100;
                    confirmPasswordLabel.Visibility = Visibility.Visible;
                    newPasswordLabel.Focus();

                    securityQuestionAnswerTextBox.IsReadOnly = true;
                    securityQuestionAnswerTextBox.Background = Brushes.SlateGray;
                    confirmSecurityAnswerButton.Visibility = Visibility.Collapsed;
                    doneButton.Opacity = 100;
                    doneButton.Visibility = Visibility.Visible;
                }
                else
                {
                    count--;
                    MessageBox.Show($"The answer entered is incorrect. You have {count} attempts left.", "Incorrect Answer", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    securityQuestionAnswerTextBox.Clear();
                    securityQuestionAnswerTextBox.Focus();
                }

                if (count == 0)
                {
                    LoginWindow loginWindow = new LoginWindow();
                    function.WindowSettings windowSettings = new function.WindowSettings();

                    windowSettings.TransitionScreen(loginWindow, this);
                }
                connection.Close();
            }            
        }
        #endregion

        #region Done Button
        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            //Open new database connection
            SqlConnection connection = new SqlConnection(connectionString);
            database.DatabaseConnection newConnection = new database.DatabaseConnection(connection.ConnectionString);
            connection.Open();

            //Command to get the actual answered security question
            string email = encryption.EncryptText(emailTextBox.Text.ToUpper().Trim());

            if (util.RegexUtilities.IsValidPassword(this.newPasswordBox.Password) &&
                   this.newPasswordBox.Password == this.confirmNewPasswordBox.Password)
            {
                SqlCommand changePassword = new SqlCommand("UPDATE Customer SET Password = @Password WHERE Email=@Email", connection);
                //Fill the parameter of the query
                changePassword.Parameters.AddWithValue("@Email", email);
                changePassword.Parameters.AddWithValue("@Password", encryption.EncryptText(confirmNewPasswordBox.Password));
                changePassword.ExecuteNonQuery();

                MessageBox.Show("Password Changed", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                LoginWindow loginWindow = new LoginWindow();
                function.WindowSettings windowSettings = new function.WindowSettings();

                windowSettings.TransitionScreen(loginWindow, this);
            }
        }
        #endregion

        #region Passwords match indicator events
        private void ConfirmNewPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.newPasswordBox.Password == this.confirmNewPasswordBox.Password)
            {

                //If the password matches, set passwords match label to green border and visible
                this.lblPasswordsMatch.Content = "Passwords match";
                this.lblPasswordsMatch.Visibility = Visibility.Visible;
                this.lblPasswordsMatch.Background = Brushes.Green;

                //Confirm password box turns green
                this.confirmNewPasswordBox.BorderThickness = new Thickness(3.0);
                this.confirmNewPasswordBox.BorderBrush = Brushes.Green;
            }
            else
            {

                //If the password does not match, set passwords match label to visible;
                this.lblPasswordsMatch.Content = "Passwords don't match";
                this.lblPasswordsMatch.Visibility = Visibility.Visible;
                this.lblPasswordsMatch.Background = Brushes.Red;


                //Confirm password box turns red
                this.confirmNewPasswordBox.BorderThickness = new Thickness(3.0);
                this.confirmNewPasswordBox.BorderBrush = Brushes.Red;
            }            
        }

        private void NewPasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {       
            //Sets the popup values, background property,
            this.popupContent.FontSize = 10;
            this.codePopup.PlacementTarget = this.newPasswordBox;
            this.popupContent.Text = "Password must contain: \n-One uppercase letter" +
                "\n-One special character\n-One number";

            this.popupContent.Background = Brushes.PeachPuff;
            this.popupContent.Foreground = Brushes.Red;
            this.codePopup.Child = this.popupContent;

            this.codePopup.IsOpen = true;

            if (util.RegexUtilities.IsValidPassword(this.newPasswordBox.Password))
            {
                this.newPasswordBox.BorderThickness = new Thickness(3.0);
                this.newPasswordBox.BorderBrush = Brushes.Green;
                this.codePopup.IsOpen = false;
            }
            else
            {
                this.newPasswordBox.BorderThickness = new Thickness(3.0);
                this.newPasswordBox.BorderBrush = Brushes.Red;
            }
        }

        private void NewPasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            this.codePopup.IsOpen = false;

            if (util.RegexUtilities.IsValidPassword(this.newPasswordBox.Password))
            {
                this.newPasswordBox.BorderThickness = new Thickness(3.0);
                this.newPasswordBox.BorderBrush = Brushes.Green;
            }
            else
            {
                this.newPasswordBox.BorderThickness = new Thickness(3.0);
                this.newPasswordBox.BorderBrush = Brushes.Red;
            }
        }

        private void NewPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (util.RegexUtilities.IsValidPassword(this.newPasswordBox.Password))
            {
                this.codePopup.IsOpen = false;
                this.newPasswordBox.BorderThickness = new Thickness(3.0);
                this.newPasswordBox.BorderBrush = Brushes.Green;
            }
            else
            {
                this.newPasswordBox.BorderThickness = new Thickness(3.0);
                this.newPasswordBox.BorderBrush = Brushes.Red;
            }
        }
        #endregion

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            function.WindowSettings windowSettings = new function.WindowSettings();

            windowSettings.TransitionScreen(loginWindow, this);
        }
    }
}
