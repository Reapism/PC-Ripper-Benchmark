using Microsoft.Win32;
using PC_Ripper_Benchmark.exception;
using PC_Ripper_Benchmark.function;
using PC_Ripper_Benchmark.util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using XamlAnimatedGif;
using static PC_Ripper_Benchmark.function.RipperTypes;

namespace PC_Ripper_Benchmark.window {

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// <para></para>Author(s): <see langword="Anthony Jaghab"/>, 
    /// David Hartglass, (c) all rights reserved.
    /// </summary>

    public partial class MainWindow : Window {

        #region Instance member(s), and enum(s), and properties.        

        /// <summary>
        /// The first name associated with this
        /// <see cref="MainWindow"/> instance.
        /// </summary>

        public string FirstName { get; set; }

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
            this.testToRun = Tab.WELCOME;
            this.rs = new RipperSettings();
            this.ws = new WindowSettings();

            Style s = new Style();
            s.Setters.Add(new Setter(UIElement.VisibilityProperty, Visibility.Collapsed));

            this.tabComponents.ItemContainerStyle = s;
            this.tabComponents.SelectedIndex = 0;
            this.btnDiskRunTest.IsEnabled = false;

            this.ws.NavigationMenu(this);

            GetWelcomeText();
            GetComputerSpecs();

        }

        /// <summary>
        /// Parameterized const
        /// </summary>
        /// <param name="userData"></param>

        public MainWindow(UserData userData) : base() {
            this.userData = userData;
        }

        private void GetWelcomeText() {
            this.txtblkWelcome.Text = $"Welcome {this.userData.FirstName} back.";
            this.txtBlkWelcomeText.Text = $"Welcome {this.userData.FirstName}! ";
        }

        /// <summary>
        /// Gets the computer specifications and places them inside the
        /// <see cref="txtBlkComputerSpecs"/> <see cref="TextBlock"/>.
        /// </summary>

        private void GetComputerSpecs() {
            List<string> lst = new List<string>();
            ComputerSpecs specs = new ComputerSpecs();

            this.txtBlkComputerSpecs.Text += $"System specifications for {specs.UserName}."
                + Environment.NewLine + Environment.NewLine;

            this.txtBlkComputerSpecs.Text += "Processor (CPU) specs" + Environment.NewLine;

            specs.GetProcessorInfo(out lst);
            foreach (string s in lst) { this.txtBlkComputerSpecs.Text += s + Environment.NewLine; }
            this.txtBlkComputerSpecs.Text += Environment.NewLine + "RAM specs" + Environment.NewLine;
            specs.GetMemoryInfo(out lst);
            foreach (string s in lst) { this.txtBlkComputerSpecs.Text += s + Environment.NewLine; }
            this.txtBlkComputerSpecs.Text += Environment.NewLine + "Disks (HDD/SSD) specs" + Environment.NewLine;
            specs.GetDiskInfo(out lst);
            foreach (string s in lst) { this.txtBlkComputerSpecs.Text += s + Environment.NewLine; }
            this.txtBlkComputerSpecs.Text += Environment.NewLine + "Video card (GPU) specs" + Environment.NewLine;
            specs.GetVideoCard(out lst);
            foreach (string s in lst) { this.txtBlkComputerSpecs.Text += s + Environment.NewLine; }
        }

        /// <summary>
        /// Shows a particular Tab to the user using
        /// the <see cref="Tab"/> type.
        /// </summary>
        /// <param name="theTab">The <see cref="Tab"/> type.</param>

        private void ShowTabWindow(Tab theTab) {

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

                    Random rnd = new Random();
                    Uri uri = ChoosePreloader();

                    if (uri != null) {
                        var image = new BitmapImage();
                        image.BeginInit();
                        image.UriSource = uri;
                        image.EndInit();
                        AnimationBehavior.SetSourceUri(this.imgPreloader, uri);
                        AnimationBehavior.SetRepeatBehavior(this.imgPreloader, RepeatBehavior.Forever);
                    }

                    this.tabComponents.SelectedIndex = (int)Tab.RUNNING_TEST;
                    break;
                }

                default: {
                    break;
                }
            }


        }

        /// <summary>
        /// Chooses a random preloader from the
        /// resources/preloader_urls.txt file.
        /// Returns a <see cref="Uri"/>.
        /// </summary>
        /// <returns></returns>

        private Uri ChoosePreloader() {
            string path = Path.Combine(Directory.GetCurrentDirectory(),
                "resources", "preloader_urls.txt");
            Uri uri;

            try {
                List<string> urls = new List<string>();
                StreamReader sr = new StreamReader(path);
                Random rnd = new Random();

                int index = 0;

                while (!sr.EndOfStream) {
                    urls.Add(sr.ReadLine());
                    index++;
                }

                int rndIndex = rnd.Next(index);
                uri = new Uri(urls[rndIndex]);

                return uri;
            } catch {
                return null;
            }

        }

        private void ExportResults(ExportType type) {
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

                if (saveFile.ShowDialog() != true) { throw new FileFormatException(); }

                range = new TextRange(this.txtResults.Document.ContentStart,
                    this.txtResults.Document.ContentEnd);
                fStream = new FileStream(saveFile.FileName, FileMode.Create);

                range.Save(fStream, format);
                fStream.Close();
            } catch (Exception e) {
                MessageBox.Show($"An exception has occured in generating the file. {e.ToString()}", "SaveFileException",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }

            MessageBox.Show($"The {saveFile.SafeFileName} was exported to " +
                $"{saveFile.FileName} successfully.", "Success",
                MessageBoxButton.OK, MessageBoxImage.Exclamation);

        }

        private void RunCPUTest() {
            CPUFunctions cpu = new CPUFunctions(ref this.rs);
            CPUResults results = new CPUResults(this.rs);

            ThreadType threadType;

            if (this.radCPUSingle.IsChecked == true) {
                threadType = ThreadType.Single;
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

            // SingleUI is going to cause problems because it is
            // threaded and it doesn't call constructor yet.
            // before we use the contents of that object.
            // we need to wait for it.

            this.txtResults.Document.Blocks.Clear();

            try {
                this.Dispatcher.Invoke(() => {
                    results = cpu.RunCPUBenchmark(threadType);
                });
            } catch (RipperThreadException ex) {
                MessageBox.Show($"Oh no. A Ripper thread exception occured.. {ex.ToString()}");
            }

            this.txtResults.AppendText($"Successfully ran the CPU test! Below is the " +
                $"results of the test.\n\n" +
                $"{results.Description}\n\n" +
                $"\n\n");

            this.txtBlkResults.Text = "Results for the CPU test:";

            ShowTabWindow(Tab.RESULTS);
        }

        private void RunRAMTest() {
            RamFunctions ram = new RamFunctions(ref this.rs);
            RamResults results = new RamResults(this.rs);

            ThreadType threadType;

            if (this.radRamSingle.IsChecked == true) {
                threadType = ThreadType.Single;
            } else if (this.radRamMultithread.IsChecked == true) {
                threadType = ThreadType.Multithreaded;
            } else {
                MessageBox.Show("Please select a type of test you'd like to perform.",
                    "RipperUnknownTestException", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Dispatcher.Invoke(() => {
                    ShowTabWindow(Tab.RAM);
                });
                return;
            }

            // SingleUI is going to cause problems because it is
            // threaded and it doesn't call constructor yet.
            // before we use the contents of that object.
            // we need to wait for it.

            this.txtResults.Document.Blocks.Clear();

            try {
                this.Dispatcher.Invoke(() => {
                    results = ram.RunRAMBenchmark(threadType);
                });

            } catch (RipperThreadException ex) {
                MessageBox.Show($"Oh no. A Ripper thread exception occured.. {ex.ToString()}");
            }

            this.txtResults.AppendText($"Successfully ran the RAM test! Below is the " +
                $"results of the test.\n\n" +
                $"{results.Description}\n\n" +
                $"\n\n");

            this.txtBlkResults.Text = "Results for the RAM test:";

            ShowTabWindow(Tab.RESULTS);
        }

        private void RunDISKTest() {
            DiskFunctions disk = new DiskFunctions(ref this.rs) {
                WorkingDir = this.workingDir
            };
            DiskResults results = new DiskResults(this.rs);

            ThreadType threadType;

            if (this.radDiskSingle.IsChecked == true) {
                threadType = ThreadType.Single;
            } else if (this.radDiskMultithread.IsChecked == true) {
                threadType = ThreadType.Multithreaded;
            } else {
                MessageBox.Show("Please select a type of test you'd like to perform.",
                    "RipperUnknownTestException", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Dispatcher.Invoke(() => {
                    ShowTabWindow(Tab.DISK);
                });
                return;
            }

            // SingleUI is going to cause problems because it is
            // threaded and it doesn't call constructor yet.
            // before we use the contents of that object.
            // we need to wait for it.

            this.txtResults.Document.Blocks.Clear();

            try {
                this.Dispatcher.Invoke(() => {
                    results = disk.RunDiskBenchmark(threadType);
                });
            } catch (RipperThreadException ex) {
                MessageBox.Show($"Oh no. A Ripper thread exception occured.. {ex.ToString()}");
            }

            this.txtResults.AppendText($"Successfully ran the DISK test! Below is the " +
                $"results of the test.\n\n" +
                $"{results.Description}\n\n" +
                $"\n\n");

            this.txtBlkResults.Text = "Results for the DISK test:";

            ShowTabWindow(Tab.RESULTS);
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
            this.txtBlkWorkingDir.Text = $"Working Directory Path: {path}";
            this.txtBlkWorkingDir.Foreground = Brushes.DarkOliveGreen;
        }

        /// <summary>
        /// Updates the color and text of the fields
        /// that show the directory to be invalid.
        /// </summary>
        /// <param name="path">The path to display
        /// to the user.</param>

        private void InvalidDirectory(string path) {
            this.btnDiskRunTest.IsEnabled = false;
            this.txtBlkWorkingDir.Text = $"Invalid path: {path}";
            this.txtBlkWorkingDir.Foreground = Brushes.LightSalmon;
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

        private void BtnSettings_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e) {

        }

        private void BtnSettings_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e) {

        }

        private void BtnSettings_Click(object sender, RoutedEventArgs e) {
            ShowTabWindow(Tab.SETTINGS);
        }

        private void MenuResultsExprtTxt_Click(object sender, RoutedEventArgs e) {
            ExportResults(ExportType.TEXTFILE);
        }

        private void MenuResultsExprtXaml_Click(object sender, RoutedEventArgs e) {
            ExportResults(ExportType.XAML);
        }

        private void BtnRunTheTest_Click(object sender, RoutedEventArgs e) {
            RunTest();
        }

        private void BtnBrowseWorkingDir_Click(object sender, RoutedEventArgs e) {
            // if path is good.
            var disk = new DiskFunctions(ref this.rs);

            if (disk.SetWorkingDirectory(out string path)) {

                // promp again for verification.
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

        #endregion                
    }
}
