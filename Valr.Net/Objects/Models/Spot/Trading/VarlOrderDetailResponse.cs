using System.Text.Json.Serialization;
using Valr.Net.Enums;

namespace Valr.Net.Objects.Models.Spot.Trading
{
    public class ValrOrderDetailResponse : ValrOrderResponseBase
    {
        [JsonPropertyName("failedReason")]
        public string FailedReason { get; set; }

        [JsonPropertyName("executedFee")]
        public decimal ExecutedFee { get; set; }

        [JsonPropertyName("executedPrice")]
        public decimal ExecutedPrice { get; set; }

        [JsonPropertyName("executedQuantity")]
        public decimal ExecutedQuantity { get; set; }

        [JsonPropertyName("timeInForce")]
        public ValrTimeInforce TimeInForce { get; set; }
    }
}
