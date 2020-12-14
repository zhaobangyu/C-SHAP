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

namespace _01_Hello
{
    /// <summary>
    /// CustomizeListView.xaml 的交互逻辑
    /// </summary>
    public partial class CustomizeListView : Window
    {
        public CustomizeListView()
        {
            InitializeComponent();
        }

        private void OneMenuButtonClick(object sender, RoutedEventArgs e)
        {
            //隐藏状态下点击,显示二级菜单
            if (this.twoMenu.Visibility == Visibility.Collapsed)
            {
                this.twoMenu.Visibility = Visibility.Visible;
            }
            //显示状态下点击,隐藏二级菜单
            else
            {
                this.twoMenu.Visibility = Visibility.Collapsed;
            }
        }

        private void TwoMenuButtonClick(object sender, RoutedEventArgs e)
        {
            //隐藏状态下点击,显示二级菜单
            if (this.threeMenu.Visibility == Visibility.Collapsed)
            {
                this.threeMenu.Visibility = Visibility.Visible;
            }
            //显示状态下点击,隐藏二级菜单
            else
            {
                this.threeMenu.Visibility = Visibility.Collapsed;
            }
        }
    }
}
