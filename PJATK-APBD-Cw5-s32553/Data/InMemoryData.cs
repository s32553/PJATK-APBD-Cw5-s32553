using YourNamespace.Models;

namespace YourNamespace.Data
{
    public static class InMemoryData
    {
        public static List<Room> Rooms = new List<Room>
        {
            new Room { Id = 1, Name = "A101", BuildingCode = "A", Floor = 1, Capacity = 20, HasProjector = true, IsActive = true },
            new Room { Id = 2, Name = "B201", BuildingCode = "B", Floor = 2, Capacity = 30, HasProjector = true, IsActive = true },
            new Room { Id = 3, Name = "C301", BuildingCode = "C", Floor = 3, Capacity = 15, HasProjector = false, IsActive = true },
            new Room { Id = 4, Name = "A102", BuildingCode = "A", Floor = 1, Capacity = 25, HasProjector = true, IsActive = false }
        };

        public static List<Reservation> Reservations = new List<Reservation>
        {
            new Reservation
            {
                Id = 1,
                RoomId = 1,
                OrganizerName = "Jan Kowalski",
                Topic = "ASP.NET Basics",
                Date = DateTime.Today,
                StartTime = new TimeSpan(10, 0, 0),
                EndTime = new TimeSpan(12, 0, 0),
                Status = "confirmed"
            },
            new Reservation
            {
                Id = 2,
                RoomId = 2,
                OrganizerName = "Anna Nowak",
                Topic = "REST API",
                Date = DateTime.Today,
                StartTime = new TimeSpan(9, 0, 0),
                EndTime = new TimeSpan(11, 0, 0),
                Status = "planned"
            }
        };
    }
}