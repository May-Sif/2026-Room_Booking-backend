namespace RoomBooking.Api.Models {
    public enum RoomStatus {
        Available = 1,
        UnderMaintenance = 2,
        Occupied = 3
    }

    public enum BookingStatus {
        Pending = 1,
        Approved = 2,
        Rejected = 3,
        Completed = 4,
        Cancelled = 5
    }
}

