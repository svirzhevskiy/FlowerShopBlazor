using System;

namespace FlowerShopBlazor.Models
{
    public class ProductModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public int Price { get; set; }
        public int? OldPrice { get; set; }
        public string Properties { get; set; }
        
        public CategoryModel Category { get; set; }
    }
}