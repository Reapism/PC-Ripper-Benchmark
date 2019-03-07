using System.Windows;

namespace PC_Ripper_Benchmark.window {

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// Author(s): Anthony Jaghab, David Hartglass, (c) all rights reserved.
    /// </summary>

    public partial class MainWindow : Window {

        /// <summary>
        /// Default constructor for the <see cref="MainWindow"/>.
        /// </summary>

        public MainWindow() {
            InitializeComponent();
        }

        private void BtnTemp_Click(object sender, RoutedEventArgs e) {
            new QuestionaireWindow().Show();
        }
    }
}
