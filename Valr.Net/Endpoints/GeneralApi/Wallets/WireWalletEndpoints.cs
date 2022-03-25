﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valr.Net.Endpoints.GeneralApi.Wallets
{
    internal static class WireWalletEndpoints
    {
        internal const string WireAccounts = "v1/wire/accounts";
        internal const string DepositInstructions = "v1/wire/accounts/:identifier/instructions";
        internal const string Withdrawal = "v1/wire/withdrawals";
    }
}
