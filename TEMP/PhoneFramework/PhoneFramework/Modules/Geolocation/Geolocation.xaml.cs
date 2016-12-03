using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Practices.Unity;
using SEDY.PhoneUIToolkit;
using System;
using System.Windows;

namespace PhoneFramework.Modules.Geolocation
{
    public partial class Geolocation : PhoneApplicationPage
    {
        public Geolocation()
        {
            InitializeComponent();

            var vm = Bootstraper.Container.Resolve<GeolocationViewModel>();
            DataContext = vm;
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
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

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            var geolocationViewModel = DataContext as GeolocationViewModel;
            if (geolocationViewModel != null)
            {
                geolocationViewModel.Uninitialize();
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var geolocationViewModel = DataContext as GeolocationViewModel;
            if (geolocationViewModel != null)
            {
                geolocationViewModel.Initialize();
                geolocationViewModel.RequestToast += ToastRequested;
            }
        }

        private static void ToastRequested(object sender, NotificationArgs e)
        {
            ToastHelper.ShowToast("Checkpoint", e.Message);
        }

        private void AppBar_Clear(object sender, EventArgs e)
        {
            var geolocationViewModel = DataContext as GeolocationViewModel;
            if (geolocationViewModel != null)
            {
                geolocationViewModel.ClearLog();
            }
        }

        private void OpenFavourites(object sender, EventArgs e)
        {
            var geolocationViewModel = DataContext as GeolocationViewModel;
            if (geolocationViewModel != null)
            {
                NavigationService.Navigate(new Uri("/Modules/Geolocation/FavouritesView.xaml", UriKind.Relative));
            }
        }
    }
}