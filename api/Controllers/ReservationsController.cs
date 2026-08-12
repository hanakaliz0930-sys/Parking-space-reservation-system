using Microsoft.AspNetCore.Mvc;
using api.Entities;

namespace api.Controllers;

[ApiController]
[Route("api/reservations")]
public class ReservationsController : ControllerBase
{
    private readonly ParkingDataContext _context;

    public ReservationsController(ParkingDataContext context)
    {
        _context = context;
    }

    [HttpPost]
    public ActionResult<Reservation> createReservation (Reservation reservation)
    {
        var existingReservations = _context.Reservations
            .Where(r => r.ParkingSpotId == reservation.ParkingSpotId)
            .ToList();

        foreach (var existingReservation in existingReservations)
        {
           if(!(reservation.EndTime < existingReservation.StartTime) && !(existingReservation.EndTime < reservation.StartTime))
           {
                ModelState.AddModelError("Reservation", "The reservation time overlaps with an existing reservation.");
           }
        }
        if(reservation.StartTime >= reservation.EndTime)
        {
            ModelState.AddModelError("Invalid time", "The Reservation time is invalid.");
        }
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        else
        {
             _context.Reservations.Add(reservation);
            _context.SaveChanges();
            return reservation;
        }
        
    }
    
    [HttpGet("{parkingSpotId}")]
    public List<Reservation> GetReservationsByParkingSpotId(int parkingSpotId)
    {
        return _context.Reservations.Where(r => r.ParkingSpotId == parkingSpotId).ToList();
    }
    
    [HttpDelete("{id}")]
    public ActionResult CancellationOfReservation(int id)
    {
        Reservation hit =_context.Reservations.Where(r => r.Id == id).FirstOrDefault();
        if(hit != null)
        {
            _context.Reservations.Remove(hit);
            _context.SaveChanges();
            return NoContent();
        }
        else
        {
            ModelState.AddModelError("No reservation", "There is no reservation with that id.");
            return BadRequest(ModelState);
        }
        
    }
}