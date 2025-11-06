using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using System.Text.Json;
using Valr.Net.Enums;
using Valr.Net.Interfaces.Clients.SpotApi;
using Valr.Net.Objects.Models;
using Valr.Net.Objects.Models.Spot.Streams;
using Valr.Net.Objects.Options;

namespace Valr.Net.Clients.SpotApi
{
    public class ValrSocketClientSpotStreams : SocketApiClient, IValrSocketClientSpotStreams
    {
        #region fields
        private readonly ValrSocketClient _baseClient;
        private readonly Log _log;
        private readonly ValrSocketClientOptions _options;
        #endregion

        #region constructor/destructor

        /// <summary>
        /// Create a new instance of ValrSocketClientSpotStreams with default options
        /// </summary>
        public ValrSocketClientSpotStreams(Log log, ValrSocketClient baseClient, ValrSocketClientOptions options) :
            base(options, options.GeneralStreamsOptions)
        {
            _baseClient = baseClient;
            _log = log;
            _options = options;
        }

        public async Task<CallResult<UpdateSubscription>> SubscribeToAggregateOrderbookUpdatesAsync(string[] symbol, Action<DataEvent<InboundStreamPayload<AggregateOrderBookData>>> stringHandler, CancellationToken ct = default)
        {
            return await Subscribe(ValrSocketOutboundEvent.AGGREGATED_ORDERBOOK_UPDATE, symbol, stringHandler, ct).ConfigureAwait(false);
        }

        public async Task<CallResult<UpdateSubscription>> SubscribeToFullOrderbookUpdatesAsync(string[] symbol, Action<DataEvent<InboundStreamPayload<FullOrderBookData>>> snapShotHandler,
            Action<DataEvent<InboundStreamPayload<FullOrderBookData>>> updateHandler, CancellationToken ct = default)
        {
            var handler = new Action<DataEvent<string>>(data =>
            {
                using var doc = JsonDocument.Parse(data.Data);
                var combinedElement = doc.RootElement;

                var eventType = combinedElement.TryGetProperty("type", out var typeProperty) ? typeProperty.GetString() : null;
                if (!Enum.TryParse(eventType, false, out ValrSocketInboundEvent parsedEventType))
                    return;

                RouteFullOrderBookEvent(snapShotHandler, updateHandler, parsedEventType, data, data.Data);
            });

            return await Subscribe(ValrSocketOutboundEvent.FULL_ORDERBOOK_UPDATE, symbol, handler, ct).ConfigureAwait(false);
        }

        private void RouteFullOrderBookEvent(Action<DataEvent<InboundStreamPayload<FullOrderBookData>>> snapShotHandler, Action<DataEvent<InboundStreamPayload<FullOrderBookData>>> updateHandler,
            ValrSocketInboundEvent parsedEventType, DataEvent<string> data, string jsonData)
        {
            switch (parsedEventType)
            {
                case ValrSocketInboundEvent.FULL_ORDERBOOK_SNAPSHOT:
                    {
                        InvokeHandler(data, jsonData, snapShotHandler);
                        break;
                    }
                case ValrSocketInboundEvent.FULL_ORDERBOOK_UPDATE:
                    {
                        InvokeHandler(data, jsonData, updateHandler);
                        break;
                    }
            }
        }

        public async Task<CallResult<UpdateSubscription>> SubscribeToMarketSummaryUpdatesAsync(string[] symbol, Action<DataEvent<InboundStreamPayload<MarketSummaryData>>> stringHandler, CancellationToken ct = default)
        {
            return await Subscribe(ValrSocketOutboundEvent.MARKET_SUMMARY_UPDATE, symbol, stringHandler, ct).ConfigureAwait(false);
        }

        public async Task<CallResult<UpdateSubscription>> SubscribeToNewTradeBucketUpdatesAsync(string[] symbol, Action<DataEvent<InboundStreamPayload<NewTradeBucketData>>> stringHandler, CancellationToken ct = default)
        {
            return await Subscribe(ValrSocketOutboundEvent.NEW_TRADE_BUCKET, symbol, stringHandler, ct).ConfigureAwait(false);
        }

        public async Task<CallResult<UpdateSubscription>> SubscribeToNewTradeUpdatesAsync(string[] symbol, Action<DataEvent<InboundStreamPayload<NewTradeData>>> stringHandler, CancellationToken ct = default)
        {
            return await Subscribe(ValrSocketOutboundEvent.NEW_TRADE, symbol, stringHandler, ct).ConfigureAwait(false);
        }
        #endregion

        protected async Task<CallResult<UpdateSubscription>> Subscribe<T>(ValrSocketOutboundEvent _event, string[] symbol, Action<DataEvent<T>> onData, CancellationToken ct, bool authenticated = false)
        {
            return await _baseClient.SubscribeInternal(this, _options.SpotStreamsOptions.BaseAddress, _event, symbol, onData, ct, authenticated).ConfigureAwait(false);
        }

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new ValrAuthenticationProvider(credentials);

        private void InvokeHandler<T>(DataEvent<string> data, string jsonData, Action<DataEvent<T>> handler)
        {
            var result = _baseClient.DeserializeInternal<T>(jsonData);
            handler.Invoke(data.As(result.Data));
        }


    }
}
