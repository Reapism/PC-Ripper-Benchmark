using System;
using System.Collections.Generic;

namespace PC_Ripper_Benchmark.util {


    /// <summary>
    /// The <see cref="DiskResults"/> class.
    /// <para></para>
    /// Represents benchmarking results for
    /// the disk tests.
    /// <para>Author: <see langword="Anthony Jaghab"/> (c),
    /// all rights reserved.</para>
    /// </summary>

    public class DiskResults : Results {

        public DiskResults() {

        }

        public override Tuple<string, TimeSpan> AverageTest => throw new NotImplementedException();

        public override List<TimeSpan> TestCollection => throw new NotImplementedException();

        public override byte Score => throw new NotImplementedException();

        public override string Description => throw new NotImplementedException();

        public override byte UniqueTestCount => throw new NotImplementedException();

        protected override Tuple<string, TimeSpan> GenerateAverageTest(List<TimeSpan> testCollection) {
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
