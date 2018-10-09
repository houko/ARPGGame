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
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

using FishEyeDemo;

namespace MyFriends
{

    public delegate void SetSelectedEventHandler(object sender, RoutedEventArgs e);


    public partial class AddFriendImageWindow
    {
        #region Instance Fields

        public int NumOfImageToFetch { get; private set; }
        public FileInfo[] Files { get; private set; }
        public int CurrentPage { get; private set; }
        private bool? UsingReflectiveImages = true;
        public string SelectedImagePath { get; private set; }


        #endregion
        #region Ctor
        public AddFriendImageWindow()
        {
            this.InitializeComponent();

        }
        #endregion


        void si_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {//鼠标单击时，保存选择的图像并关闭窗口
            if (UsingReflectiveImages.Value)
                SelectedImagePath = //获取StoredImageControl的OriginalFileUrl
                    (sender as StoredImageControl).OriginalFileUrl;
            else
                SelectedImagePath = //获取StoredImage的OriginalFileUrl
                    (sender as StoredImage).OriginalFileUrl;
            //关闭窗口
            this.Close();
        }

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            GetImages(--CurrentPage);
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            GetImages(++CurrentPage);
        }

        private void GetImages(int pageIndex)
        {//获取指定页码的图像集合，将其添加到FishEyePanel面板中
            try
            {//首先使用LINQ语法返回指定页码的的图片。NumOfImageToFetch
                //用于指定每页所放置的图片的个数。
                var imgs = (from fi in Files select fi.FullName).
                            IsImageFile().Skip(pageIndex * NumOfImageToFetch).
                            Take(NumOfImageToFetch);
                //注意：我们也可以使用另一个版本的IsImageFile，可接受一个谓词委托
                //比如IsImageFile(f => f.StartsWith("png") || f.StartsWith("jpg").Skip 
                fishPanel.Children.Clear();//清空面板
                foreach (string filename in imgs)
                {//遍历文件文件名称集合
                    if (UsingReflectiveImages.Value)
                    {//如果选中了使用返射图像
                        StoredImageControl si = new StoredImageControl
                        {//实例化StoredImageControl控件
                            OriginalFileUrl = filename,//图像路径
                            Margin = new Thickness(5)  //边框
                        };
                        si.MouseDown += new //在联鼠标事件
                            System.Windows.Input.MouseButtonEventHandler(si_MouseDown);
                        fishPanel.Children.Add(si);//添加面板
                    }
                    else
                    {//如果没有选中使用反射图像
                        StoredImage si = new StoredImage
                        {//实例化一个BitmapImage对象添加到面板中
                            Source = new BitmapImage(new Uri(filename)),
                            Width = 100,
                            OriginalFileUrl = filename,
                            Margin = new Thickness(5)
                        };
                        si.MouseDown +=//关联鼠标点击事件
                            new System.Windows.Input.MouseButtonEventHandler(si_MouseDown);
                        fishPanel.Children.Add(si);
                    }
                }
                btnPrev.IsEnabled = pageIndex > 0;//设置上一页状态
                btnNext.IsEnabled = (Files.Length - //设置下一页状诚
                    (++pageIndex * NumOfImageToFetch)) >= 10;
            }
            catch (Exception ex)
            {//异常处理方法
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //根据UsingReflectiveImages布尔变量的值决定CheckBox的显示状态
            chkUseReflectedImages.IsChecked = UsingReflectiveImages.Value;
            string SelectedImagePath = //从全局应用程序属性中获取图片路径
                Application.Current.Properties["SelectedImagePath"] as string;
            lblCurrentFolder.ToolTip = "搜录文件夹： " + SelectedImagePath + "中的图像";
            string preAmble = "文件夹 : ";
            //为了防止路径文本过长，在这里仅显示40个字符
            if (SelectedImagePath.Length + preAmble.Length > 40)
            {
                lblCurrentFolder.Content = preAmble + //包括preAmble共40个文字显示出来
                            SelectedImagePath.Substring(0, 40 - preAmble.Length) + "...";
            }
            else
            {
                lblCurrentFolder.Content = SelectedImagePath;
            }
            try
            {
                btnPrev.IsEnabled = false;//禁用上一个图片集合按钮
                NumOfImageToFetch = 10;//每次显示的图片数
                //使用LINQ to Objects查询在指定的文件夹下面是否具有图片文件
                DirectoryInfo di = new DirectoryInfo(SelectedImagePath);
                Files = di.GetFiles("*.*", SearchOption.TopDirectoryOnly); 
                int numberOfImageFiles = (from fi in Files select fi.FullName).
                            IsImageFile().Count();
                //如果有文件，则将下一个图片集合启用
                btnNext.IsEnabled = numberOfImageFiles > 0;
                if (numberOfImageFiles > 0)
                {//如果图片大于0，隐藏lblImageCount标签
                    lblImageCount.Visibility = Visibility.Hidden;
                    GetImages(CurrentPage);//调用GetImage在面板上显示图片
                    fishPanel.Visibility = Visibility.Visible;
                }
                else
                {//如果没有图片，则显示没有图片的信息
                    lblImageCount.Content = "在该文件夹内没有图像";
                    lblImageCount.Visibility = Visibility.Visible;
                    fishPanel.Visibility = Visibility.Hidden;
                }
            }
            catch
            {
            }
        }

        private void chkUseReflectedImages_Click(object sender, RoutedEventArgs e)
        {
            if (chkUseReflectedImages.IsChecked != UsingReflectiveImages)
            {
                UsingReflectiveImages = chkUseReflectedImages.IsChecked;
                GetImages(CurrentPage);
            }
        }


    }


    internal class StoredImage : Image
    {
        public string OriginalFileUrl { get; set; }
    }




}