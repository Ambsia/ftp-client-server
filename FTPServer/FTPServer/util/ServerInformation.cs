using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using FTPServer.model;
using Newtonsoft.Json;

namespace FTPServer.util
{
    public class ServerInformation
    {
        public int ServerPortAddress { get;
            set; }
        public int ServerCapacity { get; set; }
        public string CpuUsage { get; private set; }
        public string MemoryUsage { get; private set; }
        public string HddUsage { get; private set; }
        public string TotalReceivedBytes { get; private set; }
        public string TotalSentBytes { get; private set; }

        readonly PerformanceCounter _thisCpuPerformanceCounter = new PerformanceCounter("Process", "% Processor Time", "FTPServer", true);
        readonly int _thisMachinePhysicalCoreCount = new System.Management.ManagementObjectSearcher("Select * from Win32_Processor").Get().Cast<ManagementBaseObject>().Sum(item => int.Parse(item["NumberOfCores"].ToString()));
        readonly PerformanceCounter _sentBytes = new PerformanceCounter("Network Interface", "Bytes Sent/sec", (new PerformanceCounterCategory("Network Interface")).GetInstanceNames()[0]);
        readonly PerformanceCounter _receivedBytes = new PerformanceCounter("Network Interface", "Bytes Received/sec", (new PerformanceCounterCategory("Network Interface")).GetInstanceNames()[0]);
        public ServerInformation()
        {
            this.CpuUsage = "";
            this.MemoryUsage = "";
            this.TotalReceivedBytes = "0";
            this.TotalSentBytes = "0";
            this.HddUsage = "";
            Update();
        }

        public void LoadServerSettings()
        {
            try
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                using (FileStream fileStream = new FileStream("settings.dat", FileMode.Open, FileAccess.Read))
                {
                    string jsonString = (string)binaryFormatter.Deserialize(fileStream);

                    var serverInfo = JsonConvert.DeserializeObject<ServerInformation>(jsonString);
                    this.ServerPortAddress = serverInfo.ServerPortAddress;
                    this.ServerCapacity = serverInfo.ServerCapacity;
                    ConsoleBuffer.AddToStatusLog("Server settings loaded.");
                }
            }
            catch (Exception)
            {
                this.ServerPortAddress = 8080;
                this.ServerCapacity = 20;
                ConsoleBuffer.AddToStatusLog("Server settings failed to load.");
            }
          
        }

        public void SaveServerSettings()
        {
            try
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                using (FileStream fileStream = new FileStream("settings.dat", FileMode.Create, FileAccess.Write))
                {
                    binaryFormatter.Serialize(fileStream,JsonConvert.SerializeObject(this));
                }
                ConsoleBuffer.AddToStatusLog("Server settings saved.");
            }
            catch (Exception)
            {
                ConsoleBuffer.AddToStatusLog("Server settings failed to save.");
            }
        } 

        public static IPAddress GetIpAddress()
        {
            return Dns.GetHostEntry(Environment.MachineName).AddressList.FirstOrDefault(address => address.AddressFamily == AddressFamily.InterNetwork);
        }

        public void Update()
        {
            this.CpuUsage = Math.Round(this._thisCpuPerformanceCounter.NextValue() / this._thisMachinePhysicalCoreCount) + "%";
            this.MemoryUsage = Math.Round(Process.GetCurrentProcess().WorkingSet64/1048576.0) + "MB";
            this.TotalReceivedBytes = Double.Parse(this.TotalReceivedBytes.Replace("GB", "")) + Math.Round(_receivedBytes.NextValue()/ 1073741824,2) + "GB";
            this.TotalSentBytes = Double.Parse(this.TotalSentBytes.Replace("GB", "")) + Math.Round(_sentBytes.NextValue()/ 1073741824,2) + "GB";
            this.HddUsage = "Nothing";
        }
    }
}

