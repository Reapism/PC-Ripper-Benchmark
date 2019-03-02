using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PC_Ripper_Benchmark.util
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
    }
}
