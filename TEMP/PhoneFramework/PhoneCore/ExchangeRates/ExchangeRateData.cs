using System;
using System.Runtime.Serialization;

namespace SEDY.PhoneCore.DSP
{
    [DataContract]
    public class ExchangeRateData
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public double Value { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public ExchangeRateDetailsData Details { get; set; }

        [DataMember]
        public double MultiplicationFactor { get; set; }
    }

    [DataContract]
    public class ExchangeRateDetailsData
    {
        [DataMember]
        public double BuyValue { get; set; }

        [DataMember]
        public double SellValue { get; set; }
    }
}