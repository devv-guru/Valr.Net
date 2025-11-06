using System.Text.Json.Serialization;

using Valr.Net.Enums;

namespace Valr.Net.Objects.Models.Spot.Trading
{
    public class ValrOrderResponseBase
    {
        [JsonPropertyName("orderId")]
        public Guid Id { get; set; }

        [JsonPropertyName("currencyPair")]
        public string CurrencyPair { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("orderStatusType")]
        public ValrOrderStatus Status { get; set; }

        [JsonPropertyName("orderSide")]
        public ValrOrderSide Side { get; set; }

        [JsonPropertyName("originalPrice")]
        public decimal OriginalPrice { get; set; }

        [JsonPropertyName("remainingQuantity")]
        public decimal RemainingQuantity { get; set; }

        [JsonPropertyName("originalQuantity")]
        public decimal OriginalQuantity { get; set; }

        [JsonPropertyName("orderType")]
        public ValrOrderType Type { get; set; }

        [JsonPropertyName("orderUpdatedAt")]
        public DateTime LastUpdated { get; set; }

        [JsonPropertyName("orderCreatedAt")]
        public DateTime Created { get; set; }

        public decimal QuantityFilled => OriginalQuantity - RemainingQuantity;
    }
}
