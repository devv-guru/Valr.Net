using System.Text.Json.Serialization;

namespace Valr.Net.Objects.Models.General.Wallet
{
    public class ValrWalletAddress
    {
        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }
    }
}
