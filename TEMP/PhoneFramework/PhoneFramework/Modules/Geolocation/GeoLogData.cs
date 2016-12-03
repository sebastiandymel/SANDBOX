using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PhoneFramework.Modules.Geolocation
{
    [DataContract]
    public class GeoLogData
    {
        [DataMember]
        public List<string> LogEntries;
    }
}