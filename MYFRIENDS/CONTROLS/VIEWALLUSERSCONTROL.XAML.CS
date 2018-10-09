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
using System.Collections;
using System.Collections.Generic;

//Xceed.Wpf.DataGrid
using Xceed.Wpf.DataGrid;
using Xceed.Wpf.DataGrid.Views; 

namespace MyFriends
{
    public partial class ViewAllUsersControl
	{

        public bool doingInitializeComponent = false;
        public bool showGroupByControl = true;

        public ViewAllUsersControl()
		{
			this.InitializeComponent();
            DataBind();
        }


        public void DataBind()
        {
            dgFriends.ItemsSource = FriendsList.Instance();
        }

        public void SetExpandState(bool expanded)
        {
            dgFriends.Height += expanded ? 100 : -100;
        }

        private void DeleteSelectedRows(object sender, System.Windows.Input.KeyEventArgs e)
        {
            //获取当前的DataGridControl控件
            DataGridControl grid = sender as DataGridControl;
            //判断当前行是否处于编辑状态
            if (!grid.IsBeingEdited)
            {
                if (grid != null)
                {//判断用户是否键入了Delete键。
                    if (e.Key == System.Windows.Input.Key.Delete)
                    {//如果选择了多个行
                        if (grid.SelectedItems.Count > 0)
                        {
                            //首先返回一个当前选择项的幅本
                            ArrayList items = new ArrayList(grid.SelectedItems);
                            for (int i = items.Count - 1; i >= 0; i--)
                            {//然后从FriendList中移除指定的行
                                FriendsList.Instance().Remove(items[i] as MyFriends.Friend);
                            }
                            //如果当前具有Item，则设置当前Item
                            if (grid.Items.Count > 0)
                                grid.CurrentItem = grid.Items[0];
                        }
                    }
                }
            }
        }

        private void btnAssignNewImage_Click(object sender, RoutedEventArgs e)
        {
            Point topleft = this.PointToScreen(new Point(0, 0));
            DisplayStyle newDisplayStle = (DisplayStyle)//获取显示风格
                Application.Current.Properties["SelectedDisplayStyle"];
            double heightOffset = newDisplayStle == //如果显示风格为三维，偏移20个单位
                DisplayStyle.ThreeDimension ? 20 : 0;
            //实例化AddFriendImageWindow窗口，设置大小和位置值。
            AddFriendImageWindow addImageWindow = new AddFriendImageWindow();
            (addImageWindow as Window).Height = this.Height + heightOffset;
            (addImageWindow as Window).Width = this.Width;
            (addImageWindow as Window).Left = topleft.X;
            (addImageWindow as Window).Top = topleft.Y;
            addImageWindow.ShowDialog();//显示该对话框
            if (!string.IsNullOrEmpty(addImageWindow.SelectedImagePath))
            {//获取按钮父级的StackPanel对象
                StackPanel panel = VisualTreeHelper.
                    GetParent(sender as DependencyObject) as StackPanel;
                //从该StackPanel中获取Image对象
                Image image = panel.FindName("imgNew") as Image;
                if (image != null)
                {//设置新选择的图片文件为新选择的图像
                    image.Source = new BitmapImage(new
                        Uri(addImageWindow.SelectedImagePath));
                }
            }
        }


        private void btnTableView_Click(object sender, RoutedEventArgs e)
        {
            TableView tv = new TableView();
            tv.Theme = new LunaMetallicTheme();
            dgFriends.View = tv;

        }

        private void btnCardView_Click(object sender, RoutedEventArgs e)
        {
            CardView cv = new CardView();
            cv.Theme = new LunaMetallicTheme();
            dgFriends.View = cv;

        }

        private void btnVideoPlay_Click(object sender, RoutedEventArgs e)
        {
            StackPanel panel = VisualTreeHelper.GetParent(sender as DependencyObject) as StackPanel;
            MediaElement video = panel.FindName("videoSrc") as MediaElement;
            video.Play();
        }

        private void btnVideoStop_Click(object sender, RoutedEventArgs e)
        {
            StackPanel panel = VisualTreeHelper.GetParent(sender as DependencyObject) as StackPanel;
            MediaElement video = panel.FindName("videoSrc") as MediaElement;
            video.Stop();
        }

        private void btnAssignNewVideo_Click(object sender, RoutedEventArgs e)
        {
            CommonDialog ofd = new CommonDialog();//使用通用对话框类
            ofd.InitialDirectory = @"C:\";//初始文件夹
            ofd.Filter.Add(new FilterEntry("Avi Files", "*.avi"));
            ofd.Filter.Add(new FilterEntry("Windows Media Video", "*.wmv"));
            ofd.Filter.Add(new FilterEntry("All Files", "*.*"));
            ofd.Title = "添加一个新的视频文件";//指定标题
            ofd.DefaultExtension = "*.avi";//指定默认扩展名
            ofd.ShowOpen();//调用ShowOpen方法显示打开文件窗口
            if (!ofd.FileName.Equals(String.Empty))
            {
                string[] allowableFiles = { ".wmv", ".avi" };//允许的文件类型
                FileInfo f = new FileInfo(ofd.FileName);//实例化文件对象
                if (allowableFiles.Contains(f.Extension.ToLower()))
                {//如果文件类型合格
                    StackPanel panel = VisualTreeHelper.//找到StackPanel控件
                        GetParent(sender as DependencyObject) as StackPanel;
                    //找到StackPanel控件下的videoNew这个MediaElement元素
                    MediaElement video = panel.FindName("videoNew") as MediaElement;
                    if (video != null)//如果存在MediaElement元素
                    {
                        video.Source = null;//清空数据源
                        video.Source = new Uri(ofd.FileName);//将路径赋给MediaElement文件
                        ArrayList items = new ArrayList(dgFriends.SelectedItems);
                        (items[0] as MyFriends.Friend).VideoUrl = ofd.FileName;//保存视频文件
                        int idx = FriendsList.Instance().IndexOf(items[0] as MyFriends.Friend);
                        FriendsList.Instance()[idx].VideoUrl = ofd.FileName;//更新视频文件路径
                    }//显示提示信息
                    MessageBox.Show("成功的更新了视频文件\r\n" +
                                    "可以使用按钮对其进行控制");
                }
            }
            else
            {//显示失败的提示信息
                MessageBox.Show("选中了一个无效的文件\r\n" +
                                "文件必须为avi或者是wmv");
            }
        }

        private void btnViewVideo_Click(object sender, RoutedEventArgs e)
        {


            StackPanel panel = VisualTreeHelper.
                GetParent(sender as DependencyObject) as StackPanel;
            MediaElement video = panel.FindName("videoSrc") as MediaElement;

            string videoUrl = video.Source.AbsolutePath;

            if (!videoUrl.Equals(string.Empty))
            {
                Point topleft = this.PointToScreen(new Point(0, 0));
                DisplayStyle newDisplayStle = (DisplayStyle)
                    Application.Current.Properties["SelectedDisplayStyle"];
                double heightOffset = newDisplayStle == 
                    DisplayStyle.ThreeDimension ? 20 : 0;

                VideoViewerWindow vvw = new VideoViewerWindow();
                vvw.VideoUrl = videoUrl;
                vvw.Left = topleft.X;
                vvw.Top = topleft.Y-100;
                vvw.ShowDialog();
            }

           

        }

        private void btnMusicPlay_Click(object sender, RoutedEventArgs e)
        {
            StackPanel panel = VisualTreeHelper.GetParent(sender as DependencyObject) as StackPanel;
            MediaElement music = panel.FindName("musicSrc") as MediaElement;
            music.Play();

        }

        private void btnMusicStop_Click(object sender, RoutedEventArgs e)
        {
            StackPanel panel = VisualTreeHelper.GetParent(sender as DependencyObject) as StackPanel;
            MediaElement music = panel.FindName("musicSrc") as MediaElement;
            music.Stop();
        }

        private void btnAssignNewMusic_Click(object sender, RoutedEventArgs e)
        {
            CommonDialog ofd = new CommonDialog();
            ofd.InitialDirectory = @"C:\";
            ofd.Filter.Add(new FilterEntry("MP3 Files", "*.mp3"));
            ofd.Filter.Add(new FilterEntry("Windows Media Audio", "*.wma"));
            ofd.Filter.Add(new FilterEntry("All Files", "*.*"));
            ofd.Title = "Assign new audio file";
            ofd.DefaultExtension = "*.mp3";
            ofd.ShowOpen();
            
            if (!ofd.FileName.Equals(String.Empty))
            {
                string[] allowableFiles = { ".wma", ".mp3" };
                FileInfo f = new FileInfo(ofd.FileName);
                if (allowableFiles.Contains(f.Extension.ToLower()))
                {
                    StackPanel panel = VisualTreeHelper.GetParent(sender as DependencyObject) as StackPanel;
                    MediaElement music = panel.FindName("musicNew") as MediaElement;
                    if (music != null)
                    {
                        music.Source = null;
                        music.Source = new Uri(ofd.FileName);
                        ArrayList items = new ArrayList(dgFriends.SelectedItems);
                        (items[0] as MyFriends.Friend).MusicUrl = ofd.FileName;
                        int idx = FriendsList.Instance().IndexOf(items[0] as MyFriends.Friend);
                        FriendsList.Instance()[idx].MusicUrl = ofd.FileName;
                    }
                    MessageBox.Show("你成功的更新的音乐项m\r\n" +
                                    "可能通过按钮对其进行控制");
                }
            }
            else
            {
                MessageBox.Show("选择了一个无效的文件\r\n" +
                                "文件必须为wma或mp3");
            }
        }
	}
}