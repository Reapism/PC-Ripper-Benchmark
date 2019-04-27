using System;
using System.Collections.Generic;
using PC_Ripper_Benchmark.exception;
using PC_Ripper_Benchmark.function;
using PC_Ripper_Benchmark.window;
using static PC_Ripper_Benchmark.function.RipperTypes;

namespace PC_Ripper_Benchmark.util {


    /// <summary>
    /// The <see cref="RamResults"/> class.
    /// <para></para>
    /// Represents benchmarking results regarding
    /// the RAM benchmarking test.
    /// <para>Author: <see langword="Anthony Jaghab"/> (c),
    /// all rights reserved.</para>
    /// </summary>

    public class RamResults : Results {

        private readonly RipperSettings rs;
        private readonly UserData userData;
        private const byte uniqueTestCount = 3;

        /// <summary>
        /// Constructs <see cref="RamResults"/> with
        /// a <see cref="RipperSettings"/> instance.
        /// </summary>
        /// <param name="rs">Takes in an initial <see cref="RipperSettings"/>
        /// but is marked <see langword="readonly"/> internally.</param>
        /// <param name="userData">The <see cref="UserData"/> to get the
        /// UserType from.</param>

        public RamResults(RipperSettings rs, ref UserData userData) {
            this.TestCollection = new List<TimeSpan>();
            this.rs = rs;
            this.userData = userData;
        }

        /// <summary>
        /// Get the <see cref="ThreadType"/> for the test.
        /// </summary>
        
        protected override ThreadType GetThreadType { get; }

        /// <summary>
        /// Represents all the timespans for the RAM tests.
        /// </summary>

        public override List<TimeSpan> TestCollection { get; }

        /// <summary>
        /// Represents the score for the test.
        /// </summary>

        public override byte Score => GenerateScore();

        /// <summary>
        /// Represents the description for test.
        /// </summary>

        public override string Description => userData.IsAdvanced == UserData.UserSkill.Advanced ?
            GenerateAdvancedDescription() : GenerateBeginnerDescription();

        /// <summary>
        /// The number of different tests for the RAM component.
        /// </summary>

        public override byte UniqueTestCount => uniqueTestCount;


        /// <summary>
        /// Returns a <see cref="Tuple{T1, T2}"/> containing
        /// the name and average of the specific test under the
        /// <see langword="RAM component"/>.
        /// </summary>
        /// <param name="testCollection">The total collection of <see cref="TimeSpan"/>(s)
        /// for the component.</param>
        /// <param name="theTest">The test represented by <see cref="TestName"/>.</param>
        /// <returns></returns>

        protected override Tuple<string, TimeSpan> GenerateAverageTest(List<TimeSpan> testCollection, TestName theTest) {
            Tuple<string, TimeSpan> averageTest;

            switch (theTest) {

                case TestName.RAMFolderMatrix: {
                    TimeSpan totalTime = new TimeSpan();

                    for (byte b = 0; b < this.rs.IterationsPerRAMTest; b++) {
                        totalTime = totalTime.Add(this.TestCollection[b]);
                    }

                    TimeSpan average = AverageTimespan(ref totalTime, this.rs.IterationsPerRAMTest);

                    averageTest = Tuple.Create(GetTestName(theTest), average);
                    break;
                }

                case TestName.RAMBulkFile: {
                    TimeSpan totalTime = new TimeSpan();

                    for (byte b = 0; b < this.rs.IterationsPerRAMTest; b++) {
                        totalTime = totalTime.Add(this.TestCollection[b + (this.rs.IterationsPerRAMTest * 1)]);
                    }

                    TimeSpan average = AverageTimespan(ref totalTime, this.rs.IterationsPerRAMTest);
                    averageTest = Tuple.Create(GetTestName(theTest), average);
                    break;
                }

                case TestName.RAMReferenceDereferenceParse: {
                    TimeSpan totalTime = new TimeSpan();

                    for (byte b = 0; b < this.rs.IterationsPerRAMTest; b++) {
                        totalTime = totalTime.Add(this.TestCollection[b + (this.rs.IterationsPerRAMTest * 2)]);
                    }

                    TimeSpan average = AverageTimespan(ref totalTime, this.rs.IterationsPerRAMTest);
                    averageTest = Tuple.Create(GetTestName(theTest), average);
                    break;
                }

                default: {
                    return Tuple.Create("**UnknownTest**", new TimeSpan());
                }
            }

            return averageTest;
        }

        /// <summary>
        /// Generates a description for this <see cref="RamResults"/> instance.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="UnknownTestException"></exception>

        protected override string GenerateAdvancedDescription() {

            // Checking the worst case scenario. if these dont equal, something bad happened.
            if (this.UniqueTestCount * this.rs.IterationsPerRAMTest != this.TestCollection.Count) {
                throw new UnknownTestException($"Generating RAM Description: Number of Test " +
                    $"Collection elements does not add up.  {this.UniqueTestCount} * {this.rs.IterationsPerRAMTest} != " +
                    $"{this.TestCollection.Count}");
            }

            string desc = string.Empty;
            desc += $"Running a {GetThreadAsString(GetThreadType)} benchmarking test" + Environment.NewLine;
            desc += $"Username: {Environment.UserName}" + Environment.NewLine;
            desc += $"Time: {DateTime.Now.ToLongTimeString()}" + Environment.NewLine;
            desc += $"Date: {DateTime.Now.ToLongDateString()}" + Environment.NewLine;

            desc += $"The Ripper runs {this.rs.IterationsPerRAMTest} iterations of each test. Below are the durations:";
            desc += Environment.NewLine;

            desc += "Each test runs with a specific number of iterations";
            desc += Environment.NewLine;
            desc += $"\tThe {GetTestName(TestName.RAMFolderMatrix)} ran " +
                $"{this.rs.IterationsRAMFolderMatrix.ToString("n0")} iterations per test" + Environment.NewLine;
            desc += $"\tThe {GetTestName(TestName.RAMBulkFile)} ran " +
                $"{this.rs.IterationsRAMVirtualBulkFile.ToString("n0")} iterations per test" + Environment.NewLine;
            desc += $"\tThe {GetTestName(TestName.RAMReferenceDereferenceParse)} ran " +
                $"{this.rs.IterationsRAMReferenceDereference.ToString("n0")} iterations per test" + Environment.NewLine;


            byte index = 0;

            for (byte b = 0; b < this.rs.IterationsPerRAMTest; b++) {
                desc += $"\t{GetTestName(TestName.RAMFolderMatrix)}[{b + 1}] - " +
                    $"{this.TestCollection[index].ToString()}" + Environment.NewLine;
                index++;
            }

            for (byte b = 0; b < this.rs.IterationsPerRAMTest; b++) {
                desc += $"\t{GetTestName(TestName.RAMBulkFile)}[{b + 1}] - " +
                    $"{this.TestCollection[index].ToString()}" + Environment.NewLine;
                index++;
            }

            for (byte b = 0; b < this.rs.IterationsPerRAMTest; b++) {
                desc += $"\t{GetTestName(TestName.RAMReferenceDereferenceParse)}[{b + 1}] - " +
                    $"{this.TestCollection[index].ToString()}" + Environment.NewLine;
                index++;
            }


            // average per test.
            desc += Environment.NewLine;
            desc += "Average duration per test:";
            desc += Environment.NewLine;

            Tuple<string, TimeSpan> averageTest;

            averageTest =
                GenerateAverageTest(this.TestCollection, TestName.RAMFolderMatrix);
            desc += $"\t{averageTest.Item1} - {averageTest.Item2} {Environment.NewLine}";

            averageTest =
                GenerateAverageTest(this.TestCollection, TestName.RAMBulkFile);
            desc += $"\t{averageTest.Item1} - {averageTest.Item2} {Environment.NewLine}";

            averageTest =
                GenerateAverageTest(this.TestCollection, TestName.RAMReferenceDereferenceParse);
            desc += $"\t{averageTest.Item1} - {averageTest.Item2} {Environment.NewLine}";

            // total time of all tests. 
            desc += "Total duration of the test:";
            desc += $"\t{TotalTimeSpan(this.TestCollection)}";

            // score for the test.
            desc += Environment.NewLine;

            desc += $"The score for this test is {this.Score}.";

            desc += Environment.NewLine + Environment.NewLine;
            desc += "(Algorithm not implemented for generating a score)";
            return desc;

        }

        /// <summary>
        /// Represents a more simplified description for the results.
        /// Generated if the underlying user is a 
        /// <see cref="UserData.UserSkill.Beginner"/>.
        /// </summary>
        /// <returns></returns>

        protected override string GenerateBeginnerDescription() {
            string desc = string.Empty;

            desc += "Total duration of the test:";
            desc += $"\t{TotalTimeSpan(this.TestCollection)}";

            // score for the test.
            desc += Environment.NewLine;

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

        protected override string GenerateScoreDescription(UserData.TypeOfUser typeOfUser, byte score) {
            throw new NotImplementedException();
        }
    }
}
