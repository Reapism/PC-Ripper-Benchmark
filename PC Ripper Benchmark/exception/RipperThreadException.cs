using System;

namespace PC_Ripper_Benchmark.exception
{

    /// <summary>
    /// Summary description for <see cref="RipperThreadException"/>
    /// </summary>
    public class RipperThreadException
    {
        private string message;

        /// <summary>
        /// Parameterized constructor for taking 
        /// in a particular message.
        /// </summary>
        /// <param name="message">The exception message.</param>

        public RipperThreadException(string message)
        {
            this.message = message;
        }

        /// <summary>
        /// Returns the exception message for 
        /// this <see cref="RipperThreadException"/>.
        /// </summary>
        /// <returns></returns>

        public override string ToString()
        {
            return message;
        }
    }
}