using PC_Ripper_Benchmark.exception;
using System;
using System.Collections.Generic;
using static PC_Ripper_Benchmark.function.RipperTypes;

namespace PC_Ripper_Benchmark.util {

    /// <summary>
    /// The abstract <see cref="Results"/> class.
    /// <para>Contains property signatures, and method signatures for
    /// similar data for a particular component results.</para>
    /// <para>Author: <see langword="Anthony Jaghab"/> (c),
    /// all rights reserved.</para>
    /// </summary>

    public abstract class Results {

        #region Abstract properties for a given component.

        /// <summary>
        /// Gets the <see cref="ThreadType"/> for a particular
        /// test.
        /// </summary>
        protected abstract ThreadType GetThreadType { get; }

        /// <summary>
        /// Represents the individual time for each test before the averaging.
        /// </summary>
        public abstract List<TimeSpan> TestCollection { get; }

        /// <summary>
        /// Represents the score for this particular test.
        /// </summary>
        public abstract byte Score { get; }

        /// <summary>
        /// Represents the description for this particular test.
        /// </summary>
        public abstract string Description { get; }

        /// <summary>
        /// Gets the number of unique individual tests
        /// for the component.
        /// </summary>
        public abstract byte UniqueTestCount { get; }

        #endregion

        #region Abstract methods.

        /// <summary>
        /// Returns a particular score description thats contingent upon
        /// the <see cref="UserData.TypeOfUser"/>.
        /// </summary>
        /// <param name="typeOfUser">The <see cref="UserData.TypeOfUser"/>.</param>
        /// <param name="score">The score of the test.</param>
        /// <returns></returns>

        protected abstract string GenerateScoreDescription(UserData.TypeOfUser typeOfUser, byte score);

        /// <summary>
        /// Generates a particular description based on a
        /// given component test. Contains information
        /// about each test and its duration, and the 
        /// average of the tests, and the score.
        /// </summary>

        protected abstract string GenerateAdvancedDescription();

        /// <summary>
        /// Generates a particular description based on a
        /// given component test. Contains information
        /// total duration, and the score.
        /// </summary>

        protected abstract string GenerateBeginnerDescription();

        /// <summary>
        /// Generates a score for a particular component.
        /// </summary>

        protected abstract byte GenerateScore();

        /// <summary>
        /// Generates a <see cref="Tuple{T1, T2}"/> containing 
        /// the Name and average test for a particular component.
        /// </summary>
        /// <param name="testCollection">A collection of tests.</param>
        /// <param name="theTest">A <see cref="TestName"/> to represent the test.</param>
        /// <returns>A Tuple containing a </returns>

        protected abstract Tuple<string, TimeSpan> GenerateAverageTest(List<TimeSpan> testCollection, TestName theTest);

        #endregion

        #region Virtual function(s). Not meant to be overridden, base functionality is enough.       

        /// <summary>
        /// Takes in a <see cref="TestName"/> and
        /// returns a string representing that test name.
        /// </summary>
        /// <param name="theTest">The <see cref="TestName"/> enum
        /// which represents the name.</param>
        /// <returns></returns>
        
        protected virtual string GetTestName(TestName theTest) {
            switch (theTest) {
                // CPU test names.
                case TestName.CPUSuccessorship: {
                    return "Successorship";
                }

                case TestName.CPUBoolean: {
                    return "Boolean";
                }

                case TestName.CPUQueue: {
                    return "Queue";
                }

                case TestName.CPULinkedList: {
                    return "Linked List";
                }

                case TestName.CPUTree: {
                    return "Tree";
                }

                // Disk test names.
                case TestName.DISKFolderMatrix: {
                    return "Folder Matrix";
                }

                case TestName.DISKBulkFile: {
                    return "Bulk File";
                }

                case TestName.DISKReadWriteParse: {
                    return "Read/Write Parse";
                }

                case TestName.DISKRipper: {
                    return "Disk Ripper";
                }

                // Ram test names.
                case TestName.RAMFolderMatrix: {
                    return "Virtual Folder Matrix";
                }

                case TestName.RAMBulkFile: {
                    return "Virtual Bulk Data";
                }

                case TestName.RAMReferenceDereferenceParse: {
                    return "Reference/Dereference Parse";
                }

                // GPU test names.
                case TestName.GPUFolderMatrix: {
                    return "Not Implemented.";
                }

                case TestName.GPUBulkFile: {
                    return "Not Implemented.";
                }

                case TestName.GPUReadWriteParse: {
                    return "Not Implemented.";
                }

                default: {
                    throw new UnknownTestException("");
                }
            }
        }

        /// <summary>
        /// Adds a particular test to the <see cref="TestCollection"/>.
        /// </summary>
        /// <param name="duration">A <see cref="TimeSpan"/> that represents
        /// a duration for the test.</param>

        protected virtual void AddTest(TimeSpan duration) {
            this.TestCollection.Add(duration);
        }

        /// <summary>
        /// Averages a particular <see cref="TimeSpan"/> with a number and
        /// returns a new <see cref="TimeSpan"/> with the average.
        /// </summary>
        /// <param name="averageMe">A <see cref="TimeSpan"/> object to average.</param>
        /// <param name="divideBy">The number to divide the <see cref="TimeSpan"/> by.</param>
        /// <returns></returns>
        /// <exception cref="DivideByZeroException">Thrown if a number is less than or = to 0.</exception>

        protected virtual TimeSpan AverageTimespan(ref TimeSpan averageMe, int divideBy) {
            return (divideBy <= 0) ? throw new DivideByZeroException() :
                new TimeSpan(averageMe.Ticks / divideBy);
        }

        /// <summary>
        /// Returns the total duration of <see cref="TimeSpan"/>(s)
        /// by adding all the <see cref="TimeSpan"/>(s) in a 
        /// <see langword="params"/> <see cref="TimeSpan"/>[].
        /// </summary>
        /// <param name="times">A <see langword="params"/> 
        /// <see cref="TimeSpan"/>[] of timespans.</param>
        /// <returns></returns>

        protected virtual TimeSpan TotalTimeSpan(params TimeSpan[] times) {
            TimeSpan total = new TimeSpan();
            foreach (TimeSpan t in times) {
                total = total.Add(t);
            }
            return total;
        }

        /// <summary>
        /// Returns the total duration of <see cref="TimeSpan"/>(s)
        /// by adding all the <see cref="TimeSpan"/>(s) in a 
        /// <see cref="List{T}"/>.
        /// </summary>
        /// <param name="list">A <see cref="List{T}"/> of timespans.</param>
        /// <returns></returns>

        protected virtual TimeSpan TotalTimeSpan(List<TimeSpan> list) {
            TimeSpan total = new TimeSpan();
            foreach (TimeSpan t in list) {
                total = total.Add(t);
            }
            return total;
        }

        /// <summary>
        /// Returns a string representation of the <see cref="ThreadType"/> type.
        /// </summary>
        /// <param name="threadType">The <see cref="ThreadType"/> </param>
        /// <returns></returns>

        protected virtual string GetThreadAsString(ThreadType threadType) {
            switch (threadType) {
                case ThreadType.Single: { return "Single threaded"; }

                case ThreadType.SingleUI: { return "Dual threaded"; }

                case ThreadType.Multithreaded: { return "Multithreaded"; }

                default: {
                    return "";
                }
            }
        }

        /// <summary>
        /// Returns the starting score and also
        /// returns a <see cref="ScorePercentile"/>.
        /// Used for the internal score algorithm.
        /// </summary>
        /// <param name="ticksPerIteration"></param>
        /// <param name="scorePercentile"></param>
        /// <returns></returns>

        protected virtual byte GetStartingScore(uint ticksPerIteration, out ScorePercentile scorePercentile) {
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

        /// <summary>
        /// Returns the incrementing <see langword="int"/> as 
        /// a function return and also outs a startIndex for the
        /// score algorithm.
        /// </summary>
        /// <param name="scorePercentile">A <see cref="ScorePercentile"/>
        /// used to determine the increment, and start index.</param>
        /// <param name="startIndex">The startIndex to return.</param>
        /// <returns></returns>

        protected virtual int GetIncrement(ScorePercentile scorePercentile, out int startIndex) {
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
        #endregion
    }
}
