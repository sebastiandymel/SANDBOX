using System;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Practices.Unity;

namespace SpectrumVisualizer
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            DataContext = App.Container.Resolve<MainPageViewModel>();
        }

        private void OnSettingsButtonClick(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/AppSettings.xaml", UriKind.Relative));
        }
    }
}