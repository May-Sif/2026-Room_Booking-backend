using RoomBooking.Api.Models;

namespace RoomBooking.Api.Dtos;

public class BookingCreateDto 
{
    public int RoomId { get; set; }
    public int UserId { get; set; }
    public string Purpose { get; set; } = null!;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}

public class BookingReadDto 
{
    public int Id { get; set; }
    public int RoomId { get; set; }
    public int UserId { get; set; }
    public string Purpose { get; set; } = null!;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public BookingStatus Status { get; set; }
}

public class BookingUpdateDto
{
    public string Status {get; set; } = null!;
}
