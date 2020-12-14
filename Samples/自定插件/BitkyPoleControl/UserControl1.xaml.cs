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

namespace BitkyPoleControl
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        private readonly Color _colorBlue = Color.FromRgb(0, 255, 200);
        //改变颜色
        private readonly Color _colorRed = Color.FromRgb(255, 0, 0);

        public UserControl1()
        {
            InitializeComponent();
        }

        public int _id { get; private set; } = -1;
        /// <summary>
        ///     根据参数初始化该控件
        /// </summary>
        /// <param name="id">输入的参数</param>
        /// 
        public void setContent(int id)
        {
            Name = "bitkyPoleControl" + id;
            labelPoleId.Content = id;
            _id = id;
        }

        public void SetValue(int num)
        {
            labelNum.Content = num;
        }

        /// <summary>
        ///     设置背景颜色，0:绿  1:红
        /// </summary>
        /// <param name="i"></param>
        public void setColor(int i)
        {
            if (i == 0)
                Background = new SolidColorBrush(_colorBlue);
            if (i == 1)
                Background = new SolidColorBrush(_colorRed);
        }
    }
}
