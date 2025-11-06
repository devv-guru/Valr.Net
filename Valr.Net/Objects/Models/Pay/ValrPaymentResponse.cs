using System.Text.Json.Serialization;

namespace Valr.Net.Objects.Models.Pay
{
    public class ValrPaymentResponse
    {
        [JsonPropertyName("identifier")]
        public Guid Id { get; set; }

        [JsonPropertyName("transactionId")]
        public string TransactionId { get; set; }
    }
}
