using System;
using System.Globalization;
using System.Windows.Data;

namespace OpenRem.CommonUI
{
    public class FrequencyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string strVal)
            {
                if (double.TryParse(strVal, out var dblVal))
                {
                    if (dblVal >= 1000)
                    {
                        return (dblVal / 1000) + " k";
                    }
                }

                return strVal;
            }

            if (value is int intVal)
            {
                if (intVal >= 1000)
                {
                    return (intVal / 1000.0) + " k";
                }
                else
                {
                    return intVal;
                }
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}