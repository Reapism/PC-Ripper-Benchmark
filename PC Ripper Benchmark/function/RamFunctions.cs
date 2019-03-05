using PC_Ripper_Benchmark.util;
using System;
using static PC_Ripper_Benchmark.function.FunctionTypes;

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

        /// <summary>
        /// Default constructor.
        /// </summary>

        public RamFunctions() {

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
        /// Creates virtual directories in memory
        /// using the <see cref="RipperFolder"/> class.
        /// </summary>
        /// <returns></returns>

        private TimeSpan RunVirtualFolderMatrix() {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates virtual files in memory
        /// using the <see cref="RipperFile"/> class.
        /// </summary>
        /// <returns></returns>

        private TimeSpan RunVirtualBulkFile() {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates objects to reference and dereference
        /// quickly.
        /// </summary>
        /// <returns></returns>

        private TimeSpan RunReferenceDereference() {
            throw new NotImplementedException();
        }


    }
}
