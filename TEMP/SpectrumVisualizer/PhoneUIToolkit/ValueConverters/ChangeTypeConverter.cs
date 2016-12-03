using System;
using System.Windows;
using System.Windows.Data;

namespace SEDY.PhoneUIToolkit
{
    public class ChangeTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var param = parameter as string;
            if (string.IsNullOrEmpty(param))
            {
                return DependencyProperty.UnsetValue;
            }

            switch (param.ToUpperInvariant())
            {
                case "INT":
                {
                    return System.Convert.ToInt32(value);
                }
                default:
                {
                    return DependencyProperty.UnsetValue;
                }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}