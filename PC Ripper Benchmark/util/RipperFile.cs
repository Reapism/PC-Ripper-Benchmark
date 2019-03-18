namespace PC_Ripper_Benchmark.util {

    /// <summary>
    /// The <see cref="RipperFile"/> class.
    /// <para>Represents a virtual file in 
    /// memory. Stores the name of the file,
    /// data of the file, size, and path.</para>
    /// <para>Author: <see langword="Anthony Jaghab"/> (c),
    /// all rights reserved.</para>
    /// </summary>

    public class RipperFile {

        #region Properties


        /// <summary>
        /// Get's the filename for this <see cref="RipperFile"/> instance.
        /// </summary>
        public string FileName { get; }
        /// <summary>
        /// Get's the data for this <see cref="RipperFile"/> instance.
        /// </summary>
        public string Data { get; }
        /// <summary>
        /// Get's the size for this <see cref="RipperFile"/> instance.
        /// </summary>
        public ulong Size { get; }

        #endregion

        #region Constructor(s).

        /// <summary>
        /// Default constructor for creating a <see cref="RipperFile"/>.
        /// <para>It's best to use the parameterized constructor.</para>
        /// </summary>

        public RipperFile() : this("", "", 0) { }

        /// <summary>
        /// Parameterized constructor for creating a 
        /// <see cref="RipperFile"/>.
        /// </summary>
        /// <param name="fileName">The name of the 
        /// <see cref="RipperFile"/>. <para>i.e. 
        /// "file{ <see langword="N"/> }"
        /// where <see langword="N"/> represents a number.</para>
        /// </param>
        /// <param name="data">The contents of the
        /// <see cref="RipperFile"/>.</param>
        /// <param name="size">The size of the <see cref="RipperFile"/>.</param>

        public RipperFile(string fileName, string data, ulong size) {
            // This works because internally, an auto property is converted
            // to readonly, which means it can be set in the constructor,
            // and only in the constructor.

            this.FileName = fileName;
            this.Data = data;
            this.Size = size;
        }

        #endregion


    }
}
