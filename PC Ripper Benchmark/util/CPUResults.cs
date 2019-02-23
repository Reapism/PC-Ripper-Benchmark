using System;
using System.Collections.Generic;

namespace PC_Ripper_Benchmark.util {

    /// <summary>
    /// The <see cref="CPUResults"/> class.
    /// <para></para>
    /// Represents benchmarking results regarding
    /// a particular test.
    /// <para>Author: Anthony Jaghab (c), all rights reserved.</para>
    /// </summary>

    public class CPUResults : IResults
    {
        public Dictionary<string, TimeSpan> AveragePerTest => throw new NotImplementedException();

        public Dictionary<string, TimeSpan> TimePerTest => throw new NotImplementedException();

        public byte Score => throw new NotImplementedException();

        public string Description => throw new NotImplementedException();
    }
}
