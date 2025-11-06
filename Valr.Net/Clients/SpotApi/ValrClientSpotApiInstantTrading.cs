using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using Valr.Net.Endpoints.SpotApi;
using Valr.Net.Enums;
using Valr.Net.Interfaces.Clients.SpotApi;
using Valr.Net.Objects.Models.Spot.InstantTrading;

namespace Valr.Net.Clients.SpotApi
{
    public class ValrClientSpotApiInstantTrading : IValrClientSpotApiInstantTrading
    {
        private readonly ILogger _logger;
        private readonly ValrClientSpotApi _baseClient;

        internal ValrClientSpotApiInstantTrading(ILogger logger, ValrClientSpotApi valrClientSpotApi)
        {
            _baseClient = valrClientSpotApi;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<ValrInstantTradeStatusResponse>> GetInstantOrderStatusAsync(string currencyPair, Guid id, long? receiveWindow = null, CancellationToken ct = default)
        {
            string path = InstantTradingEndpoints.OrderStatus.Replace(":orderId", id.ToString())
                .Replace(":currencyPair", currencyPair);
            return await _baseClient.SendRequestInternal<ValrInstantTradeStatusResponse>(_baseClient.GetUrl(path),
                HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<ValrInstantTradeQuote>> GetQuoteAsync(string currencyPair, string symbol, ValrOrderSide side, decimal quantity, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("payInCurrency", symbol);
            parameters.AddParameter("payAmount", quantity);
            parameters.AddParameter("side", side);

            return await _baseClient.SendRequestInternal<ValrInstantTradeQuote>(_baseClient.GetUrl(InstantTradingEndpoints.Quote.Replace(":currencyPair", currencyPair)),
                HttpMethod.Post, ct, parameters: parameters, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<ValrInstantTradeResponse>> PlaceInstantOrderAsync(string currencyPair, string symbol, ValrOrderSide side, decimal quantity, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("payInCurrency", symbol);
            parameters.AddParameter("payAmount", quantity);
            parameters.AddParameter("side", side);

            return await _baseClient.SendRequestInternal<ValrInstantTradeResponse>(_baseClient.GetUrl(InstantTradingEndpoints.PlaceOrder.Replace(":currencyPair", currencyPair)),
                HttpMethod.Post, ct, parameters: parameters, signed: true).ConfigureAwait(false);
        }
    }
}
