using System;
using Microsoft.Practices.Unity;
using PhoneFramework.ViewModel;

namespace PhoneFramework
{
    public partial class MainPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            var vm = Bootstraper.Container.Resolve<MainPageViewModel>();
            DataContext = vm;
            vm.NavigationRequested += OnNavigationRequest;
        }

        private void OnNavigationRequest(object sender, NavigationRequest e)
        {
            if (e.Adress != null)
            {
                NavigationService.Navigate(new Uri(e.Adress, UriKind.Relative));
            }
        }
    }
}