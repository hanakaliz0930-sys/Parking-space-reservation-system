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
        
        if (ModelState.IsValid)
        {
            _context.Reservations.Add(reservation);
            _context.SaveChanges();
            return reservation;
        }
        return BadRequest(ModelState);
    } 
}