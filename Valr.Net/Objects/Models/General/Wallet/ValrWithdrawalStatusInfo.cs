using System.Text.Json.Serialization;
using Valr.Net.Enums;

namespace Valr.Net.Objects.Models.General.Wallet
{
    public class ValrWithdrawalStatusInfo
    {
        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("amount")]
        public string Amount { get; set; }

        [JsonPropertyName("feeAmount")]
        public string FeeAmount { get; set; }

        [JsonPropertyName("transactionHash")]
        public string TransactionHash { get; set; }

        [JsonPropertyName("confirmations")]
        public int Confirmations { get; set; }

        [JsonPropertyName("lastConfirmationAt")]
        public DateTime LastConfirmationTime { get; set; }

        [JsonPropertyName("uniqueId")]
        public string Id { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime Created { get; set; }

        [JsonPropertyName("verified")]
        public bool Verified { get; set; }

        [JsonPropertyName("status")]
        public ValrWithdrawalStatus Status { get; set; }
    }
}
