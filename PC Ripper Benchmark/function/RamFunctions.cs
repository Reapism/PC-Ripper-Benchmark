using PC_Ripper_Benchmark.util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        /// with a particular threading type.
        /// </summary>
        /// <param name="threadType">The type of threading 
        /// for the test.</param>
        /// <returns>A new <see cref="RamResults"/> instance
        /// containing the result.</returns>

        public RamResults RunRamBenchmark(ThreadType threadType) {
            var results = new RamResults();

            Action run_funcs = new Action(() => {

            });


            return results;
        }

        /// <summary>
        /// Creates (N) virtual directories in memory
        /// using the <see cref="RipperFolder"/> class
        /// with ~(N/2) <see cref="RipperFile"/>(s) thrown in.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>

        private TimeSpan RunVirtualFolderMatrix() {
            var sw = Stopwatch.StartNew();

            List<RipperFolder> lstFolders = new List<RipperFolder>();
            List<RipperFile> lstFiles = new List<RipperFile>();
            Random rnd = new Random();
            Random rnd2 = new Random();

            ulong NUM_FOLDERS = this.rs.IterationsRAMFolderMatrix;
            ulong NUM_FILES = this.rs.IterationsRAMFolderMatrix / 2;

            const int num_rnd_data = 5;

            // Create N folders with ~N/2 files randomly in them.
            // Naming convention of folder and files are in HEX.
            // File and Folders contain same name if they are embedded.
            for (ulong i = 0; i < NUM_FOLDERS; i++) {

                lstFolders.Add(new RipperFolder($"folder{string.Format("0x{0:X}", i)}",
                    $"path={string.Format("0x{0:X}", i)}", true,
                    rnd2.Next(2) == 0 ? null : new RipperFile($"file{string.Format("0x{0:X}", i)}",
                    GenerateData(ref rnd, num_rnd_data), num_rnd_data)));
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

            const int size = 1000;

            // write 
            for (ulong u = 0; u < this.rs.IterationsRAMVirtualBulkFile; u++) {
                ripperFiles.Add(new RipperFile($"file{string.Format("0x{0:X}", u)}",
                    GenerateData(ref rnd, size), size));
            }

            // read
            foreach (RipperFile file in ripperFiles) {
                string readIn = file.Data;
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

            // add objects
            for (ulong u = 0; u < this.rs.IterationsRAMReferenceDereference; u++) {
                lstObjects.Add(new RipperFile());
            }

            // reference/ dereference objects
            foreach (RipperFile file in lstObjects) {
                lstObjects.Remove(file);
            }

            if (lstObjects.Count != 0) {
                
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
