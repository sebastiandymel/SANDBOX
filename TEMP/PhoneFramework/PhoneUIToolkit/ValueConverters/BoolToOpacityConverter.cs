using System;
using System.Windows;
using System.Windows.Data;

namespace SEDY.PhoneUIToolkit
{
    public class BoolToOpacityConverter : IValueConverter
    {
        public double TrueValue { get; set; }
        public double FalseValue { get; set; }

        public BoolToOpacityConverter()
        {
            this.TrueValue = 1;
            this.FalseValue = 0.5;
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var val = System.Convert.ToBoolean(value);

            return val ? TrueValue : FalseValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}