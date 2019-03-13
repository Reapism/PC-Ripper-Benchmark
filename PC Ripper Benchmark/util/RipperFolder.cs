namespace PC_Ripper_Benchmark.util {

    /// <summary>
    /// The <see cref="RipperFolder"/> class.
    /// <para>Represents a virtual folder in 
    /// memory. Stores the name of the directory
    /// and it's path on the user.</para>
    /// <para>Author: <see langword="Anthony Jaghab"/> (c),
    /// all rights reserved.</para>
    /// </summary>

    public class RipperFolder {

        #region Instance members (fields).

        private string folderName;
        private string path;
        private bool isVirtual;

        #endregion

        #region Constructor(s).

        /// <summary>
        /// Default constructor for creating a 
        /// <see cref="RipperFolder"/>.
        /// </summary>

        public RipperFolder() : this("", "", true) { }

        /// <summary>
        /// Parameterized constructor for creating a 
        /// <see cref="RipperFolder"/>.
        /// </summary>
        /// <param name="folderName">The name of the 
        /// <see cref="RipperFolder"/>. <para>i.e. 
        /// "folder{ <see langword="N"/> }"
        /// where <see langword="N"/> represents a number.</para>
        /// </param>
        /// <param name="path">The path of the 
        /// <see cref="RipperFolder"/>. <para>
        /// [Virtually] i.e. "path={ <see langword="N"/> }"
        /// where <see langword="N"/> represents a number.</para>
        /// <para>
        /// [On Disk] i.e. "path={ <see langword="P"/> }"
        /// where <see langword="P"/> represents a path on disk.</para></param>
        /// <param name="isVirtual">Represents if a given <see cref="RipperFolder"/>
        /// is virtual or not. 
        /// <para>Useful for checking how the path is different.
        /// <para>Usage: if (this.isVirtual) { // parse path as virtual }</para>
        /// </para></param>

        public RipperFolder(string folderName, string path, bool isVirtual) {
            this.folderName = folderName;
            this.path = path;
            this.isVirtual = isVirtual;
        }

        #endregion
    }
}
