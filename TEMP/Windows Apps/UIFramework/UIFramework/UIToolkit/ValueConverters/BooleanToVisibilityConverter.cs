using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace UIToolkit
{
    /// <summary>
    /// Converts a Boolean value to one of two user-specifiable Visibility values.
    /// The default is to convert <c>true</c> to Visible and <c>false</c> to Collapsed.
    /// </summary>
    public class BooleanToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Visibility setting to use if the value is <c>true</c>.
        /// Defaults to <c>Visibility.Visible</c>.
        /// </summary>
        public Visibility True { get { return _true; } set { _true = value; } }

        /// <summary>
        /// Visibility setting to use if the value is <c>false</c>.
        /// Defaults to <c>Visibility.Collapsed</c>.
        /// </summary>
        public Visibility False { get { return _false; } set { _false = value; } }

        #region private fields

        private Visibility _true = Visibility.Visible;
        private Visibility _false = Visibility.Collapsed;

        #endregion private fields

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == typeof(Visibility) && value is bool)
            {
                return (bool)value ? True : False;
            }
            throw new FormatException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
