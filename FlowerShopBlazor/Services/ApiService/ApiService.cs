using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace FlowerShopBlazor.Services.ApiService
{
    public class ApiService : IApiService
    {
        private readonly string _apiUrl;
        private readonly HttpClient _httpClient;
        
        public ApiService(IConfiguration configuration, HttpClient httpClient)
        {
            _apiUrl = configuration.GetValue<string>("ApiUrl");
            _httpClient = httpClient;
        }
        
        public async Task<ApiResponse<T>> Get<T>(string path, IDictionary<string, string> @params = null)
        {
            var response = await _httpClient.GetAsync(BuildUri(path, @params));

            return await ApiResponse<T>.CreateAsync(response);
        }


        public async Task<ApiResponse<T>> Post<T>(string path, object value = null)
        {
            value ??= string.Empty;
            var valueString = JsonSerializer.Serialize(value);
            var content = new StringContent(valueString, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(BuildUri(path), content);

            return await ApiResponse<T>.CreateAsync(response);
        }

        private string BuildUri(string path, IDictionary<string, string> @params = null)
        {
            var result = new UriBuilder($"{_apiUrl}{path}");

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