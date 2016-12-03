using System;
using System.Windows;
using System.Windows.Data;
using System.Globalization;

namespace UIToolkit
{
    /// <summary>
    /// Converts 
    ///     True -> Visible
    ///     False -> Hidden.
    /// </summary>
    /// <remarks>
    /// This converter is obsolete.
    /// Use BooleanToVisibilityConverter and specify the properties True=Visible and False=Hidden.
    /// </remarks>
    public class BooleanToHiddenConverter : IValueConverter
    {
        // This converts a false value to Hidden and a true value to Visible
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == typeof(Visibility))
            {
                if (value is bool)
                {
                    return (bool)value ? Visibility.Visible : Visibility.Hidden;
                }
            }
            throw new FormatException();
        }

        // No need to implement converting back on a one-way dataContext 
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
