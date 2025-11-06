using System.Text.Json.Serialization;

namespace Valr.Net.Objects.Models.General.Streams
{
    public class CryptoWithdrawalStatusData
    {
        [JsonPropertyName("uniqueId")]
        public Guid Id { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("confirmations")]
        public int Confirmations { get; set; }
    }

}
