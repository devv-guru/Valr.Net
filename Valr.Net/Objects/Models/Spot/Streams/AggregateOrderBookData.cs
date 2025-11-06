using System.Text.Json.Serialization;

using Valr.Net.Enums;

namespace Valr.Net.Objects.Models.Spot.Streams;

public class AggregateOrderBookData
{
    [JsonPropertyName("Asks")]
    public AggregateOrder[] Asks { get; set; }

    [JsonPropertyName("Bids")]
    public AggregateOrder[] Bids { get; set; }

    [JsonPropertyName("LastChange")]
    public DateTime LastChange { get; set; }

    [JsonPropertyName("SequenceNumber")]
    public long SequenceNumber { get; set; }
}

public class AggregateOrder
{
    [JsonPropertyName("side")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ValrOrderSide Side { get; set; }

    [JsonPropertyName("quantity")]
    public decimal Quantity { get; set; }

    [JsonPropertyName("price")]
    public decimal Price { get; set; }

    [JsonPropertyName("currencyPair")]
    public string Symbol { get; set; }

    [JsonPropertyName("orderCount")]
    public int OrderCount { get; set; }
}