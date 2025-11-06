using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Valr.Net.Objects.Options;
using CryptoExchange.Net.Authentication;

namespace Valr.Net.Clients.SpotApi
{
    /// <summary>
    /// Migration scaffold for a CryptoExchange.Net 9.x-style Spot client.
    /// Implements a small set of real HTTP calls to kick off the port.
    /// </summary>
    public class ValrSpotClientV9 : IDisposable
    {
        public ValrRestOptions Options { get; }
        private readonly HttpClient _http;
        private readonly ValrAuthenticationProvider? _authProvider;
        private readonly string _baseAddress;

        public ValrSpotClientV9() : this(ValrRestOptions.Default, null)
        {
        }

        public ValrSpotClientV9(ValrRestOptions options, ApiCredentials? credentials)
        {
            Options = options ?? throw new ArgumentNullException(nameof(options));
            _http = new HttpClient();
            _baseAddress = Valr.Net.Objects.ValrApiAddresses.Default.RestClientAddress.TrimEnd('/');

            if (credentials != null)
                _authProvider = new ValrAuthenticationProvider(credentials);
        }

        /// <summary>
        /// Get market summary / ticker for a symbol. Uses the public VALR endpoint.
        /// Returns raw JSON string for now; will map to typed models in the next iteration.
        /// </summary>
        public async Task<string> GetTickerAsync(string symbol, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(symbol))
                throw new ArgumentException("symbol");

            var path = $"/v1/public/markets/{symbol}/summary";
            var url = _baseAddress + path;

            using var res = await _http.GetAsync(url, ct).ConfigureAwait(false);
            res.EnsureSuccessStatusCode();

            var json = await res.Content.ReadAsStringAsync(ct).ConfigureAwait(false);
            return json;
        }

        /// <summary>
        /// Place a limit order using the VALR private API. Requires credentials passed at construction.
        /// Returns raw response JSON for now.
        /// </summary>
        public async Task<string> PlaceLimitOrderAsync(string symbol, decimal quantity, decimal price, CancellationToken ct = default)
        {
            if (_authProvider == null)
                throw new InvalidOperationException("Credentials not provided. Cannot place authenticated orders.");

            var path = "/v1/orders";
            var url = _baseAddress + path;

            var body = new
            {
                currencyPair = symbol,
                orderType = "LIMIT",
                side = "BUY",
                quantity = quantity,
                price = price
            };

            // Let ValrAuthenticationProvider generate headers for the request path
            var headers = _authProvider.GetAuthenticationHeaders(path);

            using var req = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = JsonContent.Create(body)
            };

            foreach (var h in headers)
                req.Headers.TryAddWithoutValidation(h.Key, h.Value);

            using var res = await _http.SendAsync(req, ct).ConfigureAwait(false);
            res.EnsureSuccessStatusCode();
            var json = await res.Content.ReadAsStringAsync(ct).ConfigureAwait(false);
            return json;
        }

        public void Dispose()
        {
            _http.Dispose();
        }
    }
}
