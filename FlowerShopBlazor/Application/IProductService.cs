using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlowerShopBlazor.Models;

namespace FlowerShopBlazor.Application
{
    public interface IProductService
    {
        Task<List<ProductModel>> GetAll(Guid categoryId);
        Dictionary<string, bool> GetPropertyValues(string filter);
        List<ProductModel> Filter(Dictionary<string, Dictionary<string, bool>> filters, (int, int) price);
    }
}