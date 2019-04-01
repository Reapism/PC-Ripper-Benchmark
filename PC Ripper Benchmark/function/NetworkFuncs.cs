using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PC_Ripper_Benchmark.function
{

    /// <summary>
    /// The <see cref="Networkfuncs"/> class.
    /// <para> </para>
    /// Represents functions for checking the
    /// availability of a network connection
    /// <para>Author: David Hartglass
    /// all rights reserved.</para>
    /// </summary>
    public partial class Networkfuncs
    {
        /// <summary>
        /// Call this method to check if the 
        /// system has an active connection
        /// to the internet
        /// </summary>
        /// 

        public bool CheckInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                {
                    using (client.OpenRead("http://clients3.google.com/generate_204"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
