using PC_Ripper_Benchmark.exception;
using PC_Ripper_Benchmark.util;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
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

        #region Instance members (fields), and propertie(s).

        /// <summary>
        /// A <see cref="RipperSettings"/> instance
        /// used to get information about the test
        /// parameters.
        /// </summary>
        private readonly RipperSettings rs;
        private char[] charList;

        /// <summary>
        /// The working directory of the test.
        /// </summary>
        public string WorkingDir { get; set; }

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Default constructor.
        /// </summary>

        public DiskFunctions(ref RipperSettings rs) {
            this.rs = rs;
            this.WorkingDir = string.Empty;

            this.charList = ("abcdefghijklmnopqrstuvwxyz" +
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789").ToCharArray();
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
        /// Generates a random string that may contain
        /// [a-z], [A-Z], and [0-9]. (62 characters.)
        /// </summary>
        /// <param name="length">The length of the string to generate</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>

        private string GetRandomString(int length) {
            Random rnd = new Random((int)DateTime.Now.Ticks);
            string rndStr = string.Empty;

            if (length < 1) {
                throw new ArgumentOutOfRangeException("Length must be greater than 0!");
            }

            for (int i = 0; i < length; i++) {
                rndStr += this.charList[rnd.Next(this.charList.Length)];
            }

            return rndStr;
        }

        /// <summary>
        /// Prompts the user to choose a directory
        /// and outputs the <paramref name="path"/> as a string.
        /// Returns whether the directory exists.
        /// </summary>
        /// <param name="path">Sets a working directory 
        /// and if its valid, passes it out.</param>
        /// <returns></returns>

        public bool SetWorkingDirectory(out string path) {
            path = string.Empty;

            FolderBrowserDialog folderBrowser = new FolderBrowserDialog {
                Description = "Choose a directory!",
                ShowNewFolderButton = true,
            };

            if (folderBrowser.ShowDialog() == DialogResult.OK) {
                path = folderBrowser.SelectedPath + "\\";
                this.WorkingDir = path;
            }

            return Directory.Exists(path);
        }

        /// <summary>
        /// Prompts the user to choose a directory
        /// and sets the internal working directory
        /// if the its true.
        /// Returns whether the directory exists.
        /// </summary>
        /// <returns></returns>

        public bool SetWorkingDirectory() {
            string path = string.Empty;

            FolderBrowserDialog folderBrowser = new FolderBrowserDialog {
                Description = "Choose a directory!",
                ShowNewFolderButton = true,
            };

            if (folderBrowser.ShowDialog() == DialogResult.OK) {
                path = folderBrowser.SelectedPath + "\\";
                this.WorkingDir = path;
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
        /// Creates random directories on the filesystem,
        /// <para>Non-Intensive unit test.</para>
        /// </summary>
        /// <returns></returns>
        /// <exception cref="UnauthorizedAccessException"></exception>

        private TimeSpan RunFolderMatrix() {
            var sw = Stopwatch.StartNew();

            DirectoryInfo directoryInfo;
            string dirName;
            const int dirNameLength = 15;

            for (ulong u = 0; u < this.rs.IterationsDISKFolderMatrix; u++) {
                dirName = GetRandomString(dirNameLength);
                try {
                    directoryInfo = Directory.CreateDirectory(Path.Combine(this.WorkingDir, u.ToString()));
                } catch (Exception e) {
                    MessageBox.Show($"{u} {dirName} failed. {e.ToString()}");
                }
            }

            DirectoryInfo d = new DirectoryInfo(this.WorkingDir);
            DirectoryInfo[] directories = d.GetDirectories($"*");

            foreach (DirectoryInfo dir in directories) {
                try {
                    Directory.Delete(dir.FullName);
                } catch (Exception e) {
                    throw new FileNotFoundException($"Error deleting the directory!" + e.ToString());
                }
            }

            sw.Stop();
            return sw.Elapsed;
        }

        /// <summary>
        /// Writes N files with
        /// random characters to the files
        /// and reads them.
        /// <para>All these operations will be 
        /// included in the time.</para>
        /// </summary>
        /// <returns></returns>

        private TimeSpan RunBulkFile() {
            var sw = Stopwatch.StartNew();

            FileStream fileStream;
            Random rnd = new Random();
            StreamReader sr;
            StreamWriter writer;
            StreamWriter writer2;

            string fileName; // the path of the file.
            const string fileExt = ".ripperblk"; // file extension for file.
            const int fileNameLength = 15; // the length of the random name.

            // create a file config.ripperblk with a description in it.
            fileName = this.WorkingDir + "config" + fileExt;
            writer2 = new StreamWriter(fileName, true);
            writer2.Write(GetBulkFileDesc());
            writer2.Close();

            for (ulong u = 0; u < this.rs.IterationsDiskBulkFile; u++) {
                // Write each file.

                fileName = Path.Combine(this.WorkingDir, u.ToString() + fileExt);

                try {
                    fileStream = File.Create(fileName);
                    sr = new StreamReader(fileStream, true);

                    writer = new StreamWriter(fileStream, System.Text.Encoding.UTF8) {
                        AutoFlush = true
                    };

                    writer.Write(rnd.Next().ToString());

                } catch (Exception e) {
                    throw new Exception("Oh no. We don't have jurisdiction in this directory.", e);
                }

                writer.Flush();
                writer.Close();
            }



            DirectoryInfo d = new DirectoryInfo(this.WorkingDir);
            FileInfo[] Files = d.GetFiles($"*{fileExt}");

            foreach (FileInfo file in Files) {
                try {
                    File.Delete(file.FullName);
                } catch (Exception e) {
                    throw new FileNotFoundException($"Error deleting the file!" + e.ToString());
                }
            }

            sw.Stop();
            return sw.Elapsed;
        }

        /// <summary>
        /// Writes a single large file and reads it back.
        /// <para>( N * 16 ) characters in the file.</para>
        /// </summary>
        /// <returns></returns>

        private TimeSpan RunReadWriteParse() {
            var sw = Stopwatch.StartNew();

            FileStream fileStream;
            StreamReader sr;
            StreamWriter writer;

            string fileName = "BULK"; // the path of the file.
            string desc = GetReadWriteDesc(); // gets the description of the file (header).
            const string fileExt = ".ripperblk"; // file extension for file.
            const int charBlock = 16;

            fileName = Path.Combine(this.WorkingDir, fileName, fileExt);
            fileStream = File.Create(fileName);
            writer = new StreamWriter(fileStream, System.Text.Encoding.UTF8);
            writer.Write(desc);

            // Write each file.
            for (ulong i = 0; i < this.rs.IterationsDiskReadWriteParse; i++) {
                writer.Write(GetRandomString(charBlock));
            }

            sr = new StreamReader(fileStream, true);

            // Read each file.
            while (!sr.EndOfStream) {
                sr.Read();
            }

            writer.Close();


            sw.Stop();
            return sw.Elapsed;
        }

        /// <summary>
        /// Runs <see cref="RunBulkFile"/> in every
        /// directory generated by <see cref="RunFolderMatrix"/>
        /// <para>Very intensive task.</para>
        /// </summary>
        /// <returns></returns>

        private TimeSpan RunDiskRipper() {
            var sw = Stopwatch.StartNew();

            //FileStream fileStream;
            //StreamReader sr;
            //StreamWriter writer;

            //string fileName; // the path of the file.
            //string desc = GetReadWriteDesc(); // gets the description of the file (header).
            //const string fileExt = ".ripperblk"; // file extension for file.
            //const int fileNameLength = 8; // the length of the random name.
            //const int charBlock = 16;

            //DirectoryInfo d = new DirectoryInfo(this.workingDir);
            //FileInfo[] Files = d.GetFiles($"*{fileExt}");

            //foreach (FileInfo file in Files) {
            //    try {
            //        File.OpenWrite
            //        writer = new StreamWriter(fileStream, System.Text.Encoding.UTF8);
            //        writer.Write(desc);
            //        File.Delete(file.FullName);
            //    } catch (Exception e) {
            //        throw new FileNotFoundException($"Error deleting the file!" + e.ToString());
            //    }
            //}

            sw.Stop();
            return sw.Elapsed;
        }

        /// <summary>
        /// Just creates the folder structure 
        /// and doesn't perform a search
        /// </summary>

        private void FolderMatrixSnapshot() {

        }

        private void BulkFileSnapshot() {

        }

        private string GetBulkFileDesc() {
            string desc = string.Empty;

            desc += $"This file was generated by the BULK FILE algorithm" +
                $" starting at {DateTime.Now.ToLongTimeString()}. This file should" +
                $" automatically be deleted if a successful run of the algorithm." +
                $" Please report this as a bug if the file(s) persist after running" +
                $" a test.";

            return desc;
        }

        private string GetReadWriteDesc() {
            string desc = string.Empty;

            desc += $"This file was generated by the READ & WRITE PARSE algorithm" +
                $" starting at {DateTime.Now.ToLongTimeString()}. This file should" +
                $" automatically be deleted if a successful run of the algorithm." +
                $" Please report this as a bug if the file(s) persist after running" +
                $" a test.";

            return desc;
        }

        private string GetDiskRipperDesc() {
            string desc = string.Empty;

            desc += $"This file was generated by the DISK RIPPER algorithm" +
                $" starting at {DateTime.Now.ToLongTimeString()}. This file should" +
                $" automatically be deleted if a successful run of the algorithm." +
                $" Please report this as a bug if the file(s) persist after running" +
                $" a test.";

            return desc;
        }
    }
    #endregion
}
