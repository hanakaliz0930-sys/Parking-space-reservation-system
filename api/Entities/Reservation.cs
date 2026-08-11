namespace api.Entities;
public class Reservation
{
    public int Id { get; set; }

    public int ParkingSpotId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public string ClientName { get; set; } = string.Empty;
}