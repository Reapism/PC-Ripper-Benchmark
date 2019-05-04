using System.Collections.Generic;
using System.Linq;

namespace PC_Ripper_Benchmark.function {

    /// <summary>
    /// The <see cref="RipperTypes"/> class.
    /// <para>Contains public generalized functions
    /// for many functions in this project.</para>
    /// <para>Includes <see langword="enums"/>, 
    /// <see langword="functions"/>, and 
    /// <see langword="data"/>.</para>
    /// <para>Author: <see langword="Anthony Jaghab"/> (c),
    /// all rights reserved.</para>
    /// </summary>

    public class RipperTypes {

        /// <summary>
        /// The <see cref="ExportType"/> type.
        /// <para>Represents how to export information.</para>
        /// </summary>

        public enum ExportType {

            /// <summary>
            /// Export a file as type .CSV
            /// </summary>
            CSV,

            /// <summary>
            /// Export a file as type .HTML
            /// </summary>
            HTML,

            /// <summary>
            /// Export a file as type .TXT
            /// </summary>
            TEXTFILE,

            /// <summary>
            /// Export a file as type .XML
            /// </summary>
            XAML

        }

        /// <summary>
        /// The <see cref="Tab"/> enum.
        /// Represents all the tabs needed.
        /// </summary>

        public enum Tab {
            /// <summary>
            /// The welcome tab.
            /// </summary>
            WELCOME,

            /// <summary>
            /// The cpu tab.
            /// </summary>
            CPU,

            /// <summary>
            /// The disk tab.
            /// </summary>
            DISK,

            /// <summary>
            /// The ram tab.
            /// </summary>
            RAM,

            /// <summary>
            /// The gpu tab.
            /// </summary>
            GPU,

            /// <summary>
            /// The settings tab.
            /// </summary>
            SETTINGS,

            /// <summary>
            /// Results tab.
            /// </summary>
            RESULTS,

            /// <summary>
            /// Tab running a test.
            /// </summary>
            RUNNING_TEST,

            /// <summary>
            /// Tab for account settings.
            /// </summary>
            MY_ACCOUNT,

            /// <summary>
            /// Tab for parts.
            /// </summary>
            RESULTS_PARTS,

            /// <summary>
            /// Tab for account tests.
            /// </summary>
            USER_RESULTS
        }

        /// <summary>
        /// Determines which thread type a particular
        /// action should be provoked in.
        /// <para>SINGLE, SINGLEUI, MULTITHREADED</para>
        /// </summary>

        public enum ThreadType {

            /// <summary>
            /// Runs an action on the
            /// same thread as the UI thread that spawned it.
            /// <para>>> Note: This will freeze the program 
            /// untilthe test is complete.</para>
            /// </summary>
            Single,

            /// <summary>
            /// Single threaded, runs an action on a new thread
            /// separate the UI. 2 total threads.
            /// <para>>> Note: This will not freeze the
            /// UI.</para>
            /// </summary>
            SingleUI,

            /// <summary>
            /// Runs an action split between N thread(s)
            /// contingent upon how many logical processor
            /// (threads) are available on your machine. 
            /// <para></para>Total threads will be (N + 1)
            /// where 1 is the UI thread, n is # of threads on
            /// this instances CPU.
            /// <para>>> Note: This will not freeze the UI.</para>
            /// </summary>
            Multithreaded
        }

        /// <summary>
        /// Represents all the test names for all the components.
        /// </summary>

        public enum TestName {
            /// <summary>
            /// The successorship test.
            /// </summary>
            CPUSuccessorship,

            /// <summary>
            /// The boolean logic.
            /// </summary>
            CPUBoolean,

            /// <summary>
            /// The <see cref="Queue{T}"/> test.
            /// </summary>
            CPUQueue,

            /// <summary>
            /// The <see cref="LinkedList{T}"/> test.
            /// </summary>
            CPULinkedList,

            /// <summary>
            /// The <see cref="SortedSet{T}"/> test.
            /// </summary>
            CPUTree,

            /// <summary>
            /// The folder matrix test.
            /// </summary>
            DISKFolderMatrix,

            /// <summary>
            /// The bulk-file test.
            /// </summary>
            DISKBulkFile,

            /// <summary>
            /// The read and write parsing test.
            /// </summary>
            DISKReadWriteParse,

            /// <summary>
            /// The combination of folder matrix and
            /// bulk file.
            /// </summary>
            DISKRipper,

            /// <summary>
            /// The folder matrix test.
            /// </summary>
            RAMFolderMatrix,

            /// <summary>
            /// The bulk-file test.
            /// </summary>
            RAMBulkFile,

            /// <summary>
            /// The read and write parsing test.
            /// </summary>
            RAMReferenceDereferenceParse,

            /// <summary>
            /// The folder matrix test.
            /// </summary>
            GPUFolderMatrix,

            /// <summary>
            /// The bulk-file test.
            /// </summary>
            GPUBulkFile,

            /// <summary>
            /// The read and write parsing test.
            /// </summary>
            GPUReadWriteParse
        };

        /// <summary>
        /// Represents the test component name.
        /// </summary>

        public enum TestComponent {
            /// <summary>
            /// The CPU test.
            /// </summary>
            CPU,

            /// <summary>
            /// The RAM test.
            /// </summary>
            RAM,

            /// <summary>
            /// The DISK test.
            /// </summary>
            DISK
        }

        /// <summary>
        /// Represents what score percentile you're in.
        /// </summary>

        public enum ScorePercentile {

            /// <summary>
            /// Represents the score 100.
            /// </summary>
            HUNDRED,

            /// <summary>
            /// Represents the score 90-99.
            /// </summary>
            NINTIES,

            /// <summary>
            /// Represents the score 80-89.
            /// </summary>
            EIGHTIES,

            /// <summary>
            /// Represents the score 70-79.
            /// </summary>
            SEVENTIES,

            /// <summary>
            /// Represents the score 60-69.
            /// </summary>
            SIXTIES,

            /// <summary>
            /// Represents the score 50-59.
            /// </summary>
            FIFTIES,

            /// <summary>
            /// Represents the score 40-49.
            /// </summary>
            FORTIES,

            /// <summary>
            /// Represents the score 30-39.
            /// </summary>
            THIRTIES,

            /// <summary>
            /// Represents the score 20-29.
            /// </summary>
            TWENTIES,

            /// <summary>
            /// Represents the score 10-19.
            /// </summary>
            TENS,

            /// <summary>
            /// Represents the score 1-9.
            /// </summary>
            ONES,

            /// <summary>
            /// Represents the score 0.
            /// </summary>
            ZERO
        }


        private static byte GetStartingScore(uint ticksPerIteration, out ScorePercentile scorePercentile) {
            switch (ticksPerIteration) {

                case 0: {
                    scorePercentile = ScorePercentile.HUNDRED;
                    return 100;
                }

                case uint u when (ticksPerIteration >= 1 && ticksPerIteration < 21): {
                    scorePercentile = ScorePercentile.NINTIES;
                    return 99;
                }

                case uint u when (ticksPerIteration >= 21 && ticksPerIteration < 61): {
                    scorePercentile = ScorePercentile.EIGHTIES;
                    return 89;
                }

                case uint u when (ticksPerIteration >= 61 && ticksPerIteration < 121): {
                    scorePercentile = ScorePercentile.SEVENTIES;
                    return 79;
                }

                case uint u when (ticksPerIteration >= 121 && ticksPerIteration < 201): {
                    scorePercentile = ScorePercentile.SIXTIES;
                    return 69;
                }

                case uint u when (ticksPerIteration >= 201 && ticksPerIteration < 301): {
                    scorePercentile = ScorePercentile.FIFTIES;
                    return 59;
                }

                case uint u when (ticksPerIteration >= 301 && ticksPerIteration < 421): {
                    scorePercentile = ScorePercentile.FORTIES;
                    return 49;
                }

                case uint u when (ticksPerIteration >= 421 && ticksPerIteration < 561): {
                    scorePercentile = ScorePercentile.THIRTIES;
                    return 39;
                }

                case uint u when (ticksPerIteration >= 561 && ticksPerIteration < 721): {
                    scorePercentile = ScorePercentile.TWENTIES;
                    return 29;
                }

                case uint u when (ticksPerIteration >= 721 && ticksPerIteration < 901): {
                    scorePercentile = ScorePercentile.TENS;
                    return 19;
                }

                case uint u when (ticksPerIteration >= 901 && ticksPerIteration < 1101): {
                    scorePercentile = ScorePercentile.ONES;
                    return 9;
                }

                default: {
                    scorePercentile = ScorePercentile.ZERO;
                    return 0;
                }

            }
        }

        private static int GetIncrement(ScorePercentile scorePercentile, out int startIndex) {
            switch (scorePercentile) {
                case ScorePercentile.HUNDRED: {
                    startIndex = 0;
                    return 0;
                }

                case ScorePercentile.NINTIES: {
                    startIndex = 0;
                    return 2;
                }

                case ScorePercentile.EIGHTIES: {
                    startIndex = 20;
                    return 4;
                }

                case ScorePercentile.SEVENTIES: {
                    startIndex = 60;
                    return 6;
                }

                case ScorePercentile.SIXTIES: {
                    startIndex = 120;
                    return 8;
                }

                case ScorePercentile.FIFTIES: {
                    startIndex = 200;
                    return 10;
                }

                case ScorePercentile.FORTIES: {
                    startIndex = 300;
                    return 12;
                }

                case ScorePercentile.THIRTIES: {
                    startIndex = 420;
                    return 14;
                }

                case ScorePercentile.TWENTIES: {
                    startIndex = 560;
                    return 16;
                }

                case ScorePercentile.TENS: {
                    startIndex = 720;
                    return 18;
                }

                case ScorePercentile.ONES: {
                    startIndex = 900;
                    return 20;
                }

                case ScorePercentile.ZERO: {
                    startIndex = 0;
                    return 0;
                }

                default: {
                    throw new exception.RipperScoreException("Imposible scorepercentile passed into a function.");
                }
            }
        }

        public static byte GetScoreCPU(uint ticksPerIteration) {
            byte score = GetStartingScore(ticksPerIteration, out ScorePercentile scorePercentile);
            int variance = GetIncrement(scorePercentile, out int startIndex);

            // if your score is 100 or 0, no need to do work.
            if (score == 100 || score == 0) { return score; }

            int c = 0; // top range.
            while (true) {
                c += variance; // increment c by variance.
                if (score == 0) { break; }

                if (Enumerable.Range(startIndex + 1, c).Contains((int)ticksPerIteration)) {
                    return score;
                }
                score--; // if not found, decrease score.
            }
            return 0;
        }

        public static byte GetScoreRAM(ulong ticksPerIterations) {
            byte score = 100;
            int max_Iterations = 100000000;

            for (int i = 0; i < max_Iterations; i += 3) {
                if (Enumerable.Range(i, i + 3).Contains((int)ticksPerIterations)) {
                    if (score == 0) { break; }
                    return score;
                }
                score--;
            }

            return 0;
        }

        public static byte GetScoreDisk(ulong ticksPerIterations) {
            byte score = 100;
            int max_Iterations = 100000000;

            for (int i = 0; i < max_Iterations; i++) {
                if (Enumerable.Range(i, i + 3).Contains((int)ticksPerIterations)) {
                    if (score == 0) { break; }
                    return score;
                }
                score--;
            }

            return 0;
        }
    }
}