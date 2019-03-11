using PC_Ripper_Benchmark.util;
using System.Windows;
using System.Windows.Controls;

namespace PC_Ripper_Benchmark.window {

    /// <summary>
    /// Interaction logic for QuestionaireWindowxaml.xaml
    /// </summary>

    public partial class QuestionaireWindow : Window {


        public QuestionaireWindow() {
            InitializeComponent();
        }

        private void SliderUserSkill_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {

            Slider s = (Slider)sender;
            string userSkill;

            switch (e.NewValue) {
                case double d when (e.NewValue >= 0 && e.NewValue <= 1): {
                    s.Value = 1.0;
                    userSkill = "Beginner";
                    s.ToolTip = userSkill;
                    this.lblUserSkill.Content = $"How would you describe your knowledge of computers? {userSkill}";
                    break;
                }

                case double d when (e.NewValue > 1 && e.NewValue <= 2): {
                    s.Value = 2.0;
                    userSkill = "Advanced";
                    s.ToolTip = userSkill;
                    this.lblUserSkill.Content = $"How would you describe your knowledge of computers? {userSkill}";
                    break;
                }
            }
        }

        private void SliderUserSkill_Copy_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            Slider s = (Slider)sender;

            string userType;

            switch (e.NewValue) {
                case double d when (e.NewValue >= 0 && e.NewValue <= 1): {
                    s.Value = 1.0;
                    userType = "Casual";
                    s.ToolTip = userType;
                    this.lblUserType.Content = $"How do you use your computer? {userType}";
                    break;
                }

                case double d when (e.NewValue > 1 && e.NewValue <= 2): {
                    s.Value = 2.0;
                    userType = "Web surfer";
                    s.ToolTip = userType;
                    this.lblUserType.Content = $"How do you use your computer? {userType}";
                    break;
                }

                case double d when (e.NewValue > 2 && e.NewValue <= 3): {
                    s.Value = 3.0;
                    userType = "High performance";
                    s.ToolTip = userType;
                    this.lblUserType.Content = $"How do you use your computer? {userType}";
                    break;
                }

                case double d when (e.NewValue > 3 && e.NewValue <= 4): {
                    s.Value = 4.0;
                    userType = "Video editor";
                    s.ToolTip = userType;
                    this.lblUserType.Content = $"How do you use your computer? {userType}";
                    break;
                }
            }
        }

        private void SliderTheme_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            Slider s = (Slider)sender;
            ThemeManager tm;

            string userTheme;

            switch (e.NewValue) {
                case double d when (e.NewValue >= 0 && e.NewValue <= 1): {
                    s.Value = 1.0;
                    userTheme = "Light";
                    s.ToolTip = userTheme;
                    this.lblTheme.Content = $"Choose a theme! {userTheme}";

                    tm = new ThemeManager(ThemeManager.Theme.Light, this);

                    break;
                }

                case double d when (e.NewValue > 1 && e.NewValue <= 2): {
                    s.Value = 2.0;
                    userTheme = "Dark";
                    s.ToolTip = userTheme;
                    this.lblTheme.Content = $"Choose a theme! { userTheme}";

                    tm = new ThemeManager(ThemeManager.Theme.Dark, this);

                    break;
                }
            }
        }

        private void BtnFinish_Click(object sender, RoutedEventArgs e) {
            Close();
        }
    }
}
