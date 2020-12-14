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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _04_窗口动画
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Console.WriteLine("begin btAnimation2 x=" + this.btAnimation2.Margin.Left);
        }

        private void ButtonAnimation_Click1(object sender, RoutedEventArgs e)
        {
            //指定长度变化的起点,终点与持续时间
            DoubleAnimation widthAnimation =
                new DoubleAnimation(70, 400, new Duration(TimeSpan.FromSeconds(3)));

            //指定高度变化的起点,终点与持续时间
            DoubleAnimation heightAnimation =
                new DoubleAnimation(70, 0, new Duration(TimeSpan.FromSeconds(3)));

            //开始动画
            //变化不是阻塞的,而是异步,所以看上去长度与高度几乎是同时变化
            //btAnimation.BeginAnimation(Button.WidthProperty, widthAnimation);
            btAnimation1.BeginAnimation(Button.HeightProperty, heightAnimation);
        }

        /// <summary>
        /// 动画完成事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void AnimationOnCompleted(object sender, EventArgs eventArgs)
        {
            btAnimation2.Margin = new Thickness(100, 0, 0, 0);  //向左移动一个像素
        }

        private void ButtonAnimation_Click2(object sender, RoutedEventArgs e)
        {
            //DoubleAnimation doubleAnimation = new DoubleAnimation();
            //doubleAnimation.Duration = new Duration
            //    (TimeSpan.FromSeconds(5));

            //TranslateTransform transform = new TranslateTransform();
            //btAnimation2.RenderTransform = transform;
            //transform.X = 0;

            //doubleAnimation.To = 450;

            //transform.BeginAnimation(TranslateTransform.XProperty, doubleAnimation);

            Storyboard sbQue = new Storyboard();
            ThicknessAnimation ta = new ThicknessAnimation();
            ta.From = new Thickness(0, 0, 0, 0);
            ta.To = new Thickness(100, 0, 0, 0);
            ta.Duration = TimeSpan.FromMilliseconds(3000);
            //隐藏动画完成事件
            ta.Completed += AnimationOnCompleted;
            sbQue.FillBehavior = FillBehavior.Stop;
            Storyboard.SetTarget(ta, btAnimation2);
            Storyboard.SetTargetProperty(ta, new PropertyPath("(Grid.Margin)"));
            sbQue.Children.Add(ta);
            sbQue.Begin();
        }

        private void ButtonOutInfo_Click(object sender, RoutedEventArgs e)
        {
            btAnimation2.Margin = new Thickness(0, 0, 0, 0);  //向左移动一个像素
            Console.WriteLine("end btAnimation2 x=" + this.btAnimation2.Margin.Left);
        }
    }
}
