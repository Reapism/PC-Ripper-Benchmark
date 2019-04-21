using PC_Ripper_Benchmark.window;
using System;
using System.Threading.Tasks;
using System.Windows.Controls;
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

        private Theme themeType;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="themeType">The the</param>

        public ThemeManager(Theme themeType) {
            this.themeType = themeType;
        }

        /// <summary>
        /// Runs a threaded test
        /// </summary>
        /// <param name="ui"></param>

        public void RunningTest(MainWindow ui) {
            Action a = new Action(() => {
                RunningTestHelper(ui, 0);
            });

            Task t = new Task(a);
            t.Start();
            
        }

        private void RunningTestHelper(MainWindow ui, byte color) {
            const byte max_color = 8;
            if (color == max_color) { color = 0; }

            ui.Dispatcher.Invoke(() => {
               
                color++;
            });



        }

        /// <summary>
        /// Applies the theme to a specific object.
        /// </summary>
        /// <param name="windows">A list of windows.</param>

        public void ApplyTheme(params object[] windows) {
            foreach (object o in windows) {
                if (o is QuestionaireWindow q) {
                    ApplyThemeHelper(q);
                } else if (o is MainWindow w) {
                    ApplyThemeHelper(w);
                }
            }
        }

        public void ApplyThemeHelper(MainWindow w) {
            switch (this.themeType) {
                case Theme.Light: {

                    break;
                }

                case Theme.Dark: {

                    break;
                }
            }
        }

        public void ApplyThemeHelper(QuestionaireWindow q) {
            switch (this.themeType) {
                case Theme.Light: {
                    q.grdMain.Background = Brushes.White;
                    q.lblTheme.Foreground = Brushes.Black;
                    q.lblUserSkill.Foreground = Brushes.Black;
                    q.lblUserType.Foreground = Brushes.Black;
                    q.lblWelcome.Foreground = Brushes.Black;
                    break;

                }

                case Theme.Dark: {
                    q.grdMain.Background = Brushes.DarkSlateGray;
                    q.lblTheme.Foreground = Brushes.White;
                    q.lblUserSkill.Foreground = Brushes.White;
                    q.lblUserType.Foreground = Brushes.White;
                    q.lblWelcome.Foreground = Brushes.White;
                    break;
                }
            }
        }
    }
}
