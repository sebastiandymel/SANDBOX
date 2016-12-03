using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace UIToolkit
{
    /// <summary>
    /// Converts
    ///     True -> Visible
    ///     False -> Collapsed
    /// </summary>
    /// <remarks>
    /// This converter is obsolete.
    /// Use BooleanToVisibilityConverter and specify the properties True=Visible and False=Collapsed.
    /// </remarks>
    public class BooleanToCollapsedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == typeof(Visibility) && value is bool)
            {
                return (bool)value ? Visibility.Visible : Visibility.Collapsed;
            }
            throw new FormatException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
