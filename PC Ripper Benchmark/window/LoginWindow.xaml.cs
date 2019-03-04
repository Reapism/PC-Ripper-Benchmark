using PC_Ripper_Benchmark.util;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Media.Animation;

namespace PC_Ripper_Benchmark.window {

    /// <summary>
    /// Interaction logic for <see cref="LoginWindow"/>
    /// Inherits from Window base class to have properties of a windows
    /// <para>Author: David Hartglass</para>
    /// </summary>

    public partial class LoginWindow : Window {

        private WindowSettings settings = new WindowSettings();

        /// <summary>
        /// Default constructor for <see cref="LoginWindow"/>
        /// </summary>

        public LoginWindow() {
            InitializeComponent();

            //Change the progressbar visibilty to not show on screen
            this.database_progressbar.Visibility = Visibility.Collapsed;

            //Create new instance of the window settings class
            this.settings.CenterWindowOnScreen(this.windowLogin);
        }

        #region Event Handlers
        private void LoginButton_Click(object sender, RoutedEventArgs e) {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=localhost\sqle2012; Initial Catalog=LoginDB; Integrated Security=True;");
            try {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                string query = "SELECT COUNT(1) FROM tblUser WHERE Username=@Username AND Password=@Password";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon) {
                    CommandType = CommandType.Text
                };
                sqlCmd.Parameters.AddWithValue("@Username", this.txtUsername.Text);
                sqlCmd.Parameters.AddWithValue("@Password", this.txtPassword.Password);
                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                if (count == 1) {
                    //MainWindow dashboard = new MainWindow();
                    //dashboard.Show();
                    Close();
                } else {
                    MessageBox.Show("Username or password is incorrect.");
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            } finally {
                sqlCon.Close();
            }
        }

        /// <summary>
        /// Event handler for signUpButton in <see cref="LoginWindow"/>.
        /// <para>When signUpButton is clicked,the window changes to a window of type <see cref="CreateAccountWindow"/></para>
        /// </summary>

        private void SignUpButton_Click(object sender, RoutedEventArgs e) {
            CreateAccountWindow createAccountWindow = new CreateAccountWindow();
            DoubleAnimation openScreen = new DoubleAnimation();

            this.settings.TransitionToCreateAccountScreen(createAccountWindow, this, openScreen);
        }
        #endregion          
    }
}
