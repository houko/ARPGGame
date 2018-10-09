using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Linq;
using System.Data.Linq;
using System.Xml.Linq;

namespace MyFriends
{
	public partial class OptionsWindow
    {
        #region Instance Fields
        private object dummyNode = null;
        #endregion
        #region Ctor
        public OptionsWindow()
		{
			this.InitializeComponent();
        }
        #endregion
        #region Public Properties
        public string SelectedImagePath { get; set; }
        public DisplayStyle CurrentDisplayStyle { private get; set; }
        #endregion
        #region Private Methods
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (string s in Directory.GetLogicalDrives())
            {//遍历本地硬盘的驱动器列表
                TreeViewItem item = new TreeViewItem();
                item.Header = s;//指定Header为驱动器名称
                item.Tag = s;
                item.FontWeight = FontWeights.Normal;
                item.Items.Add(dummyNode);//dummyNode是一个占位符，用于显示一个+号
                //当单击展开的+号时，触发Expanded事件
                item.Expanded += new RoutedEventHandler(folder_Expanded);
                foldersItem.Items.Add(item);//添加到集合中
            }
            (foldersItem.Items[0] as TreeViewItem).IsSelected = true;
            //初始化设置正确的显示样式单选按钮
            switch (CurrentDisplayStyle)
            {
                case DisplayStyle.ThreeDimension:
                    threeD.IsChecked = true;
                    break;
                case DisplayStyle.GrowShrink:
                    GrowAndShrink.IsChecked = true;
                    break;
            }
        }

        private void folder_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)sender;//获取TreeViewItem
            if (item.Items.Count == 1 && item.Items[0] == dummyNode)
            {//如果Item下面没有子项。
                item.Items.Clear();
                try
                {
                    foreach (string s in Directory.GetDirectories(item.Tag.ToString()))
                    {//遍历指定驱动器下的文件夹
                        TreeViewItem subitem = new TreeViewItem();
                        //将文件夹名称设为Header值
                        subitem.Header = s.Substring(s.LastIndexOf("\\") + 1);
                        subitem.Tag = s;
                        subitem.FontWeight = FontWeights.Normal;
                        subitem.Items.Add(dummyNode);//添加占位符
                        subitem.Expanded += new RoutedEventHandler(folder_Expanded);
                        item.Items.Add(subitem);
                    }
                }
                catch (Exception) { }
            }
        }

        private void foldersItem_SelectedItemChanged(object sender,
            RoutedPropertyChangedEventArgs<object> e)
        {
            TreeView tree = (TreeView)sender;//获取TreeView
            TreeViewItem temp = ((TreeViewItem)tree.SelectedItem);//获取选择项            
            if (temp == null)
                return;
            SelectedImagePath = "";
            string temp1 = "";
            string temp2 = "";
            while (true)
            {//得到选择项文本
                temp1 = temp.Header.ToString();
                if (temp1.Contains(@"\"))
                {
                    temp2 = "";
                }
                //得到选择项的完整路径
                SelectedImagePath = temp1 + temp2 + SelectedImagePath;
                if (temp.Parent.GetType().Equals(typeof(TreeView)))
                {//如果选择的是一个驱动器则退出
                    break;
                }
                temp = ((TreeViewItem)temp.Parent);//依次遍历父项并添加到路径字符中
                temp2 = @"\";
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            try
            {//使用文件夹实例化一个DirectoryInfo对象
                DirectoryInfo di = new DirectoryInfo(SelectedImagePath);
                //获取文件夹下的所有图像文件
                FileInfo[] Files = di.GetFiles("*.*", SearchOption.TopDirectoryOnly);
                if ((from fi in Files select fi.FullName).IsImageFile().Count() == 0)
                {//使用LINQ语法判断是否具有指定图像格式的图像文件，
                  //如果没有，则弹出提示消息
                    MessageBoxResult mbResult = MessageBox.Show("选中的文件夹 " + 
                        SelectedImagePath +
                                    " 并没有包含任何图像文件，因此你不能够\r\n" +
                                    "赋一个新的图像给联系人，",
                                    "确认", MessageBoxButton.YesNo, 
                                    MessageBoxImage.Question);
                    if (mbResult == MessageBoxResult.Yes)
                    {//如果用户确认，则将SelectedImagePath属性的值保存到全局属性中
                        Application.Current.Properties["SelectedImagePath"] = 
                            SelectedImagePath;
                        this.Hide();
                    }
                }
                else
                {//如果具有图像文件，则将SelectedImagePath属性的值保存到全局属性中
                    Application.Current.Properties["SelectedImagePath"] = 
                        SelectedImagePath;
                    this.Hide();
                }
            }
            catch (IOException)
            {//如果产生IO异常则提醒用户驱动器可能未就绪，选择其他驱动器。
                MessageBox.Show("请选择其他的驱动器");
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if ((sender as RadioButton).Name.Equals("GrowAndShrink"))
                Application.Current.Properties["SelectedDisplayStyle"] = DisplayStyle.GrowShrink;
            else
                Application.Current.Properties["SelectedDisplayStyle"] = DisplayStyle.ThreeDimension;
        }
        #endregion
    }
}