using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace OpenRem.CommonUI
{
    public class NullOrEmptyToVisibilityConverter : IValueConverter
    {
        public Visibility NullValue { get; set; }
        public Visibility NotNullValue { get; set; }

        public NullOrEmptyToVisibilityConverter()
        {
            NullValue = Visibility.Collapsed;
            NotNullValue = Visibility.Visible;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case null:
                    return NullValue;
                case string s:
                    return string.IsNullOrEmpty(s) ? NullValue : NotNullValue;
                default:
                    return NotNullValue;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}