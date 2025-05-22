using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using WEBApi.Data;


namespace WEBApi.Controller;




{

[ApiController]
[Router("rooms")]

    public class RoomsController : ControllerBase
    {
       private readonly AppDbContext _context;
       public RoomsController(AppDbContext context)
    {
        _context = context;
    }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetRooms()
    {
        var rooms = await _context.Rooms.ToListAsync();
        return Ok(rooms);
    }
    
}

