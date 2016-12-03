using System;
using System.Windows;

namespace UIToolkit
{
    /// <summary>
    /// Converts 
    ///     True -> Collapsed
    ///     False -> Visible
    /// </summary>
    /// <remarks>
    /// This converter is obsolete.
    /// Use BooleanToVisibilityConverter and specify the properties True=Collapsed and False=Visible.
    /// </remarks>
    public class BooleanInverseToVisibilityConverter : BooleanInverseConverter
    {
        override public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType == typeof(Visibility))
            {
                // "Not" the bool and choose result:
                return (bool)base.Convert(value, targetType, parameter, culture) ? Visibility.Visible : Visibility.Collapsed;
            }
            throw new FormatException();
        }

        // No need to implement converting back on a one-way dataContext 
        override public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
