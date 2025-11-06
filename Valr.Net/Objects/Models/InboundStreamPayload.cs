using System.Text.Json.Serialization;
using Valr.Net.Enums;

namespace Valr.Net.Objects.Models
{
    public class InboundStreamPayload<T>
    {
        [JsonPropertyName("type")]
        public ValrSocketInboundEvent PayloadType { get; set; }

        [JsonPropertyName("data")]
        public T Data { get; set; }

        [JsonPropertyName("currencyPairSymbol")]
        public string? Symbol { get; set; }
    }
}
