using System.Text.Json.Serialization;

namespace Valr.Net.Objects.Models.General.Streams
{
    public class BalanceUpdateData
    {
        [JsonPropertyName("currency")]
        public CurrencyInfo Currency { get; set; }

        [JsonPropertyName("available")]
        public decimal Available { get; set; }

        [JsonPropertyName("reserved")]
        public decimal Reserved { get; set; }

        [JsonPropertyName("total")]
        public decimal Total { get; set; }

        [JsonPropertyName("updatedAt")]
        public DateTime ChangeTime { get; set; }
    }
}
