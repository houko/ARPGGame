using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Linq;
using System.Data.Linq;
using System.Xml.Linq;
using System.Xml;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using System.Windows.Interop;

namespace MyFriends
{
	public partial class AddNewFriendControl
	{

        private FriendContent friendContent;
        private bool initialising = false;
        


        #region Routed Events

        /// <summary>
        /// FriendAddedEvent路由事件, 当用户单击添加联系人按钮时触发事件
        /// </summary>
        public static readonly RoutedEvent FriendAddedEvent = 
            EventManager.RegisterRoutedEvent(
            "FriendAddedEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler), 
            typeof(AddNewFriendControl));
        /// <summary>
        ///供外部源进行访问的事件属性
        /// </summary>
        public event RoutedEventHandler FriendAdded
        {
            add { AddHandler(FriendAddedEvent, value); }
            remove { RemoveHandler(FriendAddedEvent, value); }
        }

        #endregion


        public AddNewFriendControl()
		{
			this.InitializeComponent();
            friendContent = FriendContent.Instance();

        }


        public void ReInitialise()
        {
            friendContent = FriendContent.Instance();
            initialising = true;
            txtFriendName.Text = friendContent.FriendName;
            txtEmail.Text = friendContent.FriendEmail;
            //photo
            if (friendContent.PhotoUrl != null)
                if (!friendContent.PhotoUrl.Equals(string.Empty))
                    photoSrc.Source = new BitmapImage(new Uri(friendContent.PhotoUrl));
            //video
            if (friendContent.VideoUrl != null)
                if (!friendContent.VideoUrl.Equals(string.Empty))
                    videoSrc.Source = new Uri(friendContent.VideoUrl);
            //music
            if (friendContent.MusicUrl != null)
                if (!friendContent.MusicUrl.Equals(string.Empty))
                    musicSrc.Source = new Uri(friendContent.MusicUrl);
            initialising = false;
        }

        private void imgGrid_Drop(object sender, DragEventArgs e)
        {
            string[] fileNames = e.Data.GetData//获取拖放的文件名
                (DataFormats.FileDrop, true) as string[];
            if (fileNames.Length > 0)
            {//将拖放的文件保存到FriendContent的PhotoUrl中
                friendContent.PhotoUrl = fileNames[0];
                photoSrc.Source = new BitmapImage//将拖放的文件作为Image控件的路径显示
                    (new Uri(friendContent.PhotoUrl));
            }
            //确保己经处理了事件，那么将不会调用基类的方法处理拖动操作
            e.Handled = true;
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!initialising)
            {
                friendContent.FriendEmail = txtEmail.Text;
                friendContent.FriendName = txtFriendName.Text;
            }
        }

        private void musicGrid_Drop(object sender, DragEventArgs e)
        {
            string[] fileNames =//获取拖放操作的文件名数组 
                e.Data.GetData(DataFormats.FileDrop, true) as string[];
            string[] allowableFiles = { ".wma", ".mp3" };
            if (fileNames.Length > 0)
            {//如果文件名存在
                FileInfo f = new FileInfo(fileNames[0]);
                if (allowableFiles.Contains(f.Extension.ToLower()))
                {//判断指定的文件格式是否是所允许放置的格式
                    friendContent.MusicUrl = fileNames[0];
                    musicSrc.Source = new Uri(friendContent.MusicUrl);
                    //拖放操作后，将文件路径赋给MediaElement并设置文本和图片的显示
                    txtMusic.Visibility = Visibility.Hidden;
                    imgMusic.Visibility = Visibility.Visible;
                    MessageBox.Show("己经成功的添加了一个音频文件\r\n" +
                                    "可以使用所提供的按钮来控制音频");
                }
            }
            //确保己经处理了事件，那么将不会调用基类的方法处理拖动操作
            e.Handled = true;
        }

        private void videoGrid_Drop(object sender, DragEventArgs e)
        {
            string[] fileNames = e.Data.GetData//获取拖放的文件名数组
                (DataFormats.FileDrop, true) as string[];
            string[] allowableFiles = { ".wmv", ".avi" };
            if (fileNames.Length > 0)
            {
                FileInfo f = new FileInfo(fileNames[0]);
                //如果文件类型为wmv或avi，则将文件路径赋给MediaElement对象
                if (allowableFiles.Contains(f.Extension.ToLower()))
                {
                    friendContent.VideoUrl = fileNames[0];
                    videoSrc.Source = null;
                    videoSrc.Source = new Uri(friendContent.VideoUrl);
                    MessageBox.Show("己经成功地添加了一个视频\r\n" +
                                    "可以通过所提供的按钮来控制视频！");
                }
            }
            //确保己经处理了事件，那么将不会调用基类的方法处理拖动操作
            e.Handled = true;
        }


        private void btnChooseNewImage_Click(object sender, RoutedEventArgs e)
        {
            Point topleft = 
                this.PointToScreen(new Point(0, 0));//获取屏幕左上角相对坐标
            DisplayStyle newDisplayStle = (DisplayStyle)Application.
                Current.Properties["SelectedDisplayStyle"];//获取显示风格
            double heightOffset = newDisplayStle == 
                //如果是三维模式则高度偏移20，否则不偏移
                DisplayStyle.ThreeDimension ? 20 : 0;
            AddFriendImageWindow addImageWindow = 
                new AddFriendImageWindow();//打开选择图片窗口
            (addImageWindow as Window).Height = this.Height + heightOffset;
            (addImageWindow as Window).Width = this.Width;
            (addImageWindow as Window).Left = topleft.X;
            (addImageWindow as Window).Top = topleft.Y;
            addImageWindow.ShowDialog();//设置了其位置和大小后，显示出来
            if (!string.IsNullOrEmpty(addImageWindow.SelectedImagePath))
            {//获取选择的图片，赋给Image进行显示
                friendContent.PhotoUrl = addImageWindow.SelectedImagePath;
                photoSrc.Source = new BitmapImage(new Uri(friendContent.PhotoUrl));
            }
        }


        private bool IsEntryValid(TextBox txtBox)
        {//判断用户姓名字段是否输入为空，否则变换背景色
            txtBox.Background = string.IsNullOrEmpty(txtBox.Text) ?
                Brushes.Red : Brushes.White;
            //返回是否为空的布尔值
            return !string.IsNullOrEmpty(txtBox.Text);
        }
        private void Reset()
        {//重置控件的值，使其返回初始化的为空状态
            this.txtFriendName.Text = string.Empty;
            this.txtEmail.Text = string.Empty;
            this.photoSrc.Source = null;
            this.videoSrc.Source = null;
            this.musicSrc.Source = null;
            txtMusic.Visibility = Visibility.Visible;
            imgMusic.Visibility = Visibility.Hidden;
        }
        private bool IsEmailValid(string email)
        {
            bool isValid =false;            
            string pattern = //指定验证的正则表达式
              @"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$";
            Regex regEx = new Regex(pattern);//实例化Regex对象
            isValid = regEx.IsMatch(email);//进行验证工作
            //根据验证的结果设置电子邮件框的背景色
            txtEmail.Background = !isValid ? Brushes.Red : Brushes.White;
            //返回验证结果的布尔值
            return isValid;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string xmlFilename = (string)Application.//获取保存的文件名
                Current.Properties["SavedDetailsFileName"];
            string fullXmlPath = Path.Combine//获取完整的XML文件路径
                (Environment.CurrentDirectory, xmlFilename);
            bool allRequiredFieldsFilledIn = true;//判断所需要的字段是否填充
            allRequiredFieldsFilledIn = IsEntryValid(txtFriendName) &&
                                        IsEntryValid(txtEmail);
            //判断Eamil是否正确
            allRequiredFieldsFilledIn = IsEmailValid(txtEmail.Text);
            if (allRequiredFieldsFilledIn)//如果所填的资料都正确填入
            {
                if (File.Exists(fullXmlPath))//如果也存在文件名
                {
                    try
                    {
                        //如果当前没有XML且没有联系人在内存，追加到文件
                        if (FriendsList.Instance().Count == 0)
                        {
                            Friend friend = new Friend
                            {//使用对象初始化语法初始化并给Friend对象赋值
                                ID = Guid.NewGuid(),
                                Name = friendContent.FriendName,
                                Email = friendContent.FriendEmail,
                                PhotoUrl = friendContent.PhotoUrl,
                                VideoUrl = friendContent.VideoUrl,
                                MusicUrl = friendContent.MusicUrl
                            };
                            //调用XMLFileOperation的AppendToFile方法追加文件
                            XMLFileOperations.AppendToFile(fullXmlPath, friend);
                            FriendsList.Instance().Add(friend);//添加到联系人列表中
                            RaiseEvent(new RoutedEventArgs(FriendAddedEvent));//引发路由事件
                            friendContent.Reset();
                            this.Reset();
                            MessageBox.Show("成功保存联系人");
                        }
                        //否则只是更新内存中的联系人集合中的单个联系人
                        //在应用程序关闭时将写入到磁盘
                        else
                        {
                            FriendsList.Instance().Add(new Friend
                                        {
                                            ID = Guid.NewGuid(),
                                            Name = friendContent.FriendName,
                                            Email = friendContent.FriendEmail,
                                            PhotoUrl = friendContent.PhotoUrl,
                                            VideoUrl = friendContent.VideoUrl,
                                            MusicUrl = friendContent.MusicUrl
                                        });
                            RaiseEvent(new RoutedEventArgs(FriendAddedEvent));
                            friendContent.Reset();
                            this.Reset();
                            MessageBox.Show("成功保存联系人");
                        }
                    }
                    catch
                    {
                        MessageBox.Show("更新联系人错误");
                    }
                }
                else
                {//如果不存在XML文件名，则创建一个新的文件并写入
                    try
                    {
                        Friend friend = new Friend
                        {//初始化Friend对象并赋值
                            ID = Guid.NewGuid(),
                            Name = friendContent.FriendName,
                            Email = friendContent.FriendEmail,
                            PhotoUrl = friendContent.PhotoUrl,
                            VideoUrl = friendContent.VideoUrl,
                            MusicUrl = friendContent.MusicUrl
                        };
                        //调有CreateInitialFile写入一个新的文件
                        XMLFileOperations.CreateInitialFile(fullXmlPath, friend);
                        FriendsList.Instance().Add(friend);//添加到联系人集合
                        RaiseEvent(new RoutedEventArgs(FriendAddedEvent));//触发事件
                        friendContent.Reset();//重置对象
                        this.Reset();//重置控件
                        MessageBox.Show("成功保存联系人");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("保存联系人信息时产生错误");
                    }
                }
            }
            else
            {
                MessageBox.Show("需要填充所有的字段，或者是验证是否输入错误", 
                    "错误",
                    MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }

        private void btnVideoPlay_Click(object sender, RoutedEventArgs e)
        {
            videoSrc.Play();//播放视频
        }
        private void btnVideoStop_Click(object sender, RoutedEventArgs e)
        {
            videoSrc.Stop();//停止视频播放
        }
        private void btnMusicPlay_Click(object sender, RoutedEventArgs e)
        {
            musicSrc.Play();//播放音频
        }
        private void btnMusicStop_Click(object sender, RoutedEventArgs e)
        {
            musicSrc.Stop();//停止音频播放
        }
	}
}