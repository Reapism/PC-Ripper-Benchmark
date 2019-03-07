using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_Ripper_Benchmark.util
{
    /// <summary>
    /// The <see cref="Encryption"/> class.
    /// <para></para>
    /// Contains all methods pertaining to 
    /// encrypting and decrypting passwords
    /// <para>Author: David Hartglass (c), all rights reserved.</para>
    /// </summary>
    /// 
    public class Encryption
    {         
        /// <summary>
        /// Returns whether the <paramref name="email"/> is valid.
        /// <para>Validates whether a string is a valid email address.</para>
        /// </summary>
        public String encryptPassword(String password) {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(password);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            String hash = System.Text.Encoding.ASCII.GetString(data);

            return hash;
        }        
    }
}
