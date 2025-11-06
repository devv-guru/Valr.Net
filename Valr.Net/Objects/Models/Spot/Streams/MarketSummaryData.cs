using System.Text.Json.Serialization;

namespace Valr.Net.Objects.Models.Spot.Streams
{
    public class MarketSummaryData
    {
        [JsonPropertyName("currencyPairSymbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("askPrice")]
        public decimal Ask { get; set; }

        [JsonPropertyName("bidPrice")]
        public decimal Bid { get; set; }

        [JsonPropertyName("lastTradedPrice")]
        public decimal LastTraded { get; set; }

        [JsonPropertyName("previousClosePrice")]
        public decimal PreviousClose { get; set; }

        [JsonPropertyName("baseVolume")]
        public decimal BaseVolume { get; set; }

        [JsonPropertyName("highPrice")]
        public decimal High { get; set; }

        [JsonPropertyName("lowPrice")]
        public decimal Low { get; set; }

        [JsonPropertyName("created")]
        public DateTime Created { get; set; }

        [JsonPropertyName("changeFromPrevious")]
        public decimal Change { get; set; }
    }
}
