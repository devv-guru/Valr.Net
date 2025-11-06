using System.Text.Json.Serialization;

namespace Valr.Net.Objects.Models.Spot.Streams;

public class NewTradeBucketData
{
    [JsonPropertyName("currencyPairSymbol")]
    public string Symbol { get; set; }

    [JsonPropertyName("bucketPeriodInSeconds")]
    public int BucketPeriod { get; set; }

    [JsonPropertyName("startTime")]
    public DateTime StartTime { get; set; }

    [JsonPropertyName("open")]
    public decimal Open { get; set; }

    [JsonPropertyName("high")]
    public decimal High { get; set; }

    [JsonPropertyName("low")]
    public decimal Low { get; set; }

    [JsonPropertyName("close")]
    public decimal Close { get; set; }

    [JsonPropertyName("volume")]
    public decimal Volume { get; set; }

}