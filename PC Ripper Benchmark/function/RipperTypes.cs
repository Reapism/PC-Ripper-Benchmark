using System.Collections.Generic;

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

        public static byte GetScoreCPU(ulong ticksPerIterations) {
            System.Random rnd = new System.Random();

            switch (ticksPerIterations) {
                case ulong u when (ticksPerIterations >= 0 && ticksPerIterations <= 10): {
                    return 100;
                }

                case ulong u when (ticksPerIterations >= 11 && ticksPerIterations <= 20): {
                    return (byte)rnd.Next(98, 100);
                }

                case ulong u when (ticksPerIterations >= 21 && ticksPerIterations <= 30): {
                    return (byte)rnd.Next(96, 98);
                }

                case ulong u when (ticksPerIterations >= 31 && ticksPerIterations <= 40): {
                    return (byte)rnd.Next(94, 96);
                }

                case ulong u when (ticksPerIterations >= 41 && ticksPerIterations <= 50): {
                    return (byte)rnd.Next(92, 94);
                }

                case ulong u when (ticksPerIterations >= 51 && ticksPerIterations <= 60): {
                    return (byte)rnd.Next(90, 92);
                }

                case ulong u when (ticksPerIterations >= 61 && ticksPerIterations <= 70): {
                    return (byte)rnd.Next(88, 90);
                }

                case ulong u when (ticksPerIterations >= 71 && ticksPerIterations <= 80): {
                    return (byte)rnd.Next(86, 88);
                }

                case ulong u when (ticksPerIterations >= 81 && ticksPerIterations <= 90): {
                    return (byte)rnd.Next(84, 86);
                }

                case ulong u when (ticksPerIterations >= 91 && ticksPerIterations <= 100): {
                    return (byte)rnd.Next(82, 84);
                }

                case ulong u when (ticksPerIterations >= 101 && ticksPerIterations <= 110): {
                    return (byte)rnd.Next(80, 82);
                }

                case ulong u when (ticksPerIterations >= 111 && ticksPerIterations <= 120): {
                    return (byte)rnd.Next(78, 80);
                }

                case ulong u when (ticksPerIterations >= 121 && ticksPerIterations <= 130): {
                    return (byte)rnd.Next(76, 78);
                }

                case ulong u when (ticksPerIterations >= 131 && ticksPerIterations <= 140): {
                    return (byte)rnd.Next(74, 76);
                }

                case ulong u when (ticksPerIterations >= 141 && ticksPerIterations <= 150): {
                    return (byte)rnd.Next(72, 74);
                }

                case ulong u when (ticksPerIterations >= 151 && ticksPerIterations <= 160): {
                    return (byte)rnd.Next(70, 72);
                }

                case ulong u when (ticksPerIterations >= 161 && ticksPerIterations <= 170): {
                    return (byte)rnd.Next(68, 70);
                }

                case ulong u when (ticksPerIterations >= 171 && ticksPerIterations <= 180): {
                    return (byte)rnd.Next(66, 68);
                }

                case ulong u when (ticksPerIterations >= 181 && ticksPerIterations <= 190): {
                    return (byte)rnd.Next(64, 66);
                }

                case ulong u when (ticksPerIterations >= 191 && ticksPerIterations <= 200): {
                    return (byte)rnd.Next(62, 64);
                }

                case ulong u when (ticksPerIterations >= 201 && ticksPerIterations <= 210): {
                    return (byte)rnd.Next(60, 62);
                }

                case ulong u when (ticksPerIterations >= 211 && ticksPerIterations <= 220): {
                    return (byte)rnd.Next(58, 60);
                }

                case ulong u when (ticksPerIterations >= 221 && ticksPerIterations <= 230): {
                    return (byte)rnd.Next(56, 58);
                }

                case ulong u when (ticksPerIterations >= 231 && ticksPerIterations <= 240): {
                    return (byte)rnd.Next(54, 56);
                }

                case ulong u when (ticksPerIterations >= 241 && ticksPerIterations <= 250): {
                    return (byte)rnd.Next(52, 54);
                }

                case ulong u when (ticksPerIterations >= 251 && ticksPerIterations <= 260): {
                    return (byte)rnd.Next(50, 52);
                }

                case ulong u when (ticksPerIterations >= 261 && ticksPerIterations <= 270): {
                    return (byte)rnd.Next(48, 50);
                }

                case ulong u when (ticksPerIterations >= 271 && ticksPerIterations <= 280): {
                    return (byte)rnd.Next(46, 48);
                }

                case ulong u when (ticksPerIterations >= 281 && ticksPerIterations <= 290): {
                    return (byte)rnd.Next(44, 46);
                }

                case ulong u when (ticksPerIterations >= 291 && ticksPerIterations <= 300): {
                    return (byte)rnd.Next(42, 44);
                }

                case ulong u when (ticksPerIterations >= 301 && ticksPerIterations <= 310): {
                    return (byte)rnd.Next(40, 42);
                }

                case ulong u when (ticksPerIterations >= 311 && ticksPerIterations <= 320): {
                    return (byte)rnd.Next(38, 40);
                }

                case ulong u when (ticksPerIterations >= 321 && ticksPerIterations <= 330): {
                    return (byte)rnd.Next(36, 38);
                }

                case ulong u when (ticksPerIterations >= 331 && ticksPerIterations <= 340): {
                    return (byte)rnd.Next(34, 36);
                }

                case ulong u when (ticksPerIterations >= 341 && ticksPerIterations <= 350): {
                    return (byte)rnd.Next(32, 34);
                }

                case ulong u when (ticksPerIterations >= 351 && ticksPerIterations <= 360): {
                    return (byte)rnd.Next(30, 32);
                }

                case ulong u when (ticksPerIterations >= 361 && ticksPerIterations <= 370): {
                    return (byte)rnd.Next(28, 30);
                }

                case ulong u when (ticksPerIterations >= 371 && ticksPerIterations <= 380): {
                    return (byte)rnd.Next(26, 28);
                }

                case ulong u when (ticksPerIterations >= 381 && ticksPerIterations <= 390): {
                    return (byte)rnd.Next(24, 26);
                }

                case ulong u when (ticksPerIterations >= 391 && ticksPerIterations <= 400): {
                    return (byte)rnd.Next(22, 24);
                }

                case ulong u when (ticksPerIterations >= 401 && ticksPerIterations <= 410): {
                    return (byte)rnd.Next(20, 22);
                }

                case ulong u when (ticksPerIterations >= 411 && ticksPerIterations <= 420): {
                    return (byte)rnd.Next(18, 20);
                }

                case ulong u when (ticksPerIterations >= 421 && ticksPerIterations <= 430): {
                    return (byte)rnd.Next(16, 18);
                }

                case ulong u when (ticksPerIterations >= 431 && ticksPerIterations <= 440): {
                    return (byte)rnd.Next(14, 16);
                }

                case ulong u when (ticksPerIterations >= 441 && ticksPerIterations <= 450): {
                    return (byte)rnd.Next(12, 14);
                }

                case ulong u when (ticksPerIterations >= 451 && ticksPerIterations <= 460): {
                    return (byte)rnd.Next(10, 12);
                }

                case ulong u when (ticksPerIterations >= 461 && ticksPerIterations <= 470): {
                    return (byte)rnd.Next(8, 10);
                }

                case ulong u when (ticksPerIterations >= 471 && ticksPerIterations <= 480): {
                    return (byte)rnd.Next(6, 8);
                }

                case ulong u when (ticksPerIterations >= 481 && ticksPerIterations <= 490): {
                    return (byte)rnd.Next(4, 6);
                }

                case ulong u when (ticksPerIterations >= 491 && ticksPerIterations <= 500): {
                    return (byte)rnd.Next(2, 4);
                }

                case ulong u when (ticksPerIterations > 500): {
                    return 0;
                }

                default: {
                    return 0;
                }
            }
        }

        public static byte GetScoreRAM(ulong ticksPerIterations) {
            System.Random rnd = new System.Random();

            switch (ticksPerIterations) {
                case ulong u when (ticksPerIterations >= 0 && ticksPerIterations <= 10): {
                    return 100;
                }

                case ulong u when (ticksPerIterations >= 11 && ticksPerIterations <= 20): {
                    return (byte)rnd.Next(98, 100);
                }

                case ulong u when (ticksPerIterations >= 21 && ticksPerIterations <= 30): {
                    return (byte)rnd.Next(96, 98);
                }

                case ulong u when (ticksPerIterations >= 31 && ticksPerIterations <= 40): {
                    return (byte)rnd.Next(94, 96);
                }

                case ulong u when (ticksPerIterations >= 41 && ticksPerIterations <= 50): {
                    return (byte)rnd.Next(92, 94);
                }

                case ulong u when (ticksPerIterations >= 51 && ticksPerIterations <= 60): {
                    return (byte)rnd.Next(90, 92);
                }

                case ulong u when (ticksPerIterations >= 61 && ticksPerIterations <= 70): {
                    return (byte)rnd.Next(88, 90);
                }

                case ulong u when (ticksPerIterations >= 71 && ticksPerIterations <= 80): {
                    return (byte)rnd.Next(86, 88);
                }

                case ulong u when (ticksPerIterations >= 81 && ticksPerIterations <= 90): {
                    return (byte)rnd.Next(84, 86);
                }

                case ulong u when (ticksPerIterations >= 91 && ticksPerIterations <= 100): {
                    return (byte)rnd.Next(82, 84);
                }

                case ulong u when (ticksPerIterations >= 101 && ticksPerIterations <= 110): {
                    return (byte)rnd.Next(80, 82);
                }

                case ulong u when (ticksPerIterations >= 111 && ticksPerIterations <= 120): {
                    return (byte)rnd.Next(78, 80);
                }

                case ulong u when (ticksPerIterations >= 121 && ticksPerIterations <= 130): {
                    return (byte)rnd.Next(76, 78);
                }

                case ulong u when (ticksPerIterations >= 131 && ticksPerIterations <= 140): {
                    return (byte)rnd.Next(74, 76);
                }

                case ulong u when (ticksPerIterations >= 141 && ticksPerIterations <= 150): {
                    return (byte)rnd.Next(72, 74);
                }

                case ulong u when (ticksPerIterations >= 151 && ticksPerIterations <= 160): {
                    return (byte)rnd.Next(70, 72);
                }

                case ulong u when (ticksPerIterations >= 161 && ticksPerIterations <= 170): {
                    return (byte)rnd.Next(68, 70);
                }

                case ulong u when (ticksPerIterations >= 171 && ticksPerIterations <= 180): {
                    return (byte)rnd.Next(66, 68);
                }

                case ulong u when (ticksPerIterations >= 181 && ticksPerIterations <= 190): {
                    return (byte)rnd.Next(64, 66);
                }

                case ulong u when (ticksPerIterations >= 191 && ticksPerIterations <= 200): {
                    return (byte)rnd.Next(62, 64);
                }

                case ulong u when (ticksPerIterations >= 201 && ticksPerIterations <= 210): {
                    return (byte)rnd.Next(60, 62);
                }

                case ulong u when (ticksPerIterations >= 211 && ticksPerIterations <= 220): {
                    return (byte)rnd.Next(58, 60);
                }

                case ulong u when (ticksPerIterations >= 221 && ticksPerIterations <= 230): {
                    return (byte)rnd.Next(56, 58);
                }

                case ulong u when (ticksPerIterations >= 231 && ticksPerIterations <= 240): {
                    return (byte)rnd.Next(54, 56);
                }

                case ulong u when (ticksPerIterations >= 241 && ticksPerIterations <= 250): {
                    return (byte)rnd.Next(52, 54);
                }

                case ulong u when (ticksPerIterations >= 251 && ticksPerIterations <= 260): {
                    return (byte)rnd.Next(50, 52);
                }

                case ulong u when (ticksPerIterations >= 261 && ticksPerIterations <= 270): {
                    return (byte)rnd.Next(48, 50);
                }

                case ulong u when (ticksPerIterations >= 271 && ticksPerIterations <= 280): {
                    return (byte)rnd.Next(46, 48);
                }

                case ulong u when (ticksPerIterations >= 281 && ticksPerIterations <= 290): {
                    return (byte)rnd.Next(44, 46);
                }

                case ulong u when (ticksPerIterations >= 291 && ticksPerIterations <= 300): {
                    return (byte)rnd.Next(42, 44);
                }

                case ulong u when (ticksPerIterations >= 301 && ticksPerIterations <= 310): {
                    return (byte)rnd.Next(40, 42);
                }

                case ulong u when (ticksPerIterations >= 311 && ticksPerIterations <= 320): {
                    return (byte)rnd.Next(38, 40);
                }

                case ulong u when (ticksPerIterations >= 321 && ticksPerIterations <= 330): {
                    return (byte)rnd.Next(36, 38);
                }

                case ulong u when (ticksPerIterations >= 331 && ticksPerIterations <= 340): {
                    return (byte)rnd.Next(34, 36);
                }

                case ulong u when (ticksPerIterations >= 341 && ticksPerIterations <= 350): {
                    return (byte)rnd.Next(32, 34);
                }

                case ulong u when (ticksPerIterations >= 351 && ticksPerIterations <= 360): {
                    return (byte)rnd.Next(30, 32);
                }

                case ulong u when (ticksPerIterations >= 361 && ticksPerIterations <= 370): {
                    return (byte)rnd.Next(28, 30);
                }

                case ulong u when (ticksPerIterations >= 371 && ticksPerIterations <= 380): {
                    return (byte)rnd.Next(26, 28);
                }

                case ulong u when (ticksPerIterations >= 381 && ticksPerIterations <= 390): {
                    return (byte)rnd.Next(24, 26);
                }

                case ulong u when (ticksPerIterations >= 391 && ticksPerIterations <= 400): {
                    return (byte)rnd.Next(22, 24);
                }

                case ulong u when (ticksPerIterations >= 401 && ticksPerIterations <= 410): {
                    return (byte)rnd.Next(20, 22);
                }

                case ulong u when (ticksPerIterations >= 411 && ticksPerIterations <= 420): {
                    return (byte)rnd.Next(18, 20);
                }

                case ulong u when (ticksPerIterations >= 421 && ticksPerIterations <= 430): {
                    return (byte)rnd.Next(16, 18);
                }

                case ulong u when (ticksPerIterations >= 431 && ticksPerIterations <= 440): {
                    return (byte)rnd.Next(14, 16);
                }

                case ulong u when (ticksPerIterations >= 441 && ticksPerIterations <= 450): {
                    return (byte)rnd.Next(12, 14);
                }

                case ulong u when (ticksPerIterations >= 451 && ticksPerIterations <= 460): {
                    return (byte)rnd.Next(10, 12);
                }

                case ulong u when (ticksPerIterations >= 461 && ticksPerIterations <= 470): {
                    return (byte)rnd.Next(8, 10);
                }

                case ulong u when (ticksPerIterations >= 471 && ticksPerIterations <= 480): {
                    return (byte)rnd.Next(6, 8);
                }

                case ulong u when (ticksPerIterations >= 481 && ticksPerIterations <= 490): {
                    return (byte)rnd.Next(4, 6);
                }

                case ulong u when (ticksPerIterations >= 491 && ticksPerIterations <= 500): {
                    return (byte)rnd.Next(2, 4);
                }

                case ulong u when (ticksPerIterations > 500): {
                    return 0;
                }

                default: {
                    return 0;
                }
            }
        }

        public static byte GetScoreDisk(ulong ticksPerIterations) {
            System.Random rnd = new System.Random();

            switch (ticksPerIterations) {
                case ulong u when (ticksPerIterations >= 0 && ticksPerIterations <= 10): {
                    return 100;
                }

                case ulong u when (ticksPerIterations >= 11 && ticksPerIterations <= 20): {
                    return (byte)rnd.Next(98, 100);
                }

                case ulong u when (ticksPerIterations >= 21 && ticksPerIterations <= 30): {
                    return (byte)rnd.Next(96, 98);
                }

                case ulong u when (ticksPerIterations >= 31 && ticksPerIterations <= 40): {
                    return (byte)rnd.Next(94, 96);
                }

                case ulong u when (ticksPerIterations >= 41 && ticksPerIterations <= 50): {
                    return (byte)rnd.Next(92, 94);
                }

                case ulong u when (ticksPerIterations >= 51 && ticksPerIterations <= 60): {
                    return (byte)rnd.Next(90, 92);
                }

                case ulong u when (ticksPerIterations >= 61 && ticksPerIterations <= 70): {
                    return (byte)rnd.Next(88, 90);
                }

                case ulong u when (ticksPerIterations >= 71 && ticksPerIterations <= 80): {
                    return (byte)rnd.Next(86, 88);
                }

                case ulong u when (ticksPerIterations >= 81 && ticksPerIterations <= 90): {
                    return (byte)rnd.Next(84, 86);
                }

                case ulong u when (ticksPerIterations >= 91 && ticksPerIterations <= 100): {
                    return (byte)rnd.Next(82, 84);
                }

                case ulong u when (ticksPerIterations >= 101 && ticksPerIterations <= 110): {
                    return (byte)rnd.Next(80, 82);
                }

                case ulong u when (ticksPerIterations >= 111 && ticksPerIterations <= 120): {
                    return (byte)rnd.Next(78, 80);
                }

                case ulong u when (ticksPerIterations >= 121 && ticksPerIterations <= 130): {
                    return (byte)rnd.Next(76, 78);
                }

                case ulong u when (ticksPerIterations >= 131 && ticksPerIterations <= 140): {
                    return (byte)rnd.Next(74, 76);
                }

                case ulong u when (ticksPerIterations >= 141 && ticksPerIterations <= 150): {
                    return (byte)rnd.Next(72, 74);
                }

                case ulong u when (ticksPerIterations >= 151 && ticksPerIterations <= 160): {
                    return (byte)rnd.Next(70, 72);
                }

                case ulong u when (ticksPerIterations >= 161 && ticksPerIterations <= 170): {
                    return (byte)rnd.Next(68, 70);
                }

                case ulong u when (ticksPerIterations >= 171 && ticksPerIterations <= 180): {
                    return (byte)rnd.Next(66, 68);
                }

                case ulong u when (ticksPerIterations >= 181 && ticksPerIterations <= 190): {
                    return (byte)rnd.Next(64, 66);
                }

                case ulong u when (ticksPerIterations >= 191 && ticksPerIterations <= 200): {
                    return (byte)rnd.Next(62, 64);
                }

                case ulong u when (ticksPerIterations >= 201 && ticksPerIterations <= 210): {
                    return (byte)rnd.Next(60, 62);
                }

                case ulong u when (ticksPerIterations >= 211 && ticksPerIterations <= 220): {
                    return (byte)rnd.Next(58, 60);
                }

                case ulong u when (ticksPerIterations >= 221 && ticksPerIterations <= 230): {
                    return (byte)rnd.Next(56, 58);
                }

                case ulong u when (ticksPerIterations >= 231 && ticksPerIterations <= 240): {
                    return (byte)rnd.Next(54, 56);
                }

                case ulong u when (ticksPerIterations >= 241 && ticksPerIterations <= 250): {
                    return (byte)rnd.Next(52, 54);
                }

                case ulong u when (ticksPerIterations >= 251 && ticksPerIterations <= 260): {
                    return (byte)rnd.Next(50, 52);
                }

                case ulong u when (ticksPerIterations >= 261 && ticksPerIterations <= 270): {
                    return (byte)rnd.Next(48, 50);
                }

                case ulong u when (ticksPerIterations >= 271 && ticksPerIterations <= 280): {
                    return (byte)rnd.Next(46, 48);
                }

                case ulong u when (ticksPerIterations >= 281 && ticksPerIterations <= 290): {
                    return (byte)rnd.Next(44, 46);
                }

                case ulong u when (ticksPerIterations >= 291 && ticksPerIterations <= 300): {
                    return (byte)rnd.Next(42, 44);
                }

                case ulong u when (ticksPerIterations >= 301 && ticksPerIterations <= 310): {
                    return (byte)rnd.Next(40, 42);
                }

                case ulong u when (ticksPerIterations >= 311 && ticksPerIterations <= 320): {
                    return (byte)rnd.Next(38, 40);
                }

                case ulong u when (ticksPerIterations >= 321 && ticksPerIterations <= 330): {
                    return (byte)rnd.Next(36, 38);
                }

                case ulong u when (ticksPerIterations >= 331 && ticksPerIterations <= 340): {
                    return (byte)rnd.Next(34, 36);
                }

                case ulong u when (ticksPerIterations >= 341 && ticksPerIterations <= 350): {
                    return (byte)rnd.Next(32, 34);
                }

                case ulong u when (ticksPerIterations >= 351 && ticksPerIterations <= 360): {
                    return (byte)rnd.Next(30, 32);
                }

                case ulong u when (ticksPerIterations >= 361 && ticksPerIterations <= 370): {
                    return (byte)rnd.Next(28, 30);
                }

                case ulong u when (ticksPerIterations >= 371 && ticksPerIterations <= 380): {
                    return (byte)rnd.Next(26, 28);
                }

                case ulong u when (ticksPerIterations >= 381 && ticksPerIterations <= 390): {
                    return (byte)rnd.Next(24, 26);
                }

                case ulong u when (ticksPerIterations >= 391 && ticksPerIterations <= 400): {
                    return (byte)rnd.Next(22, 24);
                }

                case ulong u when (ticksPerIterations >= 401 && ticksPerIterations <= 410): {
                    return (byte)rnd.Next(20, 22);
                }

                case ulong u when (ticksPerIterations >= 411 && ticksPerIterations <= 420): {
                    return (byte)rnd.Next(18, 20);
                }

                case ulong u when (ticksPerIterations >= 421 && ticksPerIterations <= 430): {
                    return (byte)rnd.Next(16, 18);
                }

                case ulong u when (ticksPerIterations >= 431 && ticksPerIterations <= 440): {
                    return (byte)rnd.Next(14, 16);
                }

                case ulong u when (ticksPerIterations >= 441 && ticksPerIterations <= 450): {
                    return (byte)rnd.Next(12, 14);
                }

                case ulong u when (ticksPerIterations >= 451 && ticksPerIterations <= 460): {
                    return (byte)rnd.Next(10, 12);
                }

                case ulong u when (ticksPerIterations >= 461 && ticksPerIterations <= 470): {
                    return (byte)rnd.Next(8, 10);
                }

                case ulong u when (ticksPerIterations >= 471 && ticksPerIterations <= 480): {
                    return (byte)rnd.Next(6, 8);
                }

                case ulong u when (ticksPerIterations >= 481 && ticksPerIterations <= 490): {
                    return (byte)rnd.Next(4, 6);
                }

                case ulong u when (ticksPerIterations >= 491 && ticksPerIterations <= 500): {
                    return (byte)rnd.Next(2, 4);
                }

                case ulong u when (ticksPerIterations > 500): {
                    return 0;
                }

                default: {
                    return 0;
                }
            }
        }
    }
}