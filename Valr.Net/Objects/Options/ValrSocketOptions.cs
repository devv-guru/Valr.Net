using CryptoExchange.Net.Objects.Options;

namespace Valr.Net.Objects.Options
{
    /// <summary>
    /// Options for the ValrSocketClient
    /// </summary>
    public class ValrSocketOptions : SocketExchangeOptions<ValrEnvironment>
    {
        /// <summary>
        /// Default options for new clients
        /// </summary>
        internal static ValrSocketOptions Default { get; set; } = new ValrSocketOptions()
        {
            Environment = ValrEnvironment.Live,
            SocketSubscriptionsCombineTarget = 10
        };

        /// <summary>
        /// ctor
        /// </summary>
        public ValrSocketOptions()
        {
            Default?.Set(this);
        }

        /// <summary>
        /// Options for the Spot Streams API
        /// </summary>
        public ValrSocketApiOptions SpotStreamsOptions { get; private set; } = new ValrSocketApiOptions();

        /// <summary>
        /// Options for the General Streams API
        /// </summary>
        public ValrSocketApiOptions GeneralStreamsOptions { get; private set; } = new ValrSocketApiOptions();

        internal ValrSocketOptions Set(ValrSocketOptions targetOptions)
        {
            targetOptions = base.Set<ValrSocketOptions>(targetOptions);
            targetOptions.SpotStreamsOptions = SpotStreamsOptions.Set(targetOptions.SpotStreamsOptions);
            targetOptions.GeneralStreamsOptions = GeneralStreamsOptions.Set(targetOptions.GeneralStreamsOptions);
            return targetOptions;
        }
    }
}
