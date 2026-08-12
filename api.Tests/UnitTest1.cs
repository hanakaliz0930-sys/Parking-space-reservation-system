using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using api.Entities;
using api.Controllers;
namespace api.Tests;

public class TestClass
{
    [Fact]
    public void OverlappingReservations()
    {
    SqliteConnection connectionSQL = new SqliteConnection("Data Source=:memory:");
    connectionSQL.Open();
    DbContextOptionsBuilder<ParkingDataContext> builder = new DbContextOptionsBuilder<ParkingDataContext>();
    builder.UseSqlite(connectionSQL);
    
    ParkingDataContext context = new ParkingDataContext(builder.Options);
    context.Database.EnsureCreated();
    var ParkingSpot1 = new ParkingSpot
        {
            Name = "FirstPlace"
        };
    context.ParkingSpots.Add(ParkingSpot1);
    context.SaveChanges();

    ReservationsController rcontroller = new ReservationsController(context);
    var reservation1 = new Reservation
    {
        ParkingSpotId = ParkingSpot1.Id,
        StartTime = DateTime.Now.AddHours(2),
        EndTime = DateTime.Now.AddHours(1)
    };
    rcontroller.createReservation(reservation1);
    Assert.False(rcontroller.ModelState.IsValid);
    }




     [Fact]
    public void CreateReservationNotExistSpot()
    {
    SqliteConnection connectionSQL = new SqliteConnection("Data Source=:memory:");
    connectionSQL.Open();
    DbContextOptionsBuilder<ParkingDataContext> builder = new DbContextOptionsBuilder<ParkingDataContext>();
    builder.UseSqlite(connectionSQL);
    
    ParkingDataContext context = new ParkingDataContext(builder.Options);
    context.Database.EnsureCreated();
    var ParkingSpot1 = new ParkingSpot
        {
            Name = "FirstPlace"
        };
    context.ParkingSpots.Add(ParkingSpot1);
    context.SaveChanges();

    ReservationsController rcontroller = new ReservationsController(context);
    var reservation1 = new Reservation
    {
        ParkingSpotId = 2,
        StartTime = DateTime.Now.AddHours(1),
        EndTime = DateTime.Now.AddHours(2)
    };
    rcontroller.createReservation(reservation1);
    Assert.False(rcontroller.ModelState.IsValid);
    }


    [Fact]
    public void ValidateReservation()
    {
    SqliteConnection connectionSQL = new SqliteConnection("Data Source=:memory:");
    connectionSQL.Open();
    DbContextOptionsBuilder<ParkingDataContext> builder = new DbContextOptionsBuilder<ParkingDataContext>();
    builder.UseSqlite(connectionSQL);
    
    ParkingDataContext context = new ParkingDataContext(builder.Options);
    context.Database.EnsureCreated();
    var ParkingSpot1 = new ParkingSpot
        {
            Name = "FirstPlace"
        };
    context.ParkingSpots.Add(ParkingSpot1);
    context.SaveChanges();

    ReservationsController rcontroller = new ReservationsController(context);
    var reservation1 = new Reservation
    {
        ParkingSpotId = ParkingSpot1.Id,
        StartTime = DateTime.Now.AddHours(1),
        EndTime = DateTime.Now.AddHours(2)
    };
    rcontroller.createReservation(reservation1);
    Assert.True(rcontroller.ModelState.IsValid);
    }
}