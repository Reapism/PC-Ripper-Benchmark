using PC_Ripper_Benchmark.exception;
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

        /// <summary>
        /// Generates a description for this <see cref="DiskResults"/> instance.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="UnknownTestException"></exception>

        protected override string GenerateDescription() {
            // Checking the worst case scenario. if these dont equal, something bad happened.
            if (this.UniqueTestCount * this.rs.IterationsPerDiskTest != this.TestCollection.Count) {
                throw new UnknownTestException($"Generating DISK Description: Number of test " +
                    $"collection elements does not add up.  {this.UniqueTestCount} * {this.rs.IterationsPerDiskTest} != " +
                    $"{this.TestCollection.Count}");
            }

            string desc = string.Empty;

            desc += "Each test runs with a specific number of iterations";
            desc += Environment.NewLine;
            desc += $"\tThe {GetTestName(TestName.DISKFolderMatrix)} ran " +
                $"{this.rs.IterationsSuccessorship.ToString("n0")} iterations per test" + Environment.NewLine;
            desc += $"\tThe {GetTestName(TestName.DISKBulkFile)} ran " +
                $"{this.rs.IterationsBoolean.ToString("n0")} iterations per test" + Environment.NewLine;
            desc += $"\tThe {GetTestName(TestName.DISKReadWriteParse)} ran " +
                $"{this.rs.IterationsQueue.ToString("n0")} iterations per test" + Environment.NewLine;
            desc += $"\tThe {GetTestName(TestName.DISKRipper)} ran " +
                $"{this.rs.IterationsLinkedList.ToString("n0")} iterations per test" + Environment.NewLine;  

            // each duration printed.
            // Should later put in another function.

            desc += $"The Ripper runs {this.rs.IterationsPerDiskTest} iterations of each test. Below are the durations:";
            desc += Environment.NewLine;

            byte index = 0;

            for (byte b = 0; b < this.rs.IterationsPerDiskTest; b++) {
                desc += $"\t{GetTestName(TestName.DISKFolderMatrix)}[{b + 1}] - " +
                    $"{this.TestCollection[index].ToString()}" + Environment.NewLine;
                index++;
            }

            for (byte b = 0; b < this.rs.IterationsPerDiskTest; b++) {
                desc += $"\t{GetTestName(TestName.DISKBulkFile)}[{b + 1}] - " +
                    $"{this.TestCollection[index].ToString()}" + Environment.NewLine;
                index++;
            }

            for (byte b = 0; b < this.rs.IterationsPerDiskTest; b++) {
                desc += $"\t{GetTestName(TestName.DISKReadWriteParse)}[{b + 1}] - " +
                    $"{this.TestCollection[index].ToString()}" + Environment.NewLine;
                index++;
            }

            for (byte b = 0; b < this.rs.IterationsPerDiskTest; b++) {
                desc += $"\t{GetTestName(TestName.DISKRipper)}[{b + 1}] - " +
                    $"{this.TestCollection[index].ToString()}" + Environment.NewLine;
                index++;
            }

            // total time of all tests. 
            desc += "Total duration of the test:";
            desc += $"\t{TotalTimeSpan(this.TestCollection)}";

            // average per test.
            desc += Environment.NewLine;
            desc += "Average duration per test:";
            desc += Environment.NewLine;

            Tuple<string, TimeSpan> averageTest;

            averageTest =
                GenerateAverageTest(this.TestCollection, TestName.DISKFolderMatrix);
            desc += $"\t{averageTest.Item1} - {averageTest.Item2} {Environment.NewLine}";

            averageTest =
                GenerateAverageTest(this.TestCollection, TestName.DISKBulkFile);
            desc += $"\t{averageTest.Item1} - {averageTest.Item2} {Environment.NewLine}";

            averageTest =
                GenerateAverageTest(this.TestCollection, TestName.DISKReadWriteParse);
            desc += $"\t{averageTest.Item1} - {averageTest.Item2} {Environment.NewLine}";

            averageTest =
                GenerateAverageTest(this.TestCollection, TestName.DISKRipper);
            desc += $"\t{averageTest.Item1} - {averageTest.Item2} {Environment.NewLine}";

            // score for the test.
            desc += Environment.NewLine + Environment.NewLine;

            desc += $"The score for this test is {this.Score}.";

            desc += Environment.NewLine + Environment.NewLine;
            desc += "(Algorithm not implemented for generating a score)";
            return desc;
        }

        /// <summary>
        /// Generates a score that takes in the number of iterations
        /// per test, how much iterations performed per second/tick,
        /// and total execution time for all tests.
        /// <para>Result will be from 0-100</para>
        /// <para>101+ will be errors</para>
        /// </summary>
        /// <returns></returns>

        protected override byte GenerateScore() {
            return 0;
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
