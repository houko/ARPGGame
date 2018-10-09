using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace MyFriends
{
    [ValueConversion(typeof(string), typeof(bool))]
    public class HeaderToImageConverter : IValueConverter
    {
        #region 实例化对象
        public static HeaderToImageConverter Instance = 
            new HeaderToImageConverter();
        #endregion
        #region IValueConverter接口的实现
        public object Convert(object value, Type targetType, 
            object parameter, CultureInfo culture)
        {
            if ((value as string).Contains(@"\"))
            {//如果是根目录
                Uri uri = new Uri("pack://application:,,,/Images/diskdrive.png");
                BitmapImage source = new BitmapImage(uri);
                return source;//将BitmapImage的源设置为资源中的图像并返回BitmapImage
            }
            else
            {//如果是子文件夹
                Uri uri = new Uri("pack://application:,,,/Images/folder.png");
                BitmapImage source = new BitmapImage(uri);
                return source;//显示文件夹图标
            }
        }
        public object ConvertBack(object value, Type targetType, 
            object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("不能转换回去");
        }
        #endregion
    }
}