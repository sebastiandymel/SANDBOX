using System;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Practices.Unity;
using SEDY.PhoneUIToolkit;

namespace PhoneFramework.Modules.Geolocation
{
    public partial class FavouritesView : PhoneApplicationPage
    {
        public FavouritesView()
        {
            InitializeComponent();
            var vm = Bootstraper.Container.Resolve<FavouritesViewModel>();
            DataContext = vm;
            vm.NavigationRequested += OnNavigationRequested;
        }

        private void OnNavigationRequested(object sender, NavigationEventArgs e)
        {
            NavigationService.Navigate(e.Uri);
        }

        private void Favourites_RemoveAll(object sender, EventArgs e)
        {
            var vm = (DataContext as FavouritesViewModel);
            if (vm != null)
            {
                vm.RemoveAll();
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var vm = (DataContext as ViewModelBase);
            if (vm != null)
            {
                vm.Initialize();
            }
        }
    }
}