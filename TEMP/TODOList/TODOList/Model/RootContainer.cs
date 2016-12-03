using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ListaZakupow
{
    [DataContract]
    public class RootContainer
    {
        [DataMember]
        public List<ItemGroupData> Groups { get; set; } 
    }
}