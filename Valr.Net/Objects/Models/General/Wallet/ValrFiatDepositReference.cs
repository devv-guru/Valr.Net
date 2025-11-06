using System.Text.Json.Serialization;

namespace Valr.Net.Objects.Models.General.Wallet
{
    public class ValrFiatDepositReference
    {
        [JsonPropertyName("reference")]
        public string Reference { get; set; }
    }
}
