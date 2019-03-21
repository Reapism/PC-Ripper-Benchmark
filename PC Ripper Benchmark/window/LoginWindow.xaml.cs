using PC_Ripper_Benchmark.function;
using PC_Ripper_Benchmark.window;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Media.Animation;

namespace PC_Ripper_Benchmark {

    /// <summary>
    /// Interaction logic for <see cref="LoginWindow"/>
    /// Inherits from Window base class to have properties of a windows
    /// <para>Author: David Hartglass</para>
    /// </summary>

    public partial class LoginWindow : Window {


        private WindowSettings settings = new WindowSettings();

        /// <summary>
        /// Default constructor for <see cref="LoginWindow"/>.
        /// </summary>
        
        public LoginWindow() {
            InitializeComponent();

            //Change the progressbar visibilty to not show on screen
            this.database_progressbar.Opacity = 0;

            //Create new instance of the window settings class
            this.settings.CenterWindowOnScreen(this.windowLogin);
        }

        #region Event Handlers
        private void LoginButton_Click(object sender, RoutedEventArgs e) {
            SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder();
            stringBuilder.DataSource = "tcp:bcsproject.database.windows.net,1433";
            stringBuilder.UserID = "Konrad100";
            stringBuilder.Password = "Coolguy100";
            stringBuilder.PersistSecurityInfo = false;
            stringBuilder.InitialCatalog = "CPURipper";
            stringBuilder.MultipleActiveResultSets = false;
            stringBuilder.Encrypt = true;
            stringBuilder.TrustServerCertificate = false;
            stringBuilder.ConnectTimeout = 30;

            SqlConnection connection = new SqlConnection(stringBuilder.ConnectionString);
            database.DatabaseConnection newConnection = new database.DatabaseConnection(connection.ConnectionString);

            newConnection.checkAccountExists(connection, emailTextBox.Text, passwordTextBox.Password);
           
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

        private void BtnTemp_Click(object sender, RoutedEventArgs e) {
            new MainWindow().Show();
        }
    }
}