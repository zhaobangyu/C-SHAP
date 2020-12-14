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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFApp
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        //子窗口
        public SubWindow subWindows { set; get; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //获取主窗口位置
            var startLeft = this.Left + this.ActualWidth - 20;
            var startTop = this.Top;

            if (subWindows == null)
            {
                //创建对象
                this.subWindows = new SubWindow();
                this.subWindows.Owner = this;
                this.subWindows.Loaded += (o, s) =>
                {
                    var st = startLeft;
                    var el = startLeft;

                    //窗口移动动画说明
                    //当窗口移动动画未播放完成之前，拖动窗口修改窗口位置时,会与窗口移动动画冲突，导致位置异常
                    //不建议使用窗口移动动画
                    var winMoveFs = new DoubleAnimationUsingKeyFrames();
                    winMoveFs.KeyFrames.Add(new EasingDoubleKeyFrame(el, TimeSpan.FromSeconds(0)));

                    var winOpacityFs = new DoubleAnimationUsingKeyFrames();
                    winOpacityFs.KeyFrames.Add(new EasingDoubleKeyFrame(0.0, TimeSpan.FromSeconds(0)));
                    winOpacityFs.KeyFrames.Add(new EasingDoubleKeyFrame(1.0, TimeSpan.FromSeconds(5.0)));

                    Storyboard storyboardstoryboard = new Storyboard() { FillBehavior = FillBehavior.Stop };
                    //Storyboard.SetTarget(winMoveFs, this.subWindows);
                    Storyboard.SetTarget(winOpacityFs, this.subWindows);
                    Storyboard.SetTargetProperty(winOpacityFs, new PropertyPath(OpacityProperty.Name));
                    Storyboard.SetTargetProperty(winMoveFs, new PropertyPath(Window.LeftProperty.Name));
                    //storyboardstoryboard.Children.Add(winMoveFs);
                    storyboardstoryboard.Children.Add(winOpacityFs);
                    storyboardstoryboard.Begin();
                };

                this.LocationChanged += Main_LocationChanged;
                this.StateChanged += (o, s) =>
                {
                    //if (main.WindowState == WindowState.Minimized && !this.appMangerListBox.IsCosed)
                    //{
                    //    this.appMangerListBox.Visibility = Visibility.Collapsed;
                    //}
                    //else if (main.WindowState == WindowState.Normal && !this.appMangerListBox.IsCosed)
                    //{
                    //    this.appMangerListBox.Visibility = Visibility.Visible;
                    //}
                };
            }

            //窗口对象验证
            if (this.subWindows == null)
            {
                return;
            }

            //更新位置
            this.subWindows.Top = startTop;
            this.subWindows.Left = startLeft;

            Console.WriteLine("appMode_click top=" + this.subWindows.Top + " left=" + this.subWindows.Left);

            //显示窗口
            this.subWindows.Show();
        }

        private void Main_LocationChanged(object sender, EventArgs e)
        {
            {
                if (subWindows != null)
                {
                    try
                    {
                        double leftTemp = this.Left + this.ActualWidth - 20;
                        subWindows.Left = leftTemp;
                        subWindows.Top = this.Top;

                        Console.WriteLine(
                            "main.ActualWidth=" + this.ActualWidth +
                            " main.x=" + this.Left +
                            " app.x=" + subWindows.Left +
                            " leftTemp=" + leftTemp +
                            " IsActive=" + subWindows.IsActive +
                            " ShowActivated=" + subWindows.ShowActivated);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }
    }   
}
