using System.Text.Json.Serialization;

namespace Valr.Net.Objects.Models.Spot.Streams;

public class FullOrderBookData
{
    [JsonPropertyName("LastChange")]
    public long LastChange { get; set; }

    [JsonPropertyName("Asks")]
    public Order[] Asks { get; set; }

    [JsonPropertyName("Bids")]
    public Order[] Bids { get; set; }

    [JsonPropertyName("SequenceNumber")]
    public int SequenceNumber { get; set; }
}

public class Order
{
    [JsonPropertyName("Price")]
    public decimal Price { get; set; }

    [JsonPropertyName("Orders")]
    public OrderDetails[] Orders { get; set; }
}

public class OrderDetails
{
    [JsonPropertyName("orderId")]
    public Guid Id { get; set; }

    [JsonPropertyName("quantity")]
    public decimal Quantity { get; set; }
}
