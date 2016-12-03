using System.Runtime.Serialization;

namespace ListaZakupow
{
    [DataContract]
    public class EntryData
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public bool IsChecked { get; set; }
    }
}