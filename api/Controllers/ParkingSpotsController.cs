namespace api.Controllers;
using Microsoft.AspNetCore.Mvc;
using api.Entities;
using System.Linq;
[Route("api/parking-spots")]
public class ParkingSpotsController : ControllerBase
{
    private readonly ParkingDataContext _context;
    public ParkingSpotsController(ParkingDataContext context)
    {
        _context = context;
    }
    [HttpGet]
    public List<ParkingSpot> GetParkingSpots()
    {
        return _context.ParkingSpots.ToList();
    }
}



