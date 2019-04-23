using PC_Ripper_Benchmark.database;
using PC_Ripper_Benchmark.function;
using PC_Ripper_Benchmark.util;
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

        #region Instance variables (fields).

        private HashManager encryption;
        private Popup codePopup;
        private TextBlock popupContent;
        private SystemSettings networkConnection;

        private int count;
        private string connectionString;

        #endregion

        public PasswordResetWindow() {
            Instantiate();
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

            this.confirmEmailButton.BorderThickness = new Thickness(3);
            this.confirmSecurityAnswerButton.BorderThickness = new Thickness(3);
            this.doneButton.BorderThickness = new Thickness(3);
            this.backButton.BorderThickness = new Thickness(3);

            this.confirmEmailButton.BorderBrush = Brushes.White;
            this.confirmSecurityAnswerButton.BorderBrush = Brushes.White;
            this.doneButton.BorderBrush = Brushes.White;
            this.backButton.BorderBrush = Brushes.White;

        }

        /// <summary>
        /// Instantiates the instance variables.
        /// </summary>

        private void Instantiate() {
            this.count = 3;
            this.encryption = new HashManager();
            this.codePopup = new Popup();
            this.popupContent = new TextBlock();
            this.networkConnection = new SystemSettings();
            this.connectionString = DatabaseConnection.GetConnectionString();
        }

        private void ConfirmEmailButton_Click(object sender, RoutedEventArgs e) {
            try {
                if (SystemSettings.IsInternetAvailable() == true) {
                    if (RegexUtilities.IsValidEmail(this.emailTextBox.Text)) {
                        //Open new database connection
                        SqlConnection connection = new SqlConnection(this.connectionString);
                        DatabaseConnection newConnection = new DatabaseConnection(this.connectionString);

                        //If the email exists in the database
                        if (newConnection.CheckEmailExists(this.emailTextBox.Text)) {
                            connection.Open();

                            //Show the security question labels and fields
                            this.lblSecurityQuestion.Opacity = 100;
                            this.lblSecurityQuestion.Visibility = Visibility.Visible;
                            this.securityQuestionAnswerTextBox.Opacity = 100;
                            this.securityQuestionAnswerTextBox.Visibility = Visibility.Visible;
                            this.confirmSecurityAnswerButton.Visibility = Visibility.Visible;

                            //Command to get the actual answered security question
                            string email = this.encryption.HashTextSHA256(this.emailTextBox.Text);
                            this.securityQuestionAnswerTextBox.Focus();

                            lblSecurityQuestion.Content = newConnection.GetSecurityQuestion(email);
                            this.confirmEmailButton.Visibility = Visibility.Collapsed;

                            //Command to get the security question answer for comparison
                            SqlCommand getSecurityQuestionAnswer = new SqlCommand("SELECT SecurityQuestionAnswer FROM [USER] where Email=@Email", connection);
                            string securityAnswer = this.encryption.HashTextSHA256(this.securityQuestionAnswerTextBox.Text.ToUpper().Trim());
                                                        
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

        private void BackButton_Click(object sender, RoutedEventArgs e) {
            LoginWindow loginWindow = new LoginWindow();
            function.WindowSettings windowSettings = new function.WindowSettings();

            windowSettings.TransitionScreen(loginWindow, this);
        }
        #region Confirm the security answer
        private void ConfirmSecurityAnswer_Click(object sender, RoutedEventArgs e) {
            try {
                if (SystemSettings.IsInternetAvailable() == true) {
                    //Open new database connection
                    SqlConnection connection = new SqlConnection(this.connectionString);
                    DatabaseConnection newConnection = new DatabaseConnection(connection.ConnectionString);
                    connection.Open();

                    string email = this.encryption.HashTextSHA256(this.emailTextBox.Text);

                    //Set the answer returned by the query to a variable for comparison
                    string answer = newConnection.GetSecurityQuestionAnswer(email);

                    if (this.encryption.HashTextSHA256(this.securityQuestionAnswerTextBox.Text) == answer) {
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
            string email = this.encryption.HashTextSHA256(this.emailTextBox.Text.ToUpper().Trim());

            if (util.RegexUtilities.IsValidPassword(this.newPasswordBox.Password) &&
                   this.newPasswordBox.Password == this.confirmNewPasswordBox.Password) {
                SqlCommand changePassword = new SqlCommand("UPDATE [USER] SET Password = @Password WHERE Email=@Email", connection);
                //Fill the parameter of the query
                changePassword.Parameters.AddWithValue("@Email", email);
                changePassword.Parameters.AddWithValue("@Password", this.encryption.HashTextSHA256(this.confirmNewPasswordBox.Password));
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

            if (this.newPasswordBox.Password == this.confirmNewPasswordBox.Password
                && this.confirmNewPasswordBox.Password != "" && this.confirmNewPasswordBox.Password != null)
            {

                //If the password matches, set passwords match label to green border and visible
                this.lblPasswordsMatch.Content = "Passwords match";
                this.lblPasswordsMatch.Background = Brushes.Green;
            }
            else
            {
                //If the password does not match, set passwords match label to visible;
                this.lblPasswordsMatch.Content = "Passwords don't match";
                this.lblPasswordsMatch.Background = Brushes.Red;
            }
        }
        #endregion
        #region KeyDown Events
        private void NewPasswordBox_KeyDown(object sender, KeyEventArgs e) {

            if (e.Key == System.Windows.Input.Key.Enter) {
                this.confirmNewPasswordBox.Focus();
                e.Handled = true;
            } else if (e.Key == System.Windows.Input.Key.Down) {
                this.confirmNewPasswordBox.Focus();
                e.Handled = true;
            }
        }

        private void DoneButton_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == System.Windows.Input.Key.Up) {
                this.confirmNewPasswordBox.Focus();
                e.Handled = true;
            } else if (e.Key == System.Windows.Input.Key.Down) {
                this.backButton.Focus();
                e.Handled = true;
            } else if (e.Key == Key.Enter) {
                DoneButton_Click(sender, e);
            }
        }

        private void BackButton_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == System.Windows.Input.Key.Up) {
                this.doneButton.Focus();
                e.Handled = true;
            } else if (e.Key == System.Windows.Input.Key.Down) {
                this.newPasswordBox.Focus();
                e.Handled = true;
            } else if (e.Key == Key.Enter) {
                BackButton_Click(sender, e);
            } else if (e.Key == Key.Up) {
                this.confirmEmailButton.Focus();
            }

        }

        private void EmailTextBox_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == System.Windows.Input.Key.Enter) {
                ConfirmEmailButton_Click(sender, e);
                e.Handled = true;
            } else if (e.Key == System.Windows.Input.Key.Down) {
                this.confirmEmailButton.Focus();
                e.Handled = true;
            }
        }

        private void SecurityQuestionAnswerTextBox_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == System.Windows.Input.Key.Enter) {
                ConfirmSecurityAnswer_Click(sender, e);
                e.Handled = true;
            } else if (e.Key == System.Windows.Input.Key.Down) {
                this.confirmSecurityAnswerButton.Focus();
                e.Handled = true;
            }
        }

        private void ConfirmEmailButton_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == System.Windows.Input.Key.Up) {
                this.emailTextBox.Focus();
                e.Handled = true;
            } else if (e.Key == Key.Enter) {
                ConfirmEmailButton_Click(sender, e);
            }
        }

        private void ConfirmSecurityAnswerButton_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == System.Windows.Input.Key.Up) {
                this.confirmSecurityAnswerButton.Focus();
                e.Handled = true;
            } else if (e.Key == Key.Enter) {
                ConfirmSecurityAnswer_Click(sender, e);
            }
        }

        private void ConfirmNewPasswordBox_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == System.Windows.Input.Key.Enter) {
                DoneButton_Click(sender, e);
                e.Handled = true;
            } else if (e.Key == System.Windows.Input.Key.Down) {
                this.doneButton.Focus();
                e.Handled = true;
            }
        }

        #endregion
        #region GotFocus Button Events
        private void ConfirmEmailButton_GotFocus(object sender, RoutedEventArgs e) {
            this.confirmEmailButton.BorderBrush = Brushes.Black;
            this.confirmEmailButton.Foreground = Brushes.Black;
        }

        private void ConfirmSecurityAnswerButton_GotFocus(object sender, RoutedEventArgs e) {
            this.confirmSecurityAnswerButton.BorderBrush = Brushes.Black;
            this.confirmSecurityAnswerButton.Foreground = Brushes.Black;
        }

        private void DoneButton_GotFocus(object sender, RoutedEventArgs e) {
            this.doneButton.BorderBrush = Brushes.Black;
            this.doneButton.Foreground = Brushes.Black;
        }

        private void BackButton_GotFocus(object sender, RoutedEventArgs e) {
            this.backButton.BorderBrush = Brushes.Black;
            this.backButton.Foreground = Brushes.Black;
        }
        #endregion
        #region LostFocus Button Events
        private void ConfirmEmailButton_LostFocus(object sender, RoutedEventArgs e) {
            this.confirmEmailButton.BorderBrush = Brushes.White;
            this.confirmEmailButton.Foreground = Brushes.White;
        }

        private void ConfirmSecurityAnswerButton_LostFocus(object sender, RoutedEventArgs e) {
            this.confirmSecurityAnswerButton.BorderBrush = Brushes.White;
            this.confirmSecurityAnswerButton.Foreground = Brushes.White;
        }

        private void DoneButton_LostFocus(object sender, RoutedEventArgs e) {
            this.doneButton.BorderBrush = Brushes.White;
            this.doneButton.Foreground = Brushes.White;
        }

        private void BackButton_LostFocus(object sender, RoutedEventArgs e) {
            this.backButton.BorderBrush = Brushes.White;
            this.backButton.Foreground = Brushes.White;
        }
        #endregion
        #region Mouse Enter Events
        private void ConfirmEmailButton_MouseEnter(object sender, MouseEventArgs e) {
            this.confirmEmailButton.Foreground = Brushes.Black;
            this.confirmEmailButton.BorderBrush = Brushes.Black;
        }

        private void ConfirmSecurityAnswerButton_MouseEnter(object sender, MouseEventArgs e) {
            this.confirmSecurityAnswerButton.Foreground = Brushes.Black;
            this.confirmSecurityAnswerButton.BorderBrush = Brushes.Black;
        }

        private void DoneButton_MouseEnter(object sender, MouseEventArgs e) {
            this.doneButton.Foreground = Brushes.Black;
            this.doneButton.BorderBrush = Brushes.Black;
        }

        private void BackButton_MouseEnter(object sender, MouseEventArgs e) {
            this.backButton.Foreground = Brushes.Black;
            this.backButton.BorderBrush = Brushes.Black;
        }
        #endregion
        #region Mouse Leave Events
        private void ConfirmEmailButton_MouseLeave(object sender, MouseEventArgs e) {
            this.confirmEmailButton.Foreground = Brushes.White;
            this.confirmEmailButton.BorderBrush = Brushes.White;
        }

        private void ConfirmSecurityAnswerButton_MouseLeave(object sender, MouseEventArgs e) {
            this.confirmSecurityAnswerButton.Foreground = Brushes.White;
            this.confirmSecurityAnswerButton.BorderBrush = Brushes.White;
        }

        private void DoneButton_MouseLeave(object sender, MouseEventArgs e) {
            this.doneButton.Foreground = Brushes.White;
            this.doneButton.BorderBrush = Brushes.White;
        }

        private void BackButton_MouseLeave(object sender, MouseEventArgs e) {
            this.backButton.Foreground = Brushes.White;
            this.backButton.BorderBrush = Brushes.White;
        }
        #endregion
    }
}
