using PC_Ripper_Benchmark.exception;
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

    public class CPUResults : Results {

        /// <summary>
        /// Represents a <see cref="Tuple{T1, T2}"/> which contains
        /// the name of the test, and the average. If the 
        /// </summary>

        public override Tuple<string, TimeSpan> AverageTest => GenerateAverageTest(TestCollection);

        public override List<TimeSpan> TestCollection { get; }

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
        /// <exception cref="RipperScoreException"></exception>

        protected override byte GenerateScore() {
            return 0;


        }
    }
}
