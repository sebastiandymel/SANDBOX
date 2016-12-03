using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace UIToolkit
{
    /// <summary>
    /// The behavior can be set on a textbox so it only allows integers as text    
    /// </summary>
    public class NumericOnlyBehavior : Behavior<TextBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            this.AssociatedObject.PreviewTextInput += 
                new TextCompositionEventHandler(AssociatedObject_PreviewTextInput);

            this.AssociatedObject.PreviewKeyDown += 
                new KeyEventHandler(AssociatedObject_PreviewKeyDown);
        }

        void AssociatedObject_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Ignore spaces
            if (e.Key == Key.Space)
                e.Handled = true;
        }

        void AssociatedObject_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // If the text is NOT numeric, set the event as handled, so the text will not be set.
            e.Handled = !IsNumeric(e.Text);
        }

        static bool IsNumeric(string text)
        {
            int dummy = 0;
            return int.TryParse(text, out dummy);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            this.AssociatedObject.PreviewTextInput -= AssociatedObject_PreviewTextInput;
            this.AssociatedObject.PreviewKeyDown -= AssociatedObject_PreviewKeyDown;
        }
    }
}