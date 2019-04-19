using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PC_Ripper_Benchmark.function {

    /// <summary>
    /// The <see cref="RipperDialog"/> class.
    /// <para> </para>
    /// Represents all the functions for 
    /// creating dialog boxes.
    /// <para>Author: <see langword="Anthony Jaghab"/> (c),
    /// all rights reserved.</para>
    /// </summary>

    public class RipperDialog {



        private static System.Windows.Forms.DialogResult InputBoxHelper() {
            return System.Windows.Forms.DialogResult.OK;
        }

        private static LinearGradientBrush CreateLinearGradient(Color color1, Color color2, params Point[] points) {
            LinearGradientBrush linear = new LinearGradientBrush {
                StartPoint = new Point(0, 0),
                EndPoint = new Point(1, 1)
            };
            int numPoints = points.Length;

            foreach (Point p in points) {
                linear.GradientStops.Add(new GradientStop() { Color = color1, Offset = 0.0 });
                linear.GradientStops.Add(new GradientStop() { Color = color2, Offset = 0.25 });

            }
            return linear;
        }

        public static System.Windows.Forms.DialogResult InputBox(string message, out string inputText, string caption = "Ripper Dialog") {
            Window w = new Window {
                WindowStyle = WindowStyle.None,
                Content = caption,
                Background = CreateLinearGradient(Colors.AliceBlue, Colors.AntiqueWhite, new Point(0.0,1.0))
            };

            TextBox textBox = new System.Windows.Controls.TextBox {
                Text = message,
                Background = Brushes.Transparent,
                Foreground = Brushes.White,
            };

            inputText = "";
            return System.Windows.Forms.DialogResult.OK;
        }

    }
}
