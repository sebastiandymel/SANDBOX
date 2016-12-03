using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ListaZakupow
{
    [DataContract]
    public class ItemGroupData
    {
        [DataMember]
        public List<EntryData> Entries { get; set; }

        [DataMember]
        public string GroupName { get; set; }

        [DataMember]
        public int Id { get; set; }
    }
}