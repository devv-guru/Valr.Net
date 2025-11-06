using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using Valr.Net.Clients.GeneralApi.Wallets;
using Valr.Net.Clients.SpotApi;
using Valr.Net.Interfaces.Clients.GeneralApi;
using Valr.Net.Interfaces.Clients.GeneralApi.Wallets;
using Valr.Net.Objects.Options;

namespace Valr.Net.Clients.GeneralApi
{
    public class ValrClientGeneralApi : RestApiClient, IValrClientGeneralApi
    {
        #region Fields
        /// <inheritdoc />
        public new ValrRestApiOptions ApiOptions => (ValrRestApiOptions)base.ApiOptions;
        /// <inheritdoc />
        public new ValrRestOptions ClientOptions => (ValrRestOptions)base.ClientOptions;

        private readonly ValrClient _baseClient;
        #endregion

        #region Api clients
        /// <inheritdoc />
        public IValrClientGeneralApiExchangeData ExchangeData { get; }

        /// <inheritdoc />
        public IValrClientGeneralApiAccount Account { get; }

        /// <inheritdoc />
        public IValrClientGeneralApiWallets Wallet { get; }

        /// <inheritdoc />
        public IValrClientGeneralApiSubAccount SubAccount { get; }
        #endregion

        #region constructor/destructor
        internal ValrClientGeneralApi(ILogger logger, HttpClient? httpClient, ValrClient baseClient, ValrRestOptions options)
            : base(logger, httpClient, options.Environment.SpotRestAddress, options, options.SpotOptions)
        {
            _baseClient = baseClient;

            ExchangeData = new ValrClientGeneralApiExchangeData(_logger, this);
            Account = new ValrClientGeneralApiAccount(_logger, this);
            Wallet = new ValrClientGeneralApiWallets(_logger, this);
            SubAccount = new ValrClientGeneralApiSubAccount(_logger, this);
        }

        #endregion

        #region Helpers
        internal Uri GetUrl(string endpoint)
        {
            return new Uri(BaseAddress.AppendPath(endpoint));
        }

        internal async Task<WebCallResult<T>> SendRequestInternal<T>(Uri uri, HttpMethod method, CancellationToken cancellationToken,
            Dictionary<string, object>? parameters = null, bool signed = false, HttpMethodParameterPosition? postPosition = null,
            ArrayParametersSerialization? arraySerialization = null, int weight = 1, bool ignoreRateLimit = false) where T : class
        {
            var result = await SendRequestAsync<T>(uri, method, cancellationToken, parameters, signed, postPosition, arraySerialization, requestWeight: weight, ignoreRatelimit: ignoreRateLimit).ConfigureAwait(false);
            if (!result && result.Error!.Code == -1021 && (ApiOptions.AutoTimestamp ?? ClientOptions.AutoTimestamp))
            {
                _logger.Log(LogLevel.Debug, "Received Invalid Timestamp error, triggering new time sync");
                ValrClientSpotApi.TimeSyncState.LastSyncTime = DateTime.MinValue;
            }
            return result;
        }

        public override TimeSyncInfo? GetTimeSyncInfo() =>
            new(_logger, (ApiOptions.AutoTimestamp ?? ClientOptions.AutoTimestamp), (ApiOptions.TimestampRecalculationInterval ?? ClientOptions.TimestampRecalculationInterval), ValrClientSpotApi.TimeSyncState);

        public override TimeSpan? GetTimeOffset() => ValrClientSpotApi.TimeSyncState.TimeOffset;

        protected override Task<WebCallResult<DateTime>> GetServerTimestampAsync() =>
            _baseClient.GeneralApi.ExchangeData.GetServerTimeAsync();

        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new ValrAuthenticationProvider(credentials);
        #endregion
    }
}
