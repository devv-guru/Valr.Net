using System.Text.Json.Serialization;

namespace Valr.Net.Objects.Models.General.ExchangeData
{
    public class ValrCurrency
    {
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }

        [JsonPropertyName("shortName")]
        public string ShortName { get; set; }

        [JsonPropertyName("longName")]
        public string LongName { get; set; }

        [JsonPropertyName("decimalPlaces")]
        public int DecimalPlaces { get; set; }

        [JsonPropertyName("withdrawalDecimalPlaces")]
        public int WithdrawalDecimalPlaces { get; set; }
    }

    public class ValrCurrencyWrapper
    {
        public ValrCurrency[] ValrCurrencies { get; set; }
    }

}
