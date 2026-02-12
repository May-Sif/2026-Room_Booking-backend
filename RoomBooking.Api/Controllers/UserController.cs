using Microsoft.AspNetCore.Mvc;
using RoomBooking.Api.Data;
using RoomBooking.Api.Models;
using RoomBooking.Api.Dtos;

[ApiController]
[Route("api/users")]

public class UserController : ControllerBase
{
    private readonly AppDbContext _context;

    public UserController (AppDbContext context)
    {
        _context = context;
    }

    // GET : api/users
    [HttpGet]
    public IActionResult GetUsers()
    {
        var users = _context.Users
            .Select(u => new UserReadDto
            {
                Id = u.Id,
                Username = u.Username,
                Name = u.Name,
                Email = u.Email,
                Department = u.Department,
                Role = u.Role
            }).ToList();
        return Ok(users);
    }

    // POST : api/users
    [HttpPost]
    public IActionResult CreateUser(UserCreateDto dto)
    {
        var user = new User
        {
            Username = dto.Username,
            Name = dto.Name,
            Email = dto.Email,
            Department = dto.Department,
            Role = dto.Role,
            Password = dto.Password
        };
        _context.Users.Add(user);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetUsers), new {id = user.Id}, new
        {
            user.Id,
            user.Username,
            user.Name,
            user.Email,
            user.Department,
            user.Role
        });
    }

    [HttpPost("login")]
    public IActionResult Login(UserLoginDto dto)
    {
        var user = _context.Users.FirstOrDefault(u => u.Username == dto.Username);
        if (user == null || user.Password != dto.Password)
            return Unauthorized("Username atau password salah");

        return Ok(new 
        {
            user.Id,
            user.Username,
            user.Role
        });
    }

}