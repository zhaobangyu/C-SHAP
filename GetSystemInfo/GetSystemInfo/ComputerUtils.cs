using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;

namespace _05_GetSystemInfo
{
    class ComputerUtils
    {
        public enum WindowsAPIKeys
        {
            /// <summary>
            /// 名称
            /// </summary>
            Name,
            /// <summary>
            /// 显卡芯片
            /// </summary>
            VideoProcessor,
            /// <summary>
            /// 显存大小
            /// </summary>
            AdapterRAM,
            /// <summary>
            /// 分辨率宽
            /// </summary>
            ScreenWidth,
            /// <summary>
            /// 分辨率高
            /// </summary>
            ScreenHeight,
            /// <summary>
            /// 电脑型号
            /// </summary>
            Version,
            /// <summary>
            /// 硬盘容量
            /// </summary>
            Size,
            /// <summary>
            /// 内存容量
            /// </summary>
            Capacity,
            /// <summary>
            /// cpu核心数
            /// </summary>
            NumberOfCores
        }

        /// <summary>
        /// windows api 名称
        /// </summary>
        public enum WindowsAPIType
        {
            /// <summary>
            /// 内存
            /// </summary>
            Win32_PhysicalMemory,
            /// <summary>
            /// cpu
            /// </summary>
            Win32_Processor,
            /// <summary>
            /// 硬盘
            /// </summary>
            win32_DiskDrive,
            /// <summary>
            /// 电脑型号
            /// </summary>
            Win32_ComputerSystemProduct,
            /// <summary>
            /// 分辨率
            /// </summary>
            Win32_DesktopMonitor,
            /// <summary>
            /// 显卡
            /// </summary>
            Win32_VideoController,
            /// <summary>
            /// 操作系统
            /// </summary>
            Win32_OperatingSystem
        }

        /// <summary>
        /// 获取唯一标识
        /// </summary>
        /// <returns></returns>
        public static string GetUUID()
        {
            //获取唯一标识(可能失败)
            string uuid = GetComuterSystemProduct();
            //数据验证
            if (uuid == "" || uuid == null)
            {
                //组合唯一标识
                string buffer = "CPU >> " + cpuId() + "\nBIOS >> " + biosId() + "\nBASE >> " + baseId() + "Video\n" + videoId() + "\nMAC >> " + macId();
                //获取唯一标识
                uuid = GetHash(buffer);
            }

            //MD5加密


            return uuid;
        }

        /// <summary>
        /// 字符串转Hash
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static string GetHash(string s)
        {
            MD5 sec = new MD5CryptoServiceProvider();
            ASCIIEncoding enc = new ASCIIEncoding();
            byte[] bt = enc.GetBytes(s);
            return GetHexString(sec.ComputeHash(bt));
        }

        private static string GetHexString(byte[] bt)
        {
            string s = string.Empty;
            for (int i = 0; i < bt.Length; i++)
            {
                byte b = bt[i];
                int n, n1, n2;
                n = (int)b;
                n1 = n & 15;
                n2 = (n >> 4) & 15;
                if (n2 > 9)
                    s += ((char)(n2 - 10 + (int)'A')).ToString();
                else
                    s += n2.ToString();
                if (n1 > 9)
                    s += ((char)(n1 - 10 + (int)'A')).ToString();
                else
                    s += n1.ToString();
                if ((i + 1) != bt.Length && (i + 1) % 2 == 0) s += "-";
            }
            return s;
        }

        #region Original Device ID Getting Code
        //Return a hardware identifier
        private static string identifier
        (string wmiClass, string wmiProperty, string wmiMustBeTrue)
        {
            string result = "";
            System.Management.ManagementClass mc =
        new System.Management.ManagementClass(wmiClass);
            System.Management.ManagementObjectCollection moc = mc.GetInstances();
            foreach (System.Management.ManagementObject mo in moc)
            {
                if (mo[wmiMustBeTrue].ToString() == "True")
                {
                    //Only get the first one
                    if (result == "")
                    {
                        try
                        {
                            //临时变量
                            var temp = mo[wmiProperty];
                            if (temp != null)
                            {
                                result = temp.ToString();
                            }
                            break;
                        }
                        catch
                        {
                        }
                    }
                }
            }
            return result;
        }
        //Return a hardware identifier(硬件标识符)
        private static string identifier(string wmiClass, string wmiProperty)
        {
            string result = "";
            System.Management.ManagementClass mc = new System.Management.ManagementClass(wmiClass);
            System.Management.ManagementObjectCollection moc = mc.GetInstances();
            foreach (System.Management.ManagementObject mo in moc)
            {
                //Only get the first one
                if (result == "")
                {
                    try
                    {
                        //临时变量
                        var temp = mo[wmiProperty];
                        if (temp != null)
                        {
                            result = temp.ToString();
                        }
                        break;
                    }
                    catch
                    {
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 获取取CPU标识
        /// </summary>
        /// <returns></returns>
        private static string cpuId()
        {
            //Uses first CPU identifier available in order of preference
            //Don't get all identifiers, as it is very time consuming
            string retVal = identifier("Win32_Processor", "UniqueId");
            if (retVal == "") //If no UniqueID, use ProcessorID
            {
                retVal = identifier("Win32_Processor", "ProcessorId");
                if (retVal == "") //If no ProcessorId, use Name
                {
                    retVal = identifier("Win32_Processor", "Name");
                    if (retVal == "") //If no Name, use Manufacturer
                    {
                        retVal = identifier("Win32_Processor", "Manufacturer");
                    }
                    //Add clock speed for extra security
                    retVal += identifier("Win32_Processor", "MaxClockSpeed");
                }
            }
            return retVal;
        }

        /// <summary>
        /// BIOS Identifier(BISO标识)
        /// </summary>
        /// <returns></returns>
        private static string biosId()
        {
            return identifier("Win32_BIOS", "Manufacturer")
            + identifier("Win32_BIOS", "SMBIOSBIOSVersion")
            + identifier("Win32_BIOS", "IdentificationCode")
            + identifier("Win32_BIOS", "SerialNumber")
            + identifier("Win32_BIOS", "ReleaseDate")
            + identifier("Win32_BIOS", "Version");
        }

        /// <summary>
        /// Main physical hard drive ID(硬盘标识)
        /// </summary>
        /// <returns></returns>
        private static string diskId()
        {
            return identifier("Win32_DiskDrive", "Model")
            + identifier("Win32_DiskDrive", "Manufacturer")
            + identifier("Win32_DiskDrive", "Signature")
            + identifier("Win32_DiskDrive", "TotalHeads");
        }

        /// <summary>
        /// Motherboard ID 主板标识
        /// </summary>
        /// <returns></returns>
        private static string baseId()
        {
            return identifier("Win32_BaseBoard", "Model")
            + identifier("Win32_BaseBoard", "Manufacturer")
            + identifier("Win32_BaseBoard", "Name")
            + identifier("Win32_BaseBoard", "SerialNumber");
        }

        /// <summary>
        /// Primary video controller ID(视频标识)
        /// </summary>
        /// <returns></returns>
        private static string videoId()
        {
            return identifier("Win32_VideoController", "DriverVersion")
            + identifier("Win32_VideoController", "Name");
        }

        /// <summary>
        /// First enabled network card ID(网卡标识)
        /// </summary>
        /// <returns></returns>
        private static string macId()
        {
            return identifier("Win32_NetworkAdapterConfiguration",
                "MACAddress", "IPEnabled");
        }
        #endregion

        /// <summary>  
        /// 将字节转换为GB
        /// </summary>  
        /// <param name="size">字节值</param>  
        /// <param name="mod">除数，硬盘除以1000，内存除以1024</param>  
        /// <returns></returns>  
        public static string ToGB(double size, double mod)
        {
            String[] units = new String[] { "B", "KB", "MB", "GB", "TB", "PB" };
            int i = 0;
            while (size >= mod)
            {
                size /= mod;
                i++;
            }
            return Math.Round(size) + units[i];
        }

        /// <summary>
        /// 查找cpu的名称，主频, 核心数
        /// </summary>
        /// <returns></returns>
        public static Tuple<string, string> GetCPU()
        {
            Tuple<string, string> result = null;
            try
            {
                string str = string.Empty;
                ManagementClass mcCPU = new ManagementClass(WindowsAPIType.Win32_Processor.ToString());
                ManagementObjectCollection mocCPU = mcCPU.GetInstances();
                foreach (ManagementObject m in mocCPU)
                {
                    //临时变量
                    var temp = m[WindowsAPIKeys.Name.ToString()];
                    if (temp != null)
                    {
                        string name = temp.ToString();
                        string[] parts = name.Split('@');
                        result = new Tuple<string, string>(parts[0].Split('-')[0] + "处理器", parts[1]);
                        break;
                    }
                }
            }
            catch
            {

            }
            return result;
        } 


        /// <summary>
        /// 获取cpu核心数
        /// </summary>
        /// <returns></returns>
        public static string GetCPU_Count()
        {
            string str = "查询失败";
            try
            {
                int coreCount = 0;
                foreach (var item in new System.Management.ManagementObjectSearcher("Select * from " + WindowsAPIType.Win32_Processor.ToString()).Get())
                {
                    //临时变量
                    var temp = item[WindowsAPIKeys.NumberOfCores.ToString()];
                    if (temp != null)
                    {
                        coreCount += int.Parse(temp.ToString());
                    }
                }
                if (coreCount == 2)
                {
                    return "双核";
                }
                str = coreCount.ToString() + "核";
            }
            catch
            {

            }
            return str;
        }

        /// <summary>
        /// 获取系统内存大小
        /// </summary>
        /// <returns>内存大小（单位M）</returns>
        public static string GetPhisicalMemory()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher();   //用于查询一些如系统信息的管理对象 
            searcher.Query = new SelectQuery(WindowsAPIType.Win32_PhysicalMemory.ToString(), "", new string[] { WindowsAPIKeys.Capacity.ToString() });//设置查询条件 
            ManagementObjectCollection collection = searcher.Get();   //获取内存容量 
            ManagementObjectCollection.ManagementObjectEnumerator em = collection.GetEnumerator();

            long capacity = 0;
            while (em.MoveNext())
            {
                ManagementBaseObject baseObj = em.Current;
                //临时变量
                var temp = baseObj.Properties[WindowsAPIKeys.Capacity.ToString()];
                if (temp != null)
                {
                    if (temp.Value != null)
                    {
                        try
                        {
                            capacity += long.Parse(baseObj.Properties[WindowsAPIKeys.Capacity.ToString()].Value.ToString());
                        }
                        catch
                        {
                            return "查询失败";
                        }
                    }
                }
            }
            return ToGB((double)capacity, 1024.0);
        }

        /// <summary>
        /// 获取硬盘容量
        /// </summary>
        public static string GetDiskSize()
        {
            string result = string.Empty;
            StringBuilder sb = new StringBuilder();
            try
            {
                string hdId = string.Empty;
                ManagementClass hardDisk = new ManagementClass(WindowsAPIType.win32_DiskDrive.ToString());
                ManagementObjectCollection hardDiskC = hardDisk.GetInstances();
                foreach (ManagementObject m in hardDiskC)
                {
                    // 临时变量
                    var temp = m[WindowsAPIKeys.Size.ToString()];
                    if (temp != null)
                    {
                        long capacity = Convert.ToInt64(temp.ToString());
                        sb.Append(ToGB(capacity, 1000.0) + "+");
                    }
                }
                result = sb.ToString().TrimEnd('+');
            }
            catch
            {

            }
            return result;
        }
        
        /// <summary>
        /// 电脑型号
        /// </summary>
        public static string GetVersion()
        {
            string str = "查询失败";
            try
            {
                string hdId = string.Empty;
                ManagementClass hardDisk = new ManagementClass(WindowsAPIType.Win32_ComputerSystemProduct.ToString());
                ManagementObjectCollection hardDiskC = hardDisk.GetInstances();
                foreach (ManagementObject m in hardDiskC)
                {
                    // 临时变量
                    var temp = m[WindowsAPIKeys.Version.ToString()];
                    if (temp != null)
                    {
                        str = temp.ToString();
                        break;
                    }
                }
            }
            catch
            {

            }
            return str;
        }
        
        /// <summary>
        /// 获取分辨率
        /// </summary>
        public static string GetFenbianlv()
        {
            string result = "1920*1080";
            try
            {
                string hdId = string.Empty;
                ManagementClass hardDisk = new ManagementClass(WindowsAPIType.Win32_DesktopMonitor.ToString());
                ManagementObjectCollection hardDiskC = hardDisk.GetInstances();
                foreach (ManagementObject m in hardDiskC)
                {
                    // 临时变量
                    var temp1 = m[WindowsAPIKeys.ScreenWidth.ToString()];
                    var temp2 = m[WindowsAPIKeys.ScreenHeight.ToString()];
                    if (temp1 != null && temp2 != null)
                    {
                        result = temp1.ToString() + "*" + temp2.ToString();
                        break;
                    }
                }
            }
            catch
            {

            }
            return result;
        } 

        /// <summary>
        /// 显卡 芯片,显存大小
        /// </summary>
        public static Tuple<string, string> GetVideoController()
        {
            Tuple<string, string> result = null;
            try
            {

                ManagementClass hardDisk = new ManagementClass(WindowsAPIType.Win32_VideoController.ToString());
                ManagementObjectCollection hardDiskC = hardDisk.GetInstances();
                foreach (ManagementObject m in hardDiskC)
                {
                    //参数1
                    string item1 = string.Empty;
                    //获取数据
                    var temp1 = m[WindowsAPIKeys.VideoProcessor.ToString()];
                    //参数验证
                    if (temp1 != null)
                    {
                        item1 = temp1.ToString().Replace("Family", "");
                    }

                    //参数2
                    string item2 = string.Empty;
                    //获取数据
                    var temp2 = m[WindowsAPIKeys.AdapterRAM.ToString()];
                    //参数验证
                    if (temp2 != null)
                    {
                        item2 = temp2.ToString();
                    }

                    //创建返回数据
                    result = new Tuple<string, string>(item1, item2 != null ? ToGB(Convert.ToInt64(item2), 1024.0) : "0");
                    break;
                }
            }
            catch
            {

            }
            return result;
        }

        /// <summary>
        /// 操作系统版本
        /// </summary>
        public static string GetOS_Version()
        {
            string str = "unknown";
            try
            {
                string hdId = string.Empty;
                ManagementClass hardDisk = new ManagementClass(WindowsAPIType.Win32_OperatingSystem.ToString());
                ManagementObjectCollection hardDiskC = hardDisk.GetInstances();
                foreach (ManagementObject m in hardDiskC)
                {
                    //临时变量
                    var temp = m[WindowsAPIKeys.Name.ToString()];
                    if (temp != null)
                    {
                        str = temp.ToString().Split('|')[0].Replace("Microsoft", ""); 
                    }
                    break;
                }
            }
            catch
            {

            }
            return str;
        }

        /// <summary>
        /// 获取net版本
        /// 参数1：所有net版本
        /// 参数2：最高版本
        /// </summary>
        /// <returns></returns>
        public static Tuple<List<string>, int> GetNet_Version()
        {
            //最高版本
            int maxVer = 0;
            //声明对象
            Tuple<List<string>, int> result = null ;
            //列表
            List<string> list = new List<string> { };
            try
            {
                string componentsKeyName = "SOFTWARE\\Microsoft\\Active Setup\\Installed Components", friendlyName, version;

                RegistryKey componentsKey = Registry.LocalMachine.OpenSubKey(componentsKeyName);

                string[] instComps = componentsKey.GetSubKeyNames();

                foreach (string instComp in instComps)
                {
                    RegistryKey key = componentsKey.OpenSubKey(instComp);
                    friendlyName = (string)key.GetValue(null); // Gets the (Default) value from this key
                    if (friendlyName != null && friendlyName.IndexOf(".NET Framework") >= 0)
                    {
                        version = (string)key.GetValue("Version");
                        //添加版本号
                        list.Add(version);
                        //当前版本
                        int itemVersion = int.Parse(version.Split(',')[0]);
                        if (itemVersion > maxVer)
                        {
                            maxVer = itemVersion;
                        }
                    }
                }
            }
            catch
            {

            }
            //创建数据
            result = new Tuple<List<string>, int>(list, maxVer);
            //返回数据
            return result;
        }

        /// <summary>
        /// 获取主板识别码
        /// </summary>
        /// <returns></returns>
        public static string GetComuterSystemProduct()
        {
            string code = null;
            SelectQuery query = new SelectQuery("select * from Win32_ComputerSystemProduct");
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
            {
                ManagementObjectCollection data = searcher.Get();
                //循环数据
                foreach (var item in data)
                {
                    using (item) code = item["UUID"].ToString();
                }
            }
            return code;
        }
    }
}
