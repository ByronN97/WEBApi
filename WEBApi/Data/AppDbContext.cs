using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using WEBApi.Model;
namespace WEBApi.Data
{
    public class AppDbContext : DbContext
    {
           public AppDbContext(DbContextOptions<AppDbContext> options) { }
           public DbSet<User> Users { get; set; }
           public DbSet<Room> Rooms { get; set; }
           public DbSet<Reservation> Reservations { get; set; }

    }

    protected override void onModalCreating(DbModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Reservation>()
            .HasIndex(r => new { r.RoomId, r.Fecha, r.HoraInicio, r.HoraFin })
            .IsUnique(false);

        base.onModalCreating(modelBuilder)

    }


    public class DbContextOptions<T>
    {
    }
}
