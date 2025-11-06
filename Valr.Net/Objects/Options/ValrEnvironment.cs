using CryptoExchange.Net.Objects;
using Valr.Net.Objects;

namespace Valr.Net.Objects.Options
{
    /// <summary>
    /// Valr environments
    /// </summary>
    public class ValrEnvironment : TradeEnvironment
    {
        /// <summary>
        /// Spot Rest API address
        /// </summary>
        public string SpotRestAddress { get; }

        /// <summary>
        /// Spot WebSocket address (trade streams)
        /// </summary>
        public string SpotSocketAddress { get; }

        /// <summary>
        /// General WebSocket address (account streams)
        /// </summary>
        public string GeneralSocketAddress { get; }

        /// <summary>
        /// ctor for DI, use <see cref="CreateCustom"/> for creating a custom environment
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public ValrEnvironment() : base(TradeEnvironmentNames.Live)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        { }

        /// <summary>
        /// Get the Valr environment by name
        /// </summary>
        public static ValrEnvironment? GetEnvironmentByName(string? name)
         => name switch
         {
             TradeEnvironmentNames.Live => Live,
             "" => Live,
             null => Live,
             _ => default
         };

        internal ValrEnvironment(
            string name,
            string spotRestAddress,
            string spotSocketAddress,
            string generalSocketAddress) :
            base(name)
        {
            SpotRestAddress = spotRestAddress;
            SpotSocketAddress = spotSocketAddress;
            GeneralSocketAddress = generalSocketAddress;
        }

        /// <summary>
        /// Available environment names
        /// </summary>
        public static string[] All => [Live.Name];

        /// <summary>
        /// Live environment
        /// </summary>
        public static ValrEnvironment Live { get; }
            = new ValrEnvironment(TradeEnvironmentNames.Live,
                                 ValrApiAddresses.Default.RestClientAddress,
                                 ValrApiAddresses.Default.SpotSocketClientAddress,
                                 ValrApiAddresses.Default.GeneralSocketClientAddress);

        /// <summary>
        /// Create a custom environment
        /// </summary>
        public static ValrEnvironment CreateCustom(
                        string name,
                        string spotRestAddress,
                        string spotSocketAddress,
                        string generalSocketAddress)
            => new ValrEnvironment(name, spotRestAddress, spotSocketAddress, generalSocketAddress);
    }
}
