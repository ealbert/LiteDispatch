using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace LiteDispatch.Domain.Entities
{
    public class DispatchEvent : DispatchEventBase
    {
        public long Id { get; set; }
    }

    public class DispatchEventBase
    {
        [JsonProperty(PropertyName = "dispatchNoteId")]
        public long DispatchNoteId { get; set; }

        [JsonProperty(PropertyName = "truck")]
        public string Truck { get; set; }

        [JsonProperty(PropertyName = "eventType")]
        public string EventType { get; set; }

        [JsonProperty(PropertyName = "trackingInfo")]
        public string TrackingInfo { get; set; }
    }
}
