﻿using System;
using System.IO;
using System.Windows.Forms;
using System.Windows.Threading;
using Microsoft.Win32;
using PC_Ripper_Benchmark.exception;
using PC_Ripper_Benchmark.util;

using static PC_Ripper_Benchmark.function.RipperTypes;

namespace PC_Ripper_Benchmark.function {

    /// <summary>
    /// The <see cref="DiskFunctions"/> class.
    /// Represents all the functions for 
    /// testing the disk/ssd component. Includes single
    /// and multithreaded testing using various
    /// custom algorithms for read/writing..
    /// <para>Author: <see langword="Anthony Jaghab"/> (c),
    /// all rights reserved.</para>
    /// </summary>

    public class DiskFunctions {

        #region Instance members (fields)

        /// <summary>
        /// A <see cref="RipperSettings"/> instance
        /// used to get information about the test
        /// parameters.
        /// </summary>
        private readonly RipperSettings rs;
        private string workingDir;
        #endregion

        #region Constructor(s)

        /// <summary>
        /// Default constructor.
        /// </summary>

        public DiskFunctions(ref RipperSettings rs) {
            this.rs = rs;
            workingDir = string.Empty;
        }

        /// <summary>
        /// Runs the benchmarking test on the DISK
        /// with a particular <see cref="ThreadType"/>.
        /// </summary>
        /// <param name="threadType">The type of threading 
        /// for the test.</param>
        /// <returns>A new <see cref="DiskFunctions"/> instance
        /// containing the result.</returns>
        /// <exception cref="RipperThreadException"></exception>

        public DiskResults RunDiskBenchmark(ThreadType threadType) {
            var results = new DiskResults(this.rs);

            switch (threadType) {

                case ThreadType.Single: {
                    // runs task on main thread.
                    RunTestsSingle(ref results);
                    break;
                }

                case ThreadType.SingleUI: {
                    // runs task, but doesn't wait for result.
                    

                    break;
                }

                case ThreadType.Multithreaded: {

                    break;
                }

                default: {
                    throw new RipperThreadException("Unknown thread type to call. " +
                        "public DiskResults RunDiskBenchmark(ThreadType threadType) " +
                        "in function.DiskFunctions ");
                }
            }

            return results;
        }

        /// <summary>
        /// Prompts the user to choose a directory
        /// and outputs the <paramref name="path"/> as a string.
        /// Returns whether the directory exists.
        /// </summary>
        /// <param name="path">Sets a working directory 
        /// and if its valid, passes it out.</param>
        /// <returns></returns>

        public static bool SetWorkingDirectory(out string path) {
            path = string.Empty;

            FolderBrowserDialog folderBrowser = new FolderBrowserDialog {
                Description = "Choose a directory!",
                ShowNewFolderButton = true,               
            };

            if (folderBrowser.ShowDialog() == DialogResult.OK) {
                path = folderBrowser.SelectedPath;       
            } 

            return Directory.Exists(path);
        }

        /// <summary>
        /// Runs each test <see cref="RipperSettings.IterationsPerDiskTest"/> times.
        /// <para>Should be (<see cref="DiskResults.UniqueTestCount"/> * 
        /// <see cref="RipperSettings.IterationsPerDiskTest"/>)
        /// timespans in <see cref="DiskResults.TestCollection"/>.</para>
        /// </summary>
        /// <param name="results">The <see cref="DiskResults"/> by reference 
        /// to add the <see cref="TimeSpan"/>(s).</param>

        private void RunTestsSingle(ref DiskResults results) {
            for (byte b = 0; b < this.rs.IterationsPerDiskTest; b++) {
                results.TestCollection.Add(RunFolderMatrix());
            }

            for (byte b = 0; b < this.rs.IterationsPerDiskTest; b++) {
                results.TestCollection.Add(RunBulkFile());
            }

            for (byte b = 0; b < this.rs.IterationsPerDiskTest; b++) {
                results.TestCollection.Add(RunReadWriteParse());
            }

            for (byte b = 0; b < this.rs.IterationsPerDiskTest; b++) {
                results.TestCollection.Add(RunDiskRipper());
            }
        }

        /// <summary>
        /// Creates random directories and sub directories 
        /// on the filesystem, and places files in some,
        /// and searches the random filestructure for the 
        /// given files.
        /// <para>Intensive unit test.</para>
        /// </summary>
        /// <returns></returns>

        private TimeSpan RunFolderMatrix() {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Writes a bunch of random characters
        /// to a file. 
        /// </summary>
        /// <returns></returns>

        private TimeSpan RunBulkFile() {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Attempts to read/write folders and files
        /// on the directory <see cref="RunFolderMatrix"/>
        /// creates.
        /// </summary>
        /// <returns></returns>

        private TimeSpan RunReadWriteParse() {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Runs <see cref="RunBulkFile"/> in every
        /// directory generated by <see cref="RunFolderMatrix"/>
        /// <para>Very intensive task.</para>
        /// </summary>
        /// <returns></returns>

        private TimeSpan RunDiskRipper() {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Just creates the folder structure and doesn't perform a search
        /// </summary>

        private void FolderMatrixSnapshot() {

        }
    }
    #endregion
}
