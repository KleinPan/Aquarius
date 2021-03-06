using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Aquarius.Converters
{
    /// <summary>
    /// true ��ʾ
    /// </summary>
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolean = (bool)value;
            return boolean ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    /// <summary>
    /// true �۵�
    /// </summary>
    public class BoolToVisibilityConverterInverse : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolean = (bool)value;
            return boolean ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
