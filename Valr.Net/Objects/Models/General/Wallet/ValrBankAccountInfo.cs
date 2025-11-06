using System.Text.Json.Serialization;

namespace Valr.Net.Objects.Models.General.Wallet
{
    public class ValrBankAccountInfo
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("bank")]
        public string Bank { get; set; }

        [JsonPropertyName("accountHolder")]
        public string AccountHolder { get; set; }

        [JsonPropertyName("accountNumber")]
        public string AccountNumber { get; set; }

        [JsonPropertyName("branchCode")]
        public string BranchCode { get; set; }

        [JsonPropertyName("accountType")]
        public string AccountType { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime Created { get; set; }
    }
}
