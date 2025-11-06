using System.Text.Json.Serialization;

namespace Valr.Net.Objects.Models.Spot.InstantTrading
{
    public class ValrInstantTradeQuote
    {
        [JsonPropertyName("currencyPair")]
        public string CurrencyPair { get; set; }

        [JsonPropertyName("payAmount")]
        public decimal PayAmount { get; set; }

        [JsonPropertyName("receiveAmount")]
        public decimal ReceiveAmount { get; set; }

        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }

        [JsonPropertyName("feeCurrency")]
        public string FeeCurrency { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime Created { get; set; }

        [JsonPropertyName("id")]
        public Guid Id { get; set; }
    }
}
