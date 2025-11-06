using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;
using Valr.Net.Clients.GeneralApi;
using Valr.Net.Clients.PayApi;
using Valr.Net.Clients.SpotApi;
using Valr.Net.Interfaces.Clients;
using Valr.Net.Interfaces.Clients.GeneralApi;
using Valr.Net.Interfaces.Clients.PayApi;
using Valr.Net.Interfaces.Clients.SpotApi;
using Valr.Net.Objects.Options;

namespace Valr.Net.Clients
{
    public class ValrClient : BaseRestClient, IValrClient
    {
        #region Api clients

        /// <inheritdoc />
        public IValrClientGeneralApi GeneralApi { get; }
        /// <inheritdoc />
        public IValrClientSpotApi SpotApi { get; }
        /// <inheritdoc />
        public IValrClientPayApi PaymentApi { get; }
        #endregion

        #region constructor/destructor
        /// <summary>
        /// Create a new instance of ValrClient using provided options
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public ValrClient(Action<ValrRestOptions>? optionsDelegate = null)
            : this(null, null, Options.Create(ApplyOptionsDelegate(optionsDelegate)))
        {
        }

        /// <summary>
        /// Create a new instance of ValrClient
        /// </summary>
        /// <param name="httpClient">Http client for this client</param>
        /// <param name="loggerFactory">The logger factory</param>
        /// <param name="options">Option configuration</param>
        public ValrClient(HttpClient? httpClient, ILoggerFactory? loggerFactory, IOptions<ValrRestOptions> options)
            : base(loggerFactory, "Valr")
        {
            Initialize(options.Value);

            GeneralApi = AddApiClient(new ValrClientGeneralApi(_logger, httpClient, this, options.Value));
            SpotApi = AddApiClient(new ValrClientSpotApi(_logger, httpClient, this, options.Value));
            PaymentApi = AddApiClient(new ValrClientPayApi(_logger, httpClient, this, options.Value));
        }
        #endregion

        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public static void SetDefaultOptions(Action<ValrRestOptions> optionsDelegate)
        {
            ValrRestOptions.Default = ApplyOptionsDelegate(optionsDelegate);
        }

        /// <inheritdoc />
        protected override Error ParseErrorResponse(JsonElement error)
        {
            try
            {
                if (error.ValueKind == JsonValueKind.Undefined || error.ValueKind == JsonValueKind.Null)
                    return new ServerError(error.ToString());

                if (!error.TryGetProperty("message", out var msg) && !error.TryGetProperty("code", out var code))
                    return new ServerError(error.ToString());

                if (error.TryGetProperty("message", out var message) && !error.TryGetProperty("code", out var _))
                    return new ServerError(message.GetString() ?? error.ToString());

                var c = error.TryGetProperty("code", out var codeProp) ? codeProp.GetInt32() : 0;
                var m = error.TryGetProperty("message", out var messageProp) ? messageProp.GetString() ?? string.Empty : string.Empty;
                return new ServerError(c, m);
            }
            catch
            {
                return new ServerError(error.ToString());
            }
        }

        internal Task<WebCallResult<T>> SendRequestInternal<T>(RestApiClient apiClient, Uri uri, HttpMethod method, CancellationToken cancellationToken,
            Dictionary<string, object>? parameters = null, bool signed = false, HttpMethodParameterPosition? postPosition = null,
            ArrayParametersSerialization? arraySerialization = null, int weight = 1, bool ignoreRateLimit = false) where T : class
        {
            return base.SendRequestAsync<T>(apiClient, uri, method, cancellationToken, parameters, signed, postPosition, arraySerialization, requestWeight: weight, ignoreRatelimit: ignoreRateLimit);
        }

        internal Task<WebCallResult> SendRequestInternal(RestApiClient apiClient, Uri uri, HttpMethod method, CancellationToken cancellationToken,
            Dictionary<string, object>? parameters = null, bool signed = false, HttpMethodParameterPosition? postPosition = null,
            ArrayParametersSerialization? arraySerialization = null, int weight = 1, bool ignoreRateLimit = false)
        {
            return base.SendRequestAsync(apiClient, uri, method, cancellationToken, parameters, signed, postPosition, arraySerialization, requestWeight: weight, ignoreRatelimit: ignoreRateLimit);
        }
    }
}
