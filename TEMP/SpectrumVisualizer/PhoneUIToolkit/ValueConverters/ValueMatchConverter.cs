using System;
using System.Windows;
using System.Windows.Data;

namespace SEDY.PhoneUIToolkit
{
    public abstract class ValueMatchConverterBase<T> : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // Reference type
            if (!typeof (T).IsValueType)
            {
                if (ReferenceEquals(value, parameter))
                {
                    return true;
                }
                if (value == null && parameter == null)
                {
                    return true;
                }
                if (value == null)
                {
                    return false;
                }
            }
            var valueTyped = (T)System.Convert.ChangeType(value, typeof(T));
            var comparedTo = (T) System.Convert.ChangeType(parameter, typeof (T));

            return comparedTo.Equals(valueTyped);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}