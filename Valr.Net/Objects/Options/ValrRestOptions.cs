using System;
using CryptoExchange.Net.Objects;

namespace Valr.Net.Objects.Options
{
    // Replace the previous minimal facade with a thin subclass of the real RestApiClientOptions from CryptoExchange.Net 9.x
    public class ValrRestOptions : RestApiClientOptions
    {
        public static ValrRestOptions Default { get; set; } = new ValrRestOptions();

        public TimeSpan ReceiveWindow { get; set; } = TimeSpan.FromSeconds(5);

        public ValrRestOptions() : base()
        {
        }

        internal ValrRestOptions(ValrClientOptions baseOptions) : base()
        {
            if (baseOptions == null)
                return;

            ReceiveWindow = baseOptions.ReceiveWindow;
            AutoTimestamp = baseOptions.SpotApiOptions.AutoTimestamp;
            TimestampRecalculationInterval = baseOptions.SpotApiOptions.TimestampRecalculationInterval;
        }

        internal ValrRestOptions(string baseAddress) : base(baseAddress) { }
    }
}
