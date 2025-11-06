using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using Valr.Net.Endpoints.GeneralApi.Wallets;
using Valr.Net.Interfaces.Clients.GeneralApi.Wallets;
using Valr.Net.Objects.Models.General.Wallet;

namespace Valr.Net.Clients.GeneralApi.Wallets
{
    internal class ValrClientGeneralApiWalletCrypto : IValrClientGeneralApiWalletCrypto
    {
        private readonly ILogger _logger;
        private readonly ValrClientGeneralApi _baseClient;

        public ValrClientGeneralApiWalletCrypto(ILogger logger, ValrClientGeneralApi valrClientGeneralApi)
        {
            _logger = logger;
            _baseClient = valrClientGeneralApi;
        }

        public async Task<WebCallResult<IEnumerable<ValrWalletAddress>>> GetDepositAddressAsync(string currencyCode, long? receiveWindow = null, CancellationToken ct = default)
        {
            return await _baseClient.SendRequestInternal<IEnumerable<ValrWalletAddress>>(_baseClient.GetUrl(CryptoWalletEndpoints.DepositAddress.Replace(":currencyCode", currencyCode)),
                HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        public async Task<WebCallResult<IEnumerable<ValrDepositStatusInfo>>> GetDepositHistoryAsync(string currencyCode, int skip = 0, int limit = 10, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("skip", skip);
            parameters.AddParameter("limit", limit);

            return await _baseClient.SendRequestInternal<IEnumerable<ValrDepositStatusInfo>>(_baseClient.GetUrl(CryptoWalletEndpoints.DepositHistory.Replace(":currencyCode", currencyCode)),
                HttpMethod.Get, ct, parameters: parameters, signed: true).ConfigureAwait(false);
        }

        public async Task<WebCallResult<ValrWithdrawalId>> DoWithdrawalAsync(string currencyCode, decimal amount, string address, string? paymentReference = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("amount", amount);
            parameters.AddParameter("address", address);
            parameters.AddOptionalParameter("paymentReference", paymentReference);

            return await _baseClient.SendRequestInternal<ValrWithdrawalId>(_baseClient.GetUrl(CryptoWalletEndpoints.WithdrawalHistory.Replace(":currencyCode", currencyCode)),
                HttpMethod.Post, ct, parameters: parameters, signed: true).ConfigureAwait(false);
        }

        public async Task<WebCallResult<IEnumerable<ValrWhitelistedAddress>>> GetWhitelistedWithdrawalAddressAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            return await _baseClient.SendRequestInternal<IEnumerable<ValrWhitelistedAddress>>(_baseClient.GetUrl(CryptoWalletEndpoints.WhitelistedAddress),
                HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        public async Task<WebCallResult<IEnumerable<ValrWhitelistedAddress>>> GetWhitelistedWithdrawalAddressAsync(string currencyCode, long? receiveWindow = null, CancellationToken ct = default)
        {
            return await _baseClient.SendRequestInternal<IEnumerable<ValrWhitelistedAddress>>(_baseClient.GetUrl(CryptoWalletEndpoints.WhitelistedAddressCurrency.Replace(":currencyCode", currencyCode)),
                HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        public async Task<WebCallResult<IEnumerable<ValrWithdrawalStatusInfo>>> GetWithdrawalHistoryAsync(string currencyCode, int skip = 0, int limit = 10, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("skip", skip);
            parameters.AddParameter("limit", limit);

            return await _baseClient.SendRequestInternal<IEnumerable<ValrWithdrawalStatusInfo>>(_baseClient.GetUrl(CryptoWalletEndpoints.WithdrawalHistory.Replace(":currencyCode", currencyCode)),
                HttpMethod.Get, ct, parameters: parameters, signed: true).ConfigureAwait(false);
        }

        public async Task<WebCallResult<ValrWithdrawalInfo>> GetWithdrawalInfoAsync(string currencyCode, long? receiveWindow = null, CancellationToken ct = default)
        {
            return await _baseClient.SendRequestInternal<ValrWithdrawalInfo>(_baseClient.GetUrl(CryptoWalletEndpoints.Withdrawal.Replace(":currencyCode", currencyCode)),
                HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        public async Task<WebCallResult<ValrWithdrawalStatusInfo>> GetWithdrawalStatusAsync(string currencyCode, Guid Id, long? receiveWindow = null, CancellationToken ct = default)
        {
            string url = CryptoWalletEndpoints.WithdrawalStatus.Replace(":currencyCode", currencyCode)
                .Replace(":withdrawId", Id.ToString());

            return await _baseClient.SendRequestInternal<ValrWithdrawalStatusInfo>(_baseClient.GetUrl(url),
                HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }
    }
}
