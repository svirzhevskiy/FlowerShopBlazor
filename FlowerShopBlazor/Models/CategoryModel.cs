using System;
using System.Collections.Generic;

namespace FlowerShopBlazor.Models
{
    public class CategoryModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public List<string> Properties { get; set; }
    }
}