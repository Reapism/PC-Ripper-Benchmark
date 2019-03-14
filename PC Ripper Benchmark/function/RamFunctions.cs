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
        /// with (N/2) <see cref="RipperFile"/>(s) thrown in.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>

        private TimeSpan RunVirtualFolderMatrix() {
            var sw = Stopwatch.StartNew();

            List<RipperFolder> lstFolders = new List<RipperFolder>();
            List<RipperFile> lstFiles = new List<RipperFile>();
            Random rnd = new Random();

            ulong NUM_FILES = this.rs.IterationsRAMFolderMatrix;

            // Create N folders
            for (ulong i = 0; i < this.rs.IterationsRAMFolderMatrix; i++) {
                lstFolders.Add(new RipperFolder($"folder{i}", $"path={i}", true));
            }

            // Create N/2 Files and place them in random folders.

            for (ulong i = 0; i < NUM_FILES; i++) {           
                int data = rnd.Next(int.MaxValue);

                byte[] buf = new byte[8];
                rnd.NextBytes(buf);
                // instead, use hex to name folders and filenames!
                string folderName = $"folder{(ulong)BitConverter.ToInt64(buf, 0)}";
                

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



            sw.Stop();
            return sw.Elapsed;
        }
        
    }
    #endregion
}
