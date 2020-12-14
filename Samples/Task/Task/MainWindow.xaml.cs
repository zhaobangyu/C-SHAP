using System;
using System.Collections.Generic;
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
using System.Windows.Threading;
using System.IO;
using System.Threading.Tasks;

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool isStop = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 开始按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartButtonClick(object sender, RoutedEventArgs e)
        {
            isStop = false;
            //获取线路数据
            Task task = Task.Factory.StartNew(() =>
            {
                while (isStop == false)
                {
                    //更新线路状态
                    Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
                    {
                        this.timeText.Text = DateTime.Now.ToString();
                        if (isStop)
                        {
                            this.timeText.Text = "00:00:00";
                        }
                    })); 
                }
            });
            //定义超时时间
            task.Wait(2000);
        }

        /// <summary>
        /// 停止按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StopButtonClick(object sender, RoutedEventArgs e)
        {
            isStop = true;
            
        }
    }
}
