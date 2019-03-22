using Microsoft.Win32;
using PC_Ripper_Benchmark.exception;
using PC_Ripper_Benchmark.function;
using PC_Ripper_Benchmark.util;
using System;
using System.IO;
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

        #endregion

        #region Constructor(s) and method(s).

        /// <summary>
        /// Default constructor for the <see cref="MainWindow"/>.
        /// </summary>

        public MainWindow() {
            InitializeComponent();

            this.rs = new RipperSettings();
            Style s = new Style();
            s.Setters.Add(new Setter(UIElement.VisibilityProperty, Visibility.Collapsed));

            this.tabComponents.ItemContainerStyle = s;
            this.tabComponents.SelectedIndex = 0;
        }

        private void ShowTabWindow(Tab theTab) {

            switch (theTab) {
                case Tab.WELCOME: {
                    this.tabComponents.SelectedIndex = (int)Tab.WELCOME;
                    return;
                }

                case Tab.CPU: {
                    this.tabComponents.SelectedIndex = (int)Tab.CPU;
                    return;
                }

                case Tab.DISK: {
                    this.tabComponents.SelectedIndex = (int)Tab.DISK;
                    return;
                }

                case Tab.RAM: {
                    this.tabComponents.SelectedIndex = (int)Tab.RAM;
                    return;
                }

                case Tab.GPU: {
                    this.tabComponents.SelectedIndex = (int)Tab.GPU;
                    return;
                }

                case Tab.SETTINGS: {
                    this.tabComponents.SelectedIndex = (int)Tab.SETTINGS;
                    return;
                }

                case Tab.RESULTS: {
                    this.tabComponents.SelectedIndex = (int)Tab.RESULTS;
                    return;
                }

                case Tab.RUNNING_TEST: {
                    this.tabComponents.SelectedIndex = (int)Tab.RUNNING_TEST;
                    return;
                }
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

        private void BtnRunTest_Click(object sender, RoutedEventArgs e) {
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
                return;
            }

            // SingleUI is going to cause problems because it is
            // threaded and it doesn't call constructor yet.
            // before we use the contents of that object.
            // we need to wait for it.

            this.txtResults.Document.Blocks.Clear();
            ShowTabWindow(Tab.RUNNING_TEST);

            try {
                results = cpu.RunCPUBenchmark(threadType);
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

        private void BtnRamRunTest_Click(object sender, RoutedEventArgs e) {
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
                return;
            }

            // SingleUI is going to cause problems because it is
            // threaded and it doesn't call constructor yet.
            // before we use the contents of that object.
            // we need to wait for it.

            this.txtResults.Document.Blocks.Clear();
            ShowTabWindow(Tab.RUNNING_TEST);

            try {
                results = ram.RunRAMBenchmark(threadType);
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

        private void MenuResultsExprtCSV_Click(object sender, RoutedEventArgs e) {
            ExportResults(ExportType.CSV);
        }

        private void MenuResultsExprtHtml_Click(object sender, RoutedEventArgs e) {
            ExportResults(ExportType.HTML);
        }

        private void MenuResultsExprtTxt_Click(object sender, RoutedEventArgs e) {
            ExportResults(ExportType.TEXTFILE);
        }

        private void MenuResultsExprtXaml_Click(object sender, RoutedEventArgs e) {
            ExportResults(ExportType.XAML);
        }

        #endregion
    }
}
