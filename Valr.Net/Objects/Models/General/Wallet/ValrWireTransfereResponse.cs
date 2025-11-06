using System.Text.Json.Serialization;

namespace Valr.Net.Objects.Models.General.Wallet
{
    public class ValrWireTransferResponse
    {
        [JsonPropertyName("id")]
        public Guid TransactionId { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime Created { get; set; }

        [JsonPropertyName("wireBankAccountId")]
        public Guid WireBankAccountId { get; set; }
    }
}
