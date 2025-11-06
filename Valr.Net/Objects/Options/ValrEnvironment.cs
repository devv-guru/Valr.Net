namespace Valr.Net.Objects.Options
{
    // TODO: Replacement for ValrApiAddresses to model environment endpoints in a way that maps
    // to CryptoExchange.Net 9.x ApiClient / Environment patterns.

    public class ValrEnvironment
    {
        public string RestClientAddress { get; set; } = "https://api.valr.com";
        public string SpotSocketClientAddress { get; set; } = "wss://ws.valr.com/spot";
        public string GeneralSocketClientAddress { get; set; } = "wss://ws.valr.com/general";

        public static ValrEnvironment Default { get; } = new ValrEnvironment();

        // TODO: Add constructors/factories for Test/Sandbox/Custom environments
    }
}
