using System;
using System.Collections.Generic;
using PC_Ripper_Benchmark.exception;

namespace PC_Ripper_Benchmark.util {

    /// <summary>
    /// The <see cref="CPUResults"/> class.
    /// <para></para>
    /// Represents benchmarking results regarding
    /// a particular test.
    /// <para>Author: Anthony Jaghab (c), all rights reserved.</para>
    /// </summary>

    public class CPUResults : Results {
        public override Tuple<string, TimeSpan> AverageTest => GenerateAverageTest(TestCollection);

        public override List<TimeSpan> TestCollection => throw new NotImplementedException();

        public override byte Score => GenerateScore();

        public override string Description => throw new NotImplementedException();

        public override string TestName => throw new NotImplementedException();

        public override byte UniqueTestCount => throw new NotImplementedException();

        protected override void GenerateDescription() {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generates a score that takes in the number of iterations
        /// per test, how much iterations performed per second/tick,
        /// and total execution time for all tests.
        /// <para>Result will be from 0-100</para>
        /// </summary>
        /// <returns></returns>
        /// <exception cref="UnknownTestException"></exception>

        protected override byte GenerateScore() {
            return 0;


        }
    }
}
