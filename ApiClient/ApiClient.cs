using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics;
using static BlueMoon.Common.ApiClient;

namespace BlueMoon.Common
{
    public class ApiClient
    {
        public interface IBearerTokenPool
        {
            Task<string> GetToken();
        }
        public enum AuthType
        {
            None,
            Basic,
            Digest,
            NTLM,
            Kerberos,
            Negotiate,
            OAuth,
            SAML //not implemented yet

        }
        const string LogFolder = "logs";
        static ApiClient()
        {
            if (!Directory.Exists(LogFolder)) Directory.CreateDirectory(LogFolder); 
        }
        string _baseUrl, _locker, _key;
        AuthType _authType = AuthType.None;
        HttpClient _client = null;
        IBearerTokenPool _tokenPool;
        public ApiClient(string baseUrl, AuthType authType = AuthType.None, string locker = null, string key = null, IBearerTokenPool tokenPool = null)
        {
            _baseUrl = baseUrl;
            _locker = locker;
            _key = key;
            _authType = authType;
            _tokenPool = tokenPool;
        }
        public IBearerTokenPool TokenPool
        {
            get { return _tokenPool; }
            set { _tokenPool = value; }
        }
        public string BaseUrl => _baseUrl;
        public string Locker => _locker;
        protected virtual HttpClient CreateClient()
        {
            if (_client != null) return _client;
            var handler = new HttpClientHandler();
            if (_authType != AuthType.None && !string.IsNullOrEmpty(_locker))
            {
                //handler.Credentials = new NetworkCredential(_locker, _key);
                handler.Credentials = new CredentialCache
                {
                    {
                        new Uri(_baseUrl), _authType.ToString(),
                        new NetworkCredential(_locker, _key)
                    }
                };

                handler.PreAuthenticate = true;
                handler.AllowAutoRedirect = false;
            }
            
            var client = new HttpClient(handler);
            client.BaseAddress = new Uri(_baseUrl);
            client.Timeout = TimeSpan.FromSeconds(150);
            return _client = client;
        }

        public async Task<(string data, ApiResponse apiResponse)> Execute(string path, string payload = null, string contentType = null, HttpMethod method = null, string traceId = null, [CallerMemberName] string caller = null, [CallerLineNumber] int line = 0, [CallerFilePath] string src = null)
        {
            if (string.IsNullOrEmpty(traceId)) traceId = Guid.NewGuid().ToString();
            var client = CreateClient();
            if (_tokenPool != null)
            {
                string token = await _tokenPool.GetToken();
                if (!string.IsNullOrEmpty(token)) client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri(client.BaseAddress, path);
            HttpResponseMessage response = null;
            if (string.IsNullOrEmpty(payload))
            {
                if (method == null) method = HttpMethod.Get;
            }
            else
            {
                if (method == null) method = HttpMethod.Post;
                request.Content = new StringContent(payload, null, contentType);
                request.Content.Headers.Remove("Content-Type");
                request.Content.Headers.Add("Content-Type", contentType);
            }


            
            ApiResponse apiResponse = new ApiResponse();
            apiResponse.BeginRequestTime = DateTime.Now;
            request.Method = method;
            string responseData = null;
            try
            {
                
                response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
                apiResponse.CompletedRequestTime = DateTime.Now;
                if (response != null) using (response)
                {
                    apiResponse.HttpCode = (int)response.StatusCode;
                    apiResponse.Headers = response.Headers.Concat(response.Content == null ? Enumerable.Empty<KeyValuePair<string, IEnumerable<string>>>() : response.Content.Headers).ToList();
                    responseData = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    apiResponse.ErrorMessage = "No response server";
                }
            }
            catch(HttpRequestException ex)
            {
                apiResponse.ErrorMessage = ex.Message;

            }
            catch (Exception ex)
            {
                
                apiResponse.ErrorMessage = $"{ex.Message}\r\n{ex.StackTrace}";
            }

            _ = Log($@"
[{traceId}] {DateTime.Now}:
------------------------------
{method} {request.RequestUri} {(apiResponse.HttpCode > 0 ? apiResponse.HttpCode : "[Exception]")}
{request.Headers.Concat(request.Content == null ? Enumerable.Empty<KeyValuePair<string, IEnumerable<string>>>() : request.Content.Headers).Aggregate("", (r, p) => r + ($"{p.Key}: {string.Join(",", p.Value)}\r\n"))}
{payload}
------------------------------
{(response == null ? "[Unable to get response]" : response.Headers.Concat(request.Content == null ? Enumerable.Empty<KeyValuePair<string, IEnumerable<string>>>() : response.Content.Headers).Aggregate("", (r, p) => r + ($"{p.Key}: {string.Join(",", p.Value)}\r\n")))}
{responseData}
------------------------------
Elapsed time: {apiResponse.ElapsedTime}(ms) ({apiResponse.BeginRequestTime} - {(apiResponse.ElapsedTime < 0 ? "[Timeout]" : apiResponse.CompletedRequestTime)})
Error: {apiResponse.ErrorMessage ?? "No error."}
Trace: Excuted from method {caller} at line {line} of {src};
=================================================================================================
");
            return (responseData, apiResponse);
        }
        private async Task Log(string data)
        {
            await Task.Run(() =>
            {
                try
                {
                    File.AppendAllText($"{LogFolder}/api_requests_{DateTime.Now.ToString("yyyy-MM-dd")}.log", data);
                }
                catch
                {
                }
            });
        }
    }

    public class ApiResponse
    {
        public List<KeyValuePair<string, IEnumerable<string>>> Headers { get; set; }
        public int HttpCode { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime BeginRequestTime { get; set; }
        public DateTime CompletedRequestTime { get; set; }
        public double ElapsedTime { get { return CompletedRequestTime.Subtract(BeginRequestTime).TotalMilliseconds; } }
    }
    public class EntraIdTokenManager : IBearerTokenPool
    {

        static Dictionary<string, (string token, DateTime expiredTime)> s_Token_Pool = new Dictionary<string, (string token, DateTime expiredTime)>();

        private string _tenantId, _clientId, _charm, _scope, _id;
        public EntraIdTokenManager(string tenantId,
            string clientId,
            string charm,
            string scope)
        {
            _tenantId = tenantId;
            _clientId = clientId;
            _charm = charm;
            _scope = scope;
            _id = $"{_tenantId}_{_clientId}_{_scope}";
        }

        async Task<(string token, DateTime expiredTime)> GetAccessTokenAsync()
        {
            var tokenEndpoint = $"https://login.microsoftonline.com/{_tenantId}/oauth2/v2.0/token";
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("client_id", _clientId),
                new KeyValuePair<string, string>("scope", _scope),
                new KeyValuePair<string, string>("client_secret", _charm),
                new KeyValuePair<string, string>("grant_type", "client_credentials")
            });
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.PostAsync(tokenEndpoint, content);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(json);
                string token = doc.RootElement.GetProperty("access_token").GetString();
                int expiresIn = doc.RootElement.GetProperty("expires_in").GetInt32();
                DateTime expiresAt = DateTime.UtcNow.AddSeconds(expiresIn - 30);//refresh token 30s in advance
                return (token, expiresAt);
            }
            else return default;

        }

        public async Task<string> GetToken()
        {
            if (s_Token_Pool.ContainsKey(_id))
            {
                var currentTokenInfo = s_Token_Pool[_id];
                if (currentTokenInfo.expiredTime > DateTime.UtcNow) return currentTokenInfo.token;
                else lock (s_Token_Pool) { s_Token_Pool.Remove(_id); }
            }
            //refresh key
            var tokenInfo = await GetAccessTokenAsync();
            if (tokenInfo == default) return null;
            lock (s_Token_Pool) { s_Token_Pool.Add(_id, tokenInfo); }
            return tokenInfo.token;


        }
    }
}
