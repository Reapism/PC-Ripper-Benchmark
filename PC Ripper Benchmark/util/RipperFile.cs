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

        #region Instance members (fields).

        private string fileName;
        private string data;
        private long size;
        private RipperFolder parentDir;

        #endregion

        #region Constructor(s).

        /// <summary>
        /// Default constructor for creating a <see cref="RipperFile"/>.
        /// <para>It's best to use the parameterized constructor.</para>
        /// </summary>

        public RipperFile() : this("", "", 0, null) { }

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
        /// <param name="parentDir">The parent directory of this <see cref="RipperFile"/>.</param>

        public RipperFile(string fileName, string data, long size, RipperFolder parentDir) {
            this.fileName = fileName;
            this.data = data;
            this.size = size;
            this.parentDir = parentDir;
        }

        #endregion
    }
}
