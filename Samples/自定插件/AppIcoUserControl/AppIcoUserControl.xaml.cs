using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace AppIcoUserControl
{
    public enum AppStatusEnum
    {
        //暂无加速
        STOP=0,
        //加速中
        RUNNING=1
    }

    public class AppMangerMainHandlerArgs : System.EventArgs
    {
        public AppIcoUserControl instance { set; get; }
    }

    /// <summary>
    /// AppIcoUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class AppIcoUserControl : UserControl
    {
        //AppId
        private string strAppId = "";
        //App图标
        private string strAppName = "";
        //App执行文件路径
        private string strAppExePath = "";
        //App管理主程序Handler
        public delegate void AppMangerMainHandler(object sender, AppMangerMainHandlerArgs Event);
        public event AppMangerMainHandler OnDelete;

        //当前状态
        private AppStatusEnum nAppState = AppStatusEnum.STOP;

        public AppIcoUserControl()
        {
            InitializeComponent();
        }

        //初始化配置
        public void initConfig(string strAppId, string strAppName, string appExePath, ImageSource appIco, AppMangerMainHandler handler, AppStatusEnum state = AppStatusEnum.STOP)
        {
            this.strAppId = strAppId;
            this.strAppName = strAppName;
            this.strAppExePath = appExePath;
            this.nAppState = state;
            this.OnDelete = handler;

            //更新App名称
            this.itemAppName.Text = strAppName;
            //更新App图标
            this.itemAppIco.Source = appIco;
            //状态验证
            if (state == AppStatusEnum.RUNNING)
            {
                //隐藏启动按钮
                this.btStart.Visibility = Visibility.Hidden;
                //显示加速状态
                this.speedState.Visibility = Visibility.Visible;
            }
        }

        //删除按钮
        private void delete_Click(object sender, RoutedEventArgs e)
        {
            OnDelete?.Invoke(this, new AppMangerMainHandlerArgs() { instance=this });
        }

        //启动按钮
        private void start_Click(object sender, RoutedEventArgs e)
        {

            string strPathExe = this.strAppExePath;
            Process process = new System.Diagnostics.Process();
            process.StartInfo.FileName = strPathExe;
            process.StartInfo.Arguments = null;//-s -t 可以用来关机、开机或重启
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = false;  //true
            process.StartInfo.RedirectStandardOutput = false;  //true
            process.StartInfo.RedirectStandardError = false;
            process.StartInfo.CreateNoWindow = false;
            process.Start();//启动
        }
    }
}
