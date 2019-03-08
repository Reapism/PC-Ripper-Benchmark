using System.Windows;
using System.Windows.Media;

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

        private void BtnCPU_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e) {
            btnCPU.BorderThickness = new Thickness(3.0);
            btnCPU.BorderBrush = Brushes.Salmon;
        }

        private void BtnCPU_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e) {
            btnCPU.BorderThickness = new Thickness(3.0);
            btnCPU.BorderBrush = Brushes.Transparent;
        }

        private void BtnCPU_Click(object sender, RoutedEventArgs e) {
            btnCPU.BorderThickness = new Thickness(3.0);
            btnCPU.BorderBrush = Brushes.Salmon;
        }
    }
}
