using System;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using ListaZakupow;

namespace PhoneFramework.Modules.Geolocation
{
    internal static class GeoHelper
    {
        public static async Task<FavouriteLocationCoordinates> GetCurrentCoordinates()
        {
            var loc = await new Geolocator().GetGeopositionAsync(
                TimeSpan.FromMinutes(2),
                TimeSpan.FromSeconds(5));

            return new FavouriteLocationCoordinates()
                   {
                       Latitude = loc.Coordinate.Latitude,
                       Longitude = loc.Coordinate.Longitude
                   };
        }

        private static XmlSerializerDeserializer<FavouriteLocations> serializer;

        public static XmlSerializerDeserializer<FavouriteLocations> Serializer
        {
            get
            {
                if (serializer == null)
                {
                    serializer = new XmlSerializerDeserializer<FavouriteLocations>("Favourites", "fav.dat");
                }
                return serializer;
            }
        }
    }
}