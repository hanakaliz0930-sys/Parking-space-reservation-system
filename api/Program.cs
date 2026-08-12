using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using api.Entities;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddDbContext<ParkingDataContext>(options =>
options.UseSqlite(builder.Configuration.GetConnectionString("ParkingDataContext") ?? "Data Source=parking.db"));
builder.Services
    .AddControllers()
    .AddApplicationPart(typeof(api.Controllers.ParkingSpotsController).Assembly);

var app = builder.Build();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ParkingDataContext>();
    dbContext.Database.EnsureCreated();
    if(dbContext.ParkingSpots.FirstOrDefault() == null)
    {
        var ParkingSpot1 = new ParkingSpot
        {
            Name = "FirstPlace"
        };
        dbContext.ParkingSpots.Add(ParkingSpot1);
        dbContext.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseHttpsRedirection();

app.Run();


