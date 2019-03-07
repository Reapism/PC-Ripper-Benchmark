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

        #region Instance members, properties, and enums.

        private byte iter_per_cpu_test;

        private ulong iter_cpu_successorship;
        private ulong iter_cpu_boolean;
        private ulong iter_cpu_queue;
        private ulong iter_cpu_linkedlist;
        private ulong iter_cpu_tree;

        /// <summary>
        /// Represents how many times we run a particular test
        /// to average them.
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
            IterationsPerCPUTest = Properties.Settings.Default.iter_per_cpu_test;

            IterationsSuccessorship = Properties.Settings.Default.iter_cpu_successorship;
            IterationsBoolean = Properties.Settings.Default.iter_cpu_boolean;
            IterationsQueue = Properties.Settings.Default.iter_cpu_queue;
            IterationsLinkedList = Properties.Settings.Default.iter_cpu_linkedlist;
            IterationsTree = Properties.Settings.Default.iter_cpu_tree;

            AutoCheckForUpdates = Properties.Settings.Default.auto_updates;
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

            Properties.Settings.Default.Save();
        }

        #endregion

    }
}
