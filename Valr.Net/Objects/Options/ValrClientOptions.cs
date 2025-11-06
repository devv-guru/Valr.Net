using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;

namespace Valr.Net.Objects.Options
{
    public class ValrClientOptions : BaseRestClientOptions
    {
        /// <summary>
        /// Default options for the spot client
        /// </summary>
        public static ValrClientOptions Default { get; set; } = new ValrClientOptions();

        /// <summary>
        /// The default receive window for requests
        /// </summary>
        public TimeSpan ReceiveWindow { get; set; } = TimeSpan.FromSeconds(5);

        private ValrApiClientOptions _spotApiOptions = new ValrApiClientOptions(ValrApiAddresses.Default.RestClientAddress)
        {
            AutoTimestamp = true,
            RateLimiters = new List<IRateLimiter>
            {
                new RateLimiter()
                    .AddApiKeyLimit(500, TimeSpan.FromMinutes(1),true,false)
                    .AddTotalRateLimit( 500, TimeSpan.FromMinutes(1))
            }
        };

        private ValrApiClientOptions _payApiOptions = new ValrApiClientOptions(ValrApiAddresses.Default.RestClientAddress)
        {
            AutoTimestamp = true,
            RateLimiters = new List<IRateLimiter>
            {
                new RateLimiter()
                    .AddApiKeyLimit(500, TimeSpan.FromMinutes(1),true,false)
                    .AddTotalRateLimit( 500, TimeSpan.FromMinutes(1))
            }
        };

        private ValrApiClientOptions _generalApiOptions = new ValrApiClientOptions(ValrApiAddresses.Default.RestClientAddress)
        {
            AutoTimestamp = true,
            RateLimiters = new List<IRateLimiter>
            {
                new RateLimiter()
                    .AddApiKeyLimit(500, TimeSpan.FromMinutes(1),true,false)
                    .AddTotalRateLimit( 500, TimeSpan.FromMinutes(1))
            }
        };

        /// <summary>
        /// General API options
        /// </summary>
        public ValrApiClientOptions GeneralApiOptions
        {
            get => _generalApiOptions;
            set => _generalApiOptions = new ValrApiClientOptions(_generalApiOptions, value);
        }

        /// <summary>
        /// Spot API options
        /// </summary>
        public ValrApiClientOptions SpotApiOptions
        {
            get => _spotApiOptions;
            set => _spotApiOptions = new ValrApiClientOptions(_spotApiOptions, value);
        }

        /// <summary>
        /// Pay API options
        /// </summary>
        public ValrApiClientOptions PayApiOptions
        {
            get => _payApiOptions;
            set => _payApiOptions = new ValrApiClientOptions(_payApiOptions, value);
        }

        /// <summary>
        /// ctor
        /// </summary>
        public ValrClientOptions() : this(Default)
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="baseOn">Base the new options on other options</param>
        internal ValrClientOptions(ValrClientOptions baseOn) : base(baseOn)
        {
            if (baseOn == null)
                return;

            ReceiveWindow = baseOn.ReceiveWindow;

            _spotApiOptions = new ValrApiClientOptions(baseOn.SpotApiOptions, null);
            _payApiOptions = new ValrApiClientOptions(baseOn.PayApiOptions, null);
            _generalApiOptions = new ValrApiClientOptions(baseOn.GeneralApiOptions, null);
        }

        /// <summary>
        /// Create a migration facade to the new ValrRestOptions. This is an incremental helper used while
        /// porting to CryptoExchange.Net 9.x; the actual inheritance will point to 9.x base types later.
        /// </summary>
        public ValrRestOptions ToValrRestOptions()
        {
            return new ValrRestOptions(this);
        }
    }
}
