using ListaZakupow.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ListaZakupow.View
{
    public partial class AddNewGroup : UserControl
    {
        public AddNewGroup()
        {
            InitializeComponent();
            DataContext = new AddGroupViewModel();

            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            editBox.Focus();
        }

        private void EditBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var txt = (sender as TextBox);
            BindingExpression be = txt.GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();
        }
    }
}
