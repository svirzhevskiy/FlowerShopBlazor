using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FlowerShopBlazor.Application;
using FlowerShopBlazor.Models;
using Microsoft.Extensions.Configuration;

namespace FlowerShopBlazor.Services.ApiService
{
    public class ApiService : IApiService
    {
        private readonly string _apiUrl;
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;
        
        public ApiService(IConfiguration configuration, HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _apiUrl = configuration.GetValue<string>("ApiUrl");
            _httpClient = httpClient;
            _localStorageService = localStorageService;
        }
        
        public async Task<ApiResponse<T>> Get<T>(string path, IDictionary<string, string> @params = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, BuildUri(path, @params));

            var response = await SendRequest(request);
            
            return await ApiResponse<T>.CreateAsync(response);
        }


        public async Task<ApiResponse<T>> Post<T>(string path, object value = null)
        {
            value ??= string.Empty;
            var valueString = JsonSerializer.Serialize(value);
            var content = new StringContent(valueString, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, BuildUri(path)) {Content = content};

            var response = await SendRequest(request);

            return await ApiResponse<T>.CreateAsync(response);
        }

        private async Task<HttpResponseMessage> SendRequest(HttpRequestMessage request)
        {
            var user = await _localStorageService.GetItem<UserModel>("user");
            if (user != null)
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", user.AccessToken);

            return await _httpClient.SendAsync(request);
        }

        private string BuildUri(string path, IDictionary<string, string> @params = null)
        {
            var result = new UriBuilder($"{_apiUrl}/{path}");

            if (@params == null || @params.Count <= 0)
                return result.Uri.AbsoluteUri;
            
            foreach (var key in @params.Keys)
            {
                var queryPart = key + "=" + @params[key];
                if (result.Query != null && result.Query.Length > 1)
                    result.Query = result.Query[1..] + "&" + queryPart;
                else
                    result.Query = queryPart;
            }

            return result.Uri.AbsoluteUri;
        }
    }
}