using PC_Ripper_Benchmark.exception;
using PC_Ripper_Benchmark.util;
using PC_Ripper_Benchmark.window;
using System;
using System.Diagnostics;
using System.IO;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
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
            this.fileExt = ".ripperblk";
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
        /// <param name="userData">The <see cref="UserData"/> thats passed
        /// into the instance for user information but is marked 
        /// <see langword="readonly"/> internally.</param>
        /// <param name="ui">The <see cref="MainWindow"/> instance thats passed
        /// into for UI related tasks for updating components in it.</param>
        /// <exception cref="RipperThreadException"></exception>

        public void RunDiskBenchmark(ThreadType threadType, ref UserData userData, MainWindow ui) {
            var results = new DiskResults(this.rs, ref userData);

            switch (threadType) {

                case ThreadType.Single: {
                    // runs task on main thread.
                    RunTestsSingle(ref results);

                    InteractWithUI(ref results, ui);

                    break;
                }

                case ThreadType.SingleUI: {
                    // runs task, but doesn't wait for result.

                    void a() { RunTestsSingleUI(ref results, ui); }

                    Task task = new Task(a);

                    task.Start();

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
        /// Runs each test <see cref="RipperSettings.IterationsPerDiskTest"/> times.
        /// <para>Should be (<see cref="DiskResults.UniqueTestCount"/> * 
        /// <see cref="RipperSettings.IterationsPerDiskTest"/>)
        /// timespans in <see cref="DiskResults.TestCollection"/>.</para>
        /// </summary>
        /// <param name="results">The <see cref="DiskResults"/> by reference 
        /// to add the <see cref="TimeSpan"/>(s).</param>
        /// <param name="ui">The <see cref="MainWindow"/> instance thats passed
        /// into for UI related tasks for updating components in it.</param>

        private void RunTestsSingleUI(ref DiskResults results, MainWindow ui) {

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

            InteractWithUI(ref results, ui);
        }

        private async Task<CPUResults> RunTestsMultithreaded() {
            return null;
        }

        private void InteractWithUI(ref DiskResults results, MainWindow ui) {
            string desc = results.Description;

            ui.Dispatcher.InvokeAsync(() => {
                ui.txtResults.AppendText($"Successfully ran the DISK test! Below is the " +
                    $"results of the test.\n\n" +
                    $"{desc}\n\n" +
                    $"\n\n");
            });

            ui.Dispatcher.InvokeAsync(() => {
                ui.txtBlkResults.Text = "Results for the DISK test are below! If you would " +
                "like to send the results to the database, right click the results and press " +
                "send to database!";
            });

            ui.Dispatcher.InvokeAsync(() => {
                ui.ShowTabWindow(Tab.RESULTS);
                ui.btnRunTheTest.IsEnabled = true;
                ui.txtBlkRunningTest.Text = "Are you sure you want to run this test?";
                ui.txtResults.ScrollToVerticalOffset(0);
                ui.txtBlkRunningTestTips.Visibility = System.Windows.Visibility.Hidden;
                ThemeManager.StopRunningTest();
            });
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
            DeleteConfigFile(this.WorkingDir);

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
            DeleteConfigFiles(this.WorkingDir);

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
                if (!IsDirectoryEmpty(folderBrowser.SelectedPath + "\\")) {
                    MessageBox.Show($"The following path is not empty. " +
                    $"Please specify an empty directory.", "NonEmptyDirectory"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
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
                if (!IsDirectoryEmpty(folderBrowser.SelectedPath + "\\")) {
                    MessageBox.Show($"The following path is not empty. " +
                        $"Please specify an empty directory.", "NonEmptyDirectory"
                        , MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                path = folderBrowser.SelectedPath + "\\";
                this.WorkingDir = path;
            }

            return Directory.Exists(path);
        }

        /// <summary>
        /// Returns whether a given directory in
        /// a path is empty or not.
        /// </summary>
        /// <param name="path">The path to check.</param>
        /// <returns></returns>

        private bool IsDirectoryEmpty(string path) {
            try {
                bool b = Directory.GetFileSystemEntries(path).Length == 0;
                return b;
            } catch {
                return false;
            }
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
                fileName = Path.Combine(path, "config" + this.fileExt);

                writer = new StreamWriter(fileName, true);
                writer.Write(data);
                writer.Flush();
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
                Files = d.GetFiles($"config{this.fileExt}");
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
                    directoryInfo = Directory.CreateDirectory(Path.Combine(path, "FolderMatrix" + u.ToString()));
                } catch {
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
        /// <param name="iterations">Optionally pass the number
        /// of folders to create.</param>

        private void FolderMatrixSnapshot(string path, ulong iterations) {
            DirectoryInfo directoryInfo;

            for (ulong u = 0; u < iterations; u++) {
                try {
                    directoryInfo = Directory.CreateDirectory(Path.Combine(path, "FolderMatrix" + u.ToString()));
                } catch {
                    continue;
                }
            }
        }

        /// <summary>
        /// Creates N .ripperblk files in a directory.
        /// </summary>
        /// <param name="path">The path to create 
        /// files within.</param>

        private void BulkFileSnapshot(string path) {
            FileStream fileStream;
            Random rnd = new Random();
            StreamWriter writer;

            string fileName; // the path of the file.

            // Write each file.

            for (ulong u = 0; u < this.rs.IterationsDiskBulkFile; u++) {

                try {
                    fileName = Path.Combine(path, $"BULK{GetRandomString(15)}{this.fileExt}");
                    fileStream = File.Create(fileName);

                    writer = new StreamWriter(fileStream, Encoding.UTF8) {
                        AutoFlush = true
                    };

                    writer.Write(GenerateBulkData());

                    writer.Flush();
                    writer.Close();
                    fileStream.Close();
                } catch {
                    continue;
                }
            }

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
        /// <para></para>(sizeof(int) * N = bytes)
        /// </summary>
        /// <param name="path">The directory to 
        /// create the file in.</param>
        /// <param name="name">The name of the
        /// file to generate. (No extension)</param>

        private void ReadWriteSnapshot(string path, string name = "READWRITE") {
            FileStream fileStream;
            StreamReader sr;
            StreamWriter writer;

            string filePath = Path.Combine(path, $"{name}{this.fileExt}");

            try {
                fileStream = File.Create(filePath);
                writer = new StreamWriter(fileStream, System.Text.Encoding.UTF8) {
                    AutoFlush = true
                };

                // Write each file.
                for (ulong i = 0; i < this.rs.IterationsDiskReadWriteParse; i++) {
                    writer.Write(GenerateBulkData());
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
            // create DiskRipper iteration number of folders.
            FolderMatrixSnapshot(path, this.rs.IterationsDiskRipper);

            DirectoryInfo d = new DirectoryInfo(path);
            DirectoryInfo[] directories = d.GetDirectories($"FolderMatrix*");

            ulong u = 0;

            foreach (DirectoryInfo dir in directories) {
                try {
                    // creates a DiskRipper file in every directory from
                    // FolderMatrix algorithm.
                    // FolderMatrix0/DiskRipper.ripperblk
                    string new_path = Path.Combine(this.WorkingDir, dir.Name);
                    ReadWriteSnapshot(new_path, "DiskRipper");
                    DeleteDirectory(this.WorkingDir, "FolderMatrix" + u.ToString()); // delete folder and sub folder
                    
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
            DirectoryInfo[] directories = d.GetDirectories($"FolderMatrix*", SearchOption.TopDirectoryOnly);

            foreach (DirectoryInfo dir in directories) {
                try {
                    Directory.Delete(dir.FullName, recursiveDelete);
                } catch {
                    continue;
                }
            }
        }

        /// <summary>
        /// Delete a directory within a directory.
        /// Used for cleaning up an individual folder named
        /// name.
        /// </summary>
        /// <param name="path">The parent path to delete the directory in.</param>
        /// <param name="name">The name of the folder to delete.</param>

        private void DeleteDirectory(string path, string name) {
            DirectoryInfo d = new DirectoryInfo(path);
            DirectoryInfo[] directories = d.GetDirectories(name, SearchOption.TopDirectoryOnly);

            foreach (DirectoryInfo dir in directories) {
                try {
                    Directory.Delete(dir.FullName, true);
                } catch {
                    continue;
                }
            }
        }

        /// <summary>
        /// Delete files in a directory used for 
        /// cleaning up the disk after the tests.
        /// <para>Deletes files with .ripperblk extension</para>
        /// </summary>
        /// <param name="path">The path to delete 
        /// files within.</param>

        private void DeleteFiles(string path) {
            DirectoryInfo d = new DirectoryInfo(path);
            FileInfo[] Files = d.GetFiles($"*{this.fileExt}");

            foreach (FileInfo dir in Files) {
                try {
                    File.Delete(dir.FullName);
                } catch {
                    continue;
                }
            }
        }


        /// <summary>
        /// Deletes a single file from a given path.
        /// </summary>
        /// <param name="path">The path to delete 
        /// files within.</param>

        private void DeleteFile(string path) {
            try {
                File.Delete(path);
            } catch {
                return;
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
