using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SEDY.PhoneUIToolkit
{
    public static class TextBoxExtension
    {
        public static readonly DependencyProperty NotifySourceOnTextChangedProperty = DependencyProperty.RegisterAttached(
            "NotifySourceOnTextChanged", typeof (bool), typeof (TextBoxExtension), new PropertyMetadata(default(bool), OnNotifySourceChanged));

        private static void OnNotifySourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                if ((bool)e.NewValue)
                {
                    var tb = d as TextBox;
                    if (tb != null)
                    {
                        tb.TextChanged += OnTextChanged;
                    }
                }
            }
        }

        private static void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var txt = (sender as TextBox);
            BindingExpression be = txt.GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();
        }
        

        public static void SetNotifySourceOnTextChanged(DependencyObject element, bool value)
        {
            element.SetValue(NotifySourceOnTextChangedProperty, value);
        }

        public static bool GetNotifySourceOnTextChanged(DependencyObject element)
        {
            return (bool) element.GetValue(NotifySourceOnTextChangedProperty);
        }
    }
}