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
        /// Represents a <see cref="Tuple{T1, T2}"/> containing the name of the test,
        /// and the average time it took the test.
        /// </summary>
        public abstract Tuple<string, TimeSpan> AverageTest { get; }

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
        /// given component test.
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
        /// <returns>A Tuple containing a </returns>

        protected abstract Tuple<string, TimeSpan> GenerateAverageTest(List<TimeSpan> testCollection); 

        #endregion

        #region Virtual function(s).

        /// <summary>
        /// Takes in a <see cref="TestName"/> and
        /// returns a string representing that test name.
        /// </summary>
        /// <param name="theTest">The <see cref="TestName"/> enum
        /// which represents the name.</param>
        /// <returns></returns>
        public virtual string GetTestName(TestName theTest) {
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

        #endregion
    }
}
