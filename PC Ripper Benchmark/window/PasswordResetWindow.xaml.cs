using PC_Ripper_Benchmark.function;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace PC_Ripper_Benchmark.window {
    /// <summary>
    /// Interaction logic for PasswordResetWindow.xaml
    /// </summary>
    public partial class PasswordResetWindow : Window {
        int count = 3;
        util.Encryption encryption = new util.Encryption();
        Popup codePopup = new Popup();
        TextBlock popupContent = new TextBlock();
        function.SystemSettings networkConnection = new function.SystemSettings();

        string connectionString = ConfigurationManager.ConnectionStrings["Connection_String"].ConnectionString;
        public PasswordResetWindow() {
            InitializeComponent();
            this.lblSecurityQuestion.Opacity = 0;
            this.securityQuestionAnswerTextBox.Opacity = 0;
            this.securityQuestionAnswerTextBox.Opacity = 0;

            //newPasswordLabel & Box defaults
            this.newPasswordLabel.Opacity = 0;
            this.newPasswordLabel.Visibility = Visibility.Collapsed;
            this.newPasswordBox.Opacity = 0;
            this.newPasswordBox.Visibility = Visibility.Collapsed;

            //confirmNewPasswordBox defaults
            this.confirmNewPasswordBox.Opacity = 0;
            this.confirmNewPasswordBox.Visibility = Visibility.Collapsed;
            this.confirmPasswordLabel.Opacity = 0;
            this.confirmPasswordLabel.Visibility = Visibility.Collapsed;

            //Security answer button
            this.confirmSecurityAnswerButton.Opacity = 0;
            this.confirmSecurityAnswerButton.Visibility = Visibility.Collapsed;
            this.emailTextBox.Focus();

            //Done button
            this.doneButton.Opacity = 0;
            this.doneButton.Visibility = Visibility.Collapsed;

            confirmEmailButton.BorderThickness = new Thickness(3);
            confirmSecurityAnswerButton.BorderThickness = new Thickness(3);
            doneButton.BorderThickness = new Thickness(3);
            backButton.BorderThickness = new Thickness(3);

            confirmEmailButton.BorderBrush = Brushes.White;
            confirmSecurityAnswerButton.BorderBrush = Brushes.White;
            doneButton.BorderBrush = Brushes.White;
            backButton.BorderBrush = Brushes.White;

        }

        private void ConfirmEmailButton_Click(object sender, RoutedEventArgs e) {
            try {
                if (SystemSettings.IsInternetAvailable() == true) {
                    if (util.RegexUtilities.IsValidEmail(this.emailTextBox.Text)) {
                        //Open new database connection
                        SqlConnection connection = new SqlConnection(this.connectionString);
                        database.DatabaseConnection newConnection = new database.DatabaseConnection(connection.ConnectionString);

                        //If the email exists in the database
                        if (newConnection.CheckEmailExists(connection, this.emailTextBox.Text)) {
                            connection.Open();

                            //Show the security question labels and fields
                            this.lblSecurityQuestion.Opacity = 100;
                            this.lblSecurityQuestion.Visibility = Visibility.Visible;
                            this.securityQuestionAnswerTextBox.Opacity = 100;
                            this.securityQuestionAnswerTextBox.Visibility = Visibility.Visible;
                            this.confirmSecurityAnswerButton.Visibility = Visibility.Visible;

                            //Command to get the actual answered security question
                            SqlCommand getSecurityQuestion = new SqlCommand("SELECT SecurityQuestion FROM Customer where Email=@Email", connection);
                            string email = this.encryption.EncryptText(this.emailTextBox.Text.ToUpper().Trim());
                            this.securityQuestionAnswerTextBox.Focus();

                            //Fill the parameter of the query
                            getSecurityQuestion.Parameters.AddWithValue("@Email", email);
                            this.lblSecurityQuestion.Content = (string)getSecurityQuestion.ExecuteScalar();

                            this.confirmEmailButton.Visibility = Visibility.Collapsed;

                            //Command to get the security question answer for comparison
                            SqlCommand getSecurityQuestionAnswer = new SqlCommand("SELECT SecurityQuestionAnswer FROM Customer where Email=@Email", connection);
                            string securityAnswer = this.encryption.EncryptText(this.securityQuestionAnswerTextBox.Text.ToUpper().Trim());

                            getSecurityQuestionAnswer.Parameters.AddWithValue("@Email", email);

                            //Set the answer returned by the query to a variable for comparison
                            string answer = (string)getSecurityQuestionAnswer.ExecuteScalar();

                            this.confirmSecurityAnswerButton.Opacity = 100;

                            //Make the confirmed field read only
                            this.emailTextBox.IsReadOnly = true;
                            this.emailTextBox.Background = Brushes.SlateGray;

                            connection.Close();
                        }
                    } else {
                        MessageBox.Show("Invalid email", "Incorrect email format", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            } catch (SqlException) {
                MessageBox.Show("A SQL Error was caught", "Error!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        #region Confirm the security answer
        private void ConfirmSecurityAnswer_Click(object sender, RoutedEventArgs e) {
            try {
                if (SystemSettings.IsInternetAvailable() == true) {
                    //Open new database connection
                    SqlConnection connection = new SqlConnection(this.connectionString);
                    database.DatabaseConnection newConnection = new database.DatabaseConnection(connection.ConnectionString);
                    connection.Open();

                    //Command to get the actual answered security question
                    string email = this.encryption.EncryptText(this.emailTextBox.Text.ToUpper().Trim());

                    //Command to get the security question answer for comparison
                    SqlCommand getSecurityQuestionAnswer = new SqlCommand("SELECT SecurityQuestionAnswer FROM Customer where Email=@Email", connection);
                    string securityAnswer = this.encryption.EncryptText(this.securityQuestionAnswerTextBox.Text);

                    getSecurityQuestionAnswer.Parameters.AddWithValue("@Email", email);

                    //Set the answer returned by the query to a variable for comparison
                    string answer = (string)getSecurityQuestionAnswer.ExecuteScalar();

                    if (this.encryption.EncryptText(this.securityQuestionAnswerTextBox.Text) == answer) {
                        this.newPasswordLabel.Opacity = 100;
                        this.newPasswordLabel.Visibility = Visibility.Visible;
                        this.newPasswordBox.Opacity = 100;
                        this.newPasswordBox.Visibility = Visibility.Visible;
                        this.confirmNewPasswordBox.Opacity = 100;
                        this.confirmNewPasswordBox.Visibility = Visibility.Visible;
                        this.confirmPasswordLabel.Opacity = 100;
                        this.confirmPasswordLabel.Visibility = Visibility.Visible;
                        this.newPasswordLabel.Focus();

                        this.securityQuestionAnswerTextBox.IsReadOnly = true;
                        this.securityQuestionAnswerTextBox.Background = Brushes.SlateGray;
                        this.confirmSecurityAnswerButton.Visibility = Visibility.Collapsed;
                        this.doneButton.Opacity = 100;
                        this.doneButton.Visibility = Visibility.Visible;
                        this.newPasswordBox.Focus();
                    } else {
                        this.count--;
                        MessageBox.Show($"The answer entered is incorrect. You have {this.count} attempts left.", "Incorrect Answer", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        this.securityQuestionAnswerTextBox.Clear();
                        this.securityQuestionAnswerTextBox.Focus();
                    }

                    if (this.count == 0) {
                        LoginWindow loginWindow = new LoginWindow();
                        function.WindowSettings windowSettings = new function.WindowSettings();

                        windowSettings.TransitionScreen(loginWindow, this);
                    }
                    connection.Close();
                }
            } catch (SqlException) {
                MessageBox.Show("An SQL Exception was caught!");
            }

        }
        #endregion

        #region Done Button
        private void DoneButton_Click(object sender, RoutedEventArgs e) {
            //Open new database connection
            SqlConnection connection = new SqlConnection(this.connectionString);
            database.DatabaseConnection newConnection = new database.DatabaseConnection(connection.ConnectionString);
            connection.Open();

            //Command to get the actual answered security question
            string email = this.encryption.EncryptText(this.emailTextBox.Text.ToUpper().Trim());

            if (util.RegexUtilities.IsValidPassword(this.newPasswordBox.Password) &&
                   this.newPasswordBox.Password == this.confirmNewPasswordBox.Password) {
                SqlCommand changePassword = new SqlCommand("UPDATE Customer SET Password = @Password WHERE Email=@Email", connection);
                //Fill the parameter of the query
                changePassword.Parameters.AddWithValue("@Email", email);
                changePassword.Parameters.AddWithValue("@Password", this.encryption.EncryptText(this.confirmNewPasswordBox.Password));
                changePassword.ExecuteNonQuery();

                MessageBox.Show("Password Changed", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                LoginWindow loginWindow = new LoginWindow();
                function.WindowSettings windowSettings = new function.WindowSettings();

                windowSettings.TransitionScreen(loginWindow, this);
            }
        }
        #endregion

        #region Passwords match indicator events
        private void ConfirmNewPasswordBox_PasswordChanged(object sender, RoutedEventArgs e) {
            if (this.newPasswordBox.Password == this.confirmNewPasswordBox.Password) {

                //If the password matches, set passwords match label to green border and visible
                this.lblPasswordsMatch.Content = "Passwords match";
                this.lblPasswordsMatch.Visibility = Visibility.Visible;
                this.lblPasswordsMatch.Background = Brushes.Green;

                //Confirm password box turns green
                this.confirmNewPasswordBox.BorderThickness = new Thickness(3.0);
                this.confirmNewPasswordBox.BorderBrush = Brushes.Green;
            } else {

                //If the password does not match, set passwords match label to visible;
                this.lblPasswordsMatch.Content = "Passwords don't match";
                this.lblPasswordsMatch.Visibility = Visibility.Visible;
                this.lblPasswordsMatch.Background = Brushes.Red;


                //Confirm password box turns red
                this.confirmNewPasswordBox.BorderThickness = new Thickness(3.0);
                this.confirmNewPasswordBox.BorderBrush = Brushes.Red;
            }
        }



        private void NewPasswordBox_LostFocus(object sender, RoutedEventArgs e) {
            this.codePopup.IsOpen = false;

            if (util.RegexUtilities.IsValidPassword(this.newPasswordBox.Password)) {
                this.newPasswordBox.BorderThickness = new Thickness(3.0);
                this.newPasswordBox.BorderBrush = Brushes.Green;
            } else {
                this.newPasswordBox.BorderThickness = new Thickness(3.0);
                this.newPasswordBox.BorderBrush = Brushes.Red;
            }
        }

        private void NewPasswordBox_PasswordChanged(object sender, RoutedEventArgs e) {

            //Sets the popup values, background property,
            this.popupContent.FontSize = 10;
            this.codePopup.PlacementTarget = this.newPasswordBox;
            this.popupContent.Text = "Password must contain: \n-One uppercase letter" +
                "\n-One special character\n-One number";

            this.popupContent.Background = Brushes.PeachPuff;
            this.popupContent.Foreground = Brushes.Red;
            this.codePopup.Child = this.popupContent;

            this.codePopup.IsOpen = true;
            
            if (util.RegexUtilities.IsValidPassword(this.newPasswordBox.Password)) {
                this.newPasswordBox.BorderThickness = new Thickness(3.0);
                this.newPasswordBox.BorderBrush = Brushes.Green;
                this.codePopup.IsOpen = false;
            } else {
                this.newPasswordBox.BorderThickness = new Thickness(3.0);
                this.newPasswordBox.BorderBrush = Brushes.Red;
                this.codePopup.IsOpen = true;
            }
        }
        #endregion

        private void BackButton_Click(object sender, RoutedEventArgs e) {
            LoginWindow loginWindow = new LoginWindow();
            function.WindowSettings windowSettings = new function.WindowSettings();

            windowSettings.TransitionScreen(loginWindow, this);
        }

        #region KeyDown Events
        private void NewPasswordBox_KeyDown(object sender, KeyEventArgs e) {
            
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                this.confirmNewPasswordBox.Focus();
                e.Handled = true;
            }
            else if (e.Key == System.Windows.Input.Key.Down)
            {
                this.confirmNewPasswordBox.Focus();
                e.Handled = true;
            }
        }

        private void DoneButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Up)
            {
                this.confirmNewPasswordBox.Focus();
                e.Handled = true;
            }
            else if (e.Key == System.Windows.Input.Key.Down)
            {
                this.backButton.Focus();
                e.Handled = true;
            }
            else if (e.Key == Key.Enter)
            {
                DoneButton_Click(sender, e);
            }
        }

        private void BackButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Up)
            {
                this.doneButton.Focus();
                e.Handled = true;
            }
            else if (e.Key == System.Windows.Input.Key.Down)
            {
                this.newPasswordBox.Focus();
                e.Handled = true;
            }
            else if (e.Key == Key.Enter)
            {
                BackButton_Click(sender, e);
            }
            else if (e.Key == Key.Up)
            {
                confirmEmailButton.Focus();
            }

        }

        private void EmailTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                this.confirmEmailButton.Focus();
                e.Handled = true;
            }
            else if (e.Key == System.Windows.Input.Key.Down)
            {
                this.confirmEmailButton.Focus();
                e.Handled = true;
            }
        }

        private void SecurityQuestionAnswerTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                this.confirmSecurityAnswerButton.Focus();
                e.Handled = true;
            }
            else if (e.Key == System.Windows.Input.Key.Down)
            {
                this.confirmSecurityAnswerButton.Focus();
                e.Handled = true;
            }
        }

        private void ConfirmEmailButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Up)
            {
                this.emailTextBox.Focus();
                e.Handled = true;
            }
            else if (e.Key == Key.Enter)
            {
                ConfirmEmailButton_Click(sender, e);
            }
        }

        private void ConfirmSecurityAnswerButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Up)
            {
                this.confirmSecurityAnswerButton.Focus();
                e.Handled = true;
            }
            else if (e.Key == Key.Enter)
            {
                ConfirmSecurityAnswer_Click(sender, e);
            }
        }

        private void ConfirmNewPasswordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                this.doneButton.Focus();
                e.Handled = true;
            }
            else if (e.Key == System.Windows.Input.Key.Down)
            {
                this.doneButton.Focus();
                e.Handled = true;
            }
        }

        #endregion
        #region GotFocus Button Events
        private void ConfirmEmailButton_GotFocus(object sender, RoutedEventArgs e)
        {
            this.confirmEmailButton.BorderThickness = new Thickness(3.0);
            this.confirmEmailButton.BorderBrush = Brushes.ForestGreen;
            this.confirmEmailButton.Foreground = Brushes.Black;
        }

        private void ConfirmSecurityAnswerButton_GotFocus(object sender, RoutedEventArgs e)
        {
            this.confirmSecurityAnswerButton.BorderThickness = new Thickness(3.0);
            this.confirmSecurityAnswerButton.BorderBrush = Brushes.ForestGreen;
            this.confirmSecurityAnswerButton.Foreground = Brushes.Black;
        }

        private void DoneButton_GotFocus(object sender, RoutedEventArgs e)
        {
            this.doneButton.BorderThickness = new Thickness(3.0);
            this.doneButton.BorderBrush = Brushes.ForestGreen;
            this.doneButton.Foreground = Brushes.Black;
        }

        private void BackButton_GotFocus(object sender, RoutedEventArgs e)
        {
            this.backButton.BorderThickness = new Thickness(3.0);
            this.backButton.BorderBrush = Brushes.ForestGreen;
            this.backButton.Foreground = Brushes.Black;
        }
        #endregion
        #region LostFocus Button Events
        private void ConfirmEmailButton_LostFocus(object sender, RoutedEventArgs e)
        {
            this.confirmEmailButton.BorderBrush = Brushes.White;
            this.confirmEmailButton.Foreground = Brushes.White;
        }

        private void ConfirmSecurityAnswerButton_LostFocus(object sender, RoutedEventArgs e)
        {
            this.confirmSecurityAnswerButton.BorderBrush = Brushes.White;
            this.confirmSecurityAnswerButton.Foreground = Brushes.White;
        }

        private void DoneButton_LostFocus(object sender, RoutedEventArgs e)
        {
            this.doneButton.BorderBrush = Brushes.White;
            this.doneButton.Foreground = Brushes.White;
        }

        private void BackButton_LostFocus(object sender, RoutedEventArgs e)
        {
            this.backButton.BorderBrush = Brushes.White;
            this.backButton.Foreground = Brushes.White;
        }
        #endregion
    }
}
