namespace FlowerShopBlazor.Models
{
    public class UserModel
    {
        public string Email { get; set; }
        public string Role { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}