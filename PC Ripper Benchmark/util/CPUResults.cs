using PC_Ripper_Benchmark.exception;
using System;
using System.Collections.Generic;

namespace PC_Ripper_Benchmark.util {

    /// <summary>
    /// The <see cref="CPUResults"/> class.
    /// <para></para>
    /// Represents benchmarking results regarding
    /// a particular test.
    /// <para>Author: <see langword="Anthony Jaghab"/> (c),
    /// all rights reserved.</para>
    /// </summary>

    public class CPUResults : Results {

        /// <summary>
        /// Represents a <see cref="Tuple{T1, T2}"/> which contains
        /// the name of the test, and the average. If the 
        /// </summary>

        public override Tuple<string, TimeSpan> AverageTest => GenerateAverageTest(TestCollection);

        /// <summary>
        /// Represents all the timespans for the CPU tests.
        /// </summary>

        public override List<TimeSpan> TestCollection { get; }

        /// <summary>
        /// Represents the score for the test.
        /// </summary>

        public override byte Score => GenerateScore();

        /// <summary>
        /// Represents the description for test.
        /// </summary>

        public override string Description => GenerateDescription();

        public override byte UniqueTestCount => throw new NotImplementedException();

        protected override Tuple<string, TimeSpan> GenerateAverageTest(List<TimeSpan> testCollection) {
            throw new NotImplementedException();
        }

        protected override string GenerateDescription() {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generates a score that takes in the number of iterations
        /// per test, how much iterations performed per second/tick,
        /// and total execution time for all tests.
        /// <para>Result will be from 0-100</para>
        /// </summary>
        /// <returns></returns>

        protected override byte GenerateScore() {
            return 0;
        }
    }
}
