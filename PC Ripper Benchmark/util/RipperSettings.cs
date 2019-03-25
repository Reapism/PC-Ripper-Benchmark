using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace PC_Ripper_Benchmark.util {

    /// <summary>
    /// The <see cref="RipperSettings"/> class.
    /// <para></para>
    /// Contains all application settings and 
    /// test settings for all the component
    /// benchmarking tests.
    /// <para>Author: <see langword="Anthony Jaghab"/> (c),
    /// all rights reserved.</para>
    /// </summary>

    public class RipperSettings {

        #region Instance members (fields).

        private byte iter_per_cpu_test;

        private ulong iter_cpu_successorship;
        private ulong iter_cpu_boolean;
        private ulong iter_cpu_queue;
        private ulong iter_cpu_linkedlist;
        private ulong iter_cpu_tree;

        private byte iter_per_ram_test;

        private ulong iter_ram_foldermatrix;
        private ulong iter_ram_referencedereference;    
        private ulong iter_ram_virtualbulkfile;

        private byte iter_per_disk_test;

        private ulong iter_disk_foldermatrix;
        private ulong iter_disk_bulkfile;
        private ulong iter_disk_readwriteparse;
        private ulong iter_disk_diskripper;

        #endregion

        #region Properties for fields.


        [DataMember(Name = "iter_per_disk_test")]
        public byte IterationsPerDiskTest {
            get => this.iter_per_disk_test;
            set {
                if (value > 0 && value <= byte.MaxValue)
                    this.iter_per_disk_test = value;
            }
        }


        [DataMember(Name = "iter_disk_diskripper")]
        public ulong IterationsDiskRipper {
            get => this.iter_disk_diskripper;
            set {
                if (value > 0 && value <= ulong.MaxValue)
                    this.iter_disk_diskripper = value;
            }
        }


        [DataMember(Name = "iter_disk_readwrite")]
        public ulong IterationsDiskReadWriteParse {
            get => this.iter_disk_readwriteparse;
            set {
                if (value > 0 && value <= ulong.MaxValue)
                    this.iter_disk_readwriteparse = value;
            }
        }


        [DataMember(Name = "iter_disk_bulkfile")]
        public ulong IterationsDiskBulkFile {
            get => this.iter_disk_bulkfile;
            set {
                if (value > 0 && value <= ulong.MaxValue)
                    this.iter_disk_bulkfile = value;
            }
        }

        /// <summary>
        /// The number of iterations for a creating (N) 
        /// <see cref="RipperFolder"/>(s) and placing (N/2)
        /// <see cref="RipperFile"/>(s) in them on disk.
        /// <para>- Includes writing data into each 
        /// <see cref="RipperFile"/> and reading physical
        /// <see cref="RipperFolder"/> as its directory.</para>
        /// </summary>  
        [DataMember(Name = "iter_disk_foldermatrix")]
        public ulong IterationsDISKFolderMatrix {
            get => this.iter_disk_foldermatrix;
            set {
                if (value > 0 && value <= ulong.MaxValue)
                    this.iter_disk_foldermatrix = value;
            }
        }

        /// <summary>
        /// Represents how many times we run a particular test
        /// for each ram test to average them.
        /// </summary>
        /// <returns></returns>
        [DataMember(Name = "iter_per_ram_test")]
        public byte IterationsPerRAMTest {
            get => this.iter_per_ram_test;
            set {
                if (value > 0 && value <= byte.MaxValue)
                    this.iter_per_ram_test = value;
            }
        }

        /// <summary>
        /// The number of iterations for a creating (N) 
        /// <see cref="RipperFolder"/>(s) and placing (N/2)
        /// <see cref="RipperFile"/>(s) in them.
        /// <para>- Includes writing data into each 
        /// <see cref="RipperFile"/> and reading a virtual
        /// <see cref="RipperFolder"/> as its directory.</para>
        /// </summary>  
        [DataMember(Name = "iter_ram_foldermatrix")]
        public ulong IterationsRAMFolderMatrix {
            get => this.iter_ram_foldermatrix;
            set {
                if (value > 0 && value <= ulong.MaxValue)
                    this.iter_ram_foldermatrix = value;
            }
        }

        /// <summary>
        /// The number of iterations for referencing
        /// and dereferencing <see langword="objects"/> in memory quickly.
        /// </summary>
        /// <returns></returns>
        [DataMember(Name = "iter_ram_referencedereference")]
        public ulong IterationsRAMReferenceDereference {
            get => this.iter_ram_referencedereference;
            set {
                if (value > 0 && value <= ulong.MaxValue)
                    this.iter_ram_referencedereference = value;
            }
        }

        /// <summary>
        /// The number of iterations for creating N virtual
        /// <see cref="RipperFile"/>(s) in memory
        /// and placing random numbers in them.
        /// <para>- Includes writing data to each 
        /// <see cref="RipperFile"/> and reading that data.</para>
        /// </summary>
        /// <returns></returns>
        [DataMember(Name = "iter_ram_virtualbulkfile")]
        public ulong IterationsRAMVirtualBulkFile {
            get => this.iter_ram_virtualbulkfile;
            set {
                if (value > 0 && value <= ulong.MaxValue)
                    this.iter_ram_virtualbulkfile = value;
            }
        }

        /// <summary>
        /// Represents how many times we run a particular test
        /// for each cpu test to average them.
        /// </summary>
        /// <returns></returns>
        [DataMember(Name = "iter_per_cpu_test")]
        public byte IterationsPerCPUTest {
            get => this.iter_per_cpu_test;
            set {
                if (value > 0 && value <= byte.MaxValue)
                    this.iter_per_cpu_test = value;
            }
        }

        /// <summary>
        /// The number of iterations for a <seealso cref="System.Collections.Generic.SortedSet{T}"/>
        /// <para>- Includes inserting, deleting, and searching.</para>
        /// </summary>       
        [DataMember(Name = "iter_cpu_tree")]
        public ulong IterationsTree {
            get => this.iter_cpu_tree;
            set {
                if (value > 0 && value <= ulong.MaxValue)
                    this.iter_cpu_tree = value;
            }
        }

        /// <summary>
        /// The number of iterations for a <seealso cref="System.Collections.Generic.Queue{T}"/>
        /// <para>- Includes adding, removing, searching, and sorting a
        /// <seealso cref="System.Collections.Generic.Queue{T}"/></para>
        /// </summary>
        [DataMember(Name = "iter_cpu_queue")]
        public ulong IterationsQueue {
            get => this.iter_cpu_queue;
            set {
                if (value > 0 && value <= ulong.MaxValue)
                    this.iter_cpu_queue = value;
            }
        }

        /// <summary>
        /// The number of iterations for a <seealso cref="System.Collections.Generic.LinkedList{T}"/>
        /// <para>- Includes adding, removing, searching, and sorting a 
        /// <seealso cref="System.Collections.Generic.LinkedList{T}"/>.</para>
        /// </summary>
        [DataMember(Name = "iter_cpu_linkedlist")]
        public ulong IterationsLinkedList {
            get => this.iter_cpu_linkedlist;
            set {
                if (value > 0 && value <= ulong.MaxValue)
                    this.iter_cpu_linkedlist = value;
            }
        }

        /// <summary>
        /// The number of iterations of boolean logic.
        /// </summary>
        [DataMember(Name = "iter_cpu_boolean")]
        public ulong IterationsBoolean {
            get => this.iter_cpu_boolean;
            set {
                if (value > 0 && value <= ulong.MaxValue)
                    this.iter_cpu_boolean = value;
            }
        }

        /// <summary>
        /// The number of iterations of successorship.
        /// </summary>
        [DataMember(Name = "iter_cpu_successorship")]
        public ulong IterationsSuccessorship {
            get => this.iter_cpu_successorship;
            set {
                if (value > 0 && value <= ulong.MaxValue)
                    this.iter_cpu_successorship = value;
            }
        }

        /// <summary>
        /// Automatically check for updates when
        /// the program runs.
        /// </summary>
        [DataMember(Name = "auto_updates")]
        public bool AutoCheckForUpdates { get; set; }

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Default constructor for <see cref="RipperSettings"/>.
        /// <para>Loads the application settings into this instance.</para>
        /// </summary>

        public RipperSettings() {
            LoadSettings();
        }

        /// <summary>
        /// Gets settings stored from the 
        /// <see cref="Properties.Settings.Default"/> thats
        /// stored on the machine.
        /// </summary>

        private void LoadSettings() {
            this.IterationsPerCPUTest = Properties.Settings.Default.iter_per_cpu_test;

            this.IterationsSuccessorship = Properties.Settings.Default.iter_cpu_successorship;
            this.IterationsBoolean = Properties.Settings.Default.iter_cpu_boolean;
            this.IterationsQueue = Properties.Settings.Default.iter_cpu_queue;
            this.IterationsLinkedList = Properties.Settings.Default.iter_cpu_linkedlist;
            this.IterationsTree = Properties.Settings.Default.iter_cpu_tree;

            this.IterationsPerRAMTest = Properties.Settings.Default.iter_per_ram_test;

            this.IterationsRAMFolderMatrix = Properties.Settings.Default.iter_ram_foldermatrix;
            this.IterationsRAMReferenceDereference = Properties.Settings.Default.iter_ram_referencedereference;
            this.IterationsRAMVirtualBulkFile = Properties.Settings.Default.iter_ram_virtualbulkfile;

            this.IterationsPerDiskTest = Properties.Settings.Default.iter_per_disk_test;

            this.IterationsDISKFolderMatrix = Properties.Settings.Default.iter_disk_foldermatrix;
            this.IterationsDiskBulkFile = Properties.Settings.Default.iter_disk_bulkfile;
            this.IterationsDiskReadWriteParse = Properties.Settings.Default.iter_disk_readwrite;
            this.IterationsDiskRipper = Properties.Settings.Default.iter_disk_diskripper;

            this.AutoCheckForUpdates = Properties.Settings.Default.auto_updates;
        }

        #endregion

        #region Static method(s)

        /// <summary> Serializes a <seealso cref="RipperSettings"/> class into a JSON.
        /// <para>Internally catches possible exceptions, and if found, returns false.</para>
        /// </summary>
        /// <param name="path">The path to store the JSON.</param>
        /// <param name="obj">The <seealso cref="RipperSettings"/> instance to serialize.</param>

        public static bool SerializeJSON(string path, ref RipperSettings obj) {
            StreamWriter sw;
            MemoryStream ms;
            DataContractJsonSerializer ser;
            byte[] data;
            string utf8String;

            try {
                ser = new DataContractJsonSerializer(typeof(RipperSettings));
                ms = new MemoryStream();
                ser.WriteObject(ms, obj);
                data = ms.ToArray();
                utf8String = System.Text.Encoding.UTF8.GetString(data, 0, data.Length);
                sw = new StreamWriter(path, true, System.Text.Encoding.UTF8);
                sw.Write(utf8String);
            } catch (Exception) {
                return false;
            }

            ms.Close();
            sw.Close();
            return true;
        }

        /// <summary> Deserializes a <seealso cref="RipperSettings"/> JSON into an instance.
        /// <para>Internally catches possible exceptions, and if found, returns false, and sets
        /// the out <seealso cref="RipperSettings"/> object to null.</para>
        /// </summary>
        /// <param name="path">The path to read the JSON.</param>
        /// <param name="obj">Passing a <seealso cref="RipperSettings"/> instance to serialize.</param>

        public static bool DeserializeJSON(string path, out RipperSettings obj) {
            FileStream reader;
            StreamReader sr;
            byte[] bArr;
            MemoryStream ms;
            string utf8Str;
            DataContractJsonSerializer ser;

            try {
                reader = new FileStream(path, FileMode.Open, FileAccess.Read);
                sr = new StreamReader(reader, System.Text.Encoding.UTF8);
                utf8Str = sr.ReadToEnd();
                bArr = System.Text.Encoding.Unicode.GetBytes(utf8Str);
                ms = new MemoryStream(bArr);
                ser = new DataContractJsonSerializer(typeof(RipperSettings));
                obj = (RipperSettings)ser.ReadObject(ms);
            } catch (Exception) {
                obj = null;
                return false;
            }

            reader.Close();
            sr.Close();
            ms.Close();

            return true;
        }

        /// <summary> Serializes a <seealso cref="RipperSettings"/> class into a JSON.
        /// Internally catches possible exceptions, and if found, returns false.
        /// </summary>
        /// <param name="path">The path to store the JSON.</param>
        /// <param name="obj">The <seealso cref="RipperSettings"/> instance to serialize.</param>

        public static bool SerializeXML(string path, ref RipperSettings obj) {
            StreamWriter sw;
            MemoryStream ms;
            DataContractSerializer ser;
            byte[] data;
            string utf8String;

            try {
                ser = new DataContractSerializer(typeof(RipperSettings));
                ms = new MemoryStream();
                ser.WriteObject(ms, obj);
                data = ms.ToArray();
                utf8String = System.Text.Encoding.UTF8.GetString(data, 0, data.Length);
                sw = new StreamWriter(path, true, System.Text.Encoding.UTF8);
                sw.Write(utf8String);
            } catch (Exception) {
                return false;
            }

            ms.Close();
            sw.Close();
            return true;
        }

        /// <summary> Deserializes a <seealso cref="RipperSettings"/> JSON into an instance.
        /// <para>Internally catches possible exceptions, and if found, returns false, and sets
        /// the out <seealso cref="RipperSettings"/> object to null.</para>
        /// </summary>
        /// <param name="path">The path to read the JSON.</param>
        /// <param name="obj">Passing a <seealso cref="RipperSettings"/> instance to serialize.</param>

        public static bool DeserializeXML(string path, out RipperSettings obj) {
            FileStream reader;
            StreamReader sr;
            byte[] bArr;
            MemoryStream ms;
            string utf8Str;
            DataContractSerializer ser;

            try {
                reader = new FileStream(path, FileMode.Open, FileAccess.Read);
                sr = new StreamReader(reader, System.Text.Encoding.UTF8);
                utf8Str = sr.ReadToEnd();
                bArr = System.Text.Encoding.Unicode.GetBytes(utf8Str);
                ms = new MemoryStream(bArr);
                ser = new DataContractSerializer(typeof(RipperSettings));
                obj = (RipperSettings)ser.ReadObject(ms);
            } catch (Exception) {
                obj = null;
                return false;
            }

            reader.Close();
            sr.Close();
            ms.Close();

            return true;
        }

        /// <summary>
        /// Saves the application settings using a
        /// <see cref="RipperSettings"/> instance.
        /// </summary>
        /// <param name="rs">A <see cref="RipperSettings"/> instance.</param>
        /// <returns></returns>

        public static void SaveApplicationSettings(ref RipperSettings rs) {
            Properties.Settings.Default.auto_updates = rs.AutoCheckForUpdates;

            Properties.Settings.Default.iter_cpu_successorship = rs.IterationsSuccessorship;

            Properties.Settings.Default.iter_cpu_boolean = rs.IterationsBoolean;
            Properties.Settings.Default.iter_cpu_queue = rs.IterationsQueue;
            Properties.Settings.Default.iter_cpu_linkedlist = rs.IterationsLinkedList;
            Properties.Settings.Default.iter_cpu_tree = rs.IterationsTree;

            Properties.Settings.Default.iter_per_ram_test = rs.IterationsPerRAMTest;

            Properties.Settings.Default.iter_ram_foldermatrix = rs.IterationsBoolean;
            Properties.Settings.Default.iter_ram_virtualbulkfile = rs.IterationsRAMVirtualBulkFile;
            Properties.Settings.Default.iter_ram_referencedereference = rs.IterationsRAMReferenceDereference;

            Properties.Settings.Default.iter_per_disk_test = rs.IterationsPerDiskTest;

            Properties.Settings.Default.iter_disk_foldermatrix = rs.IterationsDISKFolderMatrix;
            Properties.Settings.Default.iter_disk_bulkfile = rs.IterationsDiskBulkFile;
            Properties.Settings.Default.iter_disk_readwrite = rs.IterationsDiskReadWriteParse;
            Properties.Settings.Default.iter_disk_diskripper = rs.IterationsDiskRipper;

            Properties.Settings.Default.Save();
        }

        #endregion

    }
}
