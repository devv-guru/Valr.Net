using System.Text.Json.Serialization;

namespace Valr.Net.Objects.Models.General.Streams
{
    public class ProcessedOrderData
    {
        [JsonPropertyName("orderId")]
        public Guid Id { get; set; }

        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("failureReason")]
        public string? Reason { get; set; }
    }

}
