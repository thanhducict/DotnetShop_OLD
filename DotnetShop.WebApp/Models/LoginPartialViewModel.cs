namespace DotnetShop.WebApp.Models
{
    public class LoginPartialViewModel
    {
        public bool IsSignedIn { get; set; }
        public string ReturnUrl { get; set; }
        public string Username { get; set; }
    }
}
