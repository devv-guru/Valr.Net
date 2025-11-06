using System.Text.Json.Serialization;

using Valr.Net.Enums;

namespace Valr.Net.Objects.Models.General.ExchangeData
{
    public class ValrTrade
    {
        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("quantity")]
        public decimal Quantity { get; set; }

        [JsonPropertyName("currencyPair")]
        public string CurrencyPair { get; set; }

        [JsonPropertyName("tradedAt")]
        public DateTime TradeTime { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("side")]
        public ValrOrderSide Side { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("takerSide")]
        private ValrOrderSide TakerSide
        {
            set
            {
                Side = value;
            }
        }

        [JsonPropertyName("sequenceId")]
        public int SequenceId { get; set; }

        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("orderId")]
        public Guid OrderId { get; set; }

        [JsonPropertyName("quoteVolume")]
        public decimal? QuoteVolume { get; set; }
    }

    public class TradeWrapper
    {
        public ValrTrade[] Trades { get; set; }
    }

}
