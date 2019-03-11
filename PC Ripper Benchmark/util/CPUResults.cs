using System;
using System.Collections.Generic;
using static PC_Ripper_Benchmark.function.RipperTypes;

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

        private readonly RipperSettings rs;
        private const byte uniqueTestCount = 5;

        public CPUResults(ref RipperSettings rs) {
            this.TestCollection = new List<TimeSpan>();
            this.rs = rs;
        }

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

        /// <summary>
        /// 
        /// </summary>

        public override byte UniqueTestCount => uniqueTestCount;

        /// <summary>
        /// Returns a <see cref="Tuple{T1, T2}"/> containing
        /// the name and average of the specific test under the
        /// <see langword="CPU component"/>.
        /// </summary>
        /// <param name="testCollection">The total collection of <see cref="TimeSpan"/>(s)
        /// for the component.</param>
        /// <param name="theTest">The test represented by <see cref="TestName"/>.</param>
        /// <returns></returns>

        protected override Tuple<string, TimeSpan> GenerateAverageTest(List<TimeSpan> testCollection, TestName theTest) {
            Tuple<string, TimeSpan> averageTest;

            switch (theTest) {

                case TestName.CPUSuccessorship: {
                    TimeSpan totalTime = new TimeSpan();

                    for (byte b = 0; b < this.rs.IterationsPerCPUTest; b++) {
                        totalTime = totalTime.Add(this.TestCollection[b]);
                    }

                    TimeSpan average = AverageTimespan(ref totalTime, this.rs.IterationsPerCPUTest);

                    averageTest = Tuple.Create(GetTestName(theTest), average);
                    break;
                }

                case TestName.CPUBoolean: {
                    TimeSpan totalTime = new TimeSpan();

                    for (byte b = 0; b < this.rs.IterationsPerCPUTest; b++) {
                        totalTime = totalTime.Add(this.TestCollection[b + (this.rs.IterationsPerCPUTest * 1)]);
                    }

                    TimeSpan average = AverageTimespan(ref totalTime, this.rs.IterationsPerCPUTest);
                    averageTest = Tuple.Create(GetTestName(theTest), average);
                    break;
                }

                case TestName.CPUQueue: {
                    TimeSpan totalTime = new TimeSpan();

                    for (byte b = 0; b < this.rs.IterationsPerCPUTest; b++) {
                        totalTime = totalTime.Add(this.TestCollection[b + (this.rs.IterationsPerCPUTest * 2)]);
                    }

                    TimeSpan average = AverageTimespan(ref totalTime, this.rs.IterationsPerCPUTest);
                    averageTest = Tuple.Create(GetTestName(theTest), average);
                    break;
                }

                case TestName.CPULinkedList: {
                    TimeSpan totalTime = new TimeSpan();

                    for (byte b = 0; b < this.rs.IterationsPerCPUTest; b++) {
                        totalTime = totalTime.Add(this.TestCollection[b + (this.rs.IterationsPerCPUTest * 3)]);
                    }

                    TimeSpan average = AverageTimespan(ref totalTime, this.rs.IterationsPerCPUTest);
                    averageTest = Tuple.Create(GetTestName(theTest), average);
                    break;
                }

                case TestName.CPUTree: {
                    TimeSpan totalTime = new TimeSpan();

                    for (byte b = 0; b < this.rs.IterationsPerCPUTest; b++) {
                        totalTime = totalTime.Add(this.TestCollection[b + (this.rs.IterationsPerCPUTest * 4)]);
                    }

                    TimeSpan average = AverageTimespan(ref totalTime, this.rs.IterationsPerCPUTest);
                    averageTest = Tuple.Create(GetTestName(theTest), average);
                    break;
                }

                default: {
                    return Tuple.Create("null", new TimeSpan());
                }
            }

            return averageTest;
        }

        protected override string GenerateDescription() => "";

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
