using CryptoExchange.Net.Objects.Options;

namespace Valr.Net.Objects.Options
{
    /// <summary>
    /// Options for Valr Socket API
    /// </summary>
    public class ValrSocketApiOptions : SocketApiOptions
    {
        internal ValrSocketApiOptions Set(ValrSocketApiOptions targetOptions)
        {
            targetOptions = base.Set<ValrSocketApiOptions>(targetOptions);
            return targetOptions;
        }
    }
}
