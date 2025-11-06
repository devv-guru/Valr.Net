using System.Text.Json.Serialization;

namespace Valr.Net.Objects.Models.General.Wallet
{
    public class ValrWireDepositInstructions
    {
        [JsonPropertyName("trackingReference")]
        public string TrackingReference { get; set; }

        [JsonPropertyName("beneficiary")]
        public Beneficiary Beneficiary { get; set; }

        [JsonPropertyName("beneficiaryBank")]
        public BeneficiaryBank BeneficiaryBank { get; set; }
    }

    public class Beneficiary
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("address1")]
        public string? Address1 { get; set; }

        [JsonPropertyName("address2")]
        public string? Address2 { get; set; }
    }

    public class BeneficiaryBank
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("swiftCode")]
        public string? SwiftCode { get; set; }

        [JsonPropertyName("routingNumber")]
        public string? RoutingNumber { get; set; }

        [JsonPropertyName("accountNumber")]
        public string? AccountNumber { get; set; }

        [JsonPropertyName("address")]
        public string? Address { get; set; }

        [JsonPropertyName("city")]
        public string? City { get; set; }

        [JsonPropertyName("postalCode")]
        public string? PostalCode { get; set; }

        [JsonPropertyName("country")]
        public string? Country { get; set; }
    }

}
