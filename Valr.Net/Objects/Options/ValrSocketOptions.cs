using System;
using CryptoExchange.Net.Objects;

namespace Valr.Net.Objects.Options
{
    // TODO: This is a migration stub. When upgrading to CryptoExchange.Net 9.x,
    // this class should inherit from the new SocketExchangeOptions<T> / SocketClientOptions
    // types and map existing `ValrSocketClientOptions` properties into the new option model.

    /// <summary>
    /// New socket options surface for Valr clients (stub).
    /// </summary>
    public class ValrSocketOptions : BaseSocketClientOptions
    {
        // TODO: inherit from appropriate CryptoExchange.Net 9.x base type

        /// <summary>
        /// Default socket options instance placeholder
        /// </summary>
        public static ValrSocketOptions Default { get; set; } = new ValrSocketOptions();

        /// <summary>
        /// Socket subscription combine target (maps from old option)
        /// </summary>
        public int SocketSubscriptionsCombineTarget { get; set; } = 10;

        // TODO: add mapping constructors from existing `ValrSocketClientOptions`

        public ValrSocketOptions() : base() { }

        internal ValrSocketOptions(ValrSocketClientOptions baseOptions) : base()
        {
            if (baseOptions == null)
                return;

            SocketSubscriptionsCombineTarget = baseOptions.SocketSubscriptionsCombineTarget;
        }
    }
}
