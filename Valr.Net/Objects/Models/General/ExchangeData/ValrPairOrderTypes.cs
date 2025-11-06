using System.Text.Json.Serialization;
using Valr.Net.Enums;

namespace Valr.Net.Objects.Models.General.ExchangeData
{
    public class ValrPairOrderTypes
    {
        [JsonPropertyName("currencyPair")]
        public string CurrencyPair { get; set; }

        [JsonPropertyName("orderTypes")]
        public ValrOrderType[] OrderTypes { get; set; }
    }

    public class PairOrderTypesWrapper
    {
        public ValrPairOrderTypes[] PairOrderTypes { get; set; }
    }
}
