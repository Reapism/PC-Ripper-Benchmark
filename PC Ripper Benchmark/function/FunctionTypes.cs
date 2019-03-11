using System.Collections.Generic;

namespace PC_Ripper_Benchmark.function {

    /// <summary>
    /// The <see cref="FunctionTypes"/> class.
    /// <para>Contains public generalized functions
    /// for many functions in this project.</para>
    /// <para>Includes <see langword="enums"/>, 
    /// <see langword="functions"/>, and 
    /// <see langword="data"/>.</para>
    /// <para>Author: <see langword="Anthony Jaghab"/> (c),
    /// all rights reserved.</para>
    /// </summary>

    public class FunctionTypes {

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
            /// separate the UI but not s
            /// <para>>> Note: This will not freeze the
            /// UI.</para>
            /// </summary>
            SingleUI,

            /// <summary>
            /// Runs an action split between N thread(s)
            /// Contingent upon how many logical processor
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
            DiskFolderMatrix,

            /// <summary>
            /// The bulk-file test.
            /// </summary>
            DiskBulkFile,

            /// <summary>
            /// The read and write parsing test.
            /// </summary>
            DiskReadWriteParse,

            /// <summary>
            /// The combination of folder matrix and
            /// bulk file.
            /// </summary>
            DiskRipper,

            /// <summary>
            /// The folder matrix test.
            /// </summary>
            RamVirtualFolderMatrix,

            /// <summary>
            /// The bulk-file test.
            /// </summary>
            RamVirtualBulkData,

            /// <summary>
            /// The read and write parsing test.
            /// </summary>
            RamReferenceDereferenceParse,

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
    }
}
