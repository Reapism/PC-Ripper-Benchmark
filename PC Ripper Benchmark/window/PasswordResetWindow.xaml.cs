using System;
using System.Collections.Generic;
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

namespace PC_Ripper_Benchmark.window
{
    /// <summary>
    /// Interaction logic for PasswordResetWindow.xaml
    /// </summary>
    public partial class PasswordResetWindow : Window
    {
        public PasswordResetWindow()
        {
            InitializeComponent();
            lblSecurityQuestion.Opacity = 0;
            securityQuestionTextBox.Opacity = 0;
            securityQuestionAnswerTextBox.Opacity = 0;

        }

        private void ResetPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            SqlConnectionStringBuilder connectionString = new SqlConnectionStringBuilder
            {
                DataSource = "tcp:bcsproject.database.windows.net,1433",
                UserID = "Konrad100",
                Password = "Coolguy100",
                PersistSecurityInfo = false,
                InitialCatalog = "CPURipper",
                MultipleActiveResultSets = false,
                Encrypt = true,
                TrustServerCertificate = false,
                ConnectTimeout = 30
            };

            SqlConnection connection = new SqlConnection(connectionString.ConnectionString);
            database.DatabaseConnection newConnection = new database.DatabaseConnection(connection.ConnectionString);

            util.Encryption encryption = new util.Encryption();

            if (newConnection.CheckEmailExists(connection, emailTextBox.Text))
            {
                lblSecurityQuestion.Opacity = 100;
                securityQuestionTextBox.Opacity = 100;
                securityQuestionAnswerTextBox.Opacity = 100;
            }
        }
    }
}
