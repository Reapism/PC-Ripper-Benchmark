using System;

namespace PC_Ripper_Benchmark.exception {

    /// <summary>
    /// The <see cref="RipperThreadException"/> class.
    /// <para>Thrown when there is an exception with connecting, or
    /// querying our database.</para>
    /// <para>Author: <see langword="Anthony Jaghab"/> (c),
    /// all rights reserved.</para>
    /// </summary>

    public class RipperThreadException : Exception {
        private string message;

        /// <summary>
        /// Parameterized constructor for taking 
        /// in a particular message.
        /// </summary>
        /// <param name="message">The exception message.</param>

        public RipperThreadException(string message) {
            this.message = message;
        }

        /// <summary>
        /// Returns the exception message for 
        /// this <see cref="RipperThreadException"/>.
        /// </summary>
        /// <returns></returns>

        public override string ToString() => this.message;

    }
}