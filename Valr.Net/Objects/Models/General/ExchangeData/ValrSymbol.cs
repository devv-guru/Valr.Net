using System.Text.Json.Serialization;

namespace Valr.Net.Objects.Models.General.ExchangeData
{
    public class ValrSymbol
    {
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("baseCurrency")]
        public string BaseCurrency { get; set; }

        [JsonPropertyName("quoteCurrency")]
        public string QuoteCurrency { get; set; }

        [JsonPropertyName("shortName")]
        public string ShortName { get; set; }

        [JsonPropertyName("active")]
        public bool Active { get; set; }

        [JsonPropertyName("minBaseAmount")]
        public decimal MinBaseAmount { get; set; }

        [JsonPropertyName("maxBaseAmount")]
        public decimal MaxBaseAmount { get; set; }

        [JsonPropertyName("minQuoteAmount")]
        public decimal MinQuoteAmount { get; set; }

        [JsonPropertyName("maxQuoteAmount")]
        public decimal MaxQuoteAmount { get; set; }

        [JsonPropertyName("tickSize")]
        public decimal TickSize { get; set; }

        [JsonPropertyName("baseDecimalPlaces")]
        public int BaseDecimalPlaces { get; set; }
    }
}
