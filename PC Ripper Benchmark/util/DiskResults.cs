using PC_Ripper_Benchmark.function;
using System;
using System.Collections.Generic;
using static PC_Ripper_Benchmark.function.RipperTypes;

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

        private readonly RipperSettings rs;
        private const byte uniqueTestCount = 4;

        /// <summary>
        /// Constructs <see cref="DiskResults"/> with
        /// a <see cref="RipperSettings"/> instance.
        /// </summary>
        /// <param name="rs">Takes in an initial <see cref="RipperSettings"/>
        /// but is marked <see langword="readonly"/> internally.</param>

        public DiskResults(RipperSettings rs) {
            this.TestCollection = new List<TimeSpan>();
            this.rs = rs;
        }

        /// <summary>
        /// Represents all the timespans for the DISK tests.
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
        /// The number of different tests for the DISK component.
        /// </summary>

        public override byte UniqueTestCount => uniqueTestCount;

        protected override string GenerateDescription() {
            throw new NotImplementedException();
        }

        protected override byte GenerateScore() {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a <see cref="Tuple{T1, T2}"/> containing
        /// the name and average of the specific test under the
        /// <see langword="DISK component"/>.
        /// </summary>
        /// <param name="testCollection">The total collection of <see cref="TimeSpan"/>(s)
        /// for the component.</param>
        /// <param name="theTest">The test represented by <see cref="TestName"/>.</param>
        /// <returns></returns>

        protected override Tuple<string, TimeSpan> GenerateAverageTest(List<TimeSpan> testCollection, TestName theTest) {
            Tuple<string, TimeSpan> averageTest;

            switch (theTest) {

                case TestName.DISKFolderMatrix: {
                    TimeSpan totalTime = new TimeSpan();

                    for (byte b = 0; b < this.rs.IterationsPerDiskTest; b++) {
                        totalTime = totalTime.Add(this.TestCollection[b]);
                    }

                    TimeSpan average = AverageTimespan(ref totalTime, this.rs.IterationsPerDiskTest);

                    averageTest = Tuple.Create(GetTestName(theTest), average);
                    break;
                }

                case TestName.DISKBulkFile: {
                    TimeSpan totalTime = new TimeSpan();

                    for (byte b = 0; b < this.rs.IterationsPerDiskTest; b++) {
                        totalTime = totalTime.Add(this.TestCollection[b + (this.rs.IterationsPerDiskTest * 1)]);
                    }

                    TimeSpan average = AverageTimespan(ref totalTime, this.rs.IterationsPerDiskTest);
                    averageTest = Tuple.Create(GetTestName(theTest), average);
                    break;
                }

                case TestName.DISKReadWriteParse: {
                    TimeSpan totalTime = new TimeSpan();

                    for (byte b = 0; b < this.rs.IterationsPerDiskTest; b++) {
                        totalTime = totalTime.Add(this.TestCollection[b + (this.rs.IterationsPerDiskTest * 2)]);
                    }

                    TimeSpan average = AverageTimespan(ref totalTime, this.rs.IterationsPerDiskTest);
                    averageTest = Tuple.Create(GetTestName(theTest), average);
                    break;
                }

                case TestName.DISKRipper: {
                    TimeSpan totalTime = new TimeSpan();

                    for (byte b = 0; b < this.rs.IterationsPerDiskTest; b++) {
                        totalTime = totalTime.Add(this.TestCollection[b + (this.rs.IterationsPerDiskTest * 3)]);
                    }

                    TimeSpan average = AverageTimespan(ref totalTime, this.rs.IterationsPerDiskTest);
                    averageTest = Tuple.Create(GetTestName(theTest), average);
                    break;
                }

                default: {
                    return Tuple.Create("**UnknownTest**", new TimeSpan());
                }
            }

            return averageTest;
        }
    }
}
