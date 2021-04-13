using System.Collections.Generic;
using System.Threading.Tasks;
using FlowerShopBlazor.Services.ApiService;

namespace FlowerShopBlazor.Application
{
    public interface IApiService
    {
        Task<ApiResponse<T>> Get<T>(string path, IDictionary<string, string> @params = null);
        Task<ApiResponse<T>> Post<T>(string path, object value = null);
    }
}