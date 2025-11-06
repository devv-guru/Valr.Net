using System.Text.Json.Serialization;

namespace Valr.Net.Objects.Models.Pay
{
    public class ValrPaymentIdResponse
    {
        [JsonPropertyName("payId")]
        public string PayId { get; set; }

    }
}
