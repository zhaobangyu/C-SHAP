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
using System.Windows.Shapes;
using System.Threading;

namespace _01_Hello
{
    /// <summary>
    /// ProgressBar.xaml 的交互逻辑
    /// </summary>
    public partial class ProgressBar : Window
    {
        public ProgressBar()
        {
            InitializeComponent();     
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            pb1.Minimum = 0;
            pb1.Maximum = 100;
            //pb1.IsIndeterminate = true;
            Thread th = new Thread(ThreadMethod);
            th.Start();
            bt1.IsEnabled = false;
        }

        private void ThreadMethod()
        {
            Action<int> del = delegate (int i) { pb1.Value = i; };
            for (int i = 0; i <= 100; i++)
            {
                this.Dispatcher.Invoke(del, System.Windows.Threading.DispatcherPriority.Normal, i);
                Thread.Sleep(50);
            }
        }
    }
}
