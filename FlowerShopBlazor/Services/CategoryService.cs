using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlowerShopBlazor.Models;

namespace FlowerShopBlazor.Services
{
    public class CategoryService
    {
        private readonly CategoryModel _fakeCategory = new()
        {
            Id = Guid.NewGuid(), Title = "Flowers", Properties = new List<string> {"Category", "Color"}
        };
        
        public async Task<List<CategoryModel>> GetAll()
        {
            return new() {_fakeCategory};
        }
    }
}