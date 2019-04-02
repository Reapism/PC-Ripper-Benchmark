using System.Net.NetworkInformation;

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

        public bool IsInternetAvailable() =>
            NetworkInterface.GetIsNetworkAvailable();
       
    }
}
