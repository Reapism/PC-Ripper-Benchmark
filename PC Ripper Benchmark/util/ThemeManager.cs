using PC_Ripper_Benchmark.window;
using System.Windows;
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

        /// <summary>
        /// The <see cref="Theme"/> enum type.
        /// <para>Represents all possible themes.</para>
        /// </summary>

        public enum Theme {

            /// <summary>
            /// The light theme.
            /// </summary>
            Light,

            /// <summary>
            /// The dark theme.
            /// </summary>
            Dark
        }

        /// <summary>
        /// ApplyAllThemes delegate. us
        /// </summary>
        /// <param name="themeType"></param>
        /// <param name="w"></param>

        public delegate void ApplyAllThemes(Theme themeType, Window w);

        private ApplyAllThemes applyAllThemes;

        public ThemeManager(Theme themeType, QuestionaireWindow w) {
            //applyAllThemes = new ApplyAllThemes(ApplyTheme);
        }

        /// <summary>
        /// Applies the theme to the <see cref="QuestionaireWindow"/>
        /// window.
        /// </summary>
        /// <param name="themeType"></param>
        /// <param name="w"></param>

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
