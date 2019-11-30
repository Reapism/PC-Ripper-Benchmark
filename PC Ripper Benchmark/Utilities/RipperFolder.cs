namespace PC_Ripper_Benchmark.Utilities
{

    /// <summary>
    /// The <see cref="RipperFolder"/> class.
    /// <para>Represents a virtual folder in 
    /// memory. Stores the name of the directory
    /// and it's path on the user.</para>
    /// <para>Author: <see langword="Anthony Jaghab"/> (c),
    /// all rights reserved.</para>
    /// </summary>

    public class RipperFolder
    {

        #region Properties

        /// <summary>
        /// Get's the folder name for this <see cref="RipperFolder"/> instance.
        /// </summary>
        public string FolderName { get; }

        /// <summary>
        /// Get's the path for this <see cref="RipperFolder"/> instance.
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// Get's whether this <see cref="RipperFolder"/> instance is a 
        /// virtual one or located on disk.
        /// </summary>
        public bool IsVirtual { get; }

        /// <summary>
        /// Get's the <see cref="RipperFile"/> for this <see cref="RipperFolder"/> instance.
        /// </summary>
        public RipperFile File { get; }

        #endregion

        #region Constructor(s).

        /// <summary>
        /// Default constructor for creating a 
        /// <see cref="RipperFolder"/>.
        /// <para>It's best to use the parameterized constructor.</para>
        /// </summary>

        public RipperFolder() : this("", "", true, null) { }

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
        /// <param name="file">A <see cref="RipperFile"/> instance. Can be passed with null
        /// if no <see cref="RipperFile"/> exists for this <see cref="RipperFolder"/>.</param>

        public RipperFolder(string folderName, string path, bool isVirtual, RipperFile file)
        {
            this.FolderName = folderName;
            this.Path = path;
            this.IsVirtual = isVirtual;

            this.File = file;
        }

        #endregion
    }
}
