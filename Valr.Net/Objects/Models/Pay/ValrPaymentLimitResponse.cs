using System.Text.Json.Serialization;

namespace Valr.Net.Objects.Models.Pay
{
    public class ValrPaymentLimitResponse
    {
        [JsonPropertyName("maxPaymentAmount")]
        public decimal MaxPaymentAmount { get; set; }

        [JsonPropertyName("minPaymentAmount")]
        public decimal MinPaymentAmount { get; set; }

        [JsonPropertyName("paymentCurrency")]
        public string PaymentCurrency { get; set; }

        [JsonPropertyName("limitType")]
        public string LimitType { get; set; }
    }
}
