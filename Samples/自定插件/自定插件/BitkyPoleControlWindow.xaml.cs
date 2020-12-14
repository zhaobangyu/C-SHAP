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

namespace _03_自定插件
{
    /// <summary>
    /// BitkyPoleControlWindow.xaml 的交互逻辑
    /// </summary>
    public partial class BitkyPoleControlWindow : Window
    {
        public BitkyPoleControlWindow()
        {
            InitializeComponent();
            InitBitkyPoleShow();
        }

        /// <summary>
        ///     初始化信息显示标签界面
        /// </summary>
        private void InitBitkyPoleShow()
        {
            var controls = new List<BitkyPoleControl.UserControl1>();
            var id = 0;
            for (var i = 0; i < 8; i++)
            {
                for (var j = 0; j < 8; j++)
                {
                    var bitkyPoleControl = new BitkyPoleControl.UserControl1();
                    //在 Grid 中动态添加控件
                    GridPoleStatusShow.Children.Add(bitkyPoleControl);
                    //设定控件在 Grid 中的位置
                    Grid.SetRow(bitkyPoleControl, i);
                    Grid.SetColumn(bitkyPoleControl, j);
                    //将控件添加到集合中，方便下一步的使用
                    controls.Add(bitkyPoleControl);
                    //对控件使用自定义方法进行初始化
                    bitkyPoleControl.setContent(id);
                    id++;
                }
            }
        }
    }
}
