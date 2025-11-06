using System.Text.Json.Serialization;
using Valr.Net.Enums;

namespace Valr.Net.Objects.Models.Pay
{
    public class ValrPaymentHistoryResponse
    {
        [JsonPropertyName("identifier")]
        public Guid Id { get; set; }

        [JsonPropertyName("otherPartyIdentifier")]
        public string OtherPartyIdentifier { get; set; }

        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        [JsonPropertyName("status")]
        public ValrPaymentStatus Status { get; set; }

        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonPropertyName("transactionId")]
        public string TransactionId { get; set; }

        [JsonPropertyName("anonymous")]
        public bool Anonymous { get; set; }

        [JsonPropertyName("type")]
        public ValrPaymentType Type { get; set; }

        [JsonPropertyName("senderNote")]
        public string SenderNote { get; set; }

        [JsonPropertyName("recipientNote")]
        public string RecipientNote { get; set; }
    }
}
