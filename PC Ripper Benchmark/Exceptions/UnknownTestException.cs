using System;

namespace PC_Ripper_Benchmark.Exceptions
{

    /// <summary>
    /// The <see cref="UnknownTestException"/> class.
    /// <para>Thrown when an unknown test is being used.</para>
    /// <para>Author: <see langword="Anthony Jaghab"/> (c),
    /// all rights reserved.</para>
    /// </summary>

    public class UnknownTestException : Exception
    {

        private string message;

        /// <summary>
        /// Parameterized constructor for taking 
        /// in a particular message.
        /// </summary>
        /// <param name="message">The exception message.</param>

        public UnknownTestException(string message)
        {
            this.message = message;
        }

        /// <summary>
        /// Returns the exception message for 
        /// this <see cref="UnknownTestException"/>.
        /// </summary>
        /// <returns></returns>

        public override string ToString() => this.message;
    }
}

