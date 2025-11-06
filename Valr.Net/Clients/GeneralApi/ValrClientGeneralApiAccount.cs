using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using Valr.Net.Endpoints.GeneralApi;
using Valr.Net.Enums;
using Valr.Net.Interfaces.Clients.GeneralApi;
using Valr.Net.Objects.Models.General.Account;
using Valr.Net.Objects.Models.General.ExchangeData;

namespace Valr.Net.Clients.GeneralApi
{
    public class ValrClientGeneralApiAccount : IValrClientGeneralApiAccount
    {
        private readonly ILogger _logger;
        private readonly ValrClientGeneralApi _baseClient;

        internal ValrClientGeneralApiAccount(ILogger logger, ValrClientGeneralApi valrClientGeneralApi)
        {
            _logger = logger;
            _baseClient = valrClientGeneralApi;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<ValrAccountBalance>>> GetAccountBalancesAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            return await _baseClient.SendRequestInternal<IEnumerable<ValrAccountBalance>>(_baseClient.GetUrl(AccountEndpoints.Balances),
                HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<ValrAccountTransaction>>> GetTransactionHistoryAsync(int skip = 0, int limit = 200, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("skip", skip);
            parameters.AddParameter("limit", limit);

            return await _baseClient.SendRequestInternal<IEnumerable<ValrAccountTransaction>>(_baseClient.GetUrl(AccountEndpoints.TransactionHistory),
                HttpMethod.Get, ct, signed: true, parameters: parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<ValrAccountTransaction>>> GetTransactionHistoryBeforeIdAsync(Guid id, int limit = 200, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("limit", limit);
            parameters.AddParameter("beforeId", id);

            return await _baseClient.SendRequestInternal<IEnumerable<ValrAccountTransaction>>(_baseClient.GetUrl(AccountEndpoints.TransactionHistory),
                HttpMethod.Get, ct, signed: true, parameters: parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<ValrAccountTransaction>>> GetTransactionHistoryFilteredAsync(ValrTransactionType[] transactionTypes, DateTime startTime, DateTime endTime, string? currency = null, int skip = 0, int limit = 200, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("transactionTypes", string.Join(",", transactionTypes));
            parameters.AddParameter("skip", skip);
            parameters.AddParameter("limit", limit);
            parameters.AddParameter("startTime", startTime.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));
            parameters.AddParameter("endTime", endTime.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));
            parameters.AddOptionalParameter("currency", currency);

            return await _baseClient.SendRequestInternal<IEnumerable<ValrAccountTransaction>>(_baseClient.GetUrl(AccountEndpoints.TransactionHistory),
                HttpMethod.Get, ct, signed: true, parameters: parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<ValrTrade>>> GetRecentTradesByPairAsync(string currencyPair, int limit = 100, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("limit", limit);

            return await _baseClient.SendRequestInternal<IEnumerable<ValrTrade>>(_baseClient.GetUrl(AccountEndpoints.TradeHistory.Replace(":currencyPair", currencyPair)),
                HttpMethod.Get, ct, signed: true, parameters: parameters).ConfigureAwait(false);
        }
    }
}
