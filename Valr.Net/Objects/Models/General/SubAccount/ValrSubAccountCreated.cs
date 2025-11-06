using System.Text.Json.Serialization;

namespace Valr.Net.Objects.Models.General.SubAccount
{
    public class ValrSubAccountCreated
    {
        [JsonPropertyName("label")]
        public string Label { get; set; }
    }
}
