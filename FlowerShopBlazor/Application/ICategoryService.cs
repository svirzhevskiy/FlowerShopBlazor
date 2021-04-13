using System.Collections.Generic;
using System.Threading.Tasks;
using FlowerShopBlazor.Models;

namespace FlowerShopBlazor.Application
{
    public interface ICategoryService
    {
        Task<List<CategoryModel>> GetAll();
    }
}