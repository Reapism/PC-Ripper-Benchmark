using PC_Ripper_Benchmark.function;
using PC_Ripper_Benchmark.util;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static PC_Ripper_Benchmark.util.UserData;

namespace PC_Ripper_Benchmark.window {

    /// <summary>
    /// Interaction logic for QuestionaireWindowxaml.xaml
    /// </summary>

    public partial class QuestionaireWindow : Window {

        #region Instance member(s) and Properties.

        private UserData userData;

        private TypeOfUser UserType { get; set; }
        private UserSkill Skill { get; set; }
        private int isLight;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Default constructor. Optionally can change the 
        /// </summary>
        /// <param name="userData"></param>
        /// <param name="subtile"></param>

        public QuestionaireWindow(ref UserData userData, string subtile = "") {
            InitializeComponent();
            this.userData = userData;
            this.lblWelcome.Content = $"Hey, {userData.FirstName}.";
            if (subtile == string.Empty) {
                this.lblWelcome2.Content = $"Please finish the questionnaire " +
                    $"to personalize your experience.";
                this.lblWelcome2.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF7DA671");
            } else {
                this.lblWelcome2.Content = $"{subtile}";
                this.lblWelcome2.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFDE7575");
            }


            WindowSettings ws = new WindowSettings();
            ws.CenterWindowOnScreen(this);
        }

        #endregion

        private void SliderUserSkill_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            //Code for the slider
            Slider s = (Slider)sender;
            string userSkill;

            switch (e.NewValue) {
                case double d when (e.NewValue >= 0 && e.NewValue <= 1): {
                    s.Value = 1.0;
                    userSkill = "Beginner";
                    s.ToolTip = userSkill;
                    this.lblUserSkill.Content = $"How would you describe your knowledge of computers? {userSkill}";
                    this.Skill = UserSkill.Beginner;
                    break;
                }

                case double d when (e.NewValue > 1 && e.NewValue <= 2): {
                    s.Value = 2.0;
                    userSkill = "Advanced";
                    s.ToolTip = userSkill;
                    this.lblUserSkill.Content = $"How would you describe your knowledge of computers? {userSkill}";
                    this.Skill = UserSkill.Advanced;
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
                    this.UserType = TypeOfUser.Casual;
                    break;
                }

                case double d when (e.NewValue > 1 && e.NewValue <= 2): {
                    s.Value = 2.0;
                    userType = "Web surfer";
                    s.ToolTip = userType;
                    this.lblUserType.Content = $"How do you use your computer? {userType}";
                    this.UserType = TypeOfUser.Websurfer;
                    break;
                }

                case double d when (e.NewValue > 2 && e.NewValue <= 3): {
                    s.Value = 3.0;
                    userType = "High performance";
                    s.ToolTip = userType;
                    this.lblUserType.Content = $"How do you use your computer? {userType}";
                    this.UserType = TypeOfUser.HighPerformance;
                    break;
                }

                case double d when (e.NewValue > 3 && e.NewValue <= 4): {
                    s.Value = 4.0;
                    userType = "Video editor";
                    s.ToolTip = userType;
                    this.lblUserType.Content = $"How do you use your computer? {userType}";
                    this.UserType = TypeOfUser.Video;
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
                    this.isLight = 1;

                    tm = new ThemeManager(ThemeManager.Theme.Light);

                    break;
                }

                case double d when (e.NewValue > 1 && e.NewValue <= 2): {
                    s.Value = 2.0;
                    userTheme = "Dark";
                    s.ToolTip = userTheme;
                    this.lblTheme.Content = $"Choose a theme! { userTheme}";
                    this.isLight = 0;

                    tm = new ThemeManager(ThemeManager.Theme.Dark);

                    break;
                }

                default: {
                    tm = new ThemeManager(ThemeManager.Theme.Light);
                    break;
                }
            }

            tm.ApplyTheme(this);
        }

        private bool AddUserData() {
            try {
                this.userData.IsLight = (this.isLight == 0 ? false : true);
                this.userData.IsAdvanced = this.Skill;
                this.userData.UserType = this.UserType;
                return true;
            } catch {
                return false;
            }

        }

        private void BtnFinish_Click(object sender, RoutedEventArgs e) {

            if (!AddUserData()) { // unsuccessful run of userdata, make userData a null user.
                this.userData = UserData.GetNullUser();
                this.DialogResult = false;
            }

            this.DialogResult = true;
            Close();
        }

    }
}
