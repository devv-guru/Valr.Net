using System.Text.Json.Serialization;

namespace Valr.Net.Objects.Models.General.Streams
{
    public class FailedOrderCancellationData
    {
        [JsonPropertyName("orderId")]
        public Guid OrderId { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }
    }

}
