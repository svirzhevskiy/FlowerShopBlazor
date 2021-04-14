using System.Threading.Tasks;
using FlowerShopBlazor.Models;

namespace FlowerShopBlazor.Application
{
    public interface IAccountService
    {
        UserModel User { get; }
        Task Login(string email, string password);
        Task Logout();
        Task Initialize();
    }
}