using ListaZakupow;
using SEDY.PhoneUIToolkit;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Navigation;

namespace PhoneFramework.Modules.Geolocation
{
    public class FavouritesViewModel: ViewModelBase
    {
        private ObservableCollection<FavouriteLocation> favourites;
        private readonly XmlSerializerDeserializer<FavouriteLocations> serializer = GeoHelper.Serializer;

        public FavouritesViewModel()
        {
            var data = serializer.DeserializeData();
            if (data != null && data.Favourites != null)
            {
                Favourites = new ObservableCollection<FavouriteLocation>(data.Favourites);
            }
            else
            {
                Favourites = new ObservableCollection<FavouriteLocation>();
            }

            AddNewCommand = new RelayCommand(AddNew);
        }

        public override void Initialize()
        {
            base.Initialize();
            var data = serializer.DeserializeData();
            if (data != null && data.Favourites != null)
            {
                Favourites = new ObservableCollection<FavouriteLocation>(data.Favourites);
            }
            else
            {
                Favourites = new ObservableCollection<FavouriteLocation>();
            }
        }

        public event EventHandler<NavigationEventArgs> NavigationRequested;
        
        public ObservableCollection<FavouriteLocation> Favourites
        {
            get { return this.favourites; }
            set
            {
                this.favourites = value;
                RaisePropertyChanged();
            }
        }

        public ICommand AddNewCommand { get; private set; }

        protected virtual void OnNavigationRequested(NavigationEventArgs e)
        {
            var handler = NavigationRequested;
            if (handler != null) handler(this, e);
        }
        
        private void AddNew()
        {
            OnNavigationRequested(new NavigationEventArgs(this, new Uri("/Modules/Geolocation/AddNewLocation.xaml", UriKind.Relative)));
        }

        public void RemoveAll()
        {
            serializer.SerializeData(new FavouriteLocations());
        }
    }
}