using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Yugen.Toolkit.Standard.Core.Helpers;
using Yugen.Toolkit.Standard.Core.Models;
using Yugen.Toolkit.Standard.Json;

namespace Yugen.Toolkit.Standard.Http
{
    /// <summary>
    /// RestClient
    /// </summary>
    public class RestClient
    {
        private readonly HttpClient _httpClient = new HttpClient();

        /// <summary>
        /// Executes a request asynchronously, authenticating if needed
        /// </summary>
        /// <typeparam name="T">Target deserialization type</typeparam>
        /// <param name="uri">url</param>
        /// <param name="httpMethod">http method</param>
        /// <param name="body">body</param>
        /// <param name="bodyContentType">body Content Type</param>
        /// <param name="bearerToken">bearer Token</param>
        /// <returns>Deserialized T object wrapped in Result</returns>
        public async Task<Result<T>> ExecuteRequest<T>(string uri, HttpMethod httpMethod, object body, BodyContentType bodyContentType, string bearerToken = null)
        {
            try
            {
                Result<string> response = await ExecuteRequest(uri, httpMethod, body, bodyContentType, bearerToken);
                return Result.IsOk(response.IsSuccess, await JsonProvider.ToObjectAsync<T>(response.Value), "");
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(typeof(RestClient), exception);
                return Result.Fail<T>("");
            }
        }

        /// <summary>
        /// Executes a request asynchronously, authenticating if needed
        /// </summary>
        /// <param name="uri">url</param>
        /// <param name="httpMethod">http method</param>
        /// <param name="body">body</param>
        /// <param name="bodyContentType">body Content Type</param>
        /// <param name="bearerToken">bearer Token</param>
        /// <returns>string wrapped in Result</returns>
        public async Task<Result<string>> ExecuteRequest(string uri, HttpMethod httpMethod, object body, BodyContentType bodyContentType, string bearerToken = null)
        {
            try
            {
                var httpRequestMessage = new HttpRequestMessage(httpMethod, new Uri(uri));

                if (!string.IsNullOrEmpty(bearerToken))
                {
                    httpRequestMessage.Headers.Add("Authorization", $"Bearer {bearerToken}");
                }

                if (body != null)
                {
                    switch (bodyContentType)
                    {
                        case BodyContentType.Json:
                            httpRequestMessage.Content = await BuildJsonContent(body);
                            break;
                        case BodyContentType.MultipartFormData:
                            httpRequestMessage.Content = BuildFormUrlEncodedContent(body as Dictionary<string, string>);
                            break;
                        case BodyContentType.WwwFormUrlEncoded:
                            httpRequestMessage.Content = await BuildWwwFormUrlEncodedContent(body);
                            break;
                    }
                }

                HttpResponseMessage response = await _httpClient.SendAsync(httpRequestMessage);
                if (response.IsSuccessStatusCode)
                {
                    return Result.Ok(await response.Content.ReadAsStringAsync());
                }

                LoggerHelper.WriteLine(typeof(RestClient), $"response failed: {response}");
                return Result.Fail<string>("");
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(typeof(RestClient), exception);
                return Result.Fail<string>("");
            }
        }

        private async Task<StringContent> BuildJsonContent(object body)
        {
            var json = await JsonProvider.StringifyAsync(body);
            var content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return content;
        }

        private async Task<StringContent> BuildWwwFormUrlEncodedContent(object body)
        {
            var json = await JsonProvider.StringifyAsync(body);
            var content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            return content;
        }

        private FormUrlEncodedContent BuildFormUrlEncodedContent(Dictionary<string, string> body) =>
            new FormUrlEncodedContent(body);


        private static string ToQueryString(Dictionary<string, string> pairs)
        {
            var list = pairs.Select(pair => $"{pair.Key}={pair.Value}").ToList();
            return "?" + string.Join("&", list);
        }
    }
}