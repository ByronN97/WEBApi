using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WEBApi.Data;
using WEBApi.Model;

[ApiController]
[Route("reservations")]
[Authorize]
public class ReservationsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ReservationsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateReservation(ReservationDTO dto)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

        if (dto.HoraInicio >= dto.HoraFin)
            return BadRequest("Hora de inicio debe ser menor a la hora de fin");

        var conflictos = await _context.Reservations.AnyAsync(r =>
            r.RoomId == dto.RoomId &&
            r.Fecha == dto.Fecha &&
            ((dto.HoraInicio >= r.HoraInicio && dto.HoraInicio < r.HoraFin) ||
             (dto.HoraFin > r.HoraInicio && dto.HoraFin <= r.HoraFin) ||
             (dto.HoraInicio <= r.HoraInicio && dto.HoraFin >= r.HoraFin)));

        if (conflictos)
            return Conflict("La sala ya está reservada en ese horario");

        var yaReservado = await _context.Reservations.AnyAsync(r =>
            r.UserId == userId && r.Fecha == dto.Fecha &&
            ((dto.HoraInicio >= r.HoraInicio && dto.HoraInicio < r.HoraFin) ||
             (dto.HoraFin > r.HoraInicio && dto.HoraFin <= r.HoraFin)));

        if (yaReservado)
            return Conflict("Ya tienes una reserva en esa franja horaria");

        var reserva = new Reservation
        {
            UserId = userId,
            RoomId = dto.RoomId,
            Fecha = dto.Fecha,
            HoraInicio = dto.HoraInicio,
            HoraFin = dto.HoraFin
        };

        _context.Reservations.Add(reserva);
        await _context.SaveChangesAsync();
        return Ok(reserva);
    }

    [HttpGet("user")]
    public async Task<IActionResult> GetUserReservations()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var reservas = await _context.Reservations
            .Where(r => r.UserId == userId)
            .Include(r => r.Room)
            .ToListAsync();

        return Ok(reservas);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> CancelReservation(int id)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var reserva = await _context.Reservations.FindAsync(id);

        if (reserva == null || reserva.UserId != userId)
            return NotFound();

        _context.Reservations.Remove(reserva);
        await _context.SaveChangesAsync();
        return Ok();
    }
}