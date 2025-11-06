using System.Text.Json.Serialization;

using Valr.Net.Enums;

namespace Valr.Net.Objects.Models.Spot.Streams;

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

    [JsonPropertyName("takerSide")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ValrOrderSide Side { get; set; }
}