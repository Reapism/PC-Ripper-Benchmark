using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Management;

namespace PC_Ripper_Benchmark.util {

    /// <summary>
    /// The <see cref="ComputerSpecs"/> class.
    /// <para></para>Gets computer specifications using 
    /// WMI internal classes from <see cref="System.Management"/>.
    /// <para>Author: <see langword="Anthony Jaghab"/> (c),
    /// all rights reserved.</para>
    /// </summary>

    public class ComputerSpecs {

        /// <summary>
        /// Default constructor for <see cref="ComputerSpecs"/>.
        /// </summary>

        public ComputerSpecs() {

        }

        /// <summary>
        /// Get's the username of the system.
        /// </summary>

        public string UserName => Environment.UserName;

        /// <summary>
        /// Gets the processor name.
        /// </summary>
        /// <returns></returns>

        public void GetProcessorInfo(out List<string> lst) {
            lst = new List<string>();

            ManagementClass mgt = new ManagementClass("Win32_Processor");
            ManagementObjectCollection mgtCollection = mgt.GetInstances();

            foreach (ManagementObject item in mgtCollection) {
                lst.Add("Name: " + item.Properties["Name"].Value.ToString());
                lst.Add("MaxClockSpeed: " + item.Properties["MaxClockSpeed"].Value.ToString());
                lst.Add("Architecture: " + item.Properties["Architecture"].Value.ToString());
                lst.Add("NumberOfCores: " + item.Properties["NumberOfCores"].Value.ToString());
                lst.Add("NumberOfLogicalProcessors: " + item.Properties["NumberOfLogicalProcessors"].Value.ToString());
                lst.Add("L2CacheSize: " + item.Properties["L2CacheSize"].Value.ToString());
                lst.Add("L3CacheSize: " + item.Properties["L3CacheSize"].Value.ToString());
               
            }
        }

        public void GetDiskInfo(out List<string> lst) {
            lst = new List<string>();

            ManagementClass mgt = new ManagementClass("Win32_DiskPartition");
            ManagementObjectCollection mgtCollection = mgt.GetInstances();

            foreach (ManagementObject item in mgtCollection) {
                lst.Add("Name: " + item.Properties["Name"].Value.ToString());
                lst.Add("Size: " + item.Properties["Size"].Value.ToString());
                lst.Add("Type: " + item.Properties["Type"].Value.ToString());
            }
        }

        public void GetMemoryInfo(out List<string> lst) {
            lst = new List<string>();

            ManagementClass mgt = new ManagementClass("Win32_PhysicalMemory");
            ManagementObjectCollection mgtCollection = mgt.GetInstances();

            foreach (ManagementObject item in mgtCollection) {
                lst.Add("Name: " + item.Properties["Name"].Value.ToString());
                lst.Add($"Capacity: {item.Properties["Capacity"].Value.ToString()} bytes");
                lst.Add("Speed: " + item.Properties["Speed"].Value.ToString() + "MHz");
            }
        }

        public void GetVideoCard(out List<string> lst) {
            lst = new List<string>();

            ManagementClass mgt = new ManagementClass("Win32_VideoController");
            ManagementObjectCollection mgtCollection = mgt.GetInstances();

            foreach (ManagementObject item in mgtCollection) {
                lst.Add("Name: " + item.Properties["Name"].Value.ToString());
                lst.Add("DriverVersion: " + item.Properties["DriverVersion"].Value.ToString());
            }
        }
    }
}