using System.Text.Json.Serialization;

namespace Valr.Net.Objects.Models.General.Streams
{
    public class NewTransactionData
    {
        [JsonPropertyName("transactionType")]
        public Transactiontype TransactionType { get; set; }

        [JsonPropertyName("debitCurrency")]
        public CurrencyData DebitCurrency { get; set; }

        [JsonPropertyName("debitValue")]
        public decimal DebitValue { get; set; }

        [JsonPropertyName("creditCurrency")]
        public CurrencyData CreditCurrency { get; set; }

        [JsonPropertyName("creditValue")]
        public decimal CreditValue { get; set; }

        [JsonPropertyName("feeCurrency")]
        public CurrencyData FeeInfo { get; set; }

        [JsonPropertyName("feeValue")]
        public decimal FeeValue { get; set; }

        [JsonPropertyName("eventAt")]
        public DateTime TransactionDate { get; set; }

        [JsonPropertyName("additionalInfo")]
        public Additionalinfo AdditionalInfo { get; set; }

    }

    public class Transactiontype
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }

    public class CurrencyData
    {
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("decimalPlaces")]
        public int DecimalPlaces { get; set; }

        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }

        [JsonPropertyName("shortName")]
        public string ShortName { get; set; }

        [JsonPropertyName("longName")]
        public string LongName { get; set; }

        [JsonPropertyName("supportedWithdrawDecimalPlaces")]
        public int SupportedWithdrawDecimalPlaces { get; set; }
    }

    public class Additionalinfo
    {
        [JsonPropertyName("costPerCoin")]
        public int CostPerCoin { get; set; }

        [JsonPropertyName("costPerCoinSymbol")]
        public string CostPerCoinSymbol { get; set; }

        [JsonPropertyName("currencyPairSymbol")]
        public string CurrencyPairSymbol { get; set; }
    }
}
