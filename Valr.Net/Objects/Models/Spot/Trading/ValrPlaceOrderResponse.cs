using System.Text.Json.Serialization;

namespace Valr.Net.Objects.Models.Spot.Trading
{
    public class ValrPlaceOrderResponse
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
    }
}
