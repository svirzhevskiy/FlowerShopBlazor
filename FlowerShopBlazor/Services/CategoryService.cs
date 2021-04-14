using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlowerShopBlazor.Application;
using FlowerShopBlazor.Models;

namespace FlowerShopBlazor.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IApiService _api;
        private const string Url = "category";
        
        private readonly CategoryModel _fakeCategory = new()
        {
            Id = Guid.NewGuid(), Title = "Flowers", Properties = new List<string> {"Category", "Color"}
        };

        public CategoryService(IApiService api)
        {
            _api = api;
        }

        public async Task<List<CategoryModel>> GetAll()
        {
            try
            {
                var result = await _api.Get<List<CategoryModel>>(Url);
                if (result.HasSuccessStatusCode)
                {
                    return result.Value;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return new() {_fakeCategory};
        }
    }
}