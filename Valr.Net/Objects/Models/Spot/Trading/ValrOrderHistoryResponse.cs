using System.Text.Json.Serialization;
using Valr.Net.Enums;

namespace Valr.Net.Objects.Models.Spot.Trading
{
    public class ValrOrderHistoryResponse : ValrOrderResponseBase
    {
        [JsonPropertyName("averagePrice")]
        public decimal AveragePrice { get; set; }

        [JsonPropertyName("total")]
        public decimal Total { get; set; }

        [JsonPropertyName("totalFee")]
        public decimal TotalFee { get; set; }

        [JsonPropertyName("feeCurrency")]
        public string FeeCurrency { get; set; }

        [JsonPropertyName("failedReason")]
        public string FailedReason { get; set; }

        [JsonPropertyName("timeInForce")]
        public ValrTimeInforce TimeInForce { get; set; }

        public decimal QuantityFilled => OriginalQuantity - RemainingQuantity;
    }
}
