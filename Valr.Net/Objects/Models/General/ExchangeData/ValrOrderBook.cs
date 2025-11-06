using System.Text.Json.Serialization;

namespace Valr.Net.Objects.Models.General.ExchangeData
{
    public class ValrOrderBook
    {
        public string Symbol { get; set; }
        public ValrOrderBookSymbol[] Asks { get; set; }
        public ValrOrderBookSymbol[] Bids { get; set; }
        public DateTime LastChange { get; set; }
        public int SequenceNumber { get; set; }
    }

    public class ValrOrderBookSymbol
    {
        [JsonPropertyName("side")]
        public string Side { get; set; }

        [JsonPropertyName("quantity")]
        public decimal Quantity { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("currencyPair")]
        public string CurrencyPair { get; set; }

        /// <summary>
        /// Only used for aggregated order books to indicate how many orders are aggregated together
        /// </summary>
        [JsonPropertyName("orderCount")]
        public int? OrderCount { get; set; }

        /// <summary>
        /// Only used for full order books
        /// </summary>
        [JsonPropertyName("id")]
        public Guid? Id { get; set; }

        /// <summary>
        /// Only used for full order books
        /// </summary>
        [JsonPropertyName("positionAtPrice")]
        public int? PositionAtPrice { get; set; }
    }
}
