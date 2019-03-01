using System;

namespace PC_Ripper_Benchmark.exception
{

    /// <summary>
    /// The <see cref="RipperDatabaseException"/> class.
    /// <para>Thrown when there is insufficient information from a
    /// class that inherits <see cref="PC_Ripper_Benchmark.util.Results"/>.</para>
    /// <para>Author: Jennifer Bolk (c), all rights reserved.</para>
    /// </summary>

    public class RipperDatabaseException : Exception
    {
        private string message;

        /// <summary>
        /// Parameterized constructor for taking 
        /// in a particular message.
        /// </summary>
        /// <param name="message">The exception message.</param>

        public RipperDatabaseException(string message)
        {
            this.message = message;
        }

        /// <summary>
        /// Returns the exception message for 
        /// this <see cref="RipperDatabaseException"/>.
        /// </summary>
        /// <returns></returns>

        public override string ToString()
        {
            return message;
        }

    }
}