using System;
using System.Collections.Generic;
using PC_Ripper_Benchmark.exception;

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
        /// Generates a particular description based on a
        /// given component test. Contains information
        /// about each test and its duration, and the 
        /// average of the tests, and the score.
        /// </summary>

        protected abstract string GenerateDescription();

        /// <summary>
        /// Generates a score for a particular component.
        /// </summary>

        protected abstract byte GenerateScore();

        /// <summary>
        /// Generates a <see cref="Tuple{T1, T2}"/> containing 
        /// the Name and average test for a particular component.
        /// </summary>
        /// <param name="testCollection">A collection of tests.</param>
        /// <param name="testName">A <see cref="TestName"/> to represent the test.</param>
        /// <returns>A Tuple containing a </returns>

        protected abstract Tuple<string, TimeSpan> GenerateAverageTest(List<TimeSpan> testCollection, TestName testName);

        #endregion

        #region Virtual function(s).       

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
                case TestName.DiskFolderMatrix: {
                    return "Folder Matrix";
                }

                case TestName.DiskBulkFile: {
                    return "Bulk File";
                }

                case TestName.DiskReadWriteParse: {
                    return "Read/Write Parse";
                }

                case TestName.DiskRipper: {
                    return "Disk Ripper";
                }

                // Ram test names.
                case TestName.RamVirtualFolderMatrix: {
                    return "Virtual Folder Matrix";
                }

                case TestName.RamVirtualBulkData: {
                    return "Virtual Bulk Data";
                }

                case TestName.RamReferenceDereferenceParse: {
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

        #endregion
    }
}
