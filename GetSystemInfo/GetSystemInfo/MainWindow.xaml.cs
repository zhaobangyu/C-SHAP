using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _05_GetSystemInfo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// CPU数量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetCpuCount_Click(object sender, RoutedEventArgs e)
        {
            //获取CPU数量
            string count = ComputerUtils.GetCPU_Count();
            //弹窗显示
            MessageBox.Show(count);
        }

        /// <summary>
        /// CPU信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetCpuInfo_Click(object sender, RoutedEventArgs e)
        {
            //获取CPU信息
            Tuple<string, string> tuple = ComputerUtils.GetCPU();
            //拼接字符串
            string buffer = tuple.Item1 + "-" + tuple.Item2;
            //弹框提示
            MessageBox.Show(buffer);
        }

        /// <summary>
        /// 内存大小
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetMemory_Click(object sender, RoutedEventArgs e)
        {
            //内存大小
            string size = ComputerUtils.GetPhisicalMemory();
            //弹窗显示
            MessageBox.Show(size);
        }

        /// <summary>
        /// 硬盘大小
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetDiskSize_Click(object sender, RoutedEventArgs e)
        {
            //硬盘大小
            string size = ComputerUtils.GetDiskSize();
            //弹窗显示
            MessageBox.Show(size);
        }

        /// <summary>
        /// 电脑型号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetPcVersion_Click(object sender, RoutedEventArgs e)
        {
            //电脑型号
            string version = ComputerUtils.GetVersion();
            //弹窗显示
            MessageBox.Show(version);
        }

        /// <summary>
        /// 分辨率
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetFenbianLv_Click(object sender, RoutedEventArgs e)
        {
            //分辨率
            string buffer = ComputerUtils.GetFenbianlv();
            //弹窗显示
            MessageBox.Show(buffer);
        }

        /// <summary>
        /// 显卡,芯片,显存大小
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetVideoController_Click(object sender, RoutedEventArgs e)
        {
            //显卡,芯片,显存大小
            Tuple<string, string> tuple = ComputerUtils.GetVideoController();
            //临时变量
            string buffer = string.Empty;
            //数据验证
            if (tuple != null && tuple.Item1 != null && tuple.Item2 != null)
            {
                //拼接字符串
                buffer = tuple.Item1 + "-" + tuple.Item2;
            }
            //弹框提示
            MessageBox.Show(buffer);
        }

        /// <summary>
        /// 系统版本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetOsVersion_Click(object sender, RoutedEventArgs e)
        {
            //分辨率
            string osver = ComputerUtils.GetOS_Version();
            //弹窗显示
            MessageBox.Show(osver);
        }

        /// <summary>
        /// net版本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetNetVersion_Click(object sender, RoutedEventArgs e)
        {
            //获取net版本
            Tuple<List<string>, int> result = ComputerUtils.GetNet_Version();
            //弹窗显示
            MessageBox.Show(result.Item2.ToString());
        }

        /// <summary>
        /// 主板编号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetComuterSystemProduct_Click(object sender, RoutedEventArgs e)
        {
            //分辨率
            string uuid = ComputerUtils.GetComuterSystemProduct();
            //弹窗显示
            MessageBox.Show(uuid);
        }

        /// <summary>
        /// 获取PCGUID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetPCGuid_Click(object sender, RoutedEventArgs e)
        {
            //分辨率
            string uuid = ComputerUtils.GetUUID();
            //弹窗显示
            MessageBox.Show(uuid);
        }
    }
}
