using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using FlowerShopBlazor.Data;
using FlowerShopBlazor.Models;

namespace FlowerShopBlazor.Services
{
    public class ProductService
    {
        private List<ProductModel> _allProducts = new();
        
        public async Task<List<ProductModel>> GetAll(Guid categoryId)
        {
            _allProducts = FlowerData.GetFakeData();
            
            return _allProducts;
        }
        
        public Dictionary<string, bool> GetPropertyValues(string filter)
        {
            Dictionary<string, bool> result = new();

            foreach (var product in _allProducts)
            {
                var props = JsonSerializer.Deserialize<Dictionary<string, string>>(product.Properties);

                if (props == null) continue;

                foreach (var (key, value) in props)
                    if (key == filter && !result.ContainsKey(value))
                        result.Add(value, false);
            }

            return result;
        }
        
        public List<ProductModel> Filter(Dictionary<string, Dictionary<string, bool>> filters, (int, int) price)
        {
            var products = _allProducts
                .Where(x => price.Item2 <= 0 || x.Price >= price.Item1 && x.Price <= price.Item2);
            
            foreach (var (filterTitle, filterProps) in filters)
            {
                var selectedProps = filterProps.Where(x => x.Value).ToDictionary(x => x.Key);
                
                if (!selectedProps.Any()) continue;
                
                products = products.Where(p =>
                {
                    var productProps = JsonSerializer.Deserialize<Dictionary<string, string>>(p.Properties);
    
                    if (productProps == null) 
                        return false;

                    return productProps.TryGetValue(filterTitle, out var productProp) 
                           && selectedProps.ContainsKey(productProp);
                });
            }

            return products.ToList();
        }
    }
}