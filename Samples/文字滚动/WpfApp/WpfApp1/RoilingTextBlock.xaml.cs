using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Threading;

namespace WpfApp1
{
    /// <summary>
    /// RoilingTextBlock.xaml 的交互逻辑
    /// </summary>
    public partial class RoilingTextBlock : UserControl
    {
        //是否滚动
        private bool canRoll = false;
        //每一步的偏移量
        private double rollingInterval = 1;
        //最大的偏移量
        private double offset = 6;
        private TextBlock currentTextBlock = null;
        private DispatcherTimer currentTimer = null;

        public RoilingTextBlock()
        {
            InitializeComponent();
            Loaded += RoilingTextBlock_Loaded;
        }

        void RoilingTextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            //移动控制
            canRoll = true;
            //获取文字宽度
            Size size = this.MeasureString(this.currentTextBlock.Text);
            //设置文字宽度(将文字控件宽度设置与文字内容一样宽)
            this.currentTextBlock.Width = size.Width;
            //创建定时器
            currentTimer = new System.Windows.Threading.DispatcherTimer();
            //设置定时器间隔
            currentTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            //定时器回调
            currentTimer.Tick += new EventHandler(currentTimer_Tick);
            //开始定时器
            currentTimer.Start();
        }

        public override void OnApplyTemplate()
        {
            try
            {
                base.OnApplyTemplate();
                currentTextBlock = this.GetTemplateChild("textBlock") as TextBlock;
            }
            catch (Exception ex)
            {

            }
        }

        private Size MeasureString(string candidate)
        {
            var formattedText = new FormattedText(
                candidate,
                CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                new Typeface(this.currentTextBlock.FontFamily, this.currentTextBlock.FontStyle, this.currentTextBlock.FontWeight, this.currentTextBlock.FontStretch),
                this.currentTextBlock.FontSize,
                Brushes.Black,
                new NumberSubstitution(), TextFormattingMode.Display);

            return new Size(formattedText.Width, formattedText.Height);
        }

        void currentTimer_Tick(object sender, EventArgs e)
        {
            //获取字符长度
            Size size = this.MeasureString(this.currentTextBlock.Text);
            //文字宽度
            int textWidth = (int)(size.Width - offset);
            //控件宽度
            int controlWidth = (int)this.Width;
            //控件验证
            if (this.currentTextBlock != null && canRoll)
            {
                //验证显示内容是否在显示范围内
                if (Left < -textWidth)
                {
                    //重置坐标
                    Left = controlWidth;
                }
                else
                {
                    Left -= rollingInterval;
                }
            }
        }

        #region Dependency Properties
        public static DependencyProperty TextProperty =
           DependencyProperty.Register("Text", typeof(string), typeof(RoilingTextBlock),
           new PropertyMetadata(""));

        public static DependencyProperty FontSizeProperty =
            DependencyProperty.Register("FontSize", typeof(double), typeof(RoilingTextBlock),
            new PropertyMetadata(14D));

        public static readonly DependencyProperty ForegroundProperty =
           DependencyProperty.Register("Foreground", typeof(Brush), typeof(RoilingTextBlock), new FrameworkPropertyMetadata(Brushes.Green));

        public static DependencyProperty LeftProperty =
           DependencyProperty.Register("Left", typeof(double), typeof(RoilingTextBlock), new PropertyMetadata(0D));

        public static DependencyProperty TopProperty =
           DependencyProperty.Register("Top", typeof(double), typeof(RoilingTextBlock), new PropertyMetadata(0D));

        #endregion

        #region Public Variables
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        public Brush Foreground
        {
            get { return (Brush)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }

        public double Left
        {
            get { return (double)GetValue(LeftProperty); }
            set { SetValue(LeftProperty, value); }
        }

        public double Top
        {
            get { return (double)GetValue(TopProperty); }
            set { SetValue(TopProperty, value); }
        }
        #endregion
    }
}