using System;

namespace PC_Ripper_Benchmark.util {

    /// <summary>
    /// The <see cref="HashManager"/> class.
    /// <para></para>
    /// Contains all methods pertaining to 
    /// hashing strings.
    /// <para>Author: David Hartglass, Anthony Jaghab (c) all rights reserved.</para>
    /// </summary>

    public class HashManager {

        /// <summary>
        /// Returns the <paramref name="text"/> as an encrypted string.
        /// <para>Encrypts a stream of bytes into a string using SHA256</para>
        /// </summary>

        public string HashTextSHA256(string text) {
            string upperCaseText = text.ToUpper().Trim(); // convert string to uppercase.
            byte[] data = System.Text.Encoding.ASCII.GetBytes(upperCaseText); // convert to bytes

            // hash the byte array
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);

            return System.Text.Encoding.ASCII.GetString(data); // return the hash
        }

        /// <summary>
        /// Returns the <paramref name="text"/> as an encrypted string.
        /// <para>Encrypts a stream of bytes into a string using SHA256</para>
        /// </summary>

        public string HashUniqueTextSHA256(string text) {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(text); // convert to bytes

            // hash the byte array
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);

            return System.Text.Encoding.ASCII.GetString(data); // return the hash
        }
    }
}
