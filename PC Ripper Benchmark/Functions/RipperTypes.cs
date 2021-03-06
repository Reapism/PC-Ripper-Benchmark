﻿using System.Collections.Generic;

namespace PC_Ripper_Benchmark.Functions
{

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

    public class RipperTypes
    {

        /// <summary>
        /// The <see cref="ExportType"/> type.
        /// <para>Represents how to export information.</para>
        /// </summary>

        public enum ExportType
        {

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

        public enum Tab
        {
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

        public enum ThreadType
        {

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

        public enum TestName
        {
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

        public enum TestComponent
        {
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

        public enum ScorePercentile
        {

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
    }
}