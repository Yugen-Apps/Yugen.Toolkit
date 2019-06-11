using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using UwpCommunity.Standard.Data;
using UwpCommunity.Standard.Helpers;
using UwpCommunity.Standard.Providers;

namespace UwpCommunity.Standard.Http
{
    public class RestClient
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public async Task<Result<T>> ExecuteRequest<T>(string uri, HttpMethod httpMethod, object parameters, BodyTypeEnum bodyTypeEnum, string accessToken = null)
        {
            try
            {
                var response = await ExecuteRequest(uri, httpMethod, parameters, bodyTypeEnum, accessToken);
                return Result.IsOk(await JsonProvider.ToObjectAsync<T>(response.Value), response.Success, "");
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(typeof(RestClient), exception);
                return Result.Fail<T>("");
            }
        }

        public async Task<Result<string>> ExecuteRequest(string uri, HttpMethod httpMethod, object parameters, BodyTypeEnum bodyTypeEnum, string accessToken = null)
        {
            try
            {
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage(httpMethod, new Uri(uri));

                if (!string.IsNullOrEmpty(accessToken))
                    httpRequestMessage.Headers.Add("Authorization", $"Bearer {accessToken}");

                if (parameters != null)
                {
                    switch (bodyTypeEnum)
                    {
                        case BodyTypeEnum.Json:
                            httpRequestMessage.Content = await BuildContent(parameters);
                            break;
                        case BodyTypeEnum.MultipartFormData:
                            httpRequestMessage.Content = BuildFormUrlEncodedContent(parameters as Dictionary<string, string>);
                            break;
                        case BodyTypeEnum.UrlEncodedFormData:
                            break;
                    }
                }

                var response = await _httpClient.SendAsync(httpRequestMessage);

                if (response.IsSuccessStatusCode)
                    return Result.Ok(await response.Content.ReadAsStringAsync());

                LoggerHelper.WriteLine(typeof(RestClient), $"response failed: {response}");
                return Result.Fail<string>("");
            }
            catch (Exception exception)
            {
                LoggerHelper.WriteLine(typeof(RestClient), exception);
                return Result.Fail<string>("");
            }
        }

        private async Task<StringContent> BuildContent(object parameters)
        {
            var jsonp = await JsonProvider.StringifyAsync(parameters);
            var content = new StringContent(jsonp);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return content;
        }

        private FormUrlEncodedContent BuildFormUrlEncodedContent(Dictionary<string, string> parameters) => new FormUrlEncodedContent(parameters);
    }
}