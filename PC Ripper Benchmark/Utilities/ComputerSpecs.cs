using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Management;

namespace PC_Ripper_Benchmark.Utilities {

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
        /// Gets the computer name from your system.
        /// </summary>

        public string MachineName => Environment.MachineName;

        /// <summary>
        /// Represents the clock speed for the loaded processor.
        /// </summary>

        public string CPUClockSpeed { get; set; }

        /// <summary>
        /// Represents the clock speed for the loaded memory module.
        /// </summary>
        
        public string RAMClockSpeed { get; set; }

        /// <summary>
        /// Gets the CPU specifications and outputs it
        /// in a <see cref="List{T}"/>.
        /// </summary>
        /// <param name="lst">A <see cref="List{T}"/> that 
        /// stores the CPU specifications.</param>

        public void GetProcessorInfo(out List<string> lst) {
            lst = new List<string>();

            ManagementClass mgt = new ManagementClass("Win32_Processor");
            var mgtCollection = mgt.GetInstances();

            foreach (ManagementObject item in mgtCollection) {
                lst.Add($"Name: {item.Properties["Name"].Value.ToString()}");
                lst.Add($"Max clock speed: {item.Properties["MaxClockSpeed"].Value.ToString()} Mhz");
                lst.Add($"Number of cores: {item.Properties["NumberOfCores"].Value.ToString()}");
                lst.Add($"Number of logical processors: {item.Properties["NumberOfLogicalProcessors"].Value.ToString()}");
                lst.Add($"L2 cache size: {item.Properties["L2CacheSize"].Value.ToString()}K");
                lst.Add($"L3 cache size: {item.Properties["L3CacheSize"].Value.ToString()}K");
                if (CPUClockSpeed == null) { CPUClockSpeed = item.Properties["MaxClockSpeed"].Value.ToString(); }
            }
        }

        /// <summary>
        /// Gets the DISK specifications and outputs it
        /// in a <see cref="List{T}"/>.
        /// </summary>
        /// <param name="lst">A <see cref="List{T}"/> that
        /// stores the DISK specifications.</param>

        public void GetDiskInfo(out List<string> lst) {
            lst = new List<string>();

            ManagementClass mgt = new ManagementClass("Win32_DiskDrive");
            var mgtCollection = mgt.GetInstances();
            ulong size =0;
            foreach (ManagementObject item in mgtCollection) {
                lst.Add("Name: " + item.Properties["Model"].Value.ToString());
                size = (ulong)item.Properties["Size"].Value;
                size = size / (1024 * 1024 * 1024);
                lst.Add($"Total space: {size.ToString("n0")} GB");
            }
        }

        /// <summary>
        /// Gets the RAM specifications and outputs it
        /// in a <see cref="List{T}"/>.
        /// </summary>
        /// <param name="lst">A <see cref="List{T}"/> that
        /// stores the RAM specifications.</param>

        public void GetMemoryInfo(out List<string> lst) {
            lst = new List<string>();

            ManagementClass mgt = new ManagementClass("Win32_PhysicalMemory");
            ManagementObjectCollection mgtCollection = mgt.GetInstances();

            ulong capacity = 0;

            foreach (ManagementObject item in mgtCollection) {

                if (item.Properties["Manufacturer"].Value.ToString() == "Unknown") {
                    lst.Add("Manufacturer: Unknown RAM Manufactuer");
                } else {
                    lst.Add("Manufacturer: " + item.Properties["Manufacturer"].Value.ToString());
                }

                capacity += (ulong)item.Properties["Capacity"].Value;
                lst.Add("Speed: " + item.Properties["Speed"].Value.ToString() + "MHz");
                if (RAMClockSpeed == null) { RAMClockSpeed = item.Properties["Speed"].Value.ToString(); }
            }

            lst.Add($"Total capacity: ~{capacity / (1024 * 1024 * 1024)} GB");
            ulong capacityInMB = capacity / (1024 * 1024);
            lst.Add($"Total capacity more accurately: {capacityInMB.ToString("n0")} MB");
        }

        /// <summary>
        /// Gets the GPU specifications and outputs it
        /// in a <see cref="List{T}"/>.
        /// </summary>
        /// <param name="lst">A <see cref="List{T}"/> that stores the GPU specifications.</param>

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