using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PC_Ripper_Benchmark.window {

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// Author(s): Anthony Jaghab, David Hartglass, (c) all rights reserved.
    /// </summary>

    public partial class MainWindow : Window {

        #region Instance member(s), and enum(s).

        /// <summary>
        /// The <see cref="Tab"/> enum.
        /// Represents all the tabs needed.
        /// </summary>

        public enum Tab {
            /// <summary>
            /// The cpu tab.
            /// </summary>
            CPU,

            /// <summary>
            /// The disk tab.
            /// </summary>
            DISK,

            /// <summary>
            /// The ram tab.
            /// </summary>
            RAM,

            /// <summary>
            /// The gpu tab.
            /// </summary>
            GPU
        }

        #endregion

        #region Constructor(s) and method(s).

        /// <summary>
        /// Default constructor for the <see cref="MainWindow"/>.
        /// </summary>

        public MainWindow() {
            InitializeComponent();
            Style s = new Style();
            s.Setters.Add(new Setter(UIElement.VisibilityProperty, Visibility.Collapsed));
            tabComponents.ItemContainerStyle = s;
            tabComponents.SelectedIndex = 0;
        }

        private void ShowCPUWindow() {
            tabComponents.SelectedIndex = 1;
        }

        #endregion

        #region Event(s) and event handler(s).

        private void BtnTemp_Click(object sender, RoutedEventArgs e) {
            new QuestionaireWindow().Show();
        }

        private void BtnCPU_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e) {
            btnCPU.Foreground = Brushes.Salmon;

        }

        private void BtnCPU_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e) {
            btnCPU.Foreground = Brushes.Black;
        }

        private void BtnCPU_Click(object sender, RoutedEventArgs e) {
            ShowCPUWindow();
        }

        #endregion

    }
}
