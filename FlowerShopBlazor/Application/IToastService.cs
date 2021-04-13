using System;
using FlowerShopBlazor.Common;

namespace FlowerShopBlazor.Application
{
    public interface IToastService
    {
        event Action<string, ToastType> OnShow;
        event Action OnHide;
        void ShowToast(string message, ToastType type);
    }
}