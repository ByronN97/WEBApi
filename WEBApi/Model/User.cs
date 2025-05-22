using Microsoft.AspNetCore.Mvc;

namespace WEBApi.Model
{
    public class User 
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}
