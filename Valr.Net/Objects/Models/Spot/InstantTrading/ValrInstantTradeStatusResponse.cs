using System.Text.Json.Serialization;

namespace Valr.Net.Objects.Models.Spot.InstantTrading
{
    public class ValrInstantTradeStatusResponse
    {
        [JsonPropertyName("orderId")]
        public Guid Id { get; set; }

        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("processing")]
        public decimal Processing { get; set; }

        [JsonPropertyName("paidAmount")]
        public decimal PaidAmount { get; set; }

        [JsonPropertyName("paidCurrency")]
        public string PaidCurrency { get; set; }

        [JsonPropertyName("receivedAmount")]
        public decimal ReceivedAmount { get; set; }

        [JsonPropertyName("receivedCurrency")]
        public string ReceivedCurrency { get; set; }

        [JsonPropertyName("feeAmount")]
        public decimal FeeAmount { get; set; }

        [JsonPropertyName("feeCurrency")]
        public string FeeCurrency { get; set; }

        [JsonPropertyName("orderExecutedAt")]
        public DateTime TimeExecuted { get; set; }
    }
}
