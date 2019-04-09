using PC_Ripper_Benchmark.exception;
using PC_Ripper_Benchmark.util;
using System;
using System.Diagnostics;
using System.IO;
using System.Security.AccessControl;
using System.Text;
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

        /// <summary>
        /// The character list used to generate
        /// random strings from.
        /// </summary>
        private readonly char[] charList;

        /// <summary>
        /// The file extension for the files.
        /// </summary>
        private readonly string fileExt;

        /// <summary>
        /// The <see cref="Random"/> instance.
        /// </summary>
        private readonly Random rnd;

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
            this.fileExt = "ripperblk";
            this.charList = ("abcdefghijklmnopqrstuvwxyz" +
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789").ToCharArray();
            this.rnd = new Random();

            this.WorkingDir = string.Empty;
        }

        #endregion

        #region Disk Function(s) 

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

        private TimeSpan RunFolderMatrix() {
            var sw = Stopwatch.StartNew();

            GenerateConfigFile(this.WorkingDir, TestName.DISKFolderMatrix);
            FolderMatrixSnapshot(this.WorkingDir);
            DeleteDirectories(this.WorkingDir);

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

            GenerateConfigFile(this.WorkingDir, TestName.DISKBulkFile);
            BulkFileSnapshot(this.WorkingDir);
            DeleteFiles(this.WorkingDir);

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

            GenerateConfigFile(this.WorkingDir, TestName.DISKReadWriteParse);
            ReadWriteSnapshot(this.WorkingDir);
            DeleteFiles(this.WorkingDir);

            sw.Stop();
            return sw.Elapsed;
        }

        /// <summary>
        /// Runs <see cref="RunReadWriteParse"/> in every
        /// directory generated by <see cref="RunFolderMatrix"/>
        /// <para>Very intensive task.</para>
        /// </summary>
        /// <returns></returns>

        private TimeSpan RunDiskRipper() {
            var sw = Stopwatch.StartNew();

            GenerateConfigFile(this.WorkingDir, TestName.DISKRipper);
            DiskRipperSnapshot(this.WorkingDir);
            DeleteDirectories(this.WorkingDir, true);

            sw.Stop();
            return sw.Elapsed;
        }

        #endregion

        #region Extraneous function(s) and helper function(s).

        /// <summary>
        /// Locks a directory located at path.
        /// </summary>
        /// <param name="path">The path of the directory to lock.</param>
        /// <returns></returns>

        public bool LockDirectory(string path) {
            try {
                DirectorySecurity ds = Directory.GetAccessControl(path);
                FileSystemAccessRule accessRule = new FileSystemAccessRule(Environment.UserName,
                    FileSystemRights.FullControl, AccessControlType.Deny);
                ds.AddAccessRule(accessRule);
                Directory.SetAccessControl(path, ds);
                return true;
            } catch {
                return false;
            }
        }

        /// <summary>
        /// Unlocks a Directory given at the path.
        /// </summary>
        /// <param name="path">The path of the directory to unlock.</param>
        /// <returns></returns>

        public bool UnlockDirectory(string path) {
            try {
                DirectorySecurity ds = Directory.GetAccessControl(path);
                FileSystemAccessRule accessRule = new FileSystemAccessRule(Environment.UserName,
                    FileSystemRights.FullControl, AccessControlType.Deny);
                ds.RemoveAccessRule(accessRule);
                Directory.SetAccessControl(path, ds);
                return true;
            } catch {
                return false;
            }
        }

        /// <summary>
        /// Unlock a plethora of directories.
        /// </summary>
        /// <param name="paths">The paths as <see langword="params"/>.</param>
        /// <returns></returns>

        public bool UnlockDirectory(params string[] paths) {
            try {
                foreach (string s in paths) {
                    DirectorySecurity ds = Directory.GetAccessControl(s);
                    FileSystemAccessRule accessRule = new FileSystemAccessRule(Environment.UserName,
                        FileSystemRights.FullControl, AccessControlType.Deny);
                    ds.RemoveAccessRule(accessRule);
                    Directory.SetAccessControl(s, ds);
                }
                return true;
            } catch {
                return false;
            }
        }

        /// <summary>
        /// Generates a random string that may contain
        /// [a-z], [A-Z], and [0-9]. (62 characters.)
        /// </summary>
        /// <param name="length">The length of the string to generate</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>

        private string GetRandomString(int length) {
            string rndStr = string.Empty;

            if (length < 1) {
                throw new ArgumentOutOfRangeException("Length must be greater than 0!");
            }

            for (int i = 0; i < length; i++) {
                rndStr += this.charList[this.rnd.Next(this.charList.Length)];
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
                ShowNewFolderButton = true
            };

            if (folderBrowser.ShowDialog() == DialogResult.OK) {
                path = folderBrowser.SelectedPath + "\\";
                this.WorkingDir = path;
            }

            return Directory.Exists(path);
        }

        /// <summary>
        /// Attempts to generate a configuration file
        /// in the path. Returns whether the it was 
        /// successful.
        /// </summary>
        /// <param name="path">The path to create the config file.</param>
        /// <param name="testName">Represents a test which changes the description
        /// for the config file.</param>
        /// <returns></returns>

        private bool GenerateConfigFile(string path, TestName testName) {
            Func<string> funcGenDescription;

            switch (testName) {
                case TestName.DISKFolderMatrix: {
                    funcGenDescription = new Func<string>(GetFolderMatrixDesc);
                    break;
                }

                case TestName.DISKBulkFile: {
                    funcGenDescription = new Func<string>(GetBulkFileDesc);
                    break;
                }

                case TestName.DISKReadWriteParse: {
                    funcGenDescription = new Func<string>(GetReadWriteDesc);
                    break;
                }

                case TestName.DISKRipper: {
                    funcGenDescription = new Func<string>(GetDiskRipperDesc);
                    break;
                }

                default: {
                    funcGenDescription = new Func<string>(GetNullDesc);
                    return false;
                }
            }

            StreamWriter writer;

            string data = funcGenDescription();
            string fileName; // the path of the file.

            // create a file config.ripperblk with a description in it.
            try {
                fileName = Path.Combine(path, "config." + this.fileExt);

                writer = new StreamWriter(fileName, true);
                writer.Write(data);
                writer.Close();
            } catch {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Returns whether the the function was able
        /// to delete the file located at <paramref name="path"/>.
        /// </summary>
        /// <param name="path">The path of the file to delete.</param>
        /// <returns></returns>

        private bool DeleteConfigFile(string path) {
            try {
                File.Delete(path);
            } catch (Exception) {
                return false;
            }

            return true;
        }


        /// <summary>
        /// Attempts to delete all files that
        /// start with "config" in the specified
        /// path. Ignores files that can't be 
        /// deleted.
        /// </summary>
        /// <param name="path">The path to delete 
        /// the config files.</param>

        private void DeleteConfigFiles(string path) {
            DirectoryInfo d;
            FileInfo[] Files;

            try {
                d = new DirectoryInfo(path);
                Files = d.GetFiles($"config*");
            } catch {
                return;
            }

            foreach (FileInfo file in Files) {

                try {
                    File.Delete(file.FullName);
                } catch (Exception) {
                    continue;
                }

            }
        }

        /// <summary>
        /// Creates the folder structure 
        /// for the FolderMatrix algorithm.
        /// <para>Internally catches exceptions,
        /// and reports them as a <see cref="MessageBox"/>
        /// to the caller.</para>
        /// </summary>
        /// <param name="path">The path to create 
        /// subdirectories within.</param>

        private void FolderMatrixSnapshot(string path) {
            DirectoryInfo directoryInfo;

            for (ulong u = 0; u < this.rs.IterationsDISKFolderMatrix; u++) {
                try {
                    directoryInfo = Directory.CreateDirectory(Path.Combine(path, u.ToString()));
                } catch {
                    continue;
                }
            }
        }

        /// <summary>
        /// Creates .ripperblk files in a directory.
        /// </summary>
        /// <param name="path">The path to create 
        /// files within.</param>

        private void BulkFileSnapshot(string path) {
            FileStream fileStream;
            Random rnd = new Random();
            StreamReader sr;
            StreamWriter writer;

            string fileName; // the path of the file.

            // Write each file.

            fileName = Path.Combine(path, $"BULK{GetRandomString(15)}.{this.fileExt}");
            fileStream = File.Create(fileName);
            sr = new StreamReader(fileStream, true);

            writer = new StreamWriter(fileStream, Encoding.UTF8) {
                AutoFlush = true
            };

            writer.Write(GenerateBulkData());

            writer.Flush();
            writer.Close();
            fileStream.Close();
        }


        /// <summary>
        /// Generates bulk data and returns it as 
        /// type <see langword="string"/>.
        /// <para>Uses a <see cref="StringBuilder"/> internally
        /// for performance.</para>
        /// </summary>
        /// <returns></returns>

        private string GenerateBulkData() {
            StringBuilder data = new StringBuilder(string.Empty);

            for (ulong u = 0; u < this.rs.IterationsDiskBulkFile; u++) {
                data.Append(this.rnd.Next().ToString());
            }

            return data.ToString();
        }

        /// <summary>
        /// Creates a large file with
        /// N random number iterations inside.
        /// </summary>
        /// <param name="path">The directory to 
        /// create the file in.</param>

        private void ReadWriteSnapshot(string path) {
            FileStream fileStream;
            Random rnd = new Random();
            StreamReader sr;
            StreamWriter writer;

            string filePath = Path.Combine(path, $"BULK.{this.fileExt}");

            try {
                fileStream = File.Create(filePath);
                writer = new StreamWriter(fileStream, System.Text.Encoding.UTF8) {
                    AutoFlush = true
                };

                // Write each file.
                for (ulong i = 0; i < this.rs.IterationsDiskReadWriteParse; i++) {
                    writer.Write(rnd.Next());
                }

                writer.Close();
                sr = new StreamReader(filePath);

                // Read each file.
                while (!sr.EndOfStream) {
                    sr.Read();
                }

                sr.Close();
                fileStream.Close();
            } catch {

            }
        }

        /// <summary>
        /// Creates a large file with
        /// N random number iterations inside.
        /// </summary>
        /// <param name="path">The directory to 
        /// create directories and files within.</param>

        private void DiskRipperSnapshot(string path) {
            FolderMatrixSnapshot(path);

            DirectoryInfo d = new DirectoryInfo(path);
            DirectoryInfo[] directories = d.GetDirectories($"*");
            FileStream fs;

            ulong u = 0;

            foreach (DirectoryInfo dir in directories) {
                try {
                    // creates a DiskRipper file in every directory from
                    // FolderMatrix algorithm.
                    fs = File.Create(Path.Combine(path, u.ToString(),
                        $"DiskRipper{u.ToString() + GetRandomString(6)}.{this.fileExt}"));
                    StreamWriter sw = new StreamWriter(fs);
                    sw.WriteLine(GenerateBulkData());
                    sw.Flush();
                    u++;
                } catch {
                    continue;
                }
            }
        }

        /// <summary>
        /// Delete directories used for cleaning up the disk after
        /// the tests.
        /// </summary>
        /// <param name="path">The path to delete directories in.</param>
        /// <param name="recursiveDelete">Choose whether to 
        /// recursively delete subdirectories</param>

        private void DeleteDirectories(string path, bool recursiveDelete = false) {
            DirectoryInfo d = new DirectoryInfo(path);
            DirectoryInfo[] directories = d.GetDirectories($"*", SearchOption.TopDirectoryOnly);

            foreach (DirectoryInfo dir in directories) {
                try {
                    Directory.Delete(dir.FullName, recursiveDelete);
                } catch (Exception e) {
                    //MessageBox.Show($"Error deleting the directory!" + e.ToString());
                    continue;
                }
            }
        }

        /// <summary>
        /// Delete files in a directory used for 
        /// cleaning up the disk after the tests.
        /// </summary>
        /// <param name="path">The path to delete 
        /// files within.</param>

        private void DeleteFiles(string path) {
            DirectoryInfo d = new DirectoryInfo(path);
            FileInfo[] Files = d.GetFiles($"*");

            foreach (FileInfo dir in Files) {
                try {
                    File.Delete(dir.FullName);
                } catch (Exception e) {
                    //MessageBox.Show($"Error deleting the directory!" + e.ToString());
                    continue;
                }
            }
        }

        /// <summary>
        /// Represents a null description by returing
        /// null as a string.
        /// </summary>
        /// <returns></returns>

        private string GetNullDesc() => "NULL";

        /// <summary>
        /// Returns the FolderMatrix description that would be embedded 
        /// in a config. If the user finds this file, its
        /// likely the test failed.
        /// </summary>
        /// <returns></returns>

        private string GetFolderMatrixDesc() {
            string desc = string.Empty;

            desc += $"******This file was generated by the FOLDER MATRIX algorithm" +
                $" starting at {DateTime.Now.ToLongTimeString()}. This file should" +
                $" automatically be deleted if a successful run of the algorithm." +
                $" Please report this as a bug if the file(s) persist after running" +
                $" a test.******";

            return desc;
        }

        /// <summary>
        /// Returns the BulkFile description that would be embedded 
        /// in a config. If the user finds this file, its
        /// likely the test failed.
        /// </summary>
        /// <returns></returns>

        private string GetBulkFileDesc() {
            string desc = string.Empty;

            desc += $"******This file was generated by the BULK FILE algorithm" +
                $" starting at {DateTime.Now.ToLongTimeString()}. This file should" +
                $" automatically be deleted if a successful run of the algorithm." +
                $" Please report this as a bug if the file(s) persist after running" +
                $" a test.******";

            return desc;
        }

        /// <summary>
        /// Returns the ReadWrite description that would be embedded 
        /// in a config. If the user finds this file, its
        /// likely the test failed.
        /// </summary>
        /// <returns></returns>

        private string GetReadWriteDesc() {
            string desc = string.Empty;

            desc += $"******This file was generated by the READ & WRITE PARSE algorithm" +
                $" starting at {DateTime.Now.ToLongTimeString()}. This file should" +
                $" automatically be deleted if a successful run of the algorithm." +
                $" Please report this as a bug if the file(s) persist after running" +
                $" a test.******";

            return desc;
        }

        /// <summary>
        /// Returns the DiskRipper description that would be embedded 
        /// in a config. If the user finds this file, its
        /// likely the test failed.
        /// </summary>
        /// <returns></returns>

        private string GetDiskRipperDesc() {
            string desc = string.Empty;

            desc += $"******This file was generated by the DISK RIPPER algorithm" +
                $" starting at {DateTime.Now.ToLongTimeString()}. This file should" +
                $" automatically be deleted if a successful run of the algorithm." +
                $" Please report this as a bug if the file(s) persist after running" +
                $" a test.******";

            return desc;
        }
    }

    #endregion
}
