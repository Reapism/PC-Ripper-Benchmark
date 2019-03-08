using PC_Ripper_Benchmark.window;
using System.Windows.Media;

namespace PC_Ripper_Benchmark.util {

    /// <summary>
    /// The <see cref="ThemeManager"/> class.
    /// <para></para>Contains functions for setting a windows
    /// Theme
    /// <para>Author: <see langword="Anthony Jaghab"/> (c),
    /// all rights reserved.</para>
    /// </summary>

    public class ThemeManager {

        public enum Theme {
            Light,
            Dark
        }

        /// <summary>
        /// Default constructor.
        /// </summary>

        public ThemeManager() {
        }

        public void ApplyTheme(Theme themeType, QuestionaireWindow w) {
            switch (themeType) {
                case Theme.Light: {
                    w.grdMain.Background = Brushes.White;
                    w.lblTheme.Foreground = Brushes.Black;
                    w.lblUserSkill.Foreground = Brushes.Black;
                    w.lblUserType.Foreground = Brushes.Black;
                    w.lblWelcome.Foreground = Brushes.Black;
                    break;

                }

                case Theme.Dark: {
                    w.grdMain.Background = Brushes.DarkSlateGray;
                    w.lblTheme.Foreground = Brushes.White;
                    w.lblUserSkill.Foreground = Brushes.White;
                    w.lblUserType.Foreground = Brushes.White;
                    w.lblWelcome.Foreground = Brushes.White;
                    break;
                }
            }
        }

    }
}
