using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using Valr.Net.Endpoints.GeneralApi.Wallets;
using Valr.Net.Interfaces.Clients.GeneralApi.Wallets;
using Valr.Net.Objects.Models.General.Wallet;

namespace Valr.Net.Clients.GeneralApi.Wallets
{
    internal class ValrClientGeneralApiWalletFiat : IValrClientGeneralApiWalletFiat
    {
        private readonly ILogger _logger;
        private readonly ValrClientGeneralApi _baseClient;

        public ValrClientGeneralApiWalletFiat(ILogger logger, ValrClientGeneralApi valrClientGeneralApi)
        {
            _logger = logger;
            _baseClient = valrClientGeneralApi;
        }

        public async Task<WebCallResult<ValrWithdrawalId>> CreateWithdrawalAsync(string currencyCode, Guid bankAccountId, decimal amount, bool fast = false, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("linkedBankAccountId", bankAccountId);
            parameters.AddParameter("amount", amount);
            parameters.AddOptionalParameter("fast", fast);

            return await _baseClient.SendRequestInternal<ValrWithdrawalId>(_baseClient.GetUrl(FiatWalletEndpoints.Withdrawal.Replace(":currencyCode", currencyCode)),
                HttpMethod.Post, ct, parameters: parameters, signed: true).ConfigureAwait(false);
        }

        public async Task<WebCallResult<IEnumerable<ValrBankAccountInfo>>> GetBankAccountsAsync(string currencyCode, long? receiveWindow = null, CancellationToken ct = default)
        {
            return await _baseClient.SendRequestInternal<IEnumerable<ValrBankAccountInfo>>(_baseClient.GetUrl(FiatWalletEndpoints.BankAccounts.Replace(":currencyCode", currencyCode)),
                HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        public async Task<WebCallResult<ValrFiatDepositReference>> GetDepositReferenceAsync(string currencyCode, long? receiveWindow = null, CancellationToken ct = default)
        {
            return await _baseClient.SendRequestInternal<ValrFiatDepositReference>(_baseClient.GetUrl(FiatWalletEndpoints.DepositReference.Replace(":currencyCode", currencyCode)),
                HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }
    }
}
