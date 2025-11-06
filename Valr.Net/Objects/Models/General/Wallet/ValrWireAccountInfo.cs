using System.Text.Json.Serialization;

namespace Valr.Net.Objects.Models.General.Wallet
{
    public class ValrWireAccountInfo
    {
        [JsonPropertyName("")]
        public Guid id { get; set; }

        [JsonPropertyName("accountNumber")]
        public string AccountNumber { get; set; }

        [JsonPropertyName("routingNumber")]
        public string RoutingNumber { get; set; }

        [JsonPropertyName("billingDetails")]
        public Billingdetails BillingDetails { get; set; }

        [JsonPropertyName("bankAddress")]
        public Bankaddress BankAddress { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime Created { get; set; }
    }

    public class Billingdetails
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("city")]
        public string? City { get; set; }

        [JsonPropertyName("country")]
        public string? Country { get; set; }

        [JsonPropertyName("line1")]
        public string? Line1 { get; set; }

        [JsonPropertyName("line2")]
        public string? Line2 { get; set; }

        [JsonPropertyName("district")]
        public string? District { get; set; }

        [JsonPropertyName("postalCode")]
        public string? PostalCode { get; set; }
    }

    public class Bankaddress
    {
        [JsonPropertyName("bankName")]
        public string BankName { get; set; }

        [JsonPropertyName("city")]
        public string? City { get; set; }

        [JsonPropertyName("country")]
        public string? Country { get; set; }

        [JsonPropertyName("line1")]
        public string? Line1 { get; set; }

        [JsonPropertyName("line2")]
        public string? Line2 { get; set; }

        [JsonPropertyName("district")]
        public string? District { get; set; }
    }

}
