namespace RoomBooking.Api.Dtos;

public class UserCreateDto
{
    public string Username { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Department { get; set; } = null!;
    public string Role { get; set; } = "Student"; // student | admin
    public string Password { get; set; } = null!;
}

public class UserReadDto
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Department { get; set; } = null!;
    public string Role { get; set; } = null!;
}
