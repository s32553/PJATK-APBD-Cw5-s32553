using Microsoft.AspNetCore.Mvc;
using YourNamespace.Data;
using YourNamespace.Models;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        // GET /api/rooms + filtrowanie
        [HttpGet]
        public IActionResult GetAll(
            [FromQuery] int? minCapacity,
            [FromQuery] bool? hasProjector,
            [FromQuery] bool? activeOnly)
        {
            var rooms = InMemoryData.Rooms.AsQueryable();

            if (minCapacity.HasValue)
                rooms = rooms.Where(r => r.Capacity >= minCapacity.Value);

            if (hasProjector.HasValue)
                rooms = rooms.Where(r => r.HasProjector == hasProjector.Value);

            if (activeOnly == true)
                rooms = rooms.Where(r => r.IsActive);

            return Ok(rooms.ToList());
        }

        // GET /api/rooms/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var room = InMemoryData.Rooms.FirstOrDefault(r => r.Id == id);

            if (room == null)
                return NotFound();

            return Ok(room);
        }

        // GET /api/rooms/building/{buildingCode}
        [HttpGet("building/{buildingCode}")]
        public IActionResult GetByBuilding(string buildingCode)
        {
            var rooms = InMemoryData.Rooms
                .Where(r => r.BuildingCode == buildingCode)
                .ToList();

            return Ok(rooms);
        }

        // POST /api/rooms
        [HttpPost]
        public IActionResult Create(Room room)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            room.Id = InMemoryData.Rooms.Max(r => r.Id) + 1;
            InMemoryData.Rooms.Add(room);

            return CreatedAtAction(nameof(GetById), new { id = room.Id }, room);
        }

        // PUT /api/rooms/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, Room updated)
        {
            var room = InMemoryData.Rooms.FirstOrDefault(r => r.Id == id);

            if (room == null)
                return NotFound();

            room.Name = updated.Name;
            room.BuildingCode = updated.BuildingCode;
            room.Floor = updated.Floor;
            room.Capacity = updated.Capacity;
            room.HasProjector = updated.HasProjector;
            room.IsActive = updated.IsActive;

            return Ok(room);
        }

        // DELETE /api/rooms/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var room = InMemoryData.Rooms.FirstOrDefault(r => r.Id == id);

            if (room == null)
                return NotFound();

            // blokada usunięcia jeśli są rezerwacje
            if (InMemoryData.Reservations.Any(r => r.RoomId == id))
                return Conflict("Room has reservations");

            InMemoryData.Rooms.Remove(room);

            return NoContent();
        }
    }
}