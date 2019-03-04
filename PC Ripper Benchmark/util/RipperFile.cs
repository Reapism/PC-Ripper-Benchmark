namespace PC_Ripper_Benchmark.util {

    /// <summary>
    /// The <see cref="RipperFile"/> class.
    /// <para>Represents a virtual file in 
    /// memory. Stores the name of the file,
    /// data of the file, size, and path.</para>
    /// <para>Author: <see langword="Anthony Jaghab"/> (c),
    /// all rights reserved.</para>
    /// </summary>

    public class RipperFile : RipperFolder {

        private string fileName;
        private string data;
        private string size;
        private RipperFolder path;

    }
}
