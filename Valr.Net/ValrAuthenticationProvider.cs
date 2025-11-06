using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects;
using System.Text.Json;
using System.Security.Cryptography;
using System.Text;

namespace Valr.Net
{
    public class ValrAuthenticationProvider : AuthenticationProvider
    {
        private readonly HMACSHA512 encryptor;
        private readonly string? _testTimeStamp;
        private readonly string? _subAccountId;

        public ValrAuthenticationProvider(ApiCredentials credentials, string? subAccountId = null, string? timeStamp = null) : base(credentials)
        {
            if (credentials.Secret is null || credentials.Key is null)
                throw new ArgumentException("No valid API credentials provided. Key/Secret needed.");

            _subAccountId = subAccountId;
            _testTimeStamp = timeStamp;

            encryptor = new HMACSHA512(Encoding.UTF8.GetBytes(credentials.Secret.GetString()));
        }

        public override void ProcessRequest(RestApiClient apiClient, RestRequestConfiguration request)
        {
            // Add API key header
            request.Headers.Add("X-VALR-API-KEY", ApiKey);

            if (!request.Authenticated)
                return;

            var timestamp = string.IsNullOrEmpty(_testTimeStamp) ? GetTimestamp() : _testTimeStamp;

            // Add timestamp header
            request.Headers.Add("X-VALR-TIMESTAMP", timestamp);

            // Get the HTTP method and path
            var method = request.Method.ToString();
            var path = request.Uri.PathAndQuery;

            // Get parameters based on position
            var parameters = request.GetPositionParameters();

            // Build signature based on parameter position
            string signature;
            if (request.ParameterPosition == HttpMethodParameterPosition.InUri)
            {
                // For GET/DELETE requests with query parameters
                signature = SignRequest(timestamp, method, path, _subAccountId, parameters.Count > 0 ? parameters : null);
            }
            else
            {
                // For POST/PUT requests with body parameters
                signature = SignRequest(timestamp, method, path, _subAccountId, request.BodyParameters.Count > 0 ? request.BodyParameters : null);
            }

            // Add signature header
            request.Headers.Add("X-VALR-SIGNATURE", signature);

            // Add sub-account ID header if provided
            if (!string.IsNullOrEmpty(_subAccountId))
            {
                request.Headers.Add("X-VALR-SUB-ACCOUNT-ID", _subAccountId);
            }
        }

        public Dictionary<string, string> GetAuthenticationHeaders(string path)
        {
            var timestamp = string.IsNullOrEmpty(_testTimeStamp) ? GetTimestamp() : _testTimeStamp;

            var headers = new Dictionary<string, string>();

            headers.Add("X-VALR-API-KEY", Credentials.Key.GetString());

            headers.Add("X-VALR-SIGNATURE", SignRequest(timestamp, "GET", path, _subAccountId));
            headers.Add("X-VALR-TIMESTAMP", timestamp);

            if (!string.IsNullOrEmpty(_subAccountId))
            {
                headers.Add("X-VALR-SUB-ACCOUNT-ID", _subAccountId);
            }

            return headers;
        }

        private string GetTimestamp()
        {
            return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
        }

        private string SignRequest(string timestamp, string verb, string path, string subAccountId, Dictionary<string, object>? body = null)
        {
            string b = string.Empty;

            if (body?.Count != 0)
            {
                // Use System.Text.Json for serialization. Use options to allow numbers and strings; complex objects should be converted to primitives before calling.
                b = JsonSerializer.Serialize(body, new JsonSerializerOptions
                {
                    // Preserve property naming as-is to match previous behavior
                    PropertyNameCaseInsensitive = true,
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                });
            }

            var payload = timestamp + verb.ToUpper() + path + b + subAccountId;
            byte[] payloadBytes = Encoding.UTF8.GetBytes(payload);

            byte[] hash = encryptor.ComputeHash(payloadBytes);
            return BytesToHexString(hash);
        }

        private new string BytesToHexString(byte[] hash)
        {
            StringBuilder result = new StringBuilder(hash.Length * 2);
            foreach (var b in hash)
            {
                result.Append(b.ToString("x2"));
            }
            return result.ToString();
        }
    }
}
