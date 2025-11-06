using System.Text.Json.Serialization;

namespace Valr.Net.Objects.Models.General.Wallet
{
    public class ValrWithdrawalId
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
    }
}
