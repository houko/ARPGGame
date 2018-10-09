using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Media.Imaging;

namespace MyFriends
{
	public partial class StoredImageControl
	{
		public StoredImageControl()
		{
			this.InitializeComponent();

			// Insert code required on object creation below this point.
		}

        private string originalFileUrl;

        public string OriginalFileUrl
        {//定义一个OriginalFileUrl属性，获取和设置图像文件
            get { return originalFileUrl; }
            set
            {
                originalFileUrl = value;
                img.Source = //为图像控件设置图像源
                    new BitmapImage(new Uri(originalFileUrl));
            }
        }

	}
}