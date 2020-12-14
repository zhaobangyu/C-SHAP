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

using Microsoft.WindowsAPICodePack.Dialogs;
using Microsoft.WindowsAPICodePack.Shell;

namespace _01_Hello
{
    /// <summary>
    /// MediaElement.xaml 的交互逻辑
    /// 该示例，项目需添加工程目录WindowsAPICodePack下的2个引用
    /// </summary>
    public partial class MediaElement : Window
    {
        public MediaElement()
        {
            InitializeComponent();
            SetPlayer(false);
        }

        private void SetPlayer(bool bVal)
        {
            playBtn.IsEnabled = bVal;
            stopBtn.IsEnabled = bVal;
            backBtn.IsEnabled = bVal;
            forwardBtn.IsEnabled = bVal;
        }

        private void PlayerPause()
        {
            SetPlayer(true);
            if (playBtn.Content.ToString() == "Play")
            {
                mediaElement.Play();
                playBtn.Content = "Pause";
                mediaElement.ToolTip = "Click to Pause";
            }
            else
            {
                mediaElement.Pause();
                playBtn.Content = "Play";
                mediaElement.ToolTip = "Click to Play";
            }
        }

        private void openBtn_Click(object sender, RoutedEventArgs e)
        {
            ShellContainer selectedFolder = null;
            selectedFolder = KnownFolders.SampleVideos as ShellContainer;
            CommonOpenFileDialog cfd = new CommonOpenFileDialog();
            cfd.InitialDirectoryShellContainer = selectedFolder;
            cfd.EnsureReadOnly = true;
            cfd.Filters.Add(new CommonFileDialogFilter("WMV Files", "*.wmv"));
            cfd.Filters.Add(new CommonFileDialogFilter("AVI Files", "*.avi"));
            cfd.Filters.Add(new CommonFileDialogFilter("MP3 Files", "*.mp3"));

            if (cfd.ShowDialog() == CommonFileDialogResult.OK)
            {
                mediaElement.Source = new Uri(cfd.FileName, UriKind.Relative);
                playBtn.IsEnabled = true;
            }
        }

        private void playBtn_Click(object sender, RoutedEventArgs e)
        {
            PlayerPause();
        }

        private void stopBtn_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Stop();
            playBtn.Content = "Play";
            SetPlayer(false);
            playBtn.IsEnabled = true;
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Position = mediaElement.Position - TimeSpan.FromSeconds(10);
        }

        private void forwardBtn_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Position = mediaElement.Position + TimeSpan.FromSeconds(10);
        }

        private void mediaElement_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            PlayerPause();
        }
    }
}
