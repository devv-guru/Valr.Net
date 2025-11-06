using System.Text.Json.Serialization;

namespace Valr.Net.Objects.Models.General.Streams
{
    public class PendingCryptoDepositData
    {
        [JsonPropertyName("currency")]
        public CurrencyInfo Currency { get; set; }

        [JsonPropertyName("receiveAddress")]
        public string ReceiveAddress { get; set; }

        [JsonPropertyName("transactionHash")]
        public string TransactionHash { get; set; }

        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime Created { get; set; }

        [JsonPropertyName("confirmations")]
        public int Confirmations { get; set; }

        [JsonPropertyName("confirmed")]
        public bool Confirmed { get; set; }
    }
}
