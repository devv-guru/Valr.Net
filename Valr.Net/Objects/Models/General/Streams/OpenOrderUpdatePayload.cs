using System.Text.Json.Serialization;

using Valr.Net.Enums;

namespace Valr.Net.Objects.Models.General.Streams
{
    public class OpenOrderData
    {
        [JsonPropertyName("orderId")]
        public Guid orderId { get; set; }

        [JsonPropertyName("side")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ValrOrderSide Side { get; set; }

        [JsonPropertyName("remainingQuantity")]
        public decimal RemainingQuantity { get; set; }

        [JsonPropertyName("originalPrice")]
        public decimal OriginalPrice { get; set; }

        [JsonPropertyName("currencyPair")]
        public Currencypair CurrencyPair { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime Created { get; set; }

        [JsonPropertyName("originalQuantity")]
        public decimal OriginalQuantity { get; set; }

        [JsonPropertyName("filledPercentage")]
        public decimal FilledPercentage { get; set; }

        [JsonPropertyName("customerOrderId")]
        public string CustomerOrderId { get; set; }
    }

    public class Currencypair
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("baseCurrency")]
        public CurrencyInfo BaseCurrency { get; set; }

        [JsonPropertyName("quoteCurrency")]
        public CurrencyInfo QuoteCurrency { get; set; }

        [JsonPropertyName("shortName")]
        public string ShortName { get; set; }

        [JsonPropertyName("exchange")]
        public string Exchange { get; set; }

        [JsonPropertyName("active")]
        public bool Active { get; set; }

        [JsonPropertyName("minBaseAmount")]
        public decimal MinBaseAmount { get; set; }

        [JsonPropertyName("maxBaseAmount")]
        public decimal MaxBaseAmount { get; set; }

        [JsonPropertyName("minQuoteAmount")]
        public decimal MinQuoteAmount { get; set; }

        [JsonPropertyName("maxQuoteAmount")]
        public decimal MaxQuoteAmount { get; set; }

    }

    public class CurrencyInfo
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("decimalPlaces")]
        public int DecimalPlaces { get; set; }

        [JsonPropertyName("isActive")]
        public bool Active { get; set; }

        [JsonPropertyName("shortName")]
        public string ShortName { get; set; }

        [JsonPropertyName("longName")]
        public string LongName { get; set; }

        [JsonPropertyName("currencyDecimalPlaces")]
        public int CurrencyDecimalPlaces { get; set; }

        [JsonPropertyName("supportedWithdrawDecimalPlaces")]
        public int SupportedWithdrawDecimalPlaces { get; set; }
    }
}
