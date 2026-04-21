using Hostel_2026.Models;
using Hostel_2026.Data;
using Microsoft.AspNetCore.Mvc;

namespace Hostel_2026.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly RoomRepository _roomRepository;

        public RoomController(RoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        // 🔹 GET: api/room
        [HttpGet]
        public IActionResult GetAllRooms()
        {
            var rooms = _roomRepository.SelectAll();
            return Ok(rooms);
        }

        // 🔹 GET: api/room/5
        [HttpGet("{id:int}")]
        public IActionResult GetRoomById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid room id.");

            var room = _roomRepository.SelectByPK(id);
            if (room == null)
                return NotFound();

            return Ok(room);
        }

        // 🔹 POST: api/room
        [HttpPost]
        public IActionResult InsertRoom([FromBody] RoomModel room)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool isInserted = _roomRepository.Insert(room);

            if (!isInserted)
                return StatusCode(500, "Error inserting room.");

            return CreatedAtAction(nameof(GetRoomById),
                new { id = room.Room_id },
                room);
        }

        // 🔹 PUT: api/room/5
        [HttpPut("{id:int}")]
        public IActionResult UpdateRoom(int id, [FromBody] RoomModel room)
        {
            if (!ModelState.IsValid || id != room.Room_id)
                return BadRequest();

            var isUpdated = _roomRepository.Update(room);
            if (!isUpdated)
                return NotFound();

            return NoContent();
        }

        // 🔹 DELETE: api/room/5
        [HttpDelete("{id:int}")]
        public IActionResult DeleteRoomById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid room id.");

            var isDeleted = _roomRepository.Delete(id);
            if (!isDeleted)
                return NotFound();

            return NoContent();
        }
    }
}