using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PhoneFramework.Modules.Geolocation
{
    [DataContract]
    public class FavouriteLocation
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string EnterMessage { get; set; }

        [DataMember]
        public string LeaveMessage { get; set; }

        [DataMember]
        public FavouriteLocationCoordinates Coordinates { get; set; }
    }

    [DataContract]
    public class FavouriteLocations
    {
        [DataMember]
        public List<FavouriteLocation> Favourites { get; set; } 
    }
}