using System.Linq;
using ListaZakupow;
using SEDY.PhoneUIToolkit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using Windows.Devices.Geolocation;
using Windows.Devices.Geolocation.Geofencing;

namespace PhoneFramework.Modules.Geolocation
{
    /// <summary>
    /// Based on example:
    /// http://www.jayway.com/2014/04/22/windows-phone-8-1-for-developers-geolocation-and-geofencing/
    /// </summary>
    class GeolocationViewModel: ViewModelBase
    {
        private static bool isInitialized;
        private ObservableCollection<string> mainData = new ObservableCollection<string>();
        private readonly XmlSerializerDeserializer<GeoLogData> logSerializer = new XmlSerializerDeserializer<GeoLogData>("Geolog", "log.dat");
        private readonly XmlSerializerDeserializer<FavouriteLocations> favSerializer = GeoHelper.Serializer;
        private List<FavouriteLocation> favouriteLocations;

        public event EventHandler<NotificationArgs> RequestToast;

        protected virtual void OnRequestToast(string msg)
        {
            var handler = RequestToast;
            if (handler != null)
            {
                handler(this, new NotificationArgs()
                {
                    Message = msg
                });
            }
        }

        public async override void Initialize()
        {
            base.Initialize();
           
            var data = this.logSerializer.DeserializeData();
            if (data != null)
            {
                MainData = new ObservableCollection<string>(data.LogEntries);
            }

            var favs = this.favSerializer.DeserializeData();
            if (favs != null && favs.Favourites != null)
            {
                favouriteLocations = favs.Favourites;
            }
            else
            {
                favouriteLocations = new List<FavouriteLocation>();
                favouriteLocations.Add(new FavouriteLocation()
                {
                    Name = "OsiedlePionierowFence",
                    EnterMessage = "Wejście na osiedle pionierów",
                    LeaveMessage = "Wyjście z osiedla pionierów",
                    Coordinates = new FavouriteLocationCoordinates()
                                  {
                                      Latitude = 53.4173621,
                                      Longitude = 14.5415263,
                                  }
                });

                favouriteLocations.Add(new FavouriteLocation()
                {
                    Name = "KopanskiegoFence",
                    EnterMessage = "Wejście na ul Kopańskiego",
                    LeaveMessage = "Wyjście z ul Kopańskiego",
                    Coordinates = new FavouriteLocationCoordinates()
                    {
                        Latitude = 53.4319362,
                        Longitude = 14.4839422
                    }
                });
            }

            if (!GeolocationViewModel.isInitialized)
            {
                await InitGeofence();
                GeolocationViewModel.isInitialized = true;
            }
        }

        public override void Uninitialize()
        {
            base.Uninitialize();
            
            CleanupGeofences();

            this.logSerializer.SerializeData(new GeoLogData()
            {
                LogEntries = new List<string>(mainData)
            });
        }

        public void ClearLog()
        {
            MainData.Clear();
            this.logSerializer.SerializeData(new GeoLogData(){ LogEntries = new List<string>()});
        }

        public ObservableCollection<string> MainData
        {
            get { return this.mainData; }
            set
            {
                this.mainData = value;
                RaisePropertyChanged();
            }
        }

        private async Task InitGeofence()
        {
            var geofenceMonitor = GeofenceMonitor.Current;
            geofenceMonitor.Geofences.Clear();
            geofenceMonitor.GeofenceStateChanged -= OnGeofenceStateChanged;
            geofenceMonitor.GeofenceStateChanged += OnGeofenceStateChanged;
            geofenceMonitor.StatusChanged += OnGeoFenceStatusChanged;

            if (this.favouriteLocations != null)
            {
                foreach (var location in this.favouriteLocations)
                {
                    if (location.Coordinates != null)
                    {
                        var point = new Geopoint(new BasicGeoposition()
                                                 {
                                                     Latitude = location.Coordinates.Latitude,
                                                     Longitude = location.Coordinates.Longitude
                                                 });
                        MonitorEnterExit(point, geofenceMonitor, location.Name);
                    }
                }
            }
        }

        private void CleanupGeofences()
        {
            var geofenceMonitor = GeofenceMonitor.Current;
            geofenceMonitor.Geofences.Clear();
            geofenceMonitor.GeofenceStateChanged -= OnGeofenceStateChanged;
            GeolocationViewModel.isInitialized = false;
        }

        private static void MonitorEnterExit(Geopoint osiedlePionierow, GeofenceMonitor geofenceMonitor, string fenceName)
        {
            // Radius in meters 200 [m]
            var areaToWatch = new Geocircle(osiedlePionierow.Position, 200);
            // Specifies for how much time the user must have entered/exited the area before 
            // receiving the notification.
            var dwellTime = TimeSpan.FromSeconds(10);
            var geofencePionierow = new Geofence(
                fenceName,
                areaToWatch,
                MonitoredGeofenceStates.Entered | MonitoredGeofenceStates.Exited,
                false,
                dwellTime);
            geofenceMonitor.Geofences.Add(geofencePionierow);
        }

        private void OnGeofenceStateChanged(GeofenceMonitor sender, object args)
        {
            var geoReports = sender.ReadReports();
            foreach (var geofenceStateChangeReport in geoReports)
            {
                var id = geofenceStateChangeReport.Geofence.Id;
                var newState = geofenceStateChangeReport.NewState;
                string msg = null;

                var location = this.favouriteLocations.FirstOrDefault(c => c.Name == id);
                if (location != null)
                {
                    if (newState == GeofenceState.Entered)
                    {
                        msg = location.EnterMessage;
                    }
                    else if (newState == GeofenceState.Exited)
                    {
                        msg = location.LeaveMessage;
                    }

                    if (!string.IsNullOrEmpty(msg))
                    {
                        AddLogEntry(msg);
                        OnRequestToast(msg);
                    }
                }
            }
        }

        private void AddLogEntry(string msg)
        {
            var time = DateTime.Now.ToLongTimeString();
            var date = DateTime.Now.ToShortDateString();

            Deployment.Current.Dispatcher.BeginInvoke(
                () => MainData.Add(string.Format("[{0}] {1}: {2}",date, time, msg)));
        }

        private void OnGeoFenceStatusChanged(GeofenceMonitor sender, object args)
        {
            
        }
    }
}
