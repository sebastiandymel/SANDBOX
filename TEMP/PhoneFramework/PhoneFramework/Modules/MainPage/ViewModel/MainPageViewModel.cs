using SEDY.PhoneUIToolkit;
using System;
using System.Collections.ObjectModel;

namespace PhoneFramework.ViewModel
{
    class MainPageViewModel: ViewModelBase
    {
        private ObservableCollection<PageItem> items = new ObservableCollection<PageItem>();
 
        public MainPageViewModel()
        {
            Add("/Modules/Flickr/FlickerFeed.xaml", "Flickr images feed");
            Add("/Modules/Geolocation/Geolocation.xaml", "Geolocation");
        }

        private void Add(string loc, string name)
        {
            items.Add(
                new PageItem(uri => RaiseNavigationRequested(new NavigationRequest(uri)))
                {
                    Location = loc,
                    Name = name
                });
        }

        public ObservableCollection<PageItem> Items
        {
            get
            {
                return this.items;
            }
            set
            {
                this.items = value;
                RaisePropertyChanged();
            }
        }

        public event EventHandler<NavigationRequest> NavigationRequested;

        protected virtual void RaiseNavigationRequested(NavigationRequest e)
        {
            var handler = NavigationRequested;
            if (handler != null)
            {
                handler(this, e);
            }
        }

    }
}
