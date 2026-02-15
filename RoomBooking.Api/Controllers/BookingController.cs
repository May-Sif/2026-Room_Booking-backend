using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoomBooking.Api.Data;
using RoomBooking.Api.Dtos;
using RoomBooking.Api.Models;

[ApiController]
[Route("api/bookings")]
public class BookingController : ControllerBase
{
    private readonly AppDbContext _context;

    public BookingController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/bookings
    [HttpGet]
    public IActionResult GetBookings()
    {
        var bookings = _context.Bookings
            .Include(b => b.Room)
            .Include(b => b.User)
            .Where(b => b.DeletedAt == null)
            .Select(b => new BookingReadDto
            {
                Id = b.Id,
                RoomId = b.RoomId,
                UserId = b.UserId,
                Purpose = b.Purpose,
                StartTime = b.StartTime,
                EndTime = b.EndTime,
                Status = b.Status,
            }).ToList();

        return Ok(bookings);
    }

    // GET: api/bookings/{id}
    [HttpGet("{id:int}")]
    public IActionResult GetBookingById(int id)
    {
        var booking = _context.Bookings
            .Include(b => b.Room)
            .Include(b => b.User)
            .FirstOrDefault(b => b.Id == id && b.DeletedAt == null);

        if (booking == null) return NotFound();

        return Ok(new
        {
            Id = booking.Id,
            RoomId = booking.RoomId,
            UserId = booking.UserId,
            Purpose = booking.Purpose,
            StartTime = booking.StartTime,
            EndTime = booking.EndTime,
            Status = booking.Status
        });
    }

    // POST: api/bookings
    [HttpPost]
    public IActionResult CreateBooking(BookingCreateDto dto)
    {
        var room = _context.Rooms.Find(dto.RoomId);
        if (room == null) return BadRequest("Room not Found");

        var user = _context.Users.Find(dto.UserId);
        if (user == null) return BadRequest("User not found");

        if (dto.StartTime >= dto.EndTime) 
            return BadRequest("StartTime must be before EndTime");

        var overlappingBooking = _context.Bookings
            .Where(b => b.RoomId == dto.RoomId)
            .Where(b => b.DeletedAt == null)
            .Where(b => 
                dto.StartTime < b.EndTime && dto.EndTime > b.StartTime            
            ).FirstOrDefault();
        if (overlappingBooking != null)
        {
            return BadRequest("Room sudah dibooking di waktu yang sama");
        }

        var booking = new Booking
        {
            RoomId = dto.RoomId,
            UserId = dto.UserId,
            Purpose = dto.Purpose,
            StartTime = dto.StartTime,
            EndTime = dto.EndTime,
            Status = BookingStatus.Pending
        };

        _context.Bookings.Add(booking);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetBookingById), new { id = booking.Id }, booking);
    }

    // GET: api/bookings/room/{roomId}
    [HttpGet("room/{roomId}")]
    public IActionResult GetBookingsByRoom(int roomId)
    {
        var bookings = _context.Bookings
            .Include(b => b.Room)
            .Include(b => b.User)
            .Where(b => b.RoomId == roomId && b.DeletedAt == null)
            .Select(b => new BookingReadDto
            {
                Id = b.Id,
                RoomId = b.RoomId,
                UserId = b.UserId,
                Purpose = b.Purpose,
                StartTime = b.StartTime,
                EndTime = b.EndTime,
                Status = b.Status,
            })
            .ToList();

        return Ok(bookings);
    }


    // PATCH: api/bookings/{id}/status
    [HttpPatch("{id}/status")]
    public IActionResult UpdateBookingStatus(int id, BookingUpdateDto dto)
    {
        var booking = _context.Bookings.Find(id);
        if (booking == null) return NotFound();
        
        if (!Enum.TryParse<BookingStatus>(dto.Status, true, out var status))
            return BadRequest("Invalid status value");

        booking.Status = status;
        _context.SaveChanges();

        return NoContent();
    }

    // DELETE: api/bookings/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteBooking(int id)
    {
        var booking =_context.Bookings.Find(id);
        if (booking == null) return NotFound();

        booking.DeletedAt = DateTime.UtcNow;
        _context.SaveChanges();

        return NoContent();
    }

    [HttpGet("my/{userId}")]
    public IActionResult GetMyBookings(int userId)
    {
        var bookings = _context.Bookings
            .Include(b => b.Room)
            .Where(b => b.UserId == userId)
            .Select(b => new {
                b.Id,
                RoomCode = b.Room.RoomCode,
                b.Purpose,
                b.StartTime,
                b.EndTime,
                b.Status
            })
            .ToList();

        return Ok(bookings);
    }

}