using System;
using System.Collections.Generic;

namespace PC_Ripper_Benchmark.util {

    /// <summary>
    /// The <see cref="IResults"/> interface.
    /// <para>Contains signatures for representing generic 
    /// results for a particular components.</para>
    /// <para>Author: Anthony Jaghab (c), all rights reserved.</para>
    /// </summary>

    public interface IResults {

        /// <summary>
        /// Represents a dictionary containing the name of the test,
        /// and the average time it took the test.
        /// </summary>
        Dictionary<string, TimeSpan> AveragePerTest { get; }

        /// <summary>
        /// Represents the individual time for each test before the averaging.
        /// </summary>
        Dictionary<string, TimeSpan> TimePerTest { get; }

        /// <summary>
        /// Represents the score for this particular test.
        /// </summary>
        byte Score { get; }

        /// <summary>
        /// Represents the description for this particular test.
        /// </summary>
        string Description { get; }

    }
}
