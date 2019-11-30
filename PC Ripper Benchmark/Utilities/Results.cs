using PC_Ripper_Benchmark.Exceptions;
using System;
using System.Collections.Generic;
using static PC_Ripper_Benchmark.Functions.RipperTypes;

namespace PC_Ripper_Benchmark.Utilities
{

    /// <summary>
    /// The abstract <see cref="Results"/> class.
    /// <para>Contains property signatures, and method signatures for
    /// similar data for a particular component results.</para>
    /// <para>Author: <see langword="Anthony Jaghab"/> (c),
    /// all rights reserved.</para>
    /// </summary>

    public abstract class Results
    {

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

        /// <summary>
        /// Returns the starting score and also
        /// returns a <see cref="ScorePercentile"/>.
        /// Used for the internal score algorithm.
        /// </summary>
        /// <param name="ticksPerIteration">The number of ticks per iteration.</param>
        /// <param name="scorePercentile">A <see cref="ScorePercentile"/>
        /// used to determine the increment, and start index.</param>
        /// <returns></returns>

        protected abstract byte GetStartingScore(uint ticksPerIteration, out ScorePercentile scorePercentile);

        /// <summary>
        /// Returns the incrementing <see langword="int"/> as 
        /// a function return and also outs a startIndex for the
        /// score algorithm. 
        /// </summary>
        /// <param name="scorePercentile">A <see cref="ScorePercentile"/>
        /// used to determine the increment, and start index.</param>
        /// <param name="startIndex">The startIndex to return.</param>
        /// <returns></returns>

        protected abstract int GetIncrement(ScorePercentile scorePercentile, out int startIndex);

        #endregion

        #region Virtual function(s). Not meant to be overridden, base functionality is enough.       

        /// <summary>
        /// Takes in a <see cref="TestName"/> and
        /// returns a string representing that test name.
        /// </summary>
        /// <param name="theTest">The <see cref="TestName"/> enum
        /// which represents the name.</param>
        /// <returns></returns>

        protected virtual string GetTestName(TestName theTest)
        {
            switch (theTest)
            {
                // CPU test names.
                case TestName.CPUSuccessorship:
                {
                    return "Successorship";
                }

                case TestName.CPUBoolean:
                {
                    return "Boolean";
                }

                case TestName.CPUQueue:
                {
                    return "Queue";
                }

                case TestName.CPULinkedList:
                {
                    return "Linked List";
                }

                case TestName.CPUTree:
                {
                    return "Tree";
                }

                // Disk test names.
                case TestName.DISKFolderMatrix:
                {
                    return "Folder Matrix";
                }

                case TestName.DISKBulkFile:
                {
                    return "Bulk File";
                }

                case TestName.DISKReadWriteParse:
                {
                    return "Read/Write Parse";
                }

                case TestName.DISKRipper:
                {
                    return "Disk Ripper";
                }

                // Ram test names.
                case TestName.RAMFolderMatrix:
                {
                    return "Virtual Folder Matrix";
                }

                case TestName.RAMBulkFile:
                {
                    return "Virtual Bulk Data";
                }

                case TestName.RAMReferenceDereferenceParse:
                {
                    return "Reference/Dereference Parse";
                }

                // GPU test names.
                case TestName.GPUFolderMatrix:
                {
                    return "Not Implemented.";
                }

                case TestName.GPUBulkFile:
                {
                    return "Not Implemented.";
                }

                case TestName.GPUReadWriteParse:
                {
                    return "Not Implemented.";
                }

                default:
                {
                    throw new UnknownTestException("");
                }
            }
        }

        /// <summary>
        /// Adds a particular test to the <see cref="TestCollection"/>.
        /// </summary>
        /// <param name="duration">A <see cref="TimeSpan"/> that represents
        /// a duration for the test.</param>

        protected virtual void AddTest(TimeSpan duration)
        {
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

        protected virtual TimeSpan AverageTimespan(ref TimeSpan averageMe, int divideBy)
        {
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

        protected virtual TimeSpan TotalTimeSpan(params TimeSpan[] times)
        {
            TimeSpan total = new TimeSpan();
            foreach (TimeSpan t in times)
            {
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

        protected virtual TimeSpan TotalTimeSpan(List<TimeSpan> list)
        {
            TimeSpan total = new TimeSpan();
            foreach (TimeSpan t in list)
            {
                total = total.Add(t);
            }
            return total;
        }

        /// <summary>
        /// Returns a string representation of the <see cref="ThreadType"/> type.
        /// </summary>
        /// <param name="threadType">The <see cref="ThreadType"/> </param>
        /// <returns></returns>

        protected virtual string GetThreadAsString(ThreadType threadType)
        {
            switch (threadType)
            {
                case ThreadType.Single: { return "Single threaded"; }

                case ThreadType.SingleUI: { return "Dual threaded"; }

                case ThreadType.Multithreaded: { return "Multithreaded"; }

                default:
                {
                    return "";
                }
            }
        }
        #endregion
    }
}
