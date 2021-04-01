using System;
using System.Collections.Generic;
using FlowerShopBlazor.Models;
using Microsoft.AspNetCore.Components;

namespace FlowerShopBlazor.Services
{
    public class AppState
    {
        public event Action<ComponentBase, string> StateChanged;

        private void NotifyStateChanged(ComponentBase source, string property) =>
            StateChanged?.Invoke(source, property);

        public List<ProductModel> Cart { get; set; } = new();
    }
}