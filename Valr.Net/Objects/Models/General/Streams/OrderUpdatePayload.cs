using System.Text.Json.Serialization;

using Valr.Net.Enums;

namespace Valr.Net.Objects.Models.General.Streams
{
    public class OrderUpdateData
    {
        [JsonPropertyName("orderId")]
        public Guid Id { get; set; }

        [JsonPropertyName("orderStatusType")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ValrOrderStatus OrderStatus { get; set; }

        [JsonPropertyName("currencyPair")]
        public Currencypair Symbol { get; set; }

        [JsonPropertyName("originalPrice")]
        public decimal OriginalPrice { get; set; }

        [JsonPropertyName("remainingQuantity")]
        public decimal RemainingQuantity { get; set; }

        [JsonPropertyName("originalQuantity")]
        public decimal OriginalQuantity { get; set; }

        [JsonPropertyName("orderSide")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ValrOrderSide Side { get; set; }

        [JsonPropertyName("orderType")]
        public ValrOrderType OrderType { get; set; }

        [JsonPropertyName("failedReason")]
        public string? FailedReason { get; set; }

        [JsonPropertyName("orderUpdatedAt")]
        public DateTime Updated { get; set; }

        [JsonPropertyName("orderCreatedAt")]
        public DateTime Created { get; set; }

        [JsonPropertyName("customerOrderId")]
        public string? CustomerOrderId { get; set; }

        public decimal QuantityFilled => OriginalQuantity - RemainingQuantity;
    }
}
