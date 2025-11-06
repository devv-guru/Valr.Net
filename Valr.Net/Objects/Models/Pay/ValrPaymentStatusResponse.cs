using System.Text.Json.Serialization;
using Valr.Net.Enums;

namespace Valr.Net.Objects.Models.Pay
{
    public class ValrPaymentStatusResponse
    {
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonPropertyName("transactionId")]
        public string TransactionId { get; set; }

        [JsonPropertyName("status")]
        public ValrPaymentStatus Status { get; set; }

        [JsonPropertyName("direction")]
        public string Direction { get; set; }
    }
}
