using System.Text.Json.Serialization;

namespace Valr.Net.Objects.Models.Spot.Trading
{
    public class ValrOrderStatusResponse : ValrOrderResponseBase
    {
        [JsonPropertyName("failedReason")]
        public string FailedReason { get; set; }

        [JsonPropertyName("customerOrderId")]
        public int CustomerOrderId { get; set; }
    }
}
