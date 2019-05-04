using System;

namespace PC_Ripper_Benchmark.exception {

    /// <summary>
    /// The <see cref="RipperScoreException"/> class.
    /// <para>Thrown when there is an impossible scores passed
    /// into some of the functions that require scores.</para>
    /// <para>Author: <see langword="Anthony Jaghab"/> (c),
    /// all rights reserved.</para>
    /// </summary>

    public class RipperScoreException : Exception {
        private string message;

        /// <summary>
        /// Parameterized constructor for taking 
        /// in a particular message.
        /// </summary>
        /// <param name="message">The exception message.</param>

        public RipperScoreException(string message) {
            this.message = message;
        }

        /// <summary>
        /// Returns the exception message for 
        /// this <see cref="RipperScoreException"/>.
        /// </summary>
        /// <returns></returns>

        public override string ToString() {
            return this.message;
        }

    }
}