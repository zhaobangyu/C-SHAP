using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FileDragDrop
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("user32.dll")]

        private static extern int SetWindowLong(HandleRef hWnd, int nIndex, int dwNewLong);

        //App管理列表(Form)
        private AppMangerForm appMangerListBoxForm { set; get; }

        public MainWindow()
        {
            InitializeComponent();
            //窗口位置变更
            this.LocationChanged += Main_LocationChanged;
        }

        /// <summary>
        /// 窗口位置变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_LocationChanged(object sender, EventArgs e)
        {
            //临时坐标计算
            double left = this.Left + this.ActualWidth;
            //对象验证
            if (appMangerListBoxForm != null)
            {
                appMangerListBoxForm.Left = (int)left;
                appMangerListBoxForm.Top = (int)this.Top;
            }
        }

        /// <summary>
        /// sets the owner of a System.Windows.Forms.Form to a System.Windows.Window
        /// </summary>
        /// <param name="form"></param>
        /// <param name="owner"></param>
        public static void SetOwner(System.Windows.Forms.Form form, System.Windows.Window owner)
        {
            WindowInteropHelper helper = new WindowInteropHelper(owner);
            SetWindowLong(new HandleRef(form, form.Handle), -8, helper.Handle.ToInt32());
        }

        /// <summary>
        /// 隐藏拖拽窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ShowDragDropWindowClick(object sender, RoutedEventArgs e)
        {
            //获取主窗口位置
            var startLeft = this.Left + this.ActualWidth;
            var startTop = this.Top;

            //窗口验证
            if (this.appMangerListBoxForm == null)
            {
                //消息转发到WinForm
                //注:添加WindowsFormsIntegration引用
                WindowsFormsHost.EnableWindowsFormsInterop();
                //显示窗口
                this.appMangerListBoxForm = new AppMangerForm();
                this.appMangerListBoxForm.Left = (int)startLeft;
                this.appMangerListBoxForm.Top = (int)startTop;

                //设置窗口所有者
                SetOwner(this.appMangerListBoxForm, this);
            }

            //更新位置
            this.appMangerListBoxForm.Top = (int)startTop;
            this.appMangerListBoxForm.Left = (int)startLeft;

            //显示窗口
            this.appMangerListBoxForm.Show();
        }

        /// <summary>
        /// 显示拖拽窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_HideDragDropWindowClick(object sender, RoutedEventArgs e)
        {
            if (appMangerListBoxForm != null)
            {
                this.appMangerListBoxForm.Hide();
            }
        }
    }
}
