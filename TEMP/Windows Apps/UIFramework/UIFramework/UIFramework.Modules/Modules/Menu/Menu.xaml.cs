using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Windows.Controls;

namespace UIFramework.Modules.Modules.Menu
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    [Export(typeof(Menu))]
    public partial class Menu : UserControl, IPartImportsSatisfiedNotification
    {
        [Import]
        private MenuViewModel ViewModel { get; set; }

        public Menu()
        {
            InitializeComponent();
        }

        public void OnImportsSatisfied()
        {
            this.DataContext = ViewModel;
        }
    }
}
