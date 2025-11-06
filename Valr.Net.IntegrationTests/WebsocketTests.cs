using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using Valr.Net.Clients;
using Valr.Net.Interfaces.Clients;
using Valr.Net.Objects.Models;
using Valr.Net.Objects.Models.General.Streams;
using Valr.Net.Objects.Models.Spot.Streams;
using Valr.Net.Objects.Options;
using Valr.Net.OrderBooks;

namespace Valr.Net.IntegrationTests
{
    public class WebsocketTests
    {
        IValrSocketClient _valrSocketCLient { get; set; }
        IValrClient _valrClient { get; set; }
        private string _key;
        private string _secret;
        IConfiguration Configuration { get; set; }

        [SetUp]
        public void Setup()
        {
            Configuration = new ConfigurationBuilder()
                .AddUserSecrets<WebsocketTests>()
                .Build();

            _key = Configuration["key"];
            _secret = Configuration["secret"];
        }

        [Test]
        public async Task TestAccountSubscription()
        {
            _valrSocketCLient = new ValrSocketClient(new Objects.Options.ValrSocketClientOptions
            {
                ApiCredentials = new ApiCredentials(_key, _secret),
                SocketNoDataTimeout = TimeSpan.FromSeconds(30),
                LogLevel = LogLevel.Trace,
                AutoReconnect = true,
                MaxReconnectTries = 5
            });

            var result = await _valrSocketCLient.GeneralStreams.SubscribeToAccountUpdatesAsync(ReadyResult);

            Assert.IsTrue(result.Success);
        }

        [Test]
        public async Task TestAggregateOrderbookSubscription()
        {
            _valrSocketCLient = new ValrSocketClient(new Objects.Options.ValrSocketClientOptions
            {
                ApiCredentials = new ApiCredentials(_key, _secret),
                SocketNoDataTimeout = TimeSpan.FromSeconds(10),
                LogLevel = LogLevel.Trace,
                AutoReconnect = true,
                MaxReconnectTries = 5
            });

            var result = await _valrSocketCLient.SpotStreams.SubscribeToAggregateOrderbookUpdatesAsync(new[]
            {
                "BTCZAR"
            }, AggregateResult);

            Assert.IsTrue(result.Success);
        }

        [Test]
        public async Task TestFullOrderbookSubscription()
        {
            _valrSocketCLient = new ValrSocketClient(new Objects.Options.ValrSocketClientOptions
            {
                ApiCredentials = new ApiCredentials(_key, _secret),
                SocketNoDataTimeout = TimeSpan.FromSeconds(10),
                LogLevel = LogLevel.Trace,
                AutoReconnect = true,
                MaxReconnectTries = 5
            });

            var result = await _valrSocketCLient.SpotStreams.SubscribeToFullOrderbookUpdatesAsync(new[]
            {
                "BTCZAR"
            }, SnapShotResult, UpdateResult);

            Assert.IsTrue(result.Success);
        }

        [Test]
        public async Task TestSynchronisedOrderBook()
        {
            _valrSocketCLient = new ValrSocketClient(new Objects.Options.ValrSocketClientOptions
            {
                ApiCredentials = new ApiCredentials(_key, _secret),
                SocketNoDataTimeout = TimeSpan.FromSeconds(10),
                LogLevel = LogLevel.Trace,
                AutoReconnect = true,
                MaxReconnectTries = 5
            });

            _valrClient = new ValrClient(new ValrClientOptions()
            {
                // Specify options for the client
                ApiCredentials = new ApiCredentials(_key, _secret),
                LogLevel = LogLevel.Trace,
                SpotApiOptions = new ValrApiClientOptions
                {
                    RateLimitingBehaviour = RateLimitingBehaviour.Wait
                },
                GeneralApiOptions = new ValrApiClientOptions
                {
                    RateLimitingBehaviour = RateLimitingBehaviour.Wait
                }
            });

            var _subscription = new ValrSpotSymbolOrderBookAggregated(new[] { "BTCZAR", "ETHZAR", "XRPZAR", "SOLZAR", "USDCZAR", "BNBZAR", "SHIBZAR" },
                new ValrOrderBookOptions()
                {
                    LogLevel = LogLevel.Warning,
                    //LogWriters = new List<ILogger>() { _logger },
                    UpdateInterval = 100,
                    SocketClient = _valrSocketCLient,
                    RestClient = _valrClient
                });
            var result = await _subscription.StartAsync();

            await Task.Delay(TimeSpan.FromSeconds(10));

            Assert.IsTrue(result.Success);
        }

        private void ReadyResult(DataEvent<string> obj)
        {
            Console.WriteLine(obj.Data);
        }

        private void AggregateResult(DataEvent<InboundStreamPayload<AggregateOrderBookData>> obj)
        {
            Console.WriteLine(obj.Data);
        }

        private void SnapShotResult(DataEvent<InboundStreamPayload<FullOrderBookData>> obj)
        {
            Console.WriteLine(obj.Data);
        }

        private void UpdateResult(DataEvent<InboundStreamPayload<FullOrderBookData>> obj)
        {
            Console.WriteLine(obj.Data);
        }
    }
}
