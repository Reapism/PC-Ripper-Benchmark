using PC_Ripper_Benchmark.exception;
using PC_Ripper_Benchmark.function;
using System;
using System.Collections.Generic;
using System.Linq;
using static PC_Ripper_Benchmark.function.RipperTypes;

namespace PC_Ripper_Benchmark.util {

    /// <summary>
    /// The <see cref="CPUResults"/> class.
    /// <para></para>
    /// Represents benchmarking results regarding
    /// the CPU benchmarking test.
    /// <para>Author: <see langword="Anthony Jaghab"/> (c),
    /// all rights reserved.</para>
    /// </summary>

    public class CPUResults : Results {

        private readonly RipperSettings rs;
        private readonly UserData userData;
        private const byte uniqueTestCount = 5;

        private TimeSpan totalDuration;

        /// <summary>
        /// Constructs <see cref="CPUResults"/> with
        /// a <see cref="RipperSettings"/> instance.
        /// </summary>
        /// <param name="rs">Takes in an initial <see cref="RipperSettings"/>
        /// but is marked <see langword="readonly"/> internally.</param>
        /// <param name="userData">The <see cref="UserData"/> to get the
        /// UserType from.</param>
        /// <param name="threadType">The thread type for the test.</param>

        public CPUResults(RipperSettings rs, ref UserData userData, ThreadType threadType) {
            this.TestCollection = new List<TimeSpan>();
            this.rs = rs;
            this.userData = userData;
            this.totalDuration = new TimeSpan();
            this.GetThreadType = threadType;
        }

        /// <summary>
        /// Get the <see cref="ThreadType"/> for the test.
        /// </summary>

        protected override ThreadType GetThreadType { get; }

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

        public override string Description => this.userData.IsAdvanced == UserData.UserSkill.Advanced ?
            GenerateAdvancedDescription() : GenerateBeginnerDescription();

        /// <summary>
        /// The number of different tests for the CPU component.
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
                        totalTime = totalTime.Add(this.TestCollection[b + (this.rs.IterationsPerCPUTest * (int)TestName.CPUBoolean)]);
                    }

                    TimeSpan average = AverageTimespan(ref totalTime, this.rs.IterationsPerCPUTest);
                    averageTest = Tuple.Create(GetTestName(theTest), average);
                    break;
                }

                case TestName.CPUQueue: {
                    TimeSpan totalTime = new TimeSpan();

                    for (byte b = 0; b < this.rs.IterationsPerCPUTest; b++) {
                        totalTime = totalTime.Add(this.TestCollection[b + (this.rs.IterationsPerCPUTest * (int)TestName.CPUQueue)]);
                    }

                    TimeSpan average = AverageTimespan(ref totalTime, this.rs.IterationsPerCPUTest);
                    averageTest = Tuple.Create(GetTestName(theTest), average);
                    break;
                }

                case TestName.CPULinkedList: {
                    TimeSpan totalTime = new TimeSpan();

                    for (byte b = 0; b < this.rs.IterationsPerCPUTest; b++) {
                        totalTime = totalTime.Add(this.TestCollection[b + (this.rs.IterationsPerCPUTest * (int)TestName.CPULinkedList)]);
                    }

                    TimeSpan average = AverageTimespan(ref totalTime, this.rs.IterationsPerCPUTest);
                    averageTest = Tuple.Create(GetTestName(theTest), average);
                    break;
                }

                case TestName.CPUTree: {
                    TimeSpan totalTime = new TimeSpan();

                    for (byte b = 0; b < this.rs.IterationsPerCPUTest; b++) {
                        totalTime = totalTime.Add(this.TestCollection[b + (this.rs.IterationsPerCPUTest * (int)TestName.CPUTree)]);
                    }

                    TimeSpan average = AverageTimespan(ref totalTime, this.rs.IterationsPerCPUTest);
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
        /// Generates a description for this <see cref="CPUResults"/> instance.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="UnknownTestException"></exception>

        protected override string GenerateAdvancedDescription() {

            // Checking the worst case scenario. if these dont equal, something bad happened.
            if (this.UniqueTestCount * this.rs.IterationsPerCPUTest != this.TestCollection.Count) {
                throw new UnknownTestException($"Generating CPU Description: Number of Test" +
                    $"Collection elements does not add up. {this.UniqueTestCount} * {this.rs.IterationsPerCPUTest} != " +
                    $"{this.TestCollection.Count}");
            }

            string desc = string.Empty;

            desc += $"Running a {GetThreadAsString(this.GetThreadType)} benchmarking test" + Environment.NewLine;
            desc += $"Username: {Environment.UserName}" + Environment.NewLine;
            desc += $"Time: {DateTime.Now.ToLongTimeString()}" + Environment.NewLine;
            desc += $"Date: {DateTime.Now.ToLongDateString()}" + Environment.NewLine;

            desc += "Each test runs with a specific number of iterations";
            desc += Environment.NewLine;
            desc += $"\tThe {GetTestName(TestName.CPUSuccessorship)} ran " +
                $"{this.rs.IterationsSuccessorship.ToString("n0")} iterations per test" + Environment.NewLine;
            desc += $"\tThe {GetTestName(TestName.CPUBoolean)} ran " +
                $"{this.rs.IterationsBoolean.ToString("n0")} iterations per test" + Environment.NewLine;
            desc += $"\tThe {GetTestName(TestName.CPUQueue)} ran " +
                $"{this.rs.IterationsQueue.ToString("n0")} iterations per test" + Environment.NewLine;
            desc += $"\tThe {GetTestName(TestName.CPULinkedList)} ran " +
                $"{this.rs.IterationsLinkedList.ToString("n0")} iterations per test" + Environment.NewLine;
            desc += $"\tThe {GetTestName(TestName.CPUTree)} ran " +
                $"{this.rs.IterationsTree.ToString("n0")} iterations per test" + Environment.NewLine;

            // each duration printed.
            // Should later put in another function.

            desc += $"The Ripper runs {this.rs.IterationsPerCPUTest} iterations of each test. Below are the durations:";
            desc += Environment.NewLine;

            byte index = 0;

            for (byte b = 0; b < this.rs.IterationsPerCPUTest; b++) {
                desc += $"\t{GetTestName(TestName.CPUSuccessorship)}[{b + 1}] - " +
                    $"{this.TestCollection[index].ToString()}" + Environment.NewLine;
                index++;
            }

            for (byte b = 0; b < this.rs.IterationsPerCPUTest; b++) {
                desc += $"\t{GetTestName(TestName.CPUBoolean)}[{b + 1}] - " +
                    $"{this.TestCollection[index].ToString()}" + Environment.NewLine;
                index++;
            }

            for (byte b = 0; b < this.rs.IterationsPerCPUTest; b++) {
                desc += $"\t{GetTestName(TestName.CPUQueue)}[{b + 1}] - " +
                    $"{this.TestCollection[index].ToString()}" + Environment.NewLine;
                index++;
            }

            for (byte b = 0; b < this.rs.IterationsPerCPUTest; b++) {
                desc += $"\t{GetTestName(TestName.CPULinkedList)}[{b + 1}] - " +
                    $"{this.TestCollection[index].ToString()}" + Environment.NewLine;
                index++;
            }

            for (byte b = 0; b < this.rs.IterationsPerCPUTest; b++) {
                desc += $"\t{GetTestName(TestName.CPUTree)}[{b + 1}] - " +
                    $"{this.TestCollection[index].ToString()}" + Environment.NewLine;
                index++;
            }

            // average per test.
            desc += Environment.NewLine;
            desc += "Average duration per test:";
            desc += Environment.NewLine;

            Tuple<string, TimeSpan> averageTest;

            averageTest =
                GenerateAverageTest(this.TestCollection, TestName.CPUSuccessorship);
            desc += $"\t{averageTest.Item1} - {averageTest.Item2} {Environment.NewLine}";

            averageTest =
                GenerateAverageTest(this.TestCollection, TestName.CPUBoolean);
            desc += $"\t{averageTest.Item1} - {averageTest.Item2} {Environment.NewLine}";

            averageTest =
                GenerateAverageTest(this.TestCollection, TestName.CPUQueue);
            desc += $"\t{averageTest.Item1} - {averageTest.Item2} {Environment.NewLine}";

            averageTest =
                GenerateAverageTest(this.TestCollection, TestName.CPULinkedList);
            desc += $"\t{averageTest.Item1} - {averageTest.Item2} {Environment.NewLine}";

            averageTest =
                GenerateAverageTest(this.TestCollection, TestName.CPUTree);
            desc += $"\t{averageTest.Item1} - {averageTest.Item2} {Environment.NewLine}";

            // total time of all tests. 
            this.totalDuration = TotalTimeSpan(this.TestCollection);

            desc += "Total duration of the test:";
            desc += $"\t{this.totalDuration}";

            // score for the test.
            desc += Environment.NewLine;
            byte score = this.Score;
            desc += $"The score for this test is {score}.";

            desc += Environment.NewLine + Environment.NewLine;
            desc += GenerateScoreDescription(userData.UserType, score);

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

            this.totalDuration = TotalTimeSpan(this.TestCollection);
            desc += "Total duration of the test:";
            desc += $"\t{this.totalDuration}";

            // score for the test.
            desc += Environment.NewLine;
            byte score = this.Score;
            desc += $"The score for this test is {score}.";

            desc += Environment.NewLine + Environment.NewLine;
            desc += GenerateScoreDescription(userData.UserType, score);
            return desc;
        }

        /// <summary>
        /// Generates a descripton for the generated score.
        /// </summary>
        /// <returns></returns>

        protected override string GenerateScoreDescription(UserData.TypeOfUser typeOfUser, byte score) {
            switch (typeOfUser) {
                case UserData.TypeOfUser.Casual: {
                    return GetCasualScoreDesc(score);
                }

                case UserData.TypeOfUser.Websurfer: {
                    return GetWebScoreDesc(score);
                }

                case UserData.TypeOfUser.HighPerformance: {
                    return GetHighPerfScoreDesc(score);
                }

                case UserData.TypeOfUser.Video: {
                    return GetVideoDescription(score);
                }

                default: {
                    return "Error generating the description.";
                }
            }
        }

        /// <summary>
        /// Returns a casual description using the score.
        /// </summary>
        /// <param name="score">The score of the component.</param>
        /// <returns></returns>

        private string GetCasualScoreDesc(byte score) {
            switch (score) {
                case byte a when (score >= 0 && score <= 49): {
                    return "Not the best, but your probably okay. " +
                        "More information is given depending on the " +
                        "type of user you are. e.g. web surfer, high performance, video editor.";
                }

                case byte a when (score == 50): {
                    return "It's average. " +
                        "More information is given depending on the " +
                        "type of user you are. e.g. web surfer, high performance, video editor."; ;
                }


                case byte a when (score >= 51 && score <= 100): {
                    return "Pretty good. You are in a good spot! " +
                        "More information is given depending on the " +
                        "type of user you are. e.g. web surfer, high performance, video editor."; ;
                }

                default: {
                    return "";
                }
            }
        }

        /// <summary>
        /// Returns a web surfing description using the score.
        /// </summary>
        /// <param name="score">The score of the component.</param>
        /// <returns></returns>

        private string GetWebScoreDesc(byte score) {
            switch (score) {
                case byte a when (score >= 0 && score <= 25): {
                    return "This isn't great, but for causal websurfing it might suffice. Videos " +
                        "might stutter even with a good internet connection.";
                }

                case byte a when (score >= 26 && score <= 49): {
                    return "Almost average. However, you should be okay. You might " +
                        "not have great performance with multitasking with your internet tabs and other things open.";
                }

                case byte a when (score == 50): {
                    return "You're, well average! But this is just fine for your web purposes.";
                }

                case byte a when (score >= 51 && score <= 75): {
                    return "Better than average. This will do you just fine for your web purposes.";
                }

                case byte a when (score >= 76 && score <= 100): {
                    return "This will be more than sufficient. You are set.";
                }

                default: {
                    return "";
                }

            }
        }

        /// <summary>
        /// Returns a high performance description using the score.
        /// </summary>
        /// <param name="score">The score of the component.</param>
        /// <returns></returns>

        private string GetHighPerfScoreDesc(byte score) {
            switch (score) {
                case byte a when (score >= 0 && score <= 10): {
                    return "You're in the lowest percentile. " +
                        "You want to consider getting a new processor.";
                }

                case byte a when (score >= 11 && score <= 20): {
                    return "You're in the low percentile. " +
                        "Won't be sufficient for high performance purposes.";
                }

                case byte a when (score >= 21 && score <= 30): {
                    return "You will have issues multitasking with different applications open.";
                }

                case byte a when (score >= 31 && score <= 40): {
                    return "This will be suffient for many games, but multitasking might be a problem.";
                }

                case byte a when (score >= 41 && score <= 49): {
                    return "Close to average. You would be able to multitask okay.";
                }

                case byte a when (score == 50): {
                    return "This is average score of 50. You will be able to multitask with a game open.";
                }

                case byte a when (score >= 51 && score <= 60): {
                    return "A bit above average, A humble score. You will have no problems " +
                        "multitasking with common items.";
                }

                case byte a when (score >= 61 && score <= 70): {
                    return "A decent score for your needs. Multitasking is not a problem. You can likely watch videos " +
                        "while playing your favorite games.";
                }

                case byte a when (score >= 71 && score <= 80): {
                    return "A pretty good score for playing most games. Multitasking is easy, and efficient.";
                }

                case byte a when (score >= 81 && score <= 90): {
                    return "Sub-server level performance! You will know what your capable of.";
                }

                case byte a when (score >= 91 && score <= 95): {
                    return "A crazy score. This must be a server computer running this benchmark!";
                }

                case byte a when (score >= 96 && score <= 100): {
                    return "An insane score. This must be a large server computer running this benchmark!";
                }

                default: {
                    return "";
                }
            }
        }

        /// <summary>
        /// Returns a video description using the score.
        /// </summary>
        /// <param name="score">The score of the component.</param>
        /// <returns></returns>

        private string GetVideoDescription(byte score) {
            switch (score) {
                case byte a when (score >= 0 && score <= 10): {
                    return "You're in the lowest percentile. " +
                        "You want to consider getting a new processor.";
                }

                case byte a when (score >= 11 && score <= 20): {
                    return "You're in the low percentile. " +
                        "This won't be sufficient for video rendering performance purposes.";
                }

                case byte a when (score >= 21 && score <= 30): {
                    return "You will have issues multitasking with different applications open. " +
                        "You will not be able to render and multitask";
                }

                case byte a when (score >= 31 && score <= 40): {
                    return "This will be suffient for many rendering options, " +
                        "but multitasking might be a problem while rendering.";
                }

                case byte a when (score >= 41 && score <= 49): {
                    return "Close to average. While rendering, you might be able to run light weight apps " +
                        "fine.";
                }

                case byte a when (score == 50): {
                    return "This is average score of 50. You will be able to multitask with rendering a video " +
                        "and webbrowser open with tabs";
                }

                case byte a when (score >= 51 && score <= 60): {
                    return "A bit above average, A humble score. You will have no problems " +
                        "multitasking with common items.";
                }

                case byte a when (score >= 61 && score <= 70): {
                    return "A decent score for your needs. Multitasking is not a problem. You can likely watch videos " +
                        "while playing your favorite games.";
                }

                case byte a when (score >= 71 && score <= 80): {
                    return "A pretty good score for playing most games. Multitasking is easy, and efficient.";
                }

                case byte a when (score >= 81 && score <= 90): {
                    return "Sub-server level performance! You will know what your capable of.";
                }

                case byte a when (score >= 91 && score <= 95): {
                    return "A crazy score. This must be a server computer running this benchmark! You can " +
                        "render a video and play a game, and all sorts of things no problem.";
                }

                case byte a when (score >= 96 && score <= 100): {
                    return "An insane score. This must be a large server computer running this benchmark! " +
                        "You will not have any problems with this CPU for any common needs.";
                }

                default: {
                    return "";
                }
            }
        }

        /// <summary>
        /// Generates a score between 0-100 that takes in the number of iterations
        /// per test, how much ticks performed per iteration,
        /// and total execution time for all tests and generates a score thats 
        /// competitive at the lower ticks/iteration and becomes less competitive.
        /// </summary>
        /// <returns></returns>

        protected override byte GenerateScore() {
            var total_iterations = (this.rs.IterationsSuccessorship +
                this.rs.IterationsBoolean + this.rs.IterationsQueue +
                this.rs.IterationsLinkedList + this.rs.IterationsTree) *
                this.rs.IterationsPerCPUTest;

            // mostly because total iterations is likely ulong.
            ulong iter_per_tick = (ulong)this.totalDuration.Ticks / total_iterations;
            uint iter_per_tick_int = (uint)iter_per_tick; // converts properly.

            byte score = GetStartingScore(iter_per_tick_int, out ScorePercentile scorePercentile);
            int variance = GetIncrement(scorePercentile, out int startIndex);

            // if your score is 100 or 0, no need to do work.
            if (score == 100 || score == 0) { return score; }

            int c = 0; // top range.
            while (true) {
                c += variance; // increment c by variance.
                if (score == 0) { break; }

                if (Enumerable.Range(startIndex + 1, c).Contains((int)iter_per_tick_int)) {
                    return score;
                }
                score--; // if not found, decrease score.
            }
            return 0;
        }
    }
}
