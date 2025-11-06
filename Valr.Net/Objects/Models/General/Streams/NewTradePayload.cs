using System.Text.Json.Serialization;

using Valr.Net.Enums;

namespace Valr.Net.Objects.Models.General.Streams
{
    public class NewTradeData
    {
        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("quantity")]
        public decimal Quantity { get; set; }

        [JsonPropertyName("currencyPair")]
        public string Symbol { get; set; }

        [JsonPropertyName("tradedAt")]
        public DateTime Created { get; set; }

        [JsonPropertyName("side")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ValrOrderSide Side { get; set; }

        [JsonPropertyName("orderId")]
        public Guid OrderId { get; set; }

        [JsonPropertyName("id")]
        public Guid Id { get; set; }
    }

}
