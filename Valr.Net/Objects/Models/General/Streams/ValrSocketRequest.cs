using System.Text.Json.Serialization;

using Valr.Net.Enums;

namespace Valr.Net.Objects.Models.General.Streams
{
    internal class ValrSocketRequest
    {
        [JsonPropertyName("type")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ValrSocketEventType EventType { get; set; }

        [JsonPropertyName("subscriptions")]
        public Subscription[] Subscriptions { get; set; }
    }

    public class Subscription
    {
        [JsonPropertyName("event")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ValrSocketOutboundEvent Event { get; set; }

        [JsonPropertyName("pairs")]
        public string[] pairs { get; set; }
    }
}
