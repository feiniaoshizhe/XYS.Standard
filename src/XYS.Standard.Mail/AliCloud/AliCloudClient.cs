using Microsoft.Extensions.Logging;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace XYS.Standard.Mail.AliCloud
{
    public class AliCloudClient
    {
        private readonly HttpClient _client;
        private readonly AliCloudConfig _config;
        private readonly ILogger<AliCloudClient> _logger;
        private string _domain = "dm.aliyuncs.com";

        /// <summary>
        /// Json序列化配置项
        /// </summary>
        static readonly JsonSerializerOptions jsOptions = new JsonSerializerOptions()
        {
            IgnoreNullValues = true,
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        /// <summary>
        /// Url编码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        static string UrlEncode(string str) => WebUtility.UrlEncode(str);
        /// <summary>
        /// 哈希编码
        /// </summary>
        /// <param name="byteArray"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        static byte[] HmacSha1(byte[] byteArray, byte[] key)
        {
            using var myhmacsha1 = new HMACSHA1(key);
            var hashArray = myhmacsha1.ComputeHash(byteArray);
            return hashArray;
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="config"></param>
        /// <param name="logger"></param>
        public AliCloudClient(HttpClient httpClient,
                             AliCloudConfig config,
                             ILogger<AliCloudClient> logger)
        {
            _client = httpClient;
            _config = config;
            _client.BaseAddress = new Uri("https://" + _domain);
            _logger = logger;
        }

        #region method
        /// <summary>
        /// Get抽象方法
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="uri"></param>
        /// <param name="queryStrings"></param>
        /// <returns></returns>
        /// <exception cref="OpenApiException"></exception>
        async Task<TResult> GetAsync<TResult>(string uri, Dictionary<string, string> queryStrings = null)
        {
            var url = GenerateSignatureUrl("GET", uri, queryStrings);
            var response = await _client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<TResult>(await response.Content.ReadAsStreamAsync());
            }
            else
            {
                var error = await JsonSerializer.DeserializeAsync<ErrorResponse>(await response.Content.ReadAsStreamAsync());
                _logger.LogError($"[REQ] {uri} , {error.RequestId} , {error.Error.Code} , {error.Error.Message}");
                throw new AliCloudException(error, response.StatusCode);
            }
        }
        /// <summary>
        /// Post抽象方法
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="uri"></param>
        /// <param name="request"></param>
        /// <param name="queryStrings"></param>
        /// <returns></returns>
        /// <exception cref="OpenApiException"></exception>
        async Task<TResult> PostAsync<TRequest, TResult>(string uri, TRequest request, Dictionary<string, string> queryStrings = null)
        {
            var url = GenerateSignatureUrl("POST", uri, queryStrings);
            var jsonRequest = new StringContent(JsonSerializer.Serialize(request, jsOptions), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(url, jsonRequest);
            if (response.IsSuccessStatusCode)
            {
                var c = await response.Content.ReadAsStringAsync();
                return await JsonSerializer.DeserializeAsync<TResult>(await response.Content.ReadAsStreamAsync());
                //return default(TResult);
            }
            else
            {
                var error = await JsonSerializer.DeserializeAsync<ErrorResponse>(await response.Content.ReadAsStreamAsync());
                _logger.LogError($"[REQ] {uri} , {error.RequestId} , {error.Error.Code} , {error.Error.Message}");
                throw new AliCloudException(error, response.StatusCode);
            }

        }
        #endregion

        #region private method
        private string GenerateSignatureUrl(string method, string path, Dictionary<string, string> queryStrings)
        {
            var Expires = (3600 * 9).ToString();
            var Timestamp = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var sortedDic = new SortedDictionary<string, string>(queryStrings ?? new Dictionary<string, string>());
            var urlParam = new StringBuilder();
            urlParam.Append($"{path}?");
            var @params = new List<string>()
            {
                $"{nameof(_config.AccessKeyId)}={UrlEncode(_config.AccessKeyId)}",
                $"{nameof(Expires)}={UrlEncode(Expires)}",
                $"{nameof(Timestamp)}={UrlEncode(Timestamp)}"
            };
            foreach (var k in sortedDic)
            {
                @params.Add($"{k.Key}={UrlEncode(k.Value)}");
            }
            urlParam.Append(string.Join("&", @params));
            var p = $"{method.ToUpper()}{_domain}" + urlParam.ToString();
            var Signature = UrlEncode(Convert.ToBase64String(HmacSha1(Encoding.UTF8.GetBytes(p), Encoding.UTF8.GetBytes(_config.AccessKeySecret))));
            urlParam.Append($"&{nameof(Signature)}={Signature}");
            return urlParam.ToString();
        }
        #endregion
    }
}
