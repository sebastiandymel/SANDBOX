using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Interactivity;

namespace UIToolkit
{
    public class UpdateOnTextChangedBehavior : Behavior<TextBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            this.AssociatedObject.TextChanged +=
                new TextChangedEventHandler(AssociatedObject_TextChanged);
        }

        void AssociatedObject_TextChanged(object sender, TextChangedEventArgs e)
        {
            BindingExpression binding =
                this.AssociatedObject.GetBindingExpression(TextBox.TextProperty);
            if (binding != null)
            {
                binding.UpdateSource();
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            this.AssociatedObject.TextChanged -=
                new TextChangedEventHandler(AssociatedObject_TextChanged);
        }
    }
}
