using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using ListaZakupow;
using SEDY.PhoneUIToolkit;

namespace PhoneFramework.Modules.Geolocation
{
    public class AddNewLocationViewModel : ViewModelBase
    {
        private string errorLog;
        private string name;
        private string enterText;
        private string exitText;
        private List<FavouriteLocation> favourites;
        private readonly XmlSerializerDeserializer<FavouriteLocations> serializer = GeoHelper.Serializer;
        private bool isAdding;

        public AddNewLocationViewModel()
        {
            AddCommand = new RelayCommand(Add);

            var data = serializer.DeserializeData();
            if (data != null)
            {
                favourites = data.Favourites;
            }
        }

        public override void Initialize()
        {
            base.Initialize();
            var data = serializer.DeserializeData();
            if (data != null)
            {
                favourites = data.Favourites;
            }
        }

        public ICommand AddCommand { get; private set; }

        public string ErrorLog
        {
            get { return this.errorLog; }
            set
            {
                this.errorLog = value;
                RaisePropertyChanged();
            }
        }

        public string Name
        {
            get { return this.name; }
            set
            {
                this.name = value;
                RaisePropertyChanged();
                NameUpdated();
            }
        }

        public string EnterAreaText
        {
            get { return this.enterText; }
            set
            {
                this.enterText = value;
                RaisePropertyChanged();
            }
        }

        public string ExitAreaText
        {
            get { return this.exitText; }
            set
            {
                this.exitText = value;
                RaisePropertyChanged();
            }
        }

        private void NameUpdated()
        {
            EnterAreaText = "Entered " + Name;
            ExitAreaText = "Exited " + Name;
        }

        private async void Add()
        {
            if (!CanAdd())
            {
                return;
            }

            this.isAdding = true;

            if (this.favourites == null)
            {
                this.favourites = new List<FavouriteLocation>();
            }

            var newFavourite = new FavouriteLocation();
            newFavourite.Name = Name;
            newFavourite.EnterMessage = EnterAreaText;
            newFavourite.EnterMessage = ExitAreaText;
            newFavourite.Coordinates = await GeoHelper.GetCurrentCoordinates();

            this.favourites.Add(newFavourite);

            serializer.SerializeData(
                new FavouriteLocations()
                {
                    Favourites = this.favourites
                });

            Name = string.Empty;
            ErrorLog = string.Empty;
            EnterAreaText = string.Empty;
            ExitAreaText = string.Empty;
            this.isAdding = false;
        }

        private bool CanAdd()
        {
            if (this.isAdding)
            {
                return false;
            }

            if (string.IsNullOrEmpty(Name))
            {
                ErrorLog = "Epty name";
                return false;
            }

            if (this.favourites != null && this.favourites.Any(c => c.Name == Name))
            {
                ErrorLog = "Name already exist";
                return false;
            }

            ErrorLog = string.Empty;
            return true;
        }
    }
}