using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using Valr.Net.Endpoints.GeneralApi;
using Valr.Net.Enums;
using Valr.Net.Interfaces.Clients.GeneralApi;
using Valr.Net.Objects.Models.General.ExchangeData;

namespace Valr.Net.Clients.GeneralApi
{
    public class ValrClientGeneralApiExchangeData : IValrClientGeneralApiExchangeData
    {
        private readonly ILogger _logger;
        private readonly ValrClientGeneralApi _baseClient;

        internal ValrClientGeneralApiExchangeData(ILogger logger, ValrClientGeneralApi valrClientGeneralApi)
        {
            _logger = logger;
            _baseClient = valrClientGeneralApi;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<ValrOrderBook>> GetAuthenticatedOrderBookAggregatedAsync(string currencyPair, long? receiveWindow = null, CancellationToken ct = default)
        {
            return await _baseClient.SendRequestInternal<ValrOrderBook>(_baseClient.GetUrl(ExchangeDataEndpoints.OrderBookAuth.Replace(":currencyPair", currencyPair)),
                HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<ValrOrderBook>> GetAuthenticatedOrderBookFullAsync(string currencyPair, long? receiveWindow = null, CancellationToken ct = default)
        {
            return await _baseClient.SendRequestInternal<ValrOrderBook>(_baseClient.GetUrl(ExchangeDataEndpoints.OrderBookFullAuth.Replace(":currencyPair", currencyPair)),
                HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<ValrMarketSummary>>> GetMarketSummariesAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            return await _baseClient.SendRequestInternal<IEnumerable<ValrMarketSummary>>(_baseClient.GetUrl(ExchangeDataEndpoints.MarketSummary),
                HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<ValrMarketSummary>> GetMarketSummaryForPairAsync(string currencyPair, long? receiveWindow = null, CancellationToken ct = default)
        {
            return await _baseClient.SendRequestInternal<ValrMarketSummary>(_baseClient.GetUrl(ExchangeDataEndpoints.MarketSummaryForPair.Replace(":currencyPair", currencyPair)),
                HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<ValrPairOrderTypes>>> GetOrderTypesByPairAsync(string? currencyPair = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var result = await _baseClient.SendRequestInternal<IEnumerable<ValrPairOrderTypes>>(_baseClient.GetUrl(ExchangeDataEndpoints.OrderTypes),
                HttpMethod.Get, ct).ConfigureAwait(false);

            if (currencyPair is not null && result.Success)
            {
                return result.As(result.Data.Where(x => x.CurrencyPair == currencyPair));
            }

            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<ValrOrderBook>> GetPublicOrderBookAggregatedAsync(string currencyPair, long? receiveWindow = null, CancellationToken ct = default)
        {
            return await _baseClient.SendRequestInternal<ValrOrderBook>(_baseClient.GetUrl(ExchangeDataEndpoints.OrderBook.Replace(":currencyPair", currencyPair)),
                HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<ValrOrderBook>> GetPublicOrderBookFullAsync(string currencyPair, long? receiveWindow = null, CancellationToken ct = default)
        {
            return await _baseClient.SendRequestInternal<ValrOrderBook>(_baseClient.GetUrl(ExchangeDataEndpoints.OrderBookFull.Replace(":currencyPair", currencyPair)),
                HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
        {
            var result = await _baseClient.SendRequestInternal<ValrCheckTime>(_baseClient.GetUrl(ExchangeDataEndpoints.ServerTime), HttpMethod.Get, ct).ConfigureAwait(false);
            return result.As(result.Data?.time ?? default);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<ValrCurrency>>> GetSupportedCurrenciesAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            return await _baseClient.SendRequestInternal<IEnumerable<ValrCurrency>>(_baseClient.GetUrl(ExchangeDataEndpoints.Currencies),
                HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<ValrSymbol>>> GetSupportedPairsAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            return await _baseClient.SendRequestInternal<IEnumerable<ValrSymbol>>(_baseClient.GetUrl(ExchangeDataEndpoints.CurrencyPairs),
                HttpMethod.Get, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<ValrStatus>> GetSystemStatusAsync(CancellationToken ct = default)
        {
            var result = await _baseClient.SendRequestInternal<ValrSystemStatus>(_baseClient.GetUrl(ExchangeDataEndpoints.SystemStatus),
                HttpMethod.Get, ct).ConfigureAwait(false);

            return result.As(result.Data.status);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<ValrTrade>>> GetTradeHistoryAsync(string currencyPair, int skip = 0, int limit = 100, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("skip", skip);
            parameters.AddParameter("limit", limit);

            return await _baseClient.SendRequestInternal<IEnumerable<ValrTrade>>(_baseClient.GetUrl(ExchangeDataEndpoints.TradeHistory.Replace(":currencyPair", currencyPair)),
                HttpMethod.Get, ct, parameters: parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<ValrTrade>>> GetTradeHistoryBeforeIdAsync(string currencyPair, Guid id, int limit = 100, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("limit", limit);
            parameters.AddParameter("beforeId", id);

            return await _baseClient.SendRequestInternal<IEnumerable<ValrTrade>>(_baseClient.GetUrl(ExchangeDataEndpoints.TradeHistory.Replace(":currencyPair", currencyPair)),
                HttpMethod.Get, ct, parameters: parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<ValrTrade>>> GetTradeHistoryFilteredAsync(string currencyPair, DateTime startTime, DateTime endTime, int skip = 0, int limit = 100, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("skip", skip);
            parameters.AddParameter("limit", limit);
            parameters.AddParameter("startTime", startTime.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));
            parameters.AddParameter("endTime", endTime.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));

            return await _baseClient.SendRequestInternal<IEnumerable<ValrTrade>>(_baseClient.GetUrl(ExchangeDataEndpoints.TradeHistory.Replace(":currencyPair", currencyPair)),
                HttpMethod.Get, ct, parameters: parameters).ConfigureAwait(false);
        }
    }
}
