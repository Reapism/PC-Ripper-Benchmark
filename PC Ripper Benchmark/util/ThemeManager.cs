using PC_Ripper_Benchmark.window;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace PC_Ripper_Benchmark.util {

    /// <summary>
    /// The <see cref="ThemeManager"/> class.
    /// <para></para>Contains functions for setting a windows
    /// Theme
    /// <para>Author: <see langword="Anthony Jaghab"/> (c),
    /// all rights reserved.</para>
    /// </summary>

    public class ThemeManager {

        private static MainWindow ui;
        private static Random rnd;
        private static bool run;
        private static byte currentColor;
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
        /// Default constructor.
        /// </summary>
        /// <param name="ui">The <see cref="MainWindow"/> instance.</param>

        public ThemeManager(MainWindow ui) {
            ThemeManager.ui = ui;
            rnd = new Random();
            currentColor = 0;
            run = false;
        }

        /// <summary>
        /// Runs a threaded test
        /// </summary>
        /// <param name="ui"></param>

        public void RunningTest(MainWindow ui) {
            run = true;
            Action a = new Action(() => {
                RunningTestHelper(ui);
            });

            Task t = new Task(a);
            t.Start();

        }

        /// <summary>
        /// Stop the colorizing.
        /// </summary>

        public static void StopRunningTest() {
            run = false;
            ui.Dispatcher.Invoke(() => {
                ui.grdTop.Background = Brushes.Black;
                ui.brdrPreLoader.BorderBrush = Brushes.White;
            });
        }

        private static void RunningTestHelper(MainWindow ui) {
            if (run != true) { return; }
            ui.Dispatcher.Invoke(() => {
                ColorAnimation colorChangeAnimation = new ColorAnimation {
                    From = Random,
                    To = Random,
                    Duration = TimeSpan.FromSeconds(3)
                };

                ColorAnimation colorChangeAnimation2 = new ColorAnimation {
                    From = Random,
                    To = Random,
                    Duration = TimeSpan.FromSeconds(3)
                };

                PropertyPath colorTargetPath = new PropertyPath("(Panel.Background).(SolidColorBrush.Color)");
                Storyboard CellBackgroundChangeStory = new Storyboard();
                CellBackgroundChangeStory.Completed += CellBackgroundChangeStory_Completed;
                Storyboard.SetTarget(colorChangeAnimation, ui.grdTop);
                Storyboard.SetTargetProperty(colorChangeAnimation, colorTargetPath);
                CellBackgroundChangeStory.Children.Add(colorChangeAnimation);
                CellBackgroundChangeStory.Begin();


                PropertyPath colorTargetPath2 = new PropertyPath("(Border.BorderBrush).(SolidColorBrush.Color)");
                Storyboard CellBackgroundChangeStory2 = new Storyboard();
                Storyboard.SetTarget(colorChangeAnimation2, ui.brdrPreLoader);
                Storyboard.SetTargetProperty(colorChangeAnimation2, colorTargetPath2);
                CellBackgroundChangeStory2.Children.Add(colorChangeAnimation2);
                CellBackgroundChangeStory2.Begin();
            });
            if (run != true) { StopRunningTest(); }
        }

        private static void CellBackgroundChangeStory_Completed(object sender, EventArgs e) {
            if (run == true) {
                ui.Dispatcher.Invoke(() => {
                    RunningTestHelper(ui);
                });
            } else {
                StopRunningTest(); }
        }

        private void GetColors(out Color color1, out Color color2) {
            color1 = Random;
            color2 = Random;
        }

        private Color Blue { get => Color.FromArgb(255, 154, 217, 243); }

        private Color Red { get => Color.FromArgb(255, 207, 88, 37); }

        private Color Green { get => Color.FromArgb(255, 30, 88, 37); }

        private Color Purple { get => Color.FromArgb(255, 198, 154, 240); }

        private Color Yellow { get => Color.FromArgb(255, 225, 213, 39); }

        private Color White { get => Color.FromArgb(255, 255, 255, 255); }

        private static Color Random { get => Color.FromArgb(255, (byte)rnd.Next(255), (byte)rnd.Next(255), (byte)rnd.Next(255)); }

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
