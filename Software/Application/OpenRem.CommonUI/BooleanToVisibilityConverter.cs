using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace OpenRem.CommonUI
{
    class BooleanToVisibilityConverter : IValueConverter
    {
        public Visibility TrueValue { get; set; }
        public Visibility FalseValue { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isBoolean = value is bool;
            if (!isBoolean)
            {
                return DependencyProperty.UnsetValue;
            }

            var booleanValue = (bool) value;


            return booleanValue ? TrueValue : FalseValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}