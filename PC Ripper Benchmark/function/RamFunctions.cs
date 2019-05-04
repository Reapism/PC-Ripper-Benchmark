using PC_Ripper_Benchmark.exception;
using PC_Ripper_Benchmark.util;
using PC_Ripper_Benchmark.window;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using static PC_Ripper_Benchmark.function.RipperTypes;

namespace PC_Ripper_Benchmark.function {

    /// <summary>
    /// The <see cref="RamFunctions"/> class.
    /// <para>Represents all the functions for 
    /// testing the CPU component. Includes single
    /// and multithreaded testing using various
    /// common data structures.</para>
    /// <para>Author: <see langword="Anthony Jaghab"/> (c),
    /// all rights reserved.</para>
    /// </summary>

    public class RamFunctions {

        #region Instance members (fields)

        /// <summary>
        /// A <see cref="RipperSettings"/> instance
        /// used to get information about the test
        /// parameters.
        /// </summary>
        private readonly RipperSettings rs;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Default constructor.
        /// </summary>

        public RamFunctions(ref RipperSettings rs) {
            this.rs = rs;
        }

        /// <summary>
        /// Runs the benchmarking test on the RAM
        /// with a particular <see cref="ThreadType"/>.
        /// </summary>
        /// <param name="threadType">The type of threading 
        /// for the test.</param>
        /// <param name="userData">The <see cref="UserData"/> thats passed
        /// into the instance for user information but is marked 
        /// <see langword="readonly"/> internally.</param>
        /// <param name="ui">The <see cref="MainWindow"/> instance thats passed
        /// into for UI related tasks for updating components in it.</param>
        /// <returns>A new <see cref="RamResults"/> instance
        /// containing the result.</returns>
        /// <exception cref="RipperThreadException"></exception>

        public void RunRAMBenchmark(ThreadType threadType, ref UserData userData, MainWindow ui) {
            var results = new RamResults(this.rs, ref userData);
            switch (threadType) {

                case ThreadType.Single: {
                    // runs task on main thread.
                    RunTestsSingle(ref results);

                    InteractWithUI(ref results, ui);

                    break;
                }

                case ThreadType.SingleUI: {
                    // runs task, but doesn't wait for result.

                    void a() { RunTestsSingleUI(ref results, ui); }

                    Task task = new Task(a);

                    task.Start();

                    break;
                }

                case ThreadType.Multithreaded: {

                    break;
                }

                default: {
                    throw new RipperThreadException("Unknown thread type to call. " +
                        "public RAMResults RunRAMBenchmark(ThreadType threadType) " +
                        "in function.RamFunctions ");
                }
            }

        }

        /// <summary>
        /// Runs each test <see cref="RipperSettings.IterationsPerRAMTest"/> times.
        /// <para>Should be (<see cref="RamResults.UniqueTestCount"/> * 
        /// <see cref="RipperSettings.IterationsPerRAMTest"/>)
        /// timespans in <see cref="RamResults.TestCollection"/>.</para>
        /// </summary>
        /// <param name="results">The <see cref="RamResults"/> by reference 
        /// to add the <see cref="TimeSpan"/>(s).</param>

        private void RunTestsSingle(ref RamResults results) {
            for (byte b = 0; b < this.rs.IterationsPerRAMTest; b++) {
                results.TestCollection.Add(RunVirtualFolderMatrix());
            }

            for (byte b = 0; b < this.rs.IterationsPerRAMTest; b++) {
                results.TestCollection.Add(RunVirtualBulkFile());
            }

            for (byte b = 0; b < this.rs.IterationsPerRAMTest; b++) {
                results.TestCollection.Add(RunReferenceDereference());
            }
        }

        /// <summary>
        /// Runs each test <see cref="RipperSettings.IterationsPerRAMTest"/> times.
        /// <para>Should be (<see cref="RamResults.UniqueTestCount"/> * 
        /// <see cref="RipperSettings.IterationsPerRAMTest"/>)
        /// timespans in <see cref="RamResults.TestCollection"/>.</para>
        /// </summary>
        /// <param name="results">The <see cref="RamResults"/> by reference 
        /// to add the <see cref="TimeSpan"/>(s).</param>
        /// <param name="ui">The <see cref="MainWindow"/> instance thats passed
        /// into for UI related tasks for updating components in it.</param>

        private void RunTestsSingleUI(ref RamResults results, MainWindow ui) {
            for (byte b = 0; b < this.rs.IterationsPerRAMTest; b++) {
                results.TestCollection.Add(RunVirtualFolderMatrix());
            }

            for (byte b = 0; b < this.rs.IterationsPerRAMTest; b++) {
                results.TestCollection.Add(RunVirtualBulkFile());
            }

            for (byte b = 0; b < this.rs.IterationsPerRAMTest; b++) {
                results.TestCollection.Add(RunReferenceDereference());
            }

            InteractWithUI(ref results, ui);
        }

        private void InteractWithUI(ref RamResults results, MainWindow ui) {
            string desc = results.Description;

            ui.Dispatcher.InvokeAsync(() => {
                ui.txtResults.AppendText($"Successfully ran the RAM test! Below is the " +
                    $"results of the test.\n\n" +
                    $"{desc}\n\n" +
                    $"\n\n");
            });

            ui.Dispatcher.InvokeAsync(() => {
                ui.txtBlkResults.Text = "Results for the RAM test are below! If you would " +
                "like to send the results to the database, right click the results and press " +
                "send to database!";
            });

            ui.Dispatcher.InvokeAsync(() => {
                ui.ShowTabWindow(Tab.RESULTS);
                ui.btnRunTheTest.IsEnabled = true;
                ui.txtBlkRunningTest.Text = "Are you sure you want to run this test?";
                ui.txtResults.ScrollToVerticalOffset(0);
                ui.txtBlkRunningTestTips.Visibility = System.Windows.Visibility.Hidden;
                ThemeManager.StopRunningTest();
            });
        }


        /// <summary>
        /// Creates (N) virtual directories in memory
        /// using the <see cref="RipperFolder"/> class
        /// with N <see cref="RipperFile"/>(s) thrown in
        /// and reads all the files back into memory, size,
        /// and data. </summary>
        /// <returns></returns>

        private TimeSpan RunVirtualFolderMatrix() {
            var sw = Stopwatch.StartNew();

            List<RipperFolder> lstFolders = new List<RipperFolder>();
            List<RipperFile> lstFiles = new List<RipperFile>();
            Random rnd = new Random();

            ulong NUM_FOLDERS = this.rs.IterationsRAMFolderMatrix;
            ulong NUM_FILES = this.rs.IterationsRAMFolderMatrix / 2;

            const int num_rnd_data = 5;

            // Create N folders with N files randomly in them.
            // Naming convention of folder and files are in HEX.
            // File and Folders contain the same name if they are derived.
            for (ulong i = 0; i < NUM_FOLDERS; i++) {
                lstFolders.Add(new RipperFolder($"folder{string.Format("0x{0:X}", i)}",
                    $"path={string.Format("0x{0:X}", i)}", true,
                    new RipperFile($"file{string.Format("0x{0:X}", i)}",
                    GenerateData(ref rnd, num_rnd_data), num_rnd_data)));
            }

            // read all files in the directories
            foreach (RipperFolder dir in lstFolders) {
                RipperFile file = dir.File;
                string data = dir.File.Data;
                ulong size = dir.File.Size;
            }

            sw.Stop();
            return sw.Elapsed;
        }

        /// <summary>
        /// Creates N virtual files in memory
        /// using the <see cref="RipperFile"/> class.
        /// <para> While a <see cref="RipperFile"/> 
        /// is in scope, perform write and read
        /// of the <see cref="RipperFile"/> using
        /// random numbers.</para>
        /// </summary>
        /// <returns></returns>

        private TimeSpan RunVirtualBulkFile() {
            var sw = Stopwatch.StartNew();

            List<RipperFile> ripperFiles = new List<RipperFile>();
            Random rnd = new Random();

            const int size = 5;

            // write 
            for (ulong u = 0; u < this.rs.IterationsRAMVirtualBulkFile; u++) {
                ripperFiles.Add(new RipperFile($"file{string.Format("0x{0:X}", u)}",
                    GenerateData(ref rnd, size), size));
            }

            // read
            foreach (RipperFile file in ripperFiles) {
                string readIn = file.Data;
                ulong the_size = file.Size;
            }

            sw.Stop();
            return sw.Elapsed;
        }

        /// <summary>
        /// Creates objects to reference and dereference
        /// their locations in memory quickly.
        /// </summary>
        /// <returns></returns>

        private TimeSpan RunReferenceDereference() {
            var sw = Stopwatch.StartNew();

            List<RipperFile> lstObjects = new List<RipperFile>();
            Random rnd = new Random();

            const int size = 5;
            // add objects
            for (ulong u = 0; u < this.rs.IterationsRAMReferenceDereference; u++) {
                lstObjects.Add(new RipperFile($"file{string.Format("0x{0:X}", u)}",
                    GenerateData(ref rnd, size), size));
            }

            // reference/ dereference objects
            for (int i = 0; i < lstObjects.Count; i++) {
                lstObjects.RemoveAt(i);
            }

            sw.Stop();
            return sw.Elapsed;
        }

        /// <summary>
        /// Returns a string containing random data.
        /// </summary>
        /// <param name="rnd">The <see cref="Random"/> object by reference to use.</param>
        /// <param name="size">Size in terms of number of random data.</param>
        /// <returns></returns>

        private string GenerateData(ref Random rnd, int size) {
            string returnMe = string.Empty;

            for (int i = 0; i < size; i++) {
                returnMe += rnd.Next(int.MaxValue).ToString();
            }

            return returnMe;
        }
    }

    #endregion
}
