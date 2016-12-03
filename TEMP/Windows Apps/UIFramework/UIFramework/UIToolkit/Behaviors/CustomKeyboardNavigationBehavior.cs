using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace UIToolkit
{
    public enum CustomKeyboardNavigationKind
    {
        Undefined,
        Left,        //Left arrow
        Right,       //Right arrow
        Up,          //Up arrow
        Down,        //Down arrow
        Tab,         //Tab
        ShiftTab,    //Shift+Tab
        CtrlTab,     //Ctrl+Tab
        CtrlShiftTab //Ctrl+Shift+Tab
    }

    /// <summary>
    /// The behavior supporting controlled moves between the controls (TextBoxes)
    /// with the appropriate change of the keyboard focus.
    /// </summary>
    public class CustomKeyboardNavigationBehavior : Behavior<TextBox>
    {
        /// <summary>
        /// A command executed when navigating between Controls.
        /// It would receive an argument (CustomKeyboardNavigationKind), defining the type of the navigation
        /// NOTE: If the command's CanExecute(kind) returns false, the key event is not considered as Handled
        /// and may be handled by other means. Use this, when there are multiple handlers able to process
        /// one of the navigation keys
        /// </summary>
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(CustomKeyboardNavigationBehavior));

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        private bool commandAttached;

        protected override void OnAttached()
        {
            base.OnAttached();
            commandAttached = (null != Command);
            if (commandAttached)
                AssociatedObject.PreviewKeyDown += OnPreviewKeyDown;
        }

        protected override void OnDetaching()
        {
            if (commandAttached)
                AssociatedObject.PreviewKeyDown -= OnPreviewKeyDown;
            base.OnDetaching();
        }

        private void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            CustomKeyboardNavigationKind kind = CustomKeyboardNavigationKind.Undefined;
            switch (e.Key)
            {
                case Key.Left:
                    if (AssociatedObject.CaretIndex == 0)
                        kind = CustomKeyboardNavigationKind.Left;
                    break;
                case Key.Right:
                    if (AssociatedObject.CaretIndex == AssociatedObject.Text.Length)
                        kind = CustomKeyboardNavigationKind.Right;
                    break;
                case Key.Up:
                    kind = CustomKeyboardNavigationKind.Up;
                    break;
                case Key.Down:
                    kind = CustomKeyboardNavigationKind.Down;
                    break;
                case Key.Tab:
                    switch (Keyboard.Modifiers)
                    {
                        case ModifierKeys.None:
                            kind = CustomKeyboardNavigationKind.Tab;
                            break;
                        case ModifierKeys.Shift:
                            kind = CustomKeyboardNavigationKind.ShiftTab;
                            break;
                        case ModifierKeys.Control:
                            kind = CustomKeyboardNavigationKind.CtrlTab;
                            break;
                        case (ModifierKeys.Control | ModifierKeys.Shift):
                            kind = CustomKeyboardNavigationKind.CtrlShiftTab;
                            break;
                    }
                    break;
            }

            if (kind != CustomKeyboardNavigationKind.Undefined)
            {
                ICommand command = Command;
                if (command.CanExecute(kind))
                {
                    command.Execute(kind);
                    //The CanExecute() call decides, if the key event would be considered as handled
                    e.Handled = true;
                }
            }
        }
    }
}
