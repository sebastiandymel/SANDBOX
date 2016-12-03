using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Interactivity;
using System.Windows.Controls;
using System.Windows.Input;

namespace UIToolkit
{
    /// <summary>
    /// 
    /// </summary>
    public class SelectAllTextOnFocusBehavior : Behavior<TextBox>
    {
        bool isKeyboardUsed = true;

        protected override void OnAttached()
        {
            base.OnAttached();

            this.AssociatedObject.PreviewMouseDown +=
                new MouseButtonEventHandler(AssociatedObject_PreviewMouseDown);

            this.AssociatedObject.GotKeyboardFocus +=
                new KeyboardFocusChangedEventHandler(AssociatedObject_GotKeyboardFocus);
        }

        void AssociatedObject_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.isKeyboardUsed = false;
        }

        void AssociatedObject_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (this.isKeyboardUsed)
            {
                TextBox textBox = sender as TextBox;

                if (textBox != null)
                {
                    textBox.SelectAll();
                    e.Handled = true;
                }
            }

            this.isKeyboardUsed = true;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            this.AssociatedObject.GotKeyboardFocus -= AssociatedObject_GotKeyboardFocus;
            this.AssociatedObject.PreviewMouseDown -= AssociatedObject_PreviewMouseDown;
        }
    }
}
