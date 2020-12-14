using System;
using System.Collections.Generic;
using System.Linq;
//需引入的域
using System.Media;
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
    /// SoundPlayerAction.xaml 的交互逻辑
    /// </summary>
    ///方式1-SoundPlayer
    ///它是.NET Framework 2.0的一部分，是对Win32 PlaySound API的封装。
    ///它具有以下限制：
    ///1）仅支持.wav音频文件；
    ///2）不支持同时播放多个音频（任何新播放的操作将终止当前正在播放的）；
    ///3）无法控制声音的音量；
    ///
    ///方式2-MediaPlayer
    ///基于Windows Media Player构建起来的，因此支持Windows Media Player能播放的格式。
    ///MediaPlayer具有以下特性
    ///1）可以同时播放多个声音（创建多个MediaPlayer对象）；
    ///2）可以调整音量（Volume属性）；
    ///3）可以使用Play，Pause，Stop等方法进行控制；
    ///4）可以设置IsMuted属性为True来实现静音；
    ///5）可以用Balance属性来调整左右扬声器的平衡；
    ///6）可以通过SpeedRatio属性控制音频播放的速度；
    ///7）可以通过NaturalDuration属性得到音频的长度，通过Position属性得到当前播放进度；
    ///8）可以通过Position属性进行Seek；
    /// 
    ///方式3-MediaElement
    ///在WPF 中可以使用MediaElement为应用程序添加媒体播放控件，以完成播放音频、视频功能。由于MediaElement 属于UIElement，所以它同时也支持鼠标及键盘的操作。
    public partial class SoundPlayerAction : Window
    {
        public SoundPlayerAction()
        {
            InitializeComponent();
        }

        //简单范例
        private void Button_SoundPlayer1(object sender, RoutedEventArgs e)
        {
            SoundPlayer player = new SoundPlayer(System.Environment.CurrentDirectory + "\\res\\music.wav");
            player.Play();
        }

        //同步播放
        private void Button_SoundPlayer2(object sender, RoutedEventArgs e)
        {
            //备注:需引入using System.Media;
            SoundPlayer player = new SoundPlayer();
            //player.SoundLocation = @"d:\music\happy.mp3";
            player.SoundLocation = System.Environment.CurrentDirectory + "\\res\\finish.wav";
            player.Load();
            player.PlaySync();
        }

        //异步播放
        private void Button_SoundPlayer3(object sender, RoutedEventArgs e)
        {
            //备注:需引入using System.Media;
            SoundPlayer player = new SoundPlayer();
            player.SoundLocation = System.Environment.CurrentDirectory + "\\res\\finish.wav";
            player.LoadAsync();
            player.Play();
        }

        //异步播放
        private void Button_SoundPlayer4(object sender, RoutedEventArgs e)
        {
            SoundPlayer player = new SoundPlayer();
            player.SoundLocation = System.Environment.CurrentDirectory + "\\res\\finish.wav";
            player.Load();
            player.PlayLooping();
        }

        //播放
        MediaPlayer player = new MediaPlayer();
        private void Button_MediaPlayer1(object sender, RoutedEventArgs e)
        {
            player.Open(new Uri("res\\mp3Bg1.mp3", UriKind.Relative));
            player.Play();
        }

        private void mediaElement1_MediaOpened(object sender, RoutedEventArgs e)
        {
        }

        private void Button_MediaElement1(object sender, RoutedEventArgs e)
        {
            mediaElement1.Play();
        }

        private void Button_MediaElement2(object sender, RoutedEventArgs e)
        {
            mediaElement1.Pause();
        }

        private void Button_MediaElement3(object sender, RoutedEventArgs e)
        {
            mediaElement1.Stop();
        }

    }
}
