using System.Windows.Controls;
using System.Windows.Interactivity;

namespace UIToolkit
{
    /// <summary>
    /// 
    /// </summary>
    public class ScrollIntoViewBehavior : Behavior<ListBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            this.AssociatedObject.SelectionChanged += this.AssociatedObject_SelectionChanged;
        }

        void AssociatedObject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.AssociatedObject.ScrollIntoView(this.AssociatedObject.SelectedItem);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            this.AssociatedObject.SelectionChanged -= this.AssociatedObject_SelectionChanged;
        }
    }
}
