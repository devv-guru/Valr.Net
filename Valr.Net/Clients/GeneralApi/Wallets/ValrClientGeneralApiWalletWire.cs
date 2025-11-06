using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using Valr.Net.Endpoints.GeneralApi.Wallets;
using Valr.Net.Interfaces.Clients.GeneralApi.Wallets;
using Valr.Net.Objects.Models.General.Wallet;

namespace Valr.Net.Clients.GeneralApi.Wallets
{
    internal class ValrClientGeneralApiWalletWire : IValrClientGeneralApiWalletWire
    {
        private readonly ILogger _logger;
        private readonly ValrClientGeneralApi _baseClient;

        public ValrClientGeneralApiWalletWire(ILogger logger, ValrClientGeneralApi valrClientGeneralApi)
        {
            _logger = logger;
            _baseClient = valrClientGeneralApi;
        }

        public async Task<WebCallResult<ValrWireTransferResponse>> CreateWithdrawalAsync(Guid wireAccountId, decimal amount, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("wireBankAccountId", wireAccountId);
            parameters.AddParameter("amount", amount);

            return await _baseClient.SendRequestInternal<ValrWireTransferResponse>(_baseClient.GetUrl(WireWalletEndpoints.Withdrawal),
                HttpMethod.Post, ct, parameters: parameters, signed: true).ConfigureAwait(false);
        }

        public async Task<WebCallResult<IEnumerable<ValrWireAccountInfo>>> GetWireAccountsAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            return await _baseClient.SendRequestInternal<IEnumerable<ValrWireAccountInfo>>(_baseClient.GetUrl(WireWalletEndpoints.WireAccounts),
                HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        public async Task<WebCallResult<ValrWireDepositInstructions>> GetWireDepositInstructionsAsync(Guid wireAccountId, long? receiveWindow = null, CancellationToken ct = default)
        {
            return await _baseClient.SendRequestInternal<ValrWireDepositInstructions>(_baseClient.GetUrl(WireWalletEndpoints.DepositInstructions.Replace(":identifier", wireAccountId.ToString())),
                HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }
    }
}
