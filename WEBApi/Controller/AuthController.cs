using Microsoft.AspNetCore.Mvc;
using WEBApi.Data;
using WEBApi.Dtos;
using WEBApi.Model;

namespace WEBApi.Controller

{
    [ApiController]
    [Route("auth")]

    public class AuthController :ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public AuthController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            if (_context.Users.Any(u => u.Email == dto.Email))
                return BadRequest("Email ya registrado");

            var user = new User
            {
                Nombre = dto.Nombre,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }


}
}
