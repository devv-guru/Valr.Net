using System.Text.Json.Serialization;

namespace Valr.Net.Objects.Models.General.ExchangeData
{
    public class ValrMarketSummary
    {
        [JsonPropertyName("currencyPair")]
        public string CurrencyPair { get; set; }

        [JsonPropertyName("askPrice")]
        public decimal AskPrice { get; set; }

        [JsonPropertyName("bidPrice")]
        public decimal BidPrice { get; set; }

        [JsonPropertyName("lastTradedPrice")]
        public decimal LastTradedPrice { get; set; }

        [JsonPropertyName("previousClosePrice")]
        public decimal PreviousClosePrice { get; set; }

        [JsonPropertyName("baseVolume")]
        public decimal BaseVolume { get; set; }

        [JsonPropertyName("highPrice")]
        public decimal HighPrice { get; set; }

        [JsonPropertyName("lowPrice")]
        public decimal LowPrice { get; set; }

        [JsonPropertyName("created")]
        public DateTime Created { get; set; }

        [JsonPropertyName("changeFromPrevious")]
        public decimal ChangeFromPrevious { get; set; }
    }

    public class MarketSummaryWrapper
    {
        public ValrMarketSummary[] MarketSummaries { get; set; }
    }
}
