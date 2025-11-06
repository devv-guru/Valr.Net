using CryptoExchange.Net.Objects.Options;

namespace Valr.Net.Objects.Options
{
    /// <summary>
    /// Options for Valr Rest API
    /// </summary>
    public class ValrRestApiOptions : RestApiOptions
    {
        /// <summary>
        /// A manual offset for the timestamp. Should only be used if AutoTimestamp and regular time synchronization on the OS is not reliable enough
        /// </summary>
        public TimeSpan TimestampOffset { get; set; } = TimeSpan.Zero;

        internal ValrRestApiOptions Set(ValrRestApiOptions targetOptions)
        {
            targetOptions = base.Set<ValrRestApiOptions>(targetOptions);
            targetOptions.TimestampOffset = TimestampOffset;
            return targetOptions;
        }
    }
}
