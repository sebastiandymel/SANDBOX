using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace SEDY.PhoneUIToolkit
{
    public static class FocusExtensions
    {
        public static readonly DependencyProperty IsFocusedProperty = DependencyProperty.RegisterAttached(
            "IsFocused", typeof (bool), typeof (FocusExtensions), new PropertyMetadata(default(bool), OnIsFocusedChanged));

        private static void OnIsFocusedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // If it is a TEXTBOX also select text
            var textBox = d as TextBox;
            if (textBox != null)
            {
                textBox.Focus();
                if (!string.IsNullOrEmpty(textBox.Text))
                {
                    textBox.Select(0, textBox.Text.Length);
                }
                return;
            }

            var control = d as Control;
            if (control != null)
            {
                control.Focus();
            }
        }

        public static void SetIsFocused(DependencyObject element, bool value)
        {
            element.SetValue(IsFocusedProperty, value);
        }

        public static bool GetIsFocused(DependencyObject element)
        {
            return (bool) element.GetValue(IsFocusedProperty);
        }

        public static readonly DependencyProperty ExecuteCommandOnLostFocusProperty = DependencyProperty.RegisterAttached(
            "ExecuteCommandOnLostFocus", typeof (ICommand), typeof (FocusExtensions), new PropertyMetadata(default(ICommand), OnCommandChanged));

        private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as Control;
            var command = e.NewValue as ICommand;
            if (command != null && control != null)
            {
                control.LostFocus += (x,y) => command.Execute(null);
            }
        }

        public static void SetExecuteCommandOnLostFocus(DependencyObject element, ICommand value)
        {
            element.SetValue(ExecuteCommandOnLostFocusProperty, value);
        }

        public static ICommand GetExecuteCommandOnLostFocus(DependencyObject element)
        {
            return (ICommand) element.GetValue(ExecuteCommandOnLostFocusProperty);
        }
    }
}