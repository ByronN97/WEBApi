using Microsoft.AspNetCore.Mvc;

namespace WEBApi.Dtos
{
    public class RegisterDto
    {
        
       public string Nombre { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}
