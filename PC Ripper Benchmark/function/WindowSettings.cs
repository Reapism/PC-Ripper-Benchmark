using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace PC_Ripper_Benchmark.function
{

    /// <summary>
    /// The <see cref="WindowSettings"/> class.
    /// <para></para>
    /// Contains methods that alter the windows position, cosemetics, etc.
    /// <para>Author: David Hartglass (c), all rights reserved.</para>
    /// </summary>

    public partial class WindowSettings
    {
        /// <summary>
        /// Method in <see cref="WindowSettings"/>.
        /// <para>Centers the window on the screen</para>
        /// </summary>
        /// 
        public void CenterWindowOnScreen(Window myWindow)
        {
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
        public void TransitionToCreateAccountScreen(CreateAccountWindow destinationWindow, LoginWindow currentWindow, DoubleAnimation animation)
        {
            destinationWindow.Opacity = 0;
            destinationWindow.Show();
            currentWindow.Opacity = 0;

            animation.From = 0;
            animation.To = 1;
            animation.Duration = new Duration(TimeSpan.FromSeconds(1));
            destinationWindow.BeginAnimation(UIElement.OpacityProperty, animation);
        }


        /// <summary>
        /// Method in <see cref="WindowSettings"/>.
        /// <para>Takes the destination window, current window, and animation.
        /// The method changes the opacity of the current window
        /// to 0 and gradually opens the destination window.</para>
        /// </summary>
        public void TransitionToLoginScreen(LoginWindow destinationWindow, CreateAccountWindow currentWindow, DoubleAnimation animation)
        {
            destinationWindow.Opacity = 0;
            destinationWindow.Show();
            currentWindow.Opacity = 0;

            animation.From = 0;
            animation.To = 1;
            animation.Duration = new Duration(TimeSpan.FromSeconds(1));
            destinationWindow.BeginAnimation(UIElement.OpacityProperty, animation);
        }
    }
}
