using Microsoft.AspNetCore.Mvc;
using RoomBooking.Api.Data;
using RoomBooking.Api.Models;
using RoomBooking.Api.Dtos;

[ApiController]
[Route("api/rooms")]
public class RoomsController : ControllerBase
{
    private readonly AppDbContext _context;

    public RoomsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/rooms
    [HttpGet]
    public IActionResult GetRooms()
    {
        var rooms = _context.Rooms
            .Select(r => new RoomReadDto
            {
                Id = r.Id,
                RoomCode = r.RoomCode,
                Name = r.Name,
                Capacity = r.Capacity,
                Status = r.Status,
                Building = r.Building,
                Floor = r.Floor
            }).ToList();

        return Ok(rooms);
    }

    // GET: api/rooms/{id}
    [HttpGet("{id}")]
    public IActionResult GetRoomById(int id)
    {
        var room = _context.Rooms.Find(id);
        if (room == null) return NotFound();

        return Ok(new RoomReadDto
        {
            Id = room.Id,
            RoomCode = room.RoomCode,
            Name = room.Name,
            Capacity = room.Capacity,
            Status = room.Status,
            Building = room.Building,
            Floor = room.Floor
        });
    }

    // POST: api/rooms
    [HttpPost]
    public IActionResult CreateRoom(RoomCreateDto dto)
    {
        var room = new Room
        {
            RoomCode = dto.RoomCode,
            Name = dto.Name,
            Capacity = dto.Capacity,
            Status = RoomStatus.Available,
            Building = dto.Building,
            Floor = dto.Floor
        };

        _context.Rooms.Add(room);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetRoomById), new { id = room.Id }, new RoomReadDto
        {
            Id = room.Id,
            RoomCode = room.RoomCode,
            Name = room.Name,
            Capacity = room.Capacity,
            Status = room.Status,
            Building = room.Building,
            Floor = room.Floor
        });
    }

    // PUT: api/rooms/{id}
    [HttpPut("{id}")]
    public IActionResult UpdateRoom(int id, RoomUpdateDto dto)
    {
        var room = _context.Rooms.Find(id);
        if (room == null) return NotFound();

        room.Name = dto.Name;
        room.Capacity = dto.Capacity;
        room.Building = dto.Building;
        room.Floor = dto.Floor;
        room.Status = dto.Status;

        _context.SaveChanges();
        return NoContent();
    }

    // DELETE: api/rooms/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteRoom(int id)
    {
        var room = _context.Rooms.Find(id);
        if (room == null) return NotFound();

        _context.Rooms.Remove(room);
        _context.SaveChanges();
        return NoContent();
    }
}
