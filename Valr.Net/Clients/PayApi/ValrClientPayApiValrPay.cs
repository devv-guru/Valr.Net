using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using Valr.Net.Endpoints.PayApi;
using Valr.Net.Enums;
using Valr.Net.Interfaces.Clients.PayApi;
using Valr.Net.Objects.Models.Pay;

namespace Valr.Net.Clients.PayApi
{
    public class ValrClientPayApiValrPay : IValrClientPayApiValrPay
    {
        private readonly ILogger _logger;
        private readonly ValrClientPayApi _baseClient;

        internal ValrClientPayApiValrPay(ILogger logger, ValrClientPayApi valrClientPayApi)
        {
            _logger = logger;
            _baseClient = valrClientPayApi;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<ValrPaymentResponse>> CreatePaymentAsync(ValrPaymentIdentifierType paymentIdentifierType, string recipientAccountId, string currency, decimal amount, string recipientNote, string senderNote, bool anonymous = false, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "amount", amount },
                { "currency", currency },
                { "anonymous", anonymous }
            };

            switch (paymentIdentifierType)
            {
                case ValrPaymentIdentifierType.CellNumber:
                    {
                        parameters.AddParameter("recipientCellNumber", recipientAccountId);
                        break;
                    }

                case ValrPaymentIdentifierType.Email:
                    {
                        parameters.AddParameter("recipientEmail", recipientAccountId);
                        break;
                    }

                case ValrPaymentIdentifierType.PayId:
                    {
                        parameters.AddParameter("recipientPayId", recipientAccountId);
                        break;
                    }
            }

            parameters.AddOptionalParameter("recipientNote", recipientNote);
            parameters.AddOptionalParameter("senderNote", recipientNote);


            var result = await _baseClient.SendRequestInternal<ValrPaymentResponse>(_baseClient.GetUrl(PayEndpoints.NewPayment), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<ValrPaymentHistoryResponse>> GetPaymentDetailsAsync(Guid id, long? receiveWindow = null, CancellationToken ct = default)
        {
            return await _baseClient.SendRequestInternal<ValrPaymentHistoryResponse>(_baseClient.GetUrl(PayEndpoints.PaymentDetails.Replace(":identifier", id.ToString())),
                HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<ICollection<ValrPaymentHistoryResponse>>> GetPaymentHistoryAsync(ValrPaymentStatus[]? status = null, int skip = 0, int limit = 10, long? receiveWindow = null, CancellationToken ct = default)
        {
            return await _baseClient.SendRequestInternal<ICollection<ValrPaymentHistoryResponse>>(_baseClient.GetUrl(PayEndpoints.PaymentHistory), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<ValrPaymentIdResponse>> GetPaymentIdAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            return await _baseClient.SendRequestInternal<ValrPaymentIdResponse>(_baseClient.GetUrl(PayEndpoints.PayId), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<ValrPaymentLimitResponse>> GetPaymentLimitsAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            return await _baseClient.SendRequestInternal<ValrPaymentLimitResponse>(_baseClient.GetUrl(PayEndpoints.PaymentLimit), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<ValrPaymentStatusResponse>> GetPaymentStatusAsync(string transactionId, long? receiveWindow = null, CancellationToken ct = default)
        {
            return await _baseClient.SendRequestInternal<ValrPaymentStatusResponse>(_baseClient.GetUrl(PayEndpoints.PaymentStatus.Replace(":transactionId", transactionId)),
                HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        }
    }
}
