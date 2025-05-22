using Microsoft.AspNetCore.Mvc;

namespace WEBApi.Dtos
{
    public class LoginDto
    {
        public string Email {  get; set; }
        public string Password { get; set; }

    }
}
