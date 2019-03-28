using Microsoft.Win32;
using PC_Ripper_Benchmark.exception;
using PC_Ripper_Benchmark.function;
using PC_Ripper_Benchmark.util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using static PC_Ripper_Benchmark.function.RipperTypes;

namespace PC_Ripper_Benchmark.window {

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// <para></para>Author(s): <see langword="Anthony Jaghab"/>, 
    /// David Hartglass, (c) all rights reserved.
    /// </summary>

    public partial class MainWindow : Window {

        #region Instance member(s), and enum(s).        

        private RipperSettings rs;
        private WindowSettings ws;
        private Tab testToRun;
        private string workingDir;
        
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
                    this.tabComponents.SelectedIndex = (int)Tab.RUNNING_TEST;
                    break;
                }

                default: {
                    break;
                }
            }
            Random rnd = new Random();

            Uri uri = ChoosePreloader();

            if (uri != null) {
                this.browserPreloader.Source = uri;
            }

        }

        /// <summary>
        /// Chooses a random preloader from the
        /// resources/preloader_urls.txt file.
        /// Returns a <see cref="Nullable{T}"/> 
        /// <see cref="Uri"/>.
        /// </summary>
        /// <returns></returns>

        private Uri ChoosePreloader() {
            string path = Path.Combine(Directory.GetCurrentDirectory(),
                "resources","preloader_urls.txt");
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

                uri = new Uri(urls[rnd.Next(index)]);
              
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

        private void ValidDirectory(string path) {
            this.btnDiskRunTest.IsEnabled = true;
            this.txtBlkWorkingDir.Text = $"Working Directory Path: {path}";
            this.txtBlkWorkingDir.Foreground = Brushes.DarkOliveGreen;
        }

        private void InvalidDirectory(string path) {
            this.btnDiskRunTest.IsEnabled = false;
            this.txtBlkWorkingDir.Text = $"Invalid path: {path}";
            this.txtBlkWorkingDir.Foreground = Brushes.LightSalmon;
        }

        #endregion

        #region Event(s) and event handler(s).

        private void BtnTemp_Click(object sender, RoutedEventArgs e) {
            new QuestionaireWindow().Show();
        }

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
                this.workingDir = path;
                ValidDirectory(path);
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
        
        #endregion

        private void BtnMenu_Click(object sender, RoutedEventArgs e) {
            this.ws.NavigationMenu(this);
        }
    }
}
