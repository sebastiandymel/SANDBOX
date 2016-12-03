using System.Windows;
using System.Windows.Input;

namespace SEDY.PhoneUIToolkit
{
    public static class EventToCommand
    {
        #region Event name property

        public static readonly DependencyProperty EventNameProperty = DependencyProperty.RegisterAttached(
            "EventName", typeof (string), typeof (EventToCommand), new PropertyMetadata(default(string), OnEventNameChanged));

        private static void OnEventNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var fe = d as FrameworkElement;
            if (fe != null)
            {
                var oldEventName = e.OldValue as string;
                if (!string.IsNullOrEmpty(oldEventName))
                {
                    DettachEvent(oldEventName, fe);
                }

                var eventName = e.NewValue as string;
                if (string.IsNullOrEmpty(eventName))
                {
                    return;
                }
                AttachEvent(eventName, fe);
            }
        }

        public static void SetEventName(DependencyObject element, string value)
        {
            element.SetValue(EventNameProperty, value);
        }

        public static string GetEventName(DependencyObject element)
        {
            return (string) element.GetValue(EventNameProperty);
        }
        #endregion 

        #region Command property
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached(
            "Command", typeof(ICommand), typeof(TapToCommand), new PropertyMetadata(default(ICommand), OnCommandChanged));

        public static void SetCommand(DependencyObject element, ICommand value)
        {
            element.SetValue(CommandProperty, value);
        }

        public static ICommand GetCommand(DependencyObject element)
        {
            return (ICommand)element.GetValue(CommandProperty);
        }

        private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var fe = d as FrameworkElement;
            if (fe != null)
            {
                var eventName = fe.GetValue(EventToCommand.EventNameProperty) as string;
                if (string.IsNullOrEmpty(eventName))
                {
                    return;
                }
                AttachEvent(eventName, fe);
            }
        }

        private static void AttachEvent(string eventName, FrameworkElement fe)
        {
            switch (eventName)
            {
                case "Tap":
                    fe.Tap += ExecuteCommand;
                    break;
                case "DoubleTap":
                    fe.DoubleTap += ExecuteCommand;
                    break;
                case "LostFocus":
                    fe.LostFocus += ExecuteCommand;
                    break;
            }
        }

        private static void DettachEvent(string eventName, FrameworkElement fe)
        {
            switch (eventName)
            {
                case "Tap":
                    fe.Tap -= ExecuteCommand;
                    break;
                case "DoubleTap":
                    fe.DoubleTap -= ExecuteCommand;
                    break;
                case "LostFocus":
                    fe.LostFocus -= ExecuteCommand;
                    break;
            }
        }

        #endregion

        private static void ExecuteCommand(object sender, RoutedEventArgs e)
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
        
    }
}