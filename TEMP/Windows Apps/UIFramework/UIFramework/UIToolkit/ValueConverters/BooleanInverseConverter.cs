using System;
using System.Windows.Data;

namespace UIToolkit
{
    [ValueConversion(typeof(bool), typeof(bool))]
    public class BooleanInverseConverter : IValueConverter     
    {         
        #region IValueConverter Members     
     
        virtual public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {             
            try
            {
                return !(bool)value;
            }
            catch (InvalidCastException exception)
            {
                throw new InvalidOperationException(
                    string.Format(culture, "{0} The target must be a boolean.", exception.Message));
            }
        }   
       
        virtual public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return this.Convert(value, targetType, parameter, culture);
        }          

        #endregion     
    } 

}
