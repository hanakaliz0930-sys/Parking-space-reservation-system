using api.Entities;
using Microsoft.EntityFrameworkCore;

public class ParkingDataContext : DbContext
{
    public DbSet<ParkingSpot> ParkingSpots { get; set; }
    public DbSet<Reservation> Reservations { get; set; }

    public ParkingDataContext(DbContextOptions<ParkingDataContext> options) : base(options)
    {
    }
}