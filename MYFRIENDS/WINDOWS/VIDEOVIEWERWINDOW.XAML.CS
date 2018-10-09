using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;

namespace MyFriends
{
	public partial class VideoViewerWindow
	{


		public VideoViewerWindow()
		{
			this.InitializeComponent();
		}

        private string videoUrl=string.Empty;
        public string VideoUrl
        {
            get { return videoUrl; }
            set
            {
                videoUrl = value;
                videoSrc.Source = new Uri(videoUrl);
            }
        }

        private void btnVideoPlay_Click(object sender, RoutedEventArgs e)
        {
            videoSrc.Play();
        }

        private void btnVideoStop_Click(object sender, RoutedEventArgs e)
        {
            videoSrc.Stop();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

	}
}