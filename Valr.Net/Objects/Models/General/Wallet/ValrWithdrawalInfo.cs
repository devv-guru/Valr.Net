using System.Text.Json.Serialization;

namespace Valr.Net.Objects.Models.General.Wallet
{
    public class ValrWithdrawalInfo
    {
        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("minimumWithdrawAmount")]
        public decimal MinimumWithdrawAmount { get; set; }

        [JsonPropertyName("withdrawalDecimalPlaces")]
        public int WithdrawalDecimalPlaces { get; set; }

        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }

        [JsonPropertyName("withdrawCost")]
        public decimal WithdrawCost { get; set; }

        [JsonPropertyName("supportsPaymentReference")]
        public bool SupportsPaymentReference { get; set; }
    }
}
