using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace OpenRem.CommonUI
{
    public class KeyToResourceConverter : IValueConverter
    {
        public string Prefix { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null || value == DependencyProperty.UnsetValue)
            {
                return DependencyProperty.UnsetValue;
            }

            var key = Prefix + value;
            if (Application.Current.Resources.Contains(key))
            {
                return Application.Current.Resources[key];
            }

            throw new InvalidOperationException($"Key not found: {key}");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}