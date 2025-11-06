using System.Text.Json.Serialization;

namespace Valr.Net.Objects.Models.Spot.InstantTrading
{
    public class ValrInstantTradeResponse
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
    }
}
