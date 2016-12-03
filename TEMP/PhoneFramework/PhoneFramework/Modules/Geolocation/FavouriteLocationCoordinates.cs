using System.Runtime.Serialization;

namespace PhoneFramework.Modules.Geolocation
{
    [DataContract]
    public class FavouriteLocationCoordinates
    {
        [DataMember]
        public double Latitude { get; set; }

        [DataMember]
        public double Longitude { get; set; }
    }
}