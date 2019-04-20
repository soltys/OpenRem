using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace OpenRem.CommonUI
{
    class BooleanToWrapConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isBoolean = value is bool;
            if (!isBoolean)
            {
                return DependencyProperty.UnsetValue;
            }

            if ((bool) value)
            {
                return TextWrapping.Wrap;
            }
            return TextWrapping.NoWrap;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isBoolean = value is TextWrapping;
            if (!isBoolean)
            {
                return DependencyProperty.UnsetValue;
            }

            if ((TextWrapping)value == TextWrapping.NoWrap)
            {
                return false;
            }
            return true;
        }
    }
}
