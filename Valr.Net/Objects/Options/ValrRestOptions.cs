using CryptoExchange.Net.Objects.Options;

namespace Valr.Net.Objects.Options
{
    /// <summary>
    /// Options for the ValrRestClient
    /// </summary>
    public class ValrRestOptions : RestExchangeOptions<ValrEnvironment>
    {
        /// <summary>
        /// Default options for new clients
        /// </summary>
        internal static ValrRestOptions Default { get; set; } = new ValrRestOptions()
        {
            Environment = ValrEnvironment.Live,
            AutoTimestamp = true
        };

        /// <summary>
        /// ctor
        /// </summary>
        public ValrRestOptions()
        {
            Default?.Set(this);
        }

        /// <summary>
        /// The default receive window for requests
        /// </summary>
        public TimeSpan ReceiveWindow { get; set; } = TimeSpan.FromSeconds(5);

        /// <summary>
        /// Spot API options
        /// </summary>
        public ValrRestApiOptions SpotOptions { get; private set; } = new ValrRestApiOptions();

        internal ValrRestOptions Set(ValrRestOptions targetOptions)
        {
            targetOptions = base.Set<ValrRestOptions>(targetOptions);
            targetOptions.ReceiveWindow = ReceiveWindow;
            targetOptions.SpotOptions = SpotOptions.Set(targetOptions.SpotOptions);
            return targetOptions;
        }
    }
}
