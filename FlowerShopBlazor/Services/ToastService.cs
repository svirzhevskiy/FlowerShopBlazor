using System;
using System.Timers;
using FlowerShopBlazor.Application;
using FlowerShopBlazor.Common;

namespace FlowerShopBlazor.Services
{
    public class ToastService : IToastService, IDisposable
    {
        public event Action<string, ToastType> OnShow;
        public event Action OnHide;
        private Timer _countdown;

        public void ShowToast(string message, ToastType type)
        {
            OnShow?.Invoke(message, type);
            StartCountdown();
        }

        private void StartCountdown()
        {
            SetCountdown();

            if (_countdown.Enabled)
            {
                _countdown.Stop();
                _countdown.Start();
            }
            else
            {
                _countdown.Start();
            }
        }

        private void SetCountdown()
        {
            if (_countdown != null) 
                return;
            
            _countdown = new Timer(2500);
            _countdown.Elapsed += HideToast;
            _countdown.AutoReset = false;
        }

        private void HideToast(object source, ElapsedEventArgs args)
        {
            OnHide?.Invoke();
        }

        public void Dispose()
        {
            _countdown?.Dispose();
        }
    }
}