using RoomBooking.Api.Models;

namespace RoomBooking.Api.Dtos;

public class RoomCreateDto {
    public string RoomCode { get; set; } = null!;
    public string Name { get; set; } = null!;
    public int Capacity { get; set; }
    public RoomStatus Status { get; set; }
    public string Building { get; set; } = null!;
    public string Floor { get; set; } = null!;
}

public class RoomUpdateDto
{
    public string Name { get; set; } = null!;
    public int Capacity { get; set; }
    public RoomStatus Status { get; set; }
    public string Building { get; set; } = null!;
    public string Floor { get; set; } = null!;
}

public class RoomReadDto {
    public int Id { get; set; }
    public string RoomCode { get; set; } = null!;
    public string Name { get; set; } = null!;
    public int Capacity { get; set; }
    public RoomStatus Status { get; set; }
    public string Building { get; set; } = null!;
    public string Floor { get; set; } = null!;
}
