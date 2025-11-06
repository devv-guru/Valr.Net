using System.Text.Json.Serialization;

namespace Valr.Net.Objects.Models.General.Account
{
    public class ValrAccountBalance
    {
        [JsonPropertyName("currency")]
        public string Currency { get; set; }
        [JsonPropertyName("available")]
        public decimal Available { get; set; }
        [JsonPropertyName("reserved")]
        public decimal Reserved { get; set; }
        [JsonPropertyName("total")]
        public decimal Total { get; set; }

        [JsonPropertyName("updatedAt")]
        public DateTime UpdatedAt { get; set; }
    }


    public class AccountBalanceWrapper
    {
        public ValrAccountBalance[] Balances { get; set; }
    }
}
