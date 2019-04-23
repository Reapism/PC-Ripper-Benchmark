using Microsoft.Win32;
using PC_Ripper_Benchmark.database;
using PC_Ripper_Benchmark.exception;
using PC_Ripper_Benchmark.function;
using PC_Ripper_Benchmark.util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Animation;
using XamlAnimatedGif;
using static PC_Ripper_Benchmark.function.RipperTypes;
using static PC_Ripper_Benchmark.util.UserData;

namespace PC_Ripper_Benchmark.window {

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// <para></para>Author(s): <see langword="Anthony Jaghab"/>, 
    /// David Hartglass, (c) all rights reserved.
    /// </summary>

    public partial class MainWindow : Window {

        #region Instance member(s), and enum(s), and properties.        

        /// <summary>
        /// The unique string associated with this
        /// <see cref="MainWindow"/> instance based
        /// on the email.
        /// </summary>

        public string UniqueInstance { get => GetUniqueString(); }

        private RipperSettings rs;
        private WindowSettings ws;
        private Tab testToRun;
        private string workingDir;

        private UserData userData;

        #endregion

        #region Constructor(s) and method(s).

        /// <summary>
        /// Default constructor for the <see cref="MainWindow"/>.
        /// </summary>

        public MainWindow() {
            InitializeComponent();
            Instantiate();
            GetComputerSpecs();
            InstantiateAdvancedSettings();
        }

        /// <summary>
        /// Parameterized const
        /// </summary>
        /// <param name="userData"></param>

        public MainWindow(UserData userData) : this() {
            this.userData = userData;
            WindowSettings ws = new WindowSettings();
            ws.CenterWindowOnScreen(this);
            GetWelcomeText();
            GetUserAccountText();
        }

        /// <summary>
        /// Instantiates this <see cref="MainWindow"/>
        /// instance with default behaviors.
        /// </summary>

        private void Instantiate() {
            this.testToRun = Tab.WELCOME;
            this.rs = new RipperSettings();
            this.ws = new WindowSettings();

            Style s = new Style();
            s.Setters.Add(new Setter(UIElement.VisibilityProperty, Visibility.Collapsed));

            this.tabComponents.ItemContainerStyle = s;

            Style s2 = new Style();
            s2.Setters.Add(new Setter(UIElement.VisibilityProperty, Visibility.Collapsed));

            this.txtBlkRunningTestTips.Visibility = Visibility.Hidden;
            this.tabSettingsInner.ItemContainerStyle = s2;
            this.tabComponents.SelectedIndex = 0;
            this.btnDiskRunTest.IsEnabled = false;

            this.ws.NavigationMenu(this);

            this.radCPUSingleUI.IsChecked = true;
            this.radRamSingleUI.IsChecked = true;
            this.radDiskSingleUI.IsChecked = true;
        }

        /// <summary>
        /// Loads all the data in each settings in the
        /// advanced settings list view.
        /// </summary>

        private void LoadAdvancedSettings() {
            this.menu_cpu_iter_per_test.Header = this.menu_cpu_iter_per_test.Tag.ToString() + this.rs.IterationsPerCPUTest.ToString("n0");

            this.menu_cpu_successorship.Header = this.menu_cpu_successorship.Tag.ToString() + this.rs.IterationsSuccessorship.ToString("n0");
            this.menu_cpu_boolean.Header = this.menu_cpu_boolean.Tag.ToString() + this.rs.IterationsBoolean.ToString("n0");
            this.menu_cpu_queue.Header = this.menu_cpu_queue.Tag.ToString() + this.rs.IterationsQueue.ToString("n0");
            this.menu_cpu_linkedlist.Header = this.menu_cpu_linkedlist.Tag.ToString() + this.rs.IterationsLinkedList.ToString("n0");
            this.menu_cpu_tree.Header = this.menu_cpu_tree.Tag.ToString() + this.rs.IterationsTree.ToString("n0");

            this.menu_ram_per_test.Header = this.menu_ram_per_test.Tag.ToString() + this.rs.IterationsPerRAMTest.ToString("n0");

            this.menu_ram_foldermatrix.Header = this.menu_ram_foldermatrix.Tag.ToString() + this.rs.IterationsRAMFolderMatrix.ToString("n0");
            this.menu_ram_bulkfile.Header = this.menu_ram_bulkfile.Tag.ToString() + this.rs.IterationsRAMVirtualBulkFile.ToString("n0");
            this.menu_ram_readwriteparse.Header = this.menu_ram_readwriteparse.Tag.ToString() + this.rs.IterationsRAMReferenceDereference.ToString("n0");

            this.menu_disk_per_test.Header = this.menu_disk_per_test.Tag.ToString() + this.rs.IterationsPerDiskTest.ToString("n0");

            this.menu_disk_foldermatrix.Header = this.menu_disk_foldermatrix.Tag.ToString() + this.rs.IterationsDISKFolderMatrix.ToString("n0");
            this.menu_disk_bulkfile.Header = this.menu_disk_bulkfile.Tag.ToString() + this.rs.IterationsDiskBulkFile.ToString("n0");
            this.menu_disk_readwriteparse.Header = this.menu_disk_readwriteparse.Tag.ToString() + this.rs.IterationsDiskReadWriteParse.ToString("n0");
            this.menu_disk_ripper.Header = this.menu_disk_ripper.Tag.ToString() + this.rs.IterationsDiskRipper.ToString("n0");

            RipperSettings.SaveApplicationSettings(ref this.rs);
        }

        /// <summary>
        /// Instantiates the advanced settings
        /// tags with their proper names.
        /// </summary>

        private void InstantiateAdvancedSettings() {

            foreach (MenuItem m in this.lstAdvancedSettings.Items) {
                m.Tag = m.Header;
            }

            LoadAdvancedSettings();
        }

        /// <summary>
        /// Sets the welcome text on different labels and
        /// text blocks.
        /// </summary>

        private void GetWelcomeText() {
            this.userData.FirstName = UserData.UppercaseFirst(this.userData.FirstName);
            this.userData.LastName = UserData.UppercaseFirst(this.userData.LastName);

            this.txtblkWelcome.Text = $"Welcome {this.userData.FirstName} to the PC Ripper Benchmark.";
            this.txtBlkWelcomeText.Text = $"Welcome {this.userData.FirstName}, check out your account information below! ";

            this.lblName.Content = $"Profile: {this.userData.LastName}, {this.userData.FirstName}.";

            this.lblTypeOfUser.Content = $"You're using your computer mainly for {this.userData.GetTypeOfUserString()}.";
            this.lblUserSkill.Content = $"You've decided you're an {this.userData.GetUserSkillString()} user.";
        }

        private void GetUserAccountText()
        {
            this.userData.FirstName = UserData.UppercaseFirst(this.userData.FirstName);
            this.userData.LastName = UserData.UppercaseFirst(this.userData.LastName);

            this.accountNameLbl.Content = $"Name: {this.userData.FirstName}, {this.userData.LastName}";

            this.accountTypeOfUserLbl.Content = $"Type: {this.userData.GetTypeOfUserString()}";
            this.accountUserSkillLbl.Content = $"Skill Level: {this.userData.GetUserSkillString()} user";

        }

        /// <summary>
        /// Gets the computer specifications and places them inside the
        /// <see cref="txtComputerSpecs"/> <see cref="RichTextBox"/>.
        /// </summary>

        private void GetComputerSpecs() {
            List<string> lst = new List<string>();
            ComputerSpecs specs = new ComputerSpecs();

            this.txtComputerSpecs.Document.Blocks.Clear();

            this.txtComputerSpecs.AppendText($"System specifications for {specs.UserName}."
                + Environment.NewLine + Environment.NewLine);

            this.txtComputerSpecs.AppendText("Processor (CPU) specs" + Environment.NewLine);
            specs.GetProcessorInfo(out lst);
            foreach (string s in lst) { this.txtComputerSpecs.AppendText("   " + s + Environment.NewLine); }

            this.txtComputerSpecs.AppendText(Environment.NewLine + "RAM specs" + Environment.NewLine);
            specs.GetMemoryInfo(out lst);
            foreach (string s in lst) { this.txtComputerSpecs.AppendText("   " + s + Environment.NewLine); }

            this.txtComputerSpecs.AppendText(Environment.NewLine + "Disks (HDD/SSD) specs" + Environment.NewLine);
            specs.GetDiskInfo(out lst);
            foreach (string s in lst) { this.txtComputerSpecs.AppendText("   " + s + Environment.NewLine); }

            this.txtComputerSpecs.AppendText(Environment.NewLine + "Video card (GPU) specs" + Environment.NewLine);
            specs.GetVideoCard(out lst);
            foreach (string s in lst) { this.txtComputerSpecs.AppendText("   " + s + Environment.NewLine); }
        }

        /// <summary>
        /// Shows a particular Tab to the user using
        /// the <see cref="Tab"/> type.
        /// </summary>
        /// <param name="theTab">The <see cref="Tab"/> type.</param>

        public void ShowTabWindow(Tab theTab) {

            switch (theTab) {
                case Tab.WELCOME: {
                    this.tabComponents.SelectedIndex = (int)Tab.WELCOME;
                    break;
                }

                case Tab.CPU: {
                    this.tabComponents.SelectedIndex = (int)Tab.CPU;
                    break;
                }

                case Tab.DISK: {
                    this.tabComponents.SelectedIndex = (int)Tab.DISK;
                    break;
                }

                case Tab.RAM: {
                    this.tabComponents.SelectedIndex = (int)Tab.RAM;
                    break;
                }

                case Tab.GPU: {
                    this.tabComponents.SelectedIndex = (int)Tab.GPU;
                    break;
                }

                case Tab.SETTINGS: {
                    this.tabComponents.SelectedIndex = (int)Tab.SETTINGS;
                    break;
                }

                case Tab.RESULTS: {
                    this.tabComponents.SelectedIndex = (int)Tab.RESULTS;
                    break;
                }


                case Tab.RUNNING_TEST: {
                    LoadRunningTest();

                    this.tabComponents.SelectedIndex = (int)Tab.RUNNING_TEST;
                    break;
                }

                case Tab.MY_ACCOUNT: {
                    this.tabComponents.SelectedIndex = (int)Tab.MY_ACCOUNT;
                    break;
                }

                default: {
                    break;
                }
            }
        }

        /// <summary>
        /// Loads a random preloader. 
        /// <para>Threaded.</para>
        /// </summary>

        private void LoadRunningTest() {
            Action a = new Action(() => {
                Random rnd = new Random();
                Uri uri = ChoosePreloader();

                if (uri != null) {
                    this.Dispatcher.InvokeAsync(() => {
                        AnimationBehavior.SetSourceUri(this.imgPreloader, uri);
                        AnimationBehavior.SetRepeatBehavior(this.imgPreloader, RepeatBehavior.Forever);
                    });
                }
            });

            Task t = new Task(a);
            t.Start();
        }

        /// <summary>
        /// Save the application settings.
        /// </summary>

        private void SaveSettings() => RipperSettings.SaveApplicationSettings(ref this.rs);


        /// <summary>
        /// Gets lines from a file in the resouces directory
        /// and returns whether it succeeded or not.
        /// </summary>
        /// <param name="fileName">The file name. e.g. Reap.txt</param>
        /// <param name="lst">Outputs a <see cref="List{T}"/> holding strings.</param>
        /// <returns></returns>

        private bool GetLinesFromFile(string fileName, out List<string> lst) {
            string path = Path.Combine(Directory.GetCurrentDirectory(),
                "resources", fileName);
            try {
                lst = new List<string>();
                StreamReader sr = new StreamReader(path);

                while (!sr.EndOfStream) {
                    lst.Add(sr.ReadLine());
                }

            } catch {
                lst = null;
                return false;
            }
            return true;
        }

        /// <summary>
        /// Chooses a random preloader from the
        /// resources/preloader_urls.txt file.
        /// Returns a <see cref="Uri"/>.
        /// </summary>
        /// <returns></returns>

        private Uri ChoosePreloader() {

            Uri uri;
            Random rnd = new Random();

            if (GetLinesFromFile("preloader_urls.txt", out List<string> urls)) {
                int rndIndex = rnd.Next(urls.Count);
                uri = new Uri(urls[rndIndex]);
                return uri;
            }

            return null;
        }

        private void ExportResults(ExportType type, TextBlock textBlock) {
            SaveFileDialog saveFile = new SaveFileDialog {
                Title = "Export file to...",
                InitialDirectory = Path.GetDirectoryName(Environment.GetFolderPath(
                    Environment.SpecialFolder.DesktopDirectory)),
            };

            TextRange range;
            FileStream fStream;

            string format = string.Empty;

            switch (type) {

                case ExportType.TEXTFILE: {
                    saveFile.Filter = "Textfile|*.txt";
                    format = DataFormats.Text;
                    break;
                }

                case ExportType.XAML: {
                    saveFile.Filter = "XAML|*.xaml";
                    format = DataFormats.Xaml;
                    break;
                }
            }

            try {
                if (saveFile.ShowDialog() == true) {

                    range = new TextRange(textBlock.ContentStart,
                        textBlock.ContentEnd);
                    fStream = new FileStream(saveFile.FileName, FileMode.Create);

                    range.Save(fStream, format);
                    fStream.Close();
                    MessageBox.Show($"The {saveFile.SafeFileName} was exported to " +
                         $"{saveFile.FileName} successfully.", "Success",
                         MessageBoxButton.OK, MessageBoxImage.Information);
                }
            } catch (Exception e) {
                MessageBox.Show($"An exception has occured in generating the file. {e.ToString()}", "SaveFileException",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExportResults(ExportType type, RichTextBox richTextBox) {
            SaveFileDialog saveFile = new SaveFileDialog {
                Title = "Export file to...",
                InitialDirectory = Path.GetDirectoryName(Environment.GetFolderPath(
                    Environment.SpecialFolder.DesktopDirectory)),
            };

            TextRange range;
            FileStream fStream;

            string format = string.Empty;

            switch (type) {

                case ExportType.TEXTFILE: {
                    saveFile.Filter = "Textfile|*.txt";
                    format = DataFormats.Text;
                    break;
                }

                case ExportType.XAML: {
                    saveFile.Filter = "XAML|*.xaml";
                    format = DataFormats.Xaml;
                    break;
                }
            }

            try {
                if (saveFile.ShowDialog() == true) {

                    range = new TextRange(richTextBox.Document.ContentStart,
                        richTextBox.Document.ContentEnd);
                    fStream = new FileStream(saveFile.FileName, FileMode.Create);

                    range.Save(fStream, format);
                    fStream.Close();
                    MessageBox.Show($"The {saveFile.SafeFileName} was exported to " +
                         $"{saveFile.FileName} successfully.", "Success",
                         MessageBoxButton.OK, MessageBoxImage.Information);
                }
            } catch (Exception e) {
                MessageBox.Show($"An exception has occured in generating the file. {e.ToString()}", "SaveFileException",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void RunCPUTest() {
            CPUFunctions cpu = new CPUFunctions(ref this.rs);

            ThreadType threadType;

            if (this.radCPUSingle.IsChecked == true) {
                threadType = ThreadType.Single;
            } else if (this.radCPUSingleUI.IsChecked == true) {
                threadType = ThreadType.SingleUI;
            } else if (this.radCPUMultithread.IsChecked == true) {
                threadType = ThreadType.Multithreaded;
            } else {
                MessageBox.Show("Please select a type of test you'd like to perform.",
                    "RipperUnknownTestException", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Dispatcher.Invoke(() => {
                    ShowTabWindow(Tab.CPU);
                });
                return;
            }

            this.txtResults.Document.Blocks.Clear();

            try {
                this.Dispatcher.Invoke(() => {
                    cpu.RunCPUBenchmark(threadType, ref this.userData, this);
                });

            } catch (RipperThreadException ex) {
                MessageBox.Show($"Oh no. A Ripper thread exception occured.. {ex.ToString()}");
            }
        }

        private void RunRAMTest() {
            RamFunctions ram = new RamFunctions(ref this.rs);

            ThreadType threadType;

            if (this.radRamSingle.IsChecked == true) {
                threadType = ThreadType.Single;
            } else if (this.radRamSingleUI.IsChecked == true) {
                threadType = ThreadType.SingleUI;
            } else {
                MessageBox.Show("Please select a type of test you'd like to perform.",
                    "RipperUnknownTestException", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Dispatcher.Invoke(() => {
                    ShowTabWindow(Tab.RAM);
                });
                return;
            }

            this.txtResults.Document.Blocks.Clear();

            try {
                this.Dispatcher.Invoke(() => {
                    ram.RunRAMBenchmark(threadType, ref this.userData, this);
                });

            } catch (RipperThreadException ex) {
                MessageBox.Show($"Oh no. A Ripper thread exception occured.. {ex.ToString()}");
            }

        }

        private void RunDISKTest() {
            DiskFunctions disk = new DiskFunctions(ref this.rs) {
                WorkingDir = this.workingDir
            };

            ThreadType threadType;

            if (this.radDiskSingle.IsChecked == true) {
                threadType = ThreadType.Single;
            } else if (this.radDiskSingleUI.IsChecked == true) {
                threadType = ThreadType.SingleUI;
            } else {
                MessageBox.Show("Please select a type of test you'd like to perform.",
                    "RipperUnknownTestException", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Dispatcher.Invoke(() => {
                    ShowTabWindow(Tab.DISK);
                });
                return;
            }

            this.txtResults.Document.Blocks.Clear();

            try {
                this.Dispatcher.InvokeAsync(() => {
                    disk.RunDiskBenchmark(threadType, ref this.userData, this);
                });
            } catch (RipperThreadException ex) {
                MessageBox.Show($"Oh no. A Ripper thread exception occured.. {ex.ToString()}");
            }
        }

        private void RunTest() {
            switch (this.testToRun) {

                case Tab.CPU: {
                    RunCPUTest();
                    break;
                }

                case Tab.RAM: {
                    RunRAMTest();
                    break;
                }

                case Tab.DISK: {
                    RunDISKTest();
                    break;
                }

            }
        }

        /// <summary>
        /// Updates the color and text of the fields
        /// that show the directory to be valid.
        /// </summary>
        /// <param name="path">The path to display
        /// to the user.</param>

        private void ValidDirectory(string path) {
            this.btnDiskRunTest.IsEnabled = true;
            this.btnDiskRunTest.Foreground = Brushes.White;
            this.txtBlkWorkingDir.Text = $"Working Directory Path: {path}";
            this.txtBlkWorkingDir.Foreground = Brushes.GreenYellow;
            this.btnBrowseWorkingDir.BorderBrush = Brushes.GreenYellow;
        }

        /// <summary>
        /// Updates the color and text of the fields
        /// that show the directory to be invalid.
        /// </summary>
        /// <param name="path">The path to display
        /// to the user.</param>

        private void InvalidDirectory(string path) {
            this.btnDiskRunTest.IsEnabled = false;
            this.btnDiskRunTest.Foreground = Brushes.Black;
            this.txtBlkWorkingDir.Text = $"Invalid path: {path}";
            this.txtBlkWorkingDir.Foreground = Brushes.LightSalmon;
            this.btnBrowseWorkingDir.BorderBrush = Brushes.Gold;
        }

        /// <summary>
        /// Unlocks a particular directory on the filesystem.
        /// </summary>

        private void UnlockDir() {
            System.Windows.Forms.FolderBrowserDialog folderBrowser = new System.Windows.Forms.FolderBrowserDialog {
                Description = "Choose a directory!",
                ShowNewFolderButton = true
            };

            if (folderBrowser.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                DiskFunctions diskFunctions = new DiskFunctions(ref this.rs);
                diskFunctions.UnlockDirectory(folderBrowser.SelectedPath);
            }
        }

        /// <summary>
        /// Locks a particular directory on the filesystem.
        /// </summary>

        private void LockDir() {
            System.Windows.Forms.FolderBrowserDialog folderBrowser = new System.Windows.Forms.FolderBrowserDialog {
                Description = "Choose a directory!",
                ShowNewFolderButton = true
            };

            if (folderBrowser.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                DiskFunctions diskFunctions = new DiskFunctions(ref this.rs);
                diskFunctions.LockDirectory(folderBrowser.SelectedPath);
            }
        }

        /// <summary>
        /// Closes all windows that are tagged with the string
        /// "CLOSE".
        /// </summary>
        /// <param name="windows">The windows to close as an array.</param>

        private void CloseWindows(params Window[] windows) {
            foreach (Window window in windows) {
                if (window.Tag.ToString() == "CLOSE") { window.Close(); }
            }
        }

        /// <summary>
        /// Closes all windows that are tagged with the unique string.
        /// </summary>
        /// <param name="uniqueStr">The unique string used to close the window.</param>
        /// <param name="windows">The windows to close as an array.</param>

        private void CloseWindows(string uniqueStr, params Window[] windows) {
            foreach (Window window in windows) {
                if (window.Tag.ToString() == uniqueStr) { window.Close(); }
            }
        }

        /// <summary>
        /// Gets a unique string for this instance.
        /// </summary>

        private string GetUniqueString() => this.userData.Email + this.userData.FirstName + this.userData.GetUserSkillString();

        #endregion

        #region Event(s) and event handler(s).

        private void BtnCPU_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e) {
            this.btnCPU.Foreground = Brushes.Salmon;
        }

        private void BtnCPU_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e) {
            this.btnCPU.Foreground = Brushes.Black;
        }

        private void BtnCPU_Click(object sender, RoutedEventArgs e) {
            ShowTabWindow(Tab.CPU);
        }

        private void BtnRAM_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e) {
            this.btnRAM.Foreground = Brushes.Salmon;
        }

        private void BtnRAM_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e) {
            this.btnRAM.Foreground = Brushes.Black;
        }

        private void BtnRAM_Click(object sender, RoutedEventArgs e) {
            ShowTabWindow(Tab.RAM);
        }

        private void BtnDisk_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e) {
            this.btnDisk.Foreground = Brushes.Salmon;
        }

        private void BtnDisk_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e) {
            this.btnDisk.Foreground = Brushes.Black;
        }

        private void BtnDisk_Click(object sender, RoutedEventArgs e) {
            ShowTabWindow(Tab.DISK);
        }

        private void BtnCPURunTest_Click(object sender, RoutedEventArgs e) {
            ShowTabWindow(Tab.RUNNING_TEST);
            this.testToRun = Tab.CPU;
        }

        private void BtnRamRunTest_Click(object sender, RoutedEventArgs e) {
            ShowTabWindow(Tab.RUNNING_TEST);
            this.testToRun = Tab.RAM;
        }

        private void BtnDiskRunTest_Click(object sender, RoutedEventArgs e) {
            ShowTabWindow(Tab.RUNNING_TEST);
            this.testToRun = Tab.DISK;
        }

        private void BtnSettings_Click(object sender, RoutedEventArgs e) {
            ShowTabWindow(Tab.SETTINGS);

            if (this.userData.IsAdvanced == UserSkill.Advanced) {
                this.tabSettingsInner.SelectedIndex = 1;
            } else { this.tabSettingsInner.SelectedIndex = 0; }
        }

        private void MenuResultsExprtTxt_Click(object sender, RoutedEventArgs e) {
            ExportResults(ExportType.TEXTFILE, this.txtResults);
        }

        private void MenuResultsExprtXaml_Click(object sender, RoutedEventArgs e) {
            ExportResults(ExportType.XAML, this.txtResults);
        }

        private void BtnRunTheTest_Click(object sender, RoutedEventArgs e) {
            this.btnRunTheTest.IsEnabled = false;
            this.btnRunTheTest.Foreground = Brushes.Black;
            this.txtBlkRunningTestTips.Visibility = Visibility.Visible;

            if (GetLinesFromFile("running_test_messages.txt", out List<string> lst)) {
                Random rnd = new Random();
                this.txtBlkRunningTest.Text = lst[rnd.Next(lst.Count)];
            } else {
                this.txtBlkRunningTest.Text = "Test is running!";
            }

            if (GetLinesFromFile("message_tips.txt", out List<string> lst2)) {
                Random rnd = new Random();
                this.txtBlkRunningTestTips.Text = lst2[rnd.Next(lst2.Count)];
            } else {
                this.txtBlkRunningTestTips.Text = "Did you know you can change the test settings? Go to Settings->Advanced Settings.";
            }

            ThemeManager theme = new ThemeManager(this);
            theme.RunningTest(this);

            RunTest();
        }

        private void BtnBrowseWorkingDir_Click(object sender, RoutedEventArgs e) {
            // if path is good.
            var disk = new DiskFunctions(ref this.rs);

            if (disk.SetWorkingDirectory(out string path)) {

                // prompt again for verification.
                if (MessageBox.Show("Are you sure you would like " +
                    $"to perfom the test in this directory? {Environment.NewLine} {path}", "Confirmation",
                    MessageBoxButton.YesNo, MessageBoxImage.Question)
                    == MessageBoxResult.Yes && MessageBox.Show("While the test runs please do not " +
                    $"power off the machine or open/access any of the files/folders in folder. Please " +
                    "confirm.", "Confirmation",
                    MessageBoxButton.OKCancel, MessageBoxImage.Information)
                    == MessageBoxResult.OK) {

                    ValidDirectory(path);
                    this.workingDir = path;
                } else {
                    InvalidDirectory(string.Empty);
                }

            } else {
                InvalidDirectory(path);
            }
        }

        private void MenuResultsSelectAll_Click(object sender, RoutedEventArgs e) {
            this.txtResults.SelectAll();
        }

        private void MenuResultsCopy_Click(object sender, RoutedEventArgs e) {
            if (!this.txtResults.Selection.IsEmpty) {
                Clipboard.SetText(this.txtResults.Selection.Text);
            }
        }

        private void BtnMenu_Click(object sender, RoutedEventArgs e) {
            this.ws.NavigationMenu(this);
        }

        private void BtnUnlockDirectory_Click(object sender, RoutedEventArgs e) {
            UnlockDir();
        }

        private void BtnLockDirectory_Click(object sender, RoutedEventArgs e) {
            LockDir();
        }

        //Got focus and lost focus events for buttons on stack panel
        private void BtnCPU_GotFocus(object sender, RoutedEventArgs e) {
            this.btnCPU.BorderThickness = new Thickness(5);
            this.btnCPU.BorderBrush = Brushes.ForestGreen;
        }

        private void BtnRAM_GotFocus(object sender, RoutedEventArgs e) {
            this.btnRAM.BorderThickness = new Thickness(5);
            this.btnRAM.BorderBrush = Brushes.ForestGreen;
        }

        private void BtnDisk_GotFocus(object sender, RoutedEventArgs e) {
            this.btnDisk.BorderThickness = new Thickness(5);
            this.btnDisk.BorderBrush = Brushes.ForestGreen;
        }

        private void BtnSettings_GotFocus(object sender, RoutedEventArgs e) {
            this.btnSettings.BorderThickness = new Thickness(5);
            this.btnSettings.BorderBrush = Brushes.ForestGreen;
        }

        private void BtnCPU_LostFocus(object sender, RoutedEventArgs e) {
            this.btnCPU.BorderThickness = new Thickness(0);
        }

        private void BtnRAM_LostFocus(object sender, RoutedEventArgs e) {
            this.btnRAM.BorderThickness = new Thickness(0);
        }

        private void BtnDisk_LostFocus(object sender, RoutedEventArgs e) {
            this.btnDisk.BorderThickness = new Thickness(0);
        }

        private void BtnSettings_LostFocus(object sender, RoutedEventArgs e) {
            this.btnSettings.BorderThickness = new Thickness(0);
        }

        private void BtnWelcome_Click(object sender, RoutedEventArgs e) {
            ShowTabWindow(Tab.WELCOME);
        }

        private void BtnWelcome_GotFocus(object sender, RoutedEventArgs e) {
            this.btnWelcome.BorderThickness = new Thickness(5);
            this.btnWelcome.BorderBrush = Brushes.ForestGreen;
        }

        private void BtnWelcome_LostFocus(object sender, RoutedEventArgs e) {
            this.btnWelcome.BorderThickness = new Thickness(0);
        }

        private void BtnWelcome_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e) {

        }

        private void BtnWelcome_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e) {

        }

        private void BtnLockDir_Click(object sender, RoutedEventArgs e) {
            LockDir();
        }

        private void BtnUnlockDir_Click(object sender, RoutedEventArgs e) {
            UnlockDir();
        }

        private void BtnShowAdvanced_Click(object sender, RoutedEventArgs e) {
            this.tabSettingsInner.SelectedIndex = 1;
        }

        private void BtnExportSpecTxt_Click(object sender, RoutedEventArgs e) {
            ExportResults(ExportType.TEXTFILE, this.txtComputerSpecs);
        }

        private void MenuNewWindow_Click(object sender, RoutedEventArgs e) {
            new MainWindow(this.userData).Show(); ;
        }

        private void MenuNewWindowAsGuest_Click(object sender, RoutedEventArgs e) {
            new MainWindow(UserData.GetGuestUser()).Show();
        }

        private void MenuCloseWindow_Click(object sender, RoutedEventArgs e) {
            Close();
        }

        private void MenuCloseAllWindows_Click(object sender, RoutedEventArgs e) {
            List<Window> w = new List<Window>();
            foreach (Window window in Application.Current.Windows) {
                window.Tag = "CLOSE";
                w.Add(window);
            }
            CloseWindows(w.ToArray());
        }

        private void MenuSaveSettings_Click(object sender, RoutedEventArgs e) {
            SaveSettings();
        }

        private void MenuCreateNewAccount_Click(object sender, RoutedEventArgs e) {
            var create = new CreateAccountWindow();
            this.ws.TransitionScreen(create, this);
            Close();
        }

        private void MenuLogout_Click(object sender, RoutedEventArgs e) {
            var login = new LoginWindow();
            this.ws.TransitionScreen(login, this);

            List<Window> w = new List<Window>();
            foreach (Window window in Application.Current.Windows) {
                if (typeof(MainWindow).IsInstanceOfType(window)) {
                    MainWindow mw = (MainWindow)window;
                    mw.Tag = mw.UniqueInstance;
                    w.Add(mw);
                }
            }
            CloseWindows(this.UniqueInstance, w.ToArray());
        }

        private void MenuLogoutSave_Click(object sender, RoutedEventArgs e) {
            var login = new LoginWindow();
            this.ws.TransitionScreen(login, this);
            List<Window> w = new List<Window>();
            foreach (Window window in Application.Current.Windows) {
                if (typeof(MainWindow).IsInstanceOfType(window)) {
                    MainWindow mw = (MainWindow)window;
                    mw.Tag = mw.UniqueInstance;
                    w.Add(mw);
                }
            }
            SaveSettings();
            CloseWindows(this.UniqueInstance, w.ToArray());

        }

        private void MenuSettings_Click(object sender, RoutedEventArgs e) {
            BtnSettings_Click(sender, e);
        }

        private void MenuAboutProject_Click(object sender, RoutedEventArgs e) {
            var result = MessageBox.Show("The PC Ripper Benchmark application is a PC diagnostics program that can benchmark " +
                "the computers processor, memory, and hard drive and give it a score. Depending on the score, you might want " +
                "to possibly upgrade certain components of your PC. This project is open sourced on GitHub, would you like " +
                "to go there?", "About this project", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (result == MessageBoxResult.Yes) {
                System.Diagnostics.Process.Start("https://github.com/Reapism/PC-Ripper-Benchmark");
            }
        }

        private void Menu_cpu_iter_per_test_Click(object sender, RoutedEventArgs e) {
            if (!RipperDialog.InputBox("Please enter a new value: ", "", this.rs.IterationsPerCPUTest.ToString(), out byte output)) {
                MessageBox.Show("The value you entered cannot be parsed.", "InvalidParseException", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            this.rs.IterationsPerCPUTest = output;
            LoadAdvancedSettings();
        }

        private void Menu_cpu_successorship_Click(object sender, RoutedEventArgs e) {
            if (!RipperDialog.InputBox("Please enter a new value: ", "", this.rs.IterationsSuccessorship.ToString(), out ulong output)) {
                MessageBox.Show("The value you entered cannot be parsed, or is too small or large.", "InvalidParseException", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            this.rs.IterationsSuccessorship = output;
            LoadAdvancedSettings();
        }

        private void Menu_cpu_boolean_Click(object sender, RoutedEventArgs e) {
            if (!RipperDialog.InputBox("Please enter a new value: ", "", this.rs.IterationsBoolean.ToString(), out ulong output)) {
                MessageBox.Show("The value you entered cannot be parsed, or is too small or large.", "InvalidParseException", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            this.rs.IterationsBoolean = output;
            LoadAdvancedSettings();
        }

        private void Menu_cpu_queue_Click(object sender, RoutedEventArgs e) {
            if (!RipperDialog.InputBox("Please enter a new value: ", "", this.rs.IterationsQueue.ToString(), out ulong output)) {
                MessageBox.Show("The value you entered cannot be parsed, or is too small or large.", "InvalidParseException", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            this.rs.IterationsQueue = output;
            LoadAdvancedSettings();
        }

        private void Menu_cpu_linkedlist_Click(object sender, RoutedEventArgs e) {
            if (!RipperDialog.InputBox("Please enter a new value: ", "", this.rs.IterationsLinkedList.ToString(), out ulong output)) {
                MessageBox.Show("The value you entered cannot be parsed, or is too small or large.", "InvalidParseException", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            this.rs.IterationsLinkedList = output;
            LoadAdvancedSettings();
        }

        private void Menu_cpu_tree_Click(object sender, RoutedEventArgs e) {
            if (!RipperDialog.InputBox("Please enter a new value: ", "", this.rs.IterationsTree.ToString(), out ulong output)) {
                MessageBox.Show("The value you entered cannot be parsed, or is too small or large.", "InvalidParseException", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            this.rs.IterationsTree = output;
            LoadAdvancedSettings();
        }

        private void Menu_ram_per_test_Click(object sender, RoutedEventArgs e) {
            if (!RipperDialog.InputBox("Please enter a new value: ", "", this.rs.IterationsPerRAMTest.ToString(), out byte output)) {
                MessageBox.Show("The value you entered cannot be parsed, or is too small or large.", "InvalidParseException", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            this.rs.IterationsPerRAMTest = output;
            LoadAdvancedSettings();
        }

        private void Menu_ram_foldermatrix_Click(object sender, RoutedEventArgs e) {
            if (!RipperDialog.InputBox("Please enter a new value: ", "", this.rs.IterationsRAMFolderMatrix.ToString(), out ulong output)) {
                MessageBox.Show("The value you entered cannot be parsed, or is too small or large.", "InvalidParseException", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            this.rs.IterationsRAMFolderMatrix = output;
            LoadAdvancedSettings();
        }

        private void Menu_ram_bulkfile_Click(object sender, RoutedEventArgs e) {
            if (!RipperDialog.InputBox("Please enter a new value: ", "", this.rs.IterationsRAMVirtualBulkFile.ToString(), out ulong output)) {
                MessageBox.Show("The value you entered cannot be parsed, or is too small or large.", "InvalidParseException", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            this.rs.IterationsRAMVirtualBulkFile = output;
            LoadAdvancedSettings();
        }

        private void Menu_ram_readwriteparse_Click(object sender, RoutedEventArgs e) {
            if (!RipperDialog.InputBox("Please enter a new value: ", "", this.rs.IterationsRAMReferenceDereference.ToString(), out ulong output)) {
                MessageBox.Show("The value you entered cannot be parsed, or is too small or large.", "InvalidParseException", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            this.rs.IterationsRAMReferenceDereference = output;
            LoadAdvancedSettings();
        }

        private void Menu_disk_per_test_Click(object sender, RoutedEventArgs e) {
            if (!RipperDialog.InputBox("Please enter a new value: ", "", this.rs.IterationsRAMReferenceDereference.ToString(), out byte output)) {
                MessageBox.Show("The value you entered cannot be parsed, or is too small or large.", "InvalidParseException", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            this.rs.IterationsRAMReferenceDereference = output;
            LoadAdvancedSettings();
        }

        private void Menu_disk_foldermatrix_Click(object sender, RoutedEventArgs e) {
            if (!RipperDialog.InputBox("Please enter a new value: ", "", this.rs.IterationsDISKFolderMatrix.ToString(), out byte output)) {
                MessageBox.Show("The value you entered cannot be parsed, or is too small or large.", "InvalidParseException", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            this.rs.IterationsDISKFolderMatrix = output;
            LoadAdvancedSettings();
        }

        private void Menu_disk_bulkfile_Click(object sender, RoutedEventArgs e) {
            if (!RipperDialog.InputBox("Please enter a new value: ", "", this.rs.IterationsDiskBulkFile.ToString(), out byte output)) {
                MessageBox.Show("The value you entered cannot be parsed, or is too small or large.", "InvalidParseException", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            this.rs.IterationsDiskBulkFile = output;
            LoadAdvancedSettings();
        }

        private void Menu_disk_readwriteparse_Click(object sender, RoutedEventArgs e) {
            if (!RipperDialog.InputBox("Please enter a new value: ", "", this.rs.IterationsDiskReadWriteParse.ToString(), out byte output)) {
                MessageBox.Show("The value you entered cannot be parsed, or is too small or large.", "InvalidParseException", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            this.rs.IterationsDiskReadWriteParse = output;
            LoadAdvancedSettings();
        }

        private void Menu_disk_ripper_Click(object sender, RoutedEventArgs e) {
            if (!RipperDialog.InputBox("Please enter a new value: ", "", this.rs.IterationsDiskRipper.ToString(), out byte output)) {
                MessageBox.Show("The value you entered cannot be parsed, or is too small or large.", "InvalidParseException", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            this.rs.IterationsDiskRipper = output;
            LoadAdvancedSettings();
        }

        #endregion

        private void MenuSendToDatabase_Click(object sender, RoutedEventArgs e) {
            DatabaseConnection db = new DatabaseConnection(DatabaseConnection.GetConnectionString());
            db.Open();

            if (this.userData.Email != "guest") {
                var range = new TextRange(this.txtResults.Document.ContentStart,
                         this.txtResults.Document.ContentEnd);

                string input = RipperDialog.InputBox("Please enter a name for the test!", "Enter a name", $"{userData.FirstName}'s Test");

                if (db.AddUserResults(this.userData.Email, range.Text,input)) {
                    MessageBox.Show($"Uploaded results to your account {this.userData.FirstName}!",
                        "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            } else {
                MessageBox.Show($"Cannot send results as a guest!",
                        "ResultsFailureException!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TabTestResults_GotFocus(object sender, RoutedEventArgs e) {
            this.txtResults.ScrollToVerticalOffset(0);
            this.txtBlkRunningTestTips.Visibility = Visibility.Hidden;
        }

        private void MenuHelp_Click(object sender, RoutedEventArgs e) {
            System.Diagnostics.Process.Start("help.html");
        }

        private void MenuAccount_Click(object sender, RoutedEventArgs e) {
            ShowTabWindow(Tab.MY_ACCOUNT);
        }

        private void SliderUserSkill_Copy_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            Slider s = (Slider)sender;

            string userType;

            switch (e.NewValue) {
                case double d when (e.NewValue >= 0 && e.NewValue <= 1): {
                    s.Value = 1.0;
                    userType = "Casual";
                    s.ToolTip = userType;
                    break;
                }

                case double d when (e.NewValue > 1 && e.NewValue <= 2): {
                    s.Value = 2.0;
                    userType = "Web surfer";
                    s.ToolTip = userType;
                    break;
                }

                case double d when (e.NewValue > 2 && e.NewValue <= 3): {
                    s.Value = 3.0;
                    userType = "High performance";
                    s.ToolTip = userType;
                    break;
                }

                case double d when (e.NewValue > 3 && e.NewValue <= 4): {
                    s.Value = 4.0;
                    userType = "Video editor";
                    s.ToolTip = userType;
                    break;
                }
            }
        }
    }
}
