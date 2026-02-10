namespace RoomBooking.Api.Models;

public class Room {
    public int Id { get; set; }
    public string RoomCode { get; set; } = null!;
    public string Name { get; set; } = null!;
    public int Capacity { get; set; }
    public RoomStatus Status { get; set; }
    public string Building { get; set; } = null!;
    public string Floor { get; set; } = null!;

    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}