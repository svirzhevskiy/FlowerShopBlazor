using FlowerShopBlazor.Application;

namespace FlowerShopBlazor.Services
{
    public class AccountService
    {
        private readonly IApiService _api;
        private const string Url = "account";

        public AccountService(IApiService api)
        {
            _api = api;
        }

        
    }
}