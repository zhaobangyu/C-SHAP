using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FileDragDrop
{
    public partial class FileDragDrop : Form
    {
        public FileDragDrop()
        {
            InitializeComponent();
            //this.AllowDrop设置为false
            this.AllowDrop = false;
            ElevatedDragDropManager filter = new ElevatedDragDropManager();
            //开启拖放功能
            filter.EnableDragDrop(this.Handle);
            //添加消息过滤器
            Application.AddMessageFilter(filter);
            //设置拖放结束回调
            filter.ElevatedDragDrop += this.ElevatedDragDrop;
        }

        //拖放结束事件
        private void ElevatedDragDrop(System.Object sender, ElevatedDragDropArgs e)
        {
            try
            {
                if (e.HWnd == this.Handle)
                {
                    foreach (string file in e.Files)
                    {
                        //拖动文件
                        MessageBox.Show("ElevatedDragDrop File=" + (file) + "!");
                    }
                }
            }
            catch (Exception ex)
            {
                //异常信息
                MessageBox.Show("ElevatedDragDrop error=" + (ex.TargetSite?.Name) + "!");
            }
        }
    }
}
