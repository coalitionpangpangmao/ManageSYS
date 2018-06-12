using System;
using System.Management;

namespace MSYS.Security
{
 

    public class MachineInfo
    {
        public static string GetComputerName()
        {
            try
            {
                return Environment.GetEnvironmentVariable("ComputerName");
            }
            catch
            {
                return "unknow";
            }
        }

        public static string GetCpuInfo()
        {
            string str = " ";
            try
            {
                ManagementObjectCollection instances = new ManagementClass("Win32_Processor").GetInstances();
                foreach (ManagementObject obj2 in instances)
                {
                    str = obj2.Properties["ProcessorId"].Value.ToString();
                }
            }
            catch (Exception)
            {
                str = GetMacAddress().Replace(":", "");
            }
            return str.ToString();
        }

        public static string GetHDid()
        {
            string str = " ";
            ManagementObjectCollection instances = new ManagementClass("Win32_DiskDrive").GetInstances();
            foreach (ManagementObject obj2 in instances)
            {
                str = (string)obj2.Properties["Model"].Value;
            }
            return str.ToString();
        }

        public static string GetIPAddress()
        {
            ManagementObjectCollection instances = new ManagementClass("Win32_NetworkAdapterConfiguration").GetInstances();
            foreach (ManagementObject obj2 in instances)
            {
                if ((bool)obj2["IPEnabled"])
                {
                    Array array = (Array)obj2.Properties["IpAddress"].Value;
                    return array.GetValue(0).ToString();
                }
            }
            return "";
        }

        public static string GetMacAddress()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration");
            foreach (ManagementObject obj2 in searcher.Get())
            {
                if ((bool)obj2["IPEnabled"])
                {
                    return obj2["MACAddress"].ToString();
                }
            }
            return "";
        }

        public static string GetSystemType()
        {
            try
            {
                string str = "";
                ManagementObjectCollection instances = new ManagementClass("Win32_ComputerSystem").GetInstances();
                foreach (ManagementObject obj2 in instances)
                {
                    str = obj2["SystemType"].ToString();
                }
                instances = null;
                return str;
            }
            catch
            {
                return "unknow";
            }
        }

        public static string GetTotalPhysicalMemory()
        {
            try
            {
                string str = "";
                ManagementObjectCollection instances = new ManagementClass("Win32_ComputerSystem").GetInstances();
                foreach (ManagementObject obj2 in instances)
                {
                    str = obj2["TotalPhysicalMemory"].ToString();
                }
                instances = null;
                return str;
            }
            catch
            {
                return "unknow";
            }
        }

        public static string GetUserName()
        {
            try
            {
                string str = "";
                ManagementObjectCollection instances = new ManagementClass("Win32_ComputerSystem").GetInstances();
                foreach (ManagementObject obj2 in instances)
                {
                    str = obj2["UserName"].ToString();
                }
                instances = null;
                return str;
            }
            catch
            {
                return "unknow";
            }
        }
    }
}
