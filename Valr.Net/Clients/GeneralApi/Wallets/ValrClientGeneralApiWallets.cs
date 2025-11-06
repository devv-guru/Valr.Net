using Microsoft.Extensions.Logging;
using Valr.Net.Interfaces.Clients.GeneralApi.Wallets;

namespace Valr.Net.Clients.GeneralApi.Wallets;

public class ValrClientGeneralApiWallets : IValrClientGeneralApiWallets
{
    private readonly ILogger _logger;
    private readonly ValrClientGeneralApi _baseClient;

    public IValrClientGeneralApiWalletCrypto Crypto { get; }
    public IValrClientGeneralApiWalletFiat Fiat { get; }
    public IValrClientGeneralApiWalletWire Wire { get; }

    internal ValrClientGeneralApiWallets(ILogger logger, ValrClientGeneralApi valrClientGeneralApi)
    {
        _logger = logger;
        _baseClient = valrClientGeneralApi;

        Crypto = new ValrClientGeneralApiWalletCrypto(_logger, _baseClient);
        Fiat = new ValrClientGeneralApiWalletFiat(_logger, _baseClient);
        Wire = new ValrClientGeneralApiWalletWire(_logger, _baseClient);
    }
}