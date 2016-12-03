using Newtonsoft.Json;

namespace OutlookSync
{
    public class Meeting
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string Subject { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Location { get; set; }
        public bool IsRecurring { get; set; }
        public string Content { get; set; }
    }
}