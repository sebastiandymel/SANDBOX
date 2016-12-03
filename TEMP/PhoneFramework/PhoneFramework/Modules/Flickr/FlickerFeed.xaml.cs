using System;
using Microsoft.Phone.Controls;
using Microsoft.Practices.Unity;

namespace PhoneFramework.Modules.Flickr
{
    public partial class FlickerFeed : PhoneApplicationPage
    {
        public FlickerFeed()
        {
            InitializeComponent();
            DataContext = Bootstraper.Container.Resolve<FlickrFeedViewModel>();
            this.Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var flickrFeedViewModel = DataContext as FlickrFeedViewModel;
            if (flickrFeedViewModel != null)
            {
                flickrFeedViewModel.Initialize();
            }
        }

        private async void AppBar_UpdateData(object sender, EventArgs e)
        {
            var flickrFeedViewModel = DataContext as FlickrFeedViewModel;
            if (flickrFeedViewModel != null)
            {
                flickrFeedViewModel.Initialize();
            }
        }
    }
}