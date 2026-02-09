using Microsoft.EntityFrameworkCore;
using RoomBooking.Api.Models;

namespace RoomBooking.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<User> Users => Set<User>();
    public DbSet<Room> Rooms => Set<Room>();
    public DbSet<Booking> Bookings => Set<Booking>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ============================
        // Data Seeding Example
        // ============================

        // Users
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Username = "admin", Name = "Admin Kampus", Email = "admin@kampus.com", Department = "DTIK", Role = "Admin", Password = "admin123" },
            new User { Id = 2, Username = "student1", Name = "Budi", Email = "budi@student.com", Department = "DTIK", Role = "Student", Password = "123456" }
        );

        // Rooms
        modelBuilder.Entity<Room>().HasData(
            new Room { Id = 1, RoomCode = "R101", Name = "Lab Komputer", Capacity = 30, Status = RoomStatus.Available, Building = "Gedung A", Floor = "1" },
            new Room { Id = 2, RoomCode = "R102", Name = "Ruang Seminar", Capacity = 50, Status = RoomStatus.Available, Building = "Gedung B", Floor = "2" }
        );

        // Bookings
        modelBuilder.Entity<Booking>().HasData(
            new Booking { Id = 1, UserId = 2, RoomId = 1, Purpose = "Praktikum", StartTime = DateTime.Now.AddHours(1), EndTime = DateTime.Now.AddHours(2), Status = BookingStatus.Pending, CreatedAt = DateTime.UtcNow }
        );
    }
}
