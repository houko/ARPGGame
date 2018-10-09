using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.IO;


namespace MyFriends
{
    /// <summary>
    ///App.xaml的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private string xmlFilename = "MyFriends.xml";

        public App ()
	    {

	    }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //初始化全局应用程序属性
            Application.Current.Properties["SavedDetailsFileName"] = xmlFilename;//保存文件名
            Application.Current.Properties["FullXmlPath"] = //完整的XML路径
                Path.Combine(Environment.CurrentDirectory, xmlFilename);
            Application.Current.Properties["SaveFolder"] = //将要保存到的文件夹
                Environment.CurrentDirectory;
            Application.Current.Properties["SelectedDisplayStyle"] = //默认的显示风格
                DisplayStyle.GrowShrink;
            if (Directory.Exists(@"C:\WINDOWS\Web\Wallpaper"))
                //设置默认的选择图片路径的文件夹
                Application.Current.Properties["SelectedImagePath"] =@"C:\WINDOWS\Web\Wallpaper";
            else
                Application.Current.Properties["SelectedImagePath"] = @"C:\";
            //设置免费的Exceed的Grid的授权序列号，需要将这里更改为自己的序列号       
            Xceed.Wpf.DataGrid.Licenser.LicenseKey = "DGP30-E852N-G9C6E-DW5A";
            //返回一个FriendsList对象的实例
            FriendsList.Instance();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            try
            {//SaveOnExit方法将当前的所有联系人保存到XML文件中
                XMLFileOperations.SaveOnExit();
            }
            catch
            {//如果保存失败，显示失败信息
                MessageBox.Show("保存文件时失败！");
            }
        }
        
    }
}
