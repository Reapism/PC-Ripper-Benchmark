using PC_Ripper_Benchmark.window;
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace PC_Ripper_Benchmark.function {

    /// <summary>
    /// The <see cref="WindowSettings"/> class.
    /// <para></para>
    /// Contains methods that alter the windows position, cosemetics, etc.
    /// <para>Author: David Hartglass (c), all rights reserved.</para>
    /// </summary>

    public class WindowSettings {

        private const double expanded = 150.0;
        private const double contracted = 50.0;
        private const double grdWindowTopThickness = 100.0;

        private const double btnWidthExpanded = 130;
        private const double btnWidthContracted = 30;

        private const double btnHeightExpanded = 70;
        private const double btnHeightContracted = 30;

        private bool isNavigationShown;

        public WindowSettings() {
            this.isNavigationShown = false;
        }

        /// <summary>
        /// Method in <see cref="WindowSettings"/>.
        /// <para>Centers the window on the screen</para>
        /// </summary>

        public void CenterWindowOnScreen(Window myWindow) {
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            double windowWidth = myWindow.Width;
            double windowHeight = myWindow.Height;

            myWindow.Left = (screenWidth / 2) - (windowWidth / 2);
            myWindow.Top = (screenHeight / 2) - (windowHeight / 2);
        }

        /// <summary>
        /// Method in <see cref="WindowSettings"/>.
        /// <para>Takes the destination window, current window, and animation.
        /// The method changes the opacity of the current window
        /// to 0 and gradually opens the destination window.</para>
        /// </summary>

        public void TransitionScreen(Window destinationWindow, Window currentWindow) {
            DoubleAnimation animation = new DoubleAnimation();

            destinationWindow.Opacity = 0;
            destinationWindow.Show();
            currentWindow.Opacity = 0;

            animation.From = 0;
            animation.To = 1;
            animation.Duration = new Duration(TimeSpan.FromSeconds(1));
            destinationWindow.BeginAnimation(UIElement.OpacityProperty, animation);
            currentWindow.Close();
        }

        /// <summary>
        /// Call this method to expand and contract the
        /// navigation bar in the <see cref="MainWindow"/>.
        /// </summary>
        /// <param name="main">The <see cref="MainWindow"/> instance.</param>

        public void NavigationMenu(MainWindow main) {
            DoubleAnimation animation = new DoubleAnimation();
            DoubleAnimation animationSize = new DoubleAnimation();

            if (main.grdNavigation.Width == expanded) {
                main.grdNavigation.Width = contracted;
                main.grdNavigation.Background = Brushes.MidnightBlue;

                main.grdTop.Margin = new Thickness(0, 0, 0, 0);
                main.grdWindow.Margin = new Thickness(contracted, grdWindowTopThickness, 0, 0);

                main.btnCPU.Width = btnWidthContracted;
                main.btnCPU.Height = btnHeightContracted;

                main.btnRAM.Width = btnWidthContracted;
                main.btnRAM.Height = btnHeightContracted;

                main.btnDisk.Width = btnWidthContracted;
                main.btnDisk.Height = btnHeightContracted;

            } else {
                main.grdNavigation.Width = expanded;
                main.grdNavigation.Background = Brushes.SkyBlue;

                main.grdTop.Margin = new Thickness(0, 0, 0, 0);
                main.grdWindow.Margin = new Thickness(expanded, grdWindowTopThickness, 0, 0);

                main.btnCPU.Width = btnWidthExpanded;
                main.btnCPU.Height = btnHeightExpanded;

                main.btnRAM.Width = btnWidthExpanded;
                main.btnRAM.Height = btnHeightExpanded;

                main.btnDisk.Width = btnWidthExpanded;
                main.btnDisk.Height = btnHeightExpanded ;
            }

            animation.From = .9;
            animation.To = 1;
            animation.Duration = new Duration(TimeSpan.FromSeconds(1));
            main.grdNavigation.BeginAnimation(UIElement.OpacityProperty, animation);

        }
    }
}
