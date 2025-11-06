using System.Text.Json.Serialization;
using Valr.Net.Enums;

namespace Valr.Net.Objects.Models.General.Account
{
    public class ValrAccountTransaction
    {
        [JsonPropertyName("transactionType")]
        public Transactiontype TransactionType { get; set; }

        [JsonPropertyName("debitCurrency")]
        public string DebitCurrency { get; set; }

        [JsonPropertyName("debitValue")]
        public string DebitValue { get; set; }

        [JsonPropertyName("feeCurrency")]
        public string FeeCurrency { get; set; }

        [JsonPropertyName("feeValue")]
        public decimal Fee { get; set; }

        [JsonPropertyName("eventAt")]
        public DateTime TransactionTime { get; set; }

        [JsonPropertyName("additionalInfo")]
        public Additionalinfo AdditionalInfo { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }
    }


    public class AccountTransactionWrapper
    {
        public ValrAccountTransaction[] Transactions { get; set; }
    }

    public class Transactiontype
    {
        [JsonPropertyName("type")]
        public ValrTransactionType Type { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }

    public class Additionalinfo
    {
        [JsonPropertyName("bankName")]
        public string BankName { get; set; }

        [JsonPropertyName("accountNumber")]
        public string AccountNumber { get; set; }
    }

}
