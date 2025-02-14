﻿using CryptoExchange.Net.Objects;

namespace Valr.Net.Objects.Options
{
    public class ValrSocketClientOptions : BaseSocketClientOptions
    {
        /// <summary>
        /// Default options for the spot client
        /// </summary>
        public static ValrSocketClientOptions Default { get; set; } = new ValrSocketClientOptions()
        {
            SocketSubscriptionsCombineTarget = 10
        };

        private ApiClientOptions _spotStreamsOptions = new ApiClientOptions(ValrApiAddresses.Default.SpotSocketClientAddress);
        /// <summary>
        /// Spot streams options
        /// </summary>
        public ApiClientOptions SpotStreamsOptions
        {
            get => _spotStreamsOptions;
            set => _spotStreamsOptions = new ApiClientOptions(_spotStreamsOptions, value);
        }

        private ApiClientOptions _generalStreamsOptions = new ApiClientOptions(ValrApiAddresses.Default.GeneralSocketClientAddress);
        /// <summary>
        /// Spot streams options
        /// </summary>
        public ApiClientOptions GeneralStreamsOptions
        {
            get => _generalStreamsOptions;
            set => _generalStreamsOptions = new ApiClientOptions(_generalStreamsOptions, value);
        }

        /// <summary>
        /// ctor
        /// </summary>
        public ValrSocketClientOptions() : this(Default)
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="baseOn">Base the new options on other options</param>
        internal ValrSocketClientOptions(ValrSocketClientOptions baseOn) : base(baseOn)
        {
            if (baseOn == null)
                return;

            _spotStreamsOptions = new ApiClientOptions(baseOn.SpotStreamsOptions, null);
            _generalStreamsOptions = new ApiClientOptions(baseOn.GeneralStreamsOptions, null);
        }
    }
}
