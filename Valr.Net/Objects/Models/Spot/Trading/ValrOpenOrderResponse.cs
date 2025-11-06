using System.Text.Json.Serialization;

using Valr.Net.Enums;

namespace Valr.Net.Objects.Models.Spot.Trading
{
    public class ValrOpenOrderResponse : ValrOrderResponseBase
    {
        [JsonPropertyName("filledPercentage")]
        public decimal FilledPercentage { get; set; }

        [JsonPropertyName("stopPrice")]
        public decimal StopPrice { get; set; }

        [JsonPropertyName("timeInForce")]
        public ValrTimeInforce TimeInForce { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("status")]
        private ValrOrderStatus status
        {
            set => Status = value;
        }

        [JsonPropertyName("price")]
        private decimal price
        {
            set => OriginalPrice = value;
        }

        [JsonPropertyName("updatedAt")]
        private DateTime UpdatedAt
        {
            set => LastUpdated = value;
        }

        [JsonPropertyName("createdAt")]
        private DateTime createdAt
        {
            set => Created = value;
        }
    }
}
