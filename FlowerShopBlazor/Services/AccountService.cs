using System;
using System.Threading.Tasks;
using FlowerShopBlazor.Application;
using FlowerShopBlazor.Common;
using FlowerShopBlazor.Models;
using Microsoft.AspNetCore.Components;

namespace FlowerShopBlazor.Services
{
    public class AccountService : IAccountService
    {
        private readonly IApiService _api;
        private readonly ILocalStorageService _localStorageService;
        private readonly NavigationManager _navigationManager;
        private readonly IToastService _toastService;
        private const string Url = "account";
        
        public UserModel User { get; private set; }

        public AccountService(
            IApiService api, 
            ILocalStorageService localStorageService, 
            NavigationManager navigationManager, 
            IToastService toastService)
        {
            _api = api;
            _localStorageService = localStorageService;
            _navigationManager = navigationManager;
            _toastService = toastService;
        }

        public async Task Login(string email, string password)
        {
            try
            {
                var response = await _api.Post<UserModel>($"{Url}/Login", new {email, password});

                if (response.HasSuccessStatusCode)
                {
                    User = response.Value;
                    await _localStorageService.SetItem("user", User);
                }
            }
            catch (Exception ex)
            {
                _toastService.ShowToast("Error", ToastType.Error);
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.Source);
                Console.WriteLine(ex.StackTrace);
            }
        }

        public async Task Logout()
        {
            User = null;
            await _localStorageService.RemoveItem("user");
            _navigationManager.NavigateTo("/Login");
        }
        
        public async Task Initialize()
        {
            User = await _localStorageService.GetItem<UserModel>("user");
        }
    }
}