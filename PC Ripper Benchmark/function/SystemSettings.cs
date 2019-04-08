using System.Net;
using System.Net.NetworkInformation;
using System.Windows;
using System.Windows.Forms;

namespace PC_Ripper_Benchmark.function {

    /// <summary>
    /// The <see cref="SystemSettings"/> class.
    /// <para>Represents generic functions for the
    /// application.</para>
    /// <para>Author: <see langword="Anthony Jaghab"/> (c),
    /// all rights reserved.</para>
    /// </summary>

    public class SystemSettings {

        /// <summary>
        /// Returns whether the internet is available 
        /// or not.
        /// </summary>
        /// <returns></returns>

        public static bool IsInternetAvailable()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://clients3.google.com/generate_204"))
                {
                    return true;
                }
            }
            catch (System.Exception)
            {
                System.Windows.MessageBox.Show("No internet connection!", "No Internet", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
        }

    }
}
