using System.Text.Json.Serialization;

namespace Valr.Net.Objects.Models.General.SubAccount
{
    public class ValrSubAccount
    {
        [JsonPropertyName("label")]
        public string Label { get; set; }
        [JsonPropertyName("id")]
        public string Id { get; set; }
    }

    internal class ValrSubAccountWrapper
    {
        public IEnumerable<ValrSubAccount>? SubAccounts { get; set; }
    }
}
