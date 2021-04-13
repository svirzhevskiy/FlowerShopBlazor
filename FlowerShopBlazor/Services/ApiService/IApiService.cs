using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlowerShopBlazor.Services.ApiService
{
    public interface IApiService
    {
        Task<ApiResponse<T>> Get<T>(string path, IDictionary<string, string> @params = null);
        Task<ApiResponse<T>> Post<T>(string path, object value = null);
    }
}