using System.Text.Json.Serialization;

namespace Valr.Net.Objects.Models.General.Wallet
{
    public class ValrDepositStatusInfo
    {
        [JsonPropertyName("currencyCode")]
        public string CurrencyCode { get; set; }

        [JsonPropertyName("receiveAddress")]
        public string ReceiveAddress { get; set; }

        [JsonPropertyName("transactionHash")]
        public string TransactionHash { get; set; }

        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("confirmations")]
        public int Confirmations { get; set; }

        [JsonPropertyName("confirmed")]
        public bool Confirmed { get; set; }

        [JsonPropertyName("confirmedAt")]
        public DateTime? ConfirmedAt { get; set; }
    }
}
