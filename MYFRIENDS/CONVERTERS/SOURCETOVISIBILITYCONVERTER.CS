using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace MyFriends
{
    [ValueConversion(typeof(Uri), typeof(Visibility))]
    public class SourceToVisibilityConverter : IValueConverter
    {
        #region Instance Fields
        public static SourceToVisibilityConverter Instance = new SourceToVisibilityConverter();
        #endregion
        #region IValueConverter implementation
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return (value as Uri).AbsolutePath.Equals(string.Empty) ? Visibility.Collapsed : Visibility.Visible;
            }
            catch
            {
                return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Cannot convert back");
        }
        #endregion
    }
}