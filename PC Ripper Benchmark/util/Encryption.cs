using System;

namespace PC_Ripper_Benchmark.util {

    /// <summary>
    /// The <see cref="Encryption"/> class.
    /// <para></para>
    /// Contains all methods pertaining to 
    /// encrypting and decrypting passwords
    /// <para>Author: David Hartglass (c), all rights reserved.</para>
    /// </summary>

    public class Encryption {

        /// <summary>
        /// Returns the <paramref name="text"/> as an encrypted string.
        /// <para>Encrypts a stream of bytes into a string using SHA256</para>
        /// </summary>

        public string EncryptText(string text) {
            String upperCaseText = text.ToUpper().Trim();
            byte[] data = System.Text.Encoding.ASCII.GetBytes(upperCaseText);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);

            String hash = System.Text.Encoding.ASCII.GetString(data);
            return hash;
        }
    }
}
