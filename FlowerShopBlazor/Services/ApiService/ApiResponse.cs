using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace FlowerShopBlazor.Services.ApiService
{
    public class ApiResponse<T>
    {
        public T Value { get; set; }
        public string[] Errors { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public bool HasSuccessStatusCode { get; set; }
        
        private ApiResponse() { }

        private async Task<ApiResponse<T>> Initialize(HttpResponseMessage response)
        {
            StatusCode = response.StatusCode;
            HasSuccessStatusCode = response.IsSuccessStatusCode;
            Value = default(T);
            
            var data = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    Value = JsonSerializer.Deserialize<T>(data, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
            else
            {
                try
                {
                    Errors = JsonSerializer.Deserialize<string[]>(data, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                }
                catch (Exception ex)
                {
                    Errors = new[]
                    {
                        data
                    };
                }
            }

            return this;
        }

        public static Task<ApiResponse<T>> CreateAsync(HttpResponseMessage response)
        {
            var result = new ApiResponse<T>();
            return result.Initialize(response);
        }
    }
}