using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using Valr.Net.Endpoints.SpotApi;
using Valr.Net.Enums;
using Valr.Net.Interfaces.Clients.SpotApi;
using Valr.Net.Objects.Models.Spot.Trading;

namespace Valr.Net.Clients.SpotApi
{
    public class ValrClientSpotApiTrading : IValrClientSpotApiTrading
    {
        private readonly ILogger _logger;
        private readonly ValrClientSpotApi _baseClient;

        internal ValrClientSpotApiTrading(ILogger logger, ValrClientSpotApi valrClientSpotApi)
        {
            _baseClient = valrClientSpotApi;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<WebCallResult> CancelOrderAsync(Guid id, string symbol, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("orderId", id);
            parameters.AddParameter("pair", symbol);

            var result = await _baseClient.SendRequestInternal(_baseClient.GetUrl(TradingEndpoints.DeleteOrder),
                HttpMethod.Delete, ct, signed: true, parameters: parameters).ConfigureAwait(false);

            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult> CancelOrderAsync(int clientOrderId, string symbol, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("customerOrderId", clientOrderId);
            parameters.AddParameter("pair", symbol);

            var result = await _baseClient.SendRequestInternal(_baseClient.GetUrl(TradingEndpoints.DeleteOrder),
                HttpMethod.Delete, ct, signed: true, parameters: parameters).ConfigureAwait(false);

            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<ValrOpenOrderResponse>>> GetOpenOrderAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            return await _baseClient.SendRequestInternal<IEnumerable<ValrOpenOrderResponse>>(_baseClient.GetUrl(TradingEndpoints.OpenOrders),
                HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<ValrOrderDetailResponse>>> GetOrderDetailAsync(Guid id, long? receiveWindow = null, CancellationToken ct = default)
        {
            return await _baseClient.SendRequestInternal<IEnumerable<ValrOrderDetailResponse>>(_baseClient.GetUrl(TradingEndpoints.OrderHistoryDetailId.Replace(":orderId", id.ToString())),
                HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<ValrOrderDetailResponse>>> GetOrderDetailAsync(int clientOrderId, long? receiveWindow = null, CancellationToken ct = default)
        {
            return await _baseClient.SendRequestInternal<IEnumerable<ValrOrderDetailResponse>>(_baseClient.GetUrl(TradingEndpoints.OrderHistoryDetailCustomId.Replace(":customerOrderId", clientOrderId.ToString())),
                HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<ValrOrderHistoryResponse>>> GetOrderHistoryAsync(int skip = 0, int limit = 10, long? receiveWindow = null, CancellationToken ct = default)
        {
            return await _baseClient.SendRequestInternal<IEnumerable<ValrOrderHistoryResponse>>(_baseClient.GetUrl(TradingEndpoints.OrderHistory),
                HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<ValrOrderStatusResponse>> GetOrderStatusAsync(string symbol, Guid id, long? receiveWindow = null, CancellationToken ct = default)
        {
            string path = TradingEndpoints.OrderStatusId.Replace(":orderId", id.ToString()).Replace(":currencyPair", symbol);

            return await _baseClient.SendRequestInternal<ValrOrderStatusResponse>(_baseClient.GetUrl(path),
                HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<ValrOrderStatusResponse>> GetOrderStatusAsync(string symbol, int clientOrderId, long? receiveWindow = null, CancellationToken ct = default)
        {
            string path = TradingEndpoints.OrderStatusId.Replace(":customerOrderId", clientOrderId.ToString()).Replace(":currencyPair", symbol);

            return await _baseClient.SendRequestInternal<ValrOrderStatusResponse>(_baseClient.GetUrl(path),
                HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<ValrOrderHistoryResponse>> GetOrderSummaryAsync(Guid id, long? receiveWindow = null, CancellationToken ct = default)
        {
            return await _baseClient.SendRequestInternal<ValrOrderHistoryResponse>(_baseClient.GetUrl(TradingEndpoints.OrderHistoryDetailId.Replace(":orderId", id.ToString())),
                HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<ValrOrderHistoryResponse>> GetOrderSummaryAsync(int clientOrderId, long? receiveWindow = null, CancellationToken ct = default)
        {
            return await _baseClient.SendRequestInternal<ValrOrderHistoryResponse>(_baseClient.GetUrl(TradingEndpoints.OrderHistoryDetailId.Replace(":customerOrderId", clientOrderId.ToString())),
                HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<ValrPlaceOrderResponse>> PlaceLimitOrderAsync(string symbol, ValrOrderSide side, decimal quantity, decimal price, bool postOnly = false, ValrTimeInforce timeInForce = ValrTimeInforce.GTC, int? clientOrderId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("side", side);
            parameters.AddParameter("quantity", quantity);
            parameters.AddParameter("price", price);
            parameters.AddParameter("pair", symbol);
            parameters.AddOptionalParameter("postOnly", postOnly);
            parameters.AddOptionalParameter("customerOrderId", symbol);
            parameters.AddOptionalParameter("timeInForce", timeInForce);


            return await _baseClient.SendRequestInternal<ValrPlaceOrderResponse>(_baseClient.GetUrl(TradingEndpoints.LimitOrder), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<ValrPlaceOrderResponse>> PlaceMarketOrderAsync(string symbol, ValrOrderSide side, decimal quantity, int? newClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("side", side);
            parameters.AddParameter("baseAmount", quantity);
            parameters.AddParameter("pair", symbol);
            parameters.AddOptionalParameter("customerOrderId", symbol);

            return await _baseClient.SendRequestInternal<ValrPlaceOrderResponse>(_baseClient.GetUrl(TradingEndpoints.MarketOrder), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<ValrPlaceOrderResponse>> PlaceStopTakeLimitOrderAsync(string symbol, ValrOrderSide side, decimal quantity, decimal price, decimal stopPrice, ValrOrderType type, ValrTimeInforce timeInForce = ValrTimeInforce.GTC, int? newClientOrderId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (type is not ValrOrderType.TAKE_PROFIT_LIMIT or ValrOrderType.STOP_LOSS_LIMIT)
            {
                throw new ArgumentException($"Order type {type} not allowed", nameof(type));
            }

            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("side", side);
            parameters.AddParameter("quantity", quantity);
            parameters.AddParameter("price", price);
            parameters.AddParameter("pair", symbol);
            parameters.AddParameter("stopPrice", stopPrice);
            parameters.AddParameter("type", type);
            parameters.AddOptionalParameter("customerOrderId", symbol);
            parameters.AddOptionalParameter("timeInForce", timeInForce);


            return await _baseClient.SendRequestInternal<ValrPlaceOrderResponse>(_baseClient.GetUrl(TradingEndpoints.LimitOrder), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }
    }
}
