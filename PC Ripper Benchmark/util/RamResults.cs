using System;
using System.Collections.Generic;
using PC_Ripper_Benchmark.function;

namespace PC_Ripper_Benchmark.util {

    /// <summary>
    /// The <see cref="RamResults"/> class.
    /// <para></para>
    /// <para>Author: <see langword="Anthony Jaghab"/> (c),
    /// all rights reserved.</para>
    /// </summary>

    public class RamResults : Results {

        /// <summary>
        /// Returns a <see cref="Tuple{T1, T2}"/> containing
        /// the name of the test, and number.
        /// </summary>

        public override List<TimeSpan> TestCollection { get; }

        public override byte Score => throw new NotImplementedException();

        public override string Description => throw new NotImplementedException();

        public override byte UniqueTestCount => throw new NotImplementedException();

        protected override Tuple<string, TimeSpan> GenerateAverageTest(List<TimeSpan> testCollection, RipperTypes.TestName testName) {
            throw new NotImplementedException();
        }

        protected override string GenerateDescription() {
            throw new NotImplementedException();
        }

        protected override byte GenerateScore() {
            throw new NotImplementedException();
        }
    }
}
