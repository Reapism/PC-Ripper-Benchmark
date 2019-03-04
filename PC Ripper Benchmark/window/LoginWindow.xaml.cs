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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PC_Ripper_Benchmark {

    /// <summary>
    /// Interaction logic for <see cref="LoginWindow"/>
    /// Inherits from Window base class to have properties of a windows
    /// <para>Author: David Hartglass</para>
    /// </summary>

    public partial class LoginWindow : Window {

        /// <summary>
        /// Default constructor for <see cref="LoginWindow"/>
        /// </summary>
        function.WindowSettings settings = new function.WindowSettings();

        public LoginWindow() {
            InitializeComponent();

            //Change the progressbar visibilty to not show on screen
            this.database_progressbar.Visibility = Visibility.Collapsed;

            //Create new instance of the window settings class
            settings.CenterWindowOnScreen(this.windowLogin);
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

        private void signUpButton_Click(object sender, RoutedEventArgs e)
        {
            CreateAccountWindow createAccountWindow = new CreateAccountWindow();
            DoubleAnimation openScreen = new DoubleAnimation();

            settings.TransitionToCreateAccountScreen(createAccountWindow, this, openScreen);
        }
        #endregion          
    }
}
