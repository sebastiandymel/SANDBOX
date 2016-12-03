using System.Windows;
using System.Windows.Input;

namespace SEDY.PhoneUIToolkit
{
    public static class TapToCommand
    {
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached(
            "Command", typeof (ICommand), typeof (TapToCommand), new PropertyMetadata(default(ICommand), OnCommandChanged));

        private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var fe = d as FrameworkElement;
            if (fe != null)
            {
                fe.Tap += ExecuteCommand;
            }
        }

        private static void ExecuteCommand(object sender, GestureEventArgs e)
        {
            var fe = sender as FrameworkElement;
            if (fe != null)
            {
                var command = fe.GetValue(TapToCommand.CommandProperty) as ICommand;
                if (command != null && command.CanExecute(null))
                {
                    command.Execute(null);
                }
            }
        }

        public static void SetCommand(DependencyObject element, ICommand value)
        {
            element.SetValue(CommandProperty, value);
        }

        public static ICommand GetCommand(DependencyObject element)
        {
            return (ICommand) element.GetValue(CommandProperty);
        }
    }
}