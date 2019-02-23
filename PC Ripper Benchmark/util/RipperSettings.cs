using System;
using System.Configuration;
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
    /// <para>Author: Anthony Jaghab (c), all rights reserved.</para>
    /// </summary>

    public class RipperSettings {

        #region Instance members, properties, and enums.

        /// <summary>
        /// The number of iterations for a <seealso cref="System.Collections.Generic.SortedSet{T}"/>
        /// <para>- Includes inserting, deleting, and searching.</para>
        /// </summary>       
        [DataMember(Name = "iter_tree")]
        public ulong IterationsTree { get; set; }

        /// <summary>
        /// The number of iterations for a <seealso cref="System.Collections.Generic.Queue{T}"/>
        /// <para>- Includes adding, removing, searching, and sorting a
        /// <seealso cref="System.Collections.Generic.Queue{T}"/></para>
        /// </summary>
        [DataMember(Name = "iter_queue")]
        public ulong IterationsQueue { get; set; }

        /// <summary>
        /// The number of iterations for a <seealso cref="System.Collections.Generic.LinkedList{T}"/>
        /// <para>- Includes adding, removing, searching, and sorting a 
        /// <seealso cref="System.Collections.Generic.LinkedList{T}"/>.</para>
        /// </summary>
        [DataMember(Name = "iter_linkedlist")]
        public ulong IterationsLinkedList { get; set; }

        /// <summary>
        /// The number of iterations of boolean logic.
        /// </summary>
        [DataMember(Name = "iter_boolean")]
        public ulong IterationsBoolean { get; set; }

        /// <summary>
        /// The number of iterations of successorship.
        /// </summary>
        [DataMember(Name = "iter_successorship")]
        public ulong IterationsSuccessorship { get; set; }

        /// <summary>
        /// Automatically check for updates when
        /// the program runs.
        /// </summary>
        [DataMember(Name = "auto_updates")]
        public bool AutoCheckForUpdates { get; set; }

        #endregion

        #region Constructor(s)

        public RipperSettings() {

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

        #endregion

        #region Static method(s)

        /// <summary>
        /// Saves the application settings using a
        /// <see cref="RipperSettings"/> instance.
        /// </summary>
        /// <param name="rs">A <see cref="RipperSettings"/> instance.</param>
        /// <returns></returns>

        public static void SaveApplicationSettings(ref RipperSettings rs) {
            Properties.Settings.Default.auto_updates = rs.AutoCheckForUpdates;
            Properties.Settings.Default.iter_successorship = rs.IterationsSuccessorship;
            Properties.Settings.Default.iter_boolean = rs.IterationsBoolean;
            Properties.Settings.Default.iter_queue = rs.IterationsQueue;
            Properties.Settings.Default.iter_linkedlist = rs.IterationsLinkedList;
            Properties.Settings.Default.iter_tree = rs.IterationsTree;

            Properties.Settings.Default.Save();
        }

        #endregion

    }
}
