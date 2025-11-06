using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.CommonObjects;
using CryptoExchange.Net.Interfaces.CommonClients;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using System.Globalization;
using Valr.Net.Converters;
using Valr.Net.Enums;
using Valr.Net.Interfaces.Clients.SpotApi;
using Valr.Net.Objects.Models.Spot.Trading;
using Valr.Net.Objects.Options;

namespace Valr.Net.Clients.SpotApi
{
    public class ValrClientSpotApi : RestApiClient, IValrClientSpotApi, ISpotClient
    {
        #region fields
        /// <inheritdoc />
        public new ValrRestApiOptions ApiOptions => (ValrRestApiOptions)base.ApiOptions;
        /// <inheritdoc />
        public new ValrRestOptions ClientOptions => (ValrRestOptions)base.ClientOptions;

        private readonly ValrClient _baseClient;
        internal DateTime? LastExchangeInfoUpdate;

        internal static TimeSyncState TimeSyncState = new TimeSyncState("Spot Api");
        #endregion

        #region Api clients
        /// <inheritdoc />
        public IValrClientSpotApiTrading Spot { get; }
        /// <inheritdoc />
        public IValrClientSpotApiInstantTrading InstantTrade { get; }

        /// <inheritdoc />
        public ISpotClient CommonSpotClient => this;

        string IBaseRestClient.ExchangeName => "Valr";
        #endregion

        /// <summary>
        /// Event triggered when an order is placed via this client. Only available for Spot orders
        /// </summary>
        public event Action<OrderId>? OnOrderPlaced;
        /// <summary>
        /// Event triggered when an order is canceled via this client. Note that this does not trigger when using CancelAllOrdersAsync. Only available for Spot orders
        /// </summary>
        public event Action<OrderId>? OnOrderCanceled;

        internal ValrClientSpotApi(ILogger logger, HttpClient? httpClient, ValrClient baseClient, ValrRestOptions options)
            : base(logger, httpClient, options.Environment.SpotRestAddress, options, options.SpotOptions)
        {
            _baseClient = baseClient;

            Spot = new ValrClientSpotApiTrading(_logger, this);
            InstantTrade = new ValrClientSpotApiInstantTrading(_logger, this);
        }

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new ValrAuthenticationProvider(credentials);

        #region helpers
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
                TimeSyncState.LastSyncTime = DateTime.MinValue;
            }
            return result;
        }

        internal async Task<WebCallResult> SendRequestInternal(Uri uri, HttpMethod method, CancellationToken cancellationToken,
            Dictionary<string, object>? parameters = null, bool signed = false, HttpMethodParameterPosition? postPosition = null,
            ArrayParametersSerialization? arraySerialization = null, int weight = 1, bool ignoreRateLimit = false)
        {
            var result = await SendRequestAsync(uri, method, cancellationToken, parameters, signed, postPosition, arraySerialization, requestWeight: weight, ignoreRatelimit: ignoreRateLimit).ConfigureAwait(false);
            if (!result && result.Error!.Code == -1021 && (ApiOptions.AutoTimestamp ?? ClientOptions.AutoTimestamp))
            {
                _logger.Log(LogLevel.Debug, "Received Invalid Timestamp error, triggering new time sync");
                TimeSyncState.LastSyncTime = DateTime.MinValue;
            }
            return result;
        }

        #endregion

        #region RestApi Client
        /// <inheritdoc />
        protected override Task<WebCallResult<DateTime>> GetServerTimestampAsync()
            => _baseClient.GeneralApi.ExchangeData.GetServerTimeAsync();

        /// <inheritdoc />
        public override TimeSyncInfo? GetTimeSyncInfo()
            => new TimeSyncInfo(_logger, (ApiOptions.AutoTimestamp ?? ClientOptions.AutoTimestamp), (ApiOptions.TimestampRecalculationInterval ?? ClientOptions.TimestampRecalculationInterval), TimeSyncState);

        /// <inheritdoc />
        public override TimeSpan? GetTimeOffset()
            => TimeSyncState.TimeOffset;

        /// <inheritdoc />
        public string GetSymbolName(string baseAsset, string quoteAsset) =>
            (baseAsset + quoteAsset).ToUpper(CultureInfo.InvariantCulture);
        #endregion

        #region Spot Client
        async Task<WebCallResult<OrderId>> ISpotClient.PlaceOrderAsync(string symbol, CommonOrderSide side, CommonOrderType type, decimal quantity, decimal? price, string? accountId, string? clientOrderId, CancellationToken ct)
        {
            int? clientId = null;
            if (clientOrderId != null)
            {
                if (!int.TryParse(clientOrderId, out var id))
                    throw new ArgumentException("ClientOrderId for Valr should be parsable to int");
                else
                    clientId = id;
            }

            WebCallResult<ValrPlaceOrderResponse> result = null;

            switch (type)
            {
                case CommonOrderType.Market:
                    {
                        result = await _baseClient.SpotApi.Spot.PlaceMarketOrderAsync(symbol, EnumConverter.ConvertFromCommonOrderSde(side), quantity, clientId);
                        break;
                    }
                case CommonOrderType.Limit:
                    {
                        if (!price.HasValue)
                        {
                            throw new ArgumentException("Prices needs to have a value for limit orders");
                        }
                        result = await _baseClient.SpotApi.Spot.PlaceLimitOrderAsync(symbol,
                            EnumConverter.ConvertFromCommonOrderSde(side), quantity, price.Value, clientOrderId: clientId);
                        break;
                    }
                default:
                    {
                        throw new ArgumentException("Order type other not supported by the common api, for more other order types please see VarlApiClient.SpotApi.Spot or VarlApiClient.SpotApi.InstantTrade");
                    }
            }




            if (!result)
                return result.As<OrderId>(null);

            return result.As(new OrderId
            {
                SourceObject = result.Data,
                Id = result.Data.Id.ToString()
            });
        }

        /// <summary>
        /// Get the name of a symbol for Valr based on the base and quote asset
        /// </summary>
        /// <param name="baseAsset"></param>
        /// <param name="quoteAsset"></param>
        /// <returns></returns>
        string IBaseRestClient.GetSymbolName(string baseAsset, string quoteAsset) => (baseAsset + quoteAsset).ToUpperInvariant();

        /// <inheritdoc />
        async Task<WebCallResult<IEnumerable<Symbol>>> IBaseRestClient.GetSymbolsAsync(CancellationToken ct)
        {
            var exchangeInfo = await _baseClient.GeneralApi.ExchangeData.GetSupportedPairsAsync();

            if (!exchangeInfo)
                return exchangeInfo.As<IEnumerable<Symbol>>(null);

            return exchangeInfo.As(exchangeInfo.Data.Select(s => new Symbol
            {
                SourceObject = s,
                Name = s.Symbol,
                MinTradeQuantity = s.MinBaseAmount,
                QuantityStep = s.TickSize,
                QuantityDecimals = s.BaseDecimalPlaces
            }));
        }

        /// <inheritdoc />
        async Task<WebCallResult<Ticker>> IBaseRestClient.GetTickerAsync(string symbol, CancellationToken ct)
        {
            var asset = await _baseClient.GeneralApi.ExchangeData.GetMarketSummaryForPairAsync(symbol);

            if (!asset)
                return asset.As<Ticker>(null);

            var t = asset.Data;

            return asset.As(new Ticker
            {
                HighPrice = t.HighPrice,
                LastPrice = t.LastTradedPrice,
                LowPrice = t.LowPrice,
                Volume = t.BaseVolume,
                Symbol = t.CurrencyPair,
                Price24H = t.PreviousClosePrice,
                SourceObject = t
            });
        }

        /// <inheritdoc />
        async Task<WebCallResult<IEnumerable<Ticker>>> IBaseRestClient.GetTickersAsync(CancellationToken ct)
        {
            var asset = await _baseClient.GeneralApi.ExchangeData.GetMarketSummariesAsync();

            if (!asset)
                return asset.As<IEnumerable<Ticker>>(null);

            return asset.As(asset.Data.Select(t => new Ticker
            {
                HighPrice = t.HighPrice,
                LastPrice = t.LastTradedPrice,
                LowPrice = t.LowPrice,
                Volume = t.BaseVolume,
                Symbol = t.CurrencyPair,
                Price24H = t.PreviousClosePrice,
                SourceObject = t
            }));
        }

        /// <inheritdoc />
        async Task<WebCallResult<IEnumerable<Kline>>> IBaseRestClient.GetKlinesAsync(string symbol, TimeSpan timespan, DateTime? startTime, DateTime? endTime, int? limit, CancellationToken ct)
        {
            throw new NotImplementedException("Valr does not support KLines at the moment");
        }

        /// <inheritdoc />
        async Task<WebCallResult<OrderBook>> IBaseRestClient.GetOrderBookAsync(string symbol, CancellationToken ct)
        {
            var book = await _baseClient.GeneralApi.ExchangeData.GetPublicOrderBookFullAsync(symbol);

            if (!book)
                return book.As<OrderBook>(null);

            var b = book.Data;

            return book.As(new OrderBook
            {
                SourceObject = b,
                Asks = b.Asks.Select(s => new OrderBookEntry { Price = s.Price, Quantity = s.Quantity }),
                Bids = b.Bids.Select(s => new OrderBookEntry { Price = s.Price, Quantity = s.Quantity })
            });
        }

        /// <inheritdoc />
        async Task<WebCallResult<IEnumerable<Trade>>> IBaseRestClient.GetRecentTradesAsync(string symbol, CancellationToken ct)
        {
            var trades = await _baseClient.GeneralApi.ExchangeData.GetTradeHistoryAsync(symbol);

            if (!trades)
                return trades.As<IEnumerable<Trade>>(null);

            return trades.As(trades.Data.Select(s => new Trade
            {
                SourceObject = s,
                Price = s.Price,
                Quantity = s.Quantity,
                Symbol = s.CurrencyPair,
                Timestamp = s.TradeTime
            }));
        }

        /// <inheritdoc />
        async Task<WebCallResult<IEnumerable<Balance>>> IBaseRestClient.GetBalancesAsync(string? accountId, CancellationToken ct)
        {
            var balances = await _baseClient.GeneralApi.Account.GetAccountBalancesAsync();

            if (!balances)
                return balances.As<IEnumerable<Balance>>(null);


            return balances.As(balances.Data.Select(s => new Balance
            {
                SourceObject = s,
                Asset = s.Currency,
                Available = s.Available,
                Total = s.Total
            }));
        }

        /// <inheritdoc />
        async Task<WebCallResult<Order>> IBaseRestClient.GetOrderAsync(string orderId, string? symbol, CancellationToken ct)
        {
            var order = await _baseClient.SpotApi.Spot.GetOrderStatusAsync(symbol, Guid.Parse(orderId));

            if (!order)
                return order.As<Order>(null);

            var o = order.Data;

            return order.As(new Order
            {
                SourceObject = o,
                Symbol = o.CurrencyPair,
                Quantity = o.OriginalQuantity,
                Price = o.OriginalPrice,
                Side = EnumConverter.ConvertToCommonOrderSde(o.Side),
                Timestamp = o.Created,
                Id = o.Id.ToString(),
                QuantityFilled = o.OriginalQuantity - o.RemainingQuantity,
                Status = EnumConverter.ConvertToCommonOrderSide(o.Status),
                Type = EnumConverter.ConvertToCommonOrderType(o.Type)
            });
        }

        /// <inheritdoc />
        async Task<WebCallResult<IEnumerable<UserTrade>>> IBaseRestClient.GetOrderTradesAsync(string orderId, string? symbol, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        async Task<WebCallResult<IEnumerable<Order>>> IBaseRestClient.GetOpenOrdersAsync(string? symbol, CancellationToken ct)
        {
            var orders = await _baseClient.SpotApi.Spot.GetOpenOrderAsync();

            if (!orders)
                return orders.As<IEnumerable<Order>>(null);

            if (!string.IsNullOrEmpty(symbol))
            {
                orders = orders.As(orders.Data.Where(w => w.CurrencyPair == symbol));
            }

            return orders.As(orders.Data.Select(s => new Order
            {
                SourceObject = s,
                Symbol = s.CurrencyPair,
                Quantity = s.OriginalQuantity,
                Price = s.OriginalPrice,
                Side = EnumConverter.ConvertToCommonOrderSde(s.Side),
                Timestamp = s.Created,
                Id = s.Id.ToString(),
                QuantityFilled = s.OriginalQuantity - s.RemainingQuantity,
                Status = EnumConverter.ConvertToCommonOrderSide(s.Status),
                Type = EnumConverter.ConvertToCommonOrderType(s.Type)

            }));
        }

        /// <inheritdoc />
        async Task<WebCallResult<IEnumerable<Order>>> IBaseRestClient.GetClosedOrdersAsync(string? symbol, CancellationToken ct)
        {
            var orders = await _baseClient.SpotApi.Spot.GetOrderHistoryAsync();

            if (!orders)
                return orders.As<IEnumerable<Order>>(null);

            if (!string.IsNullOrEmpty(symbol))
            {
                orders = orders.As(orders.Data.Where(w => w.CurrencyPair == symbol));
            }

            return orders.As(orders.Data
                .Where(w => w.Status is ValrOrderStatus.Failed
                    or ValrOrderStatus.Cancelled
                    or ValrOrderStatus.Filled)
                .Select(s => new Order
                {
                    SourceObject = s,
                    Symbol = s.CurrencyPair,
                    Quantity = s.OriginalQuantity,
                    Price = s.OriginalPrice,
                    Side = EnumConverter.ConvertToCommonOrderSde(s.Side),
                    Timestamp = s.Created,
                    Id = s.Id.ToString(),
                    QuantityFilled = s.OriginalQuantity - s.RemainingQuantity,
                    Status = EnumConverter.ConvertToCommonOrderSide(s.Status),
                    Type = EnumConverter.ConvertToCommonOrderType(s.Type)

                }));
        }

        /// <inheritdoc />
        async Task<WebCallResult<OrderId>> IBaseRestClient.CancelOrderAsync(string orderId, string? symbol, CancellationToken ct)
        {
            var result = await _baseClient.SpotApi.Spot.CancelOrderAsync(Guid.Parse(orderId), symbol);
            if (result.Success)
            {
                return new WebCallResult<OrderId>(result.ResponseStatusCode, result.ResponseHeaders,
                    result.ResponseTime, null, result.RequestUrl, null, HttpMethod.Delete, result.RequestHeaders, new OrderId { Id = orderId }, null);
            }
            return new WebCallResult<OrderId>(result.ResponseStatusCode, result.ResponseHeaders,
                result.ResponseTime, null, result.RequestUrl, null, HttpMethod.Delete, result.RequestHeaders, null, result.Error);
        }
        #endregion

        internal void InvokeOrderPlaced(OrderId id)
        {
            OnOrderPlaced?.Invoke(id);
        }

        internal void InvokeOrderCanceled(OrderId id)
        {
            OnOrderCanceled?.Invoke(id);
        }
    }
}
