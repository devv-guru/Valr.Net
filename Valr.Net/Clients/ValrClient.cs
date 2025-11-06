using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json.Linq;
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
        /// Create a new instance of ValrClient using the default options
        /// </summary>
        public ValrClient() : this(ValrClientOptions.Default)
        {
        }

        /// <summary>
        /// Create a new instance of ValrClient using provided options
        /// </summary>
        /// <param name="options">The options to use for this client</param>
        public ValrClient(ValrClientOptions options) : base("Valr", options)
        {
            GeneralApi = AddApiClient(new ValrClientGeneralApi(_logger, this, options));
            SpotApi = AddApiClient(new ValrClientSpotApi(_logger, this, options));
            PaymentApi = AddApiClient(new ValrClientPayApi(_logger, this, options));

            //requestBodyEmptyContent = "";
            //requestBodyFormat = RequestBodyFormat.Json;
            //arraySerialization = ArrayParametersSerialization.MultipleValues;
        }
        #endregion

        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="options">Options to use as default</param>
        public static void SetDefaultOptions(ValrClientOptions options)
        {
            ValrClientOptions.Default = options;
        }

        /// <inheritdoc />
        protected override Error ParseErrorResponse(JToken error)
        {
            if (!error.HasValues)
                return new ServerError(error.ToString());

            if (error["message"] == null && error["code"] == null)
                return new ServerError(error.ToString());

            if (error["message"] != null && error["code"] == null)
                return new ServerError((string)error["msg"]!);

            return new ServerError((int)error["code"]!, (string)error["message"]!);
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
