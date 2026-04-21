using Hostel_2026.Data;
using Hostel_2026.Models;
using Hostel_Consume_2026.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hostel_2026.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomBookingController : ControllerBase
    {
        private readonly RoomBookingRepository _roomBookingRepository;

        public RoomBookingController(RoomBookingRepository roomBookingRepository)
        {
            _roomBookingRepository = roomBookingRepository;
        }

        // 🔹 GET: api/room
        [HttpGet]
        public IActionResult GetAllRoomBookings()
        {
            var books = _roomBookingRepository.SelectAll();
            return Ok(books);
        }

        // 🔹 GET: api/room/5
        [HttpGet("{id:int}")]
        public IActionResult GetRoomBookingById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid room id.");

            var books = _roomBookingRepository.SelectByPK(id);
            if (books == null)
                return NotFound();

            return Ok(books);
        }

        // 🔹 POST: api/room
        [HttpPost]
        public IActionResult InsertRoomBooking([FromBody] RoomBookingModel books)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool isInserted = _roomBookingRepository.Insert(books);

            if (!isInserted)
                return StatusCode(500, "Error inserting room.");

            return CreatedAtAction(nameof(GetRoomBookingById),
                new { id = books.Booking_Id },
                books);
        }

        // 🔹 PUT: api/room/5
        [HttpPut("{id:int}")]
        public IActionResult UpdateRoomBooking(int id, [FromBody] RoomBookingModel books)
        {
            if (!ModelState.IsValid || id != books.Booking_Id)
                return BadRequest();

            var isUpdated = _roomBookingRepository.Update(books);
            if (!isUpdated)
                return NotFound();

            return NoContent();
        }

        // 🔹 DELETE: api/room/5
        [HttpDelete("{id:int}")]
        public IActionResult DeleteRoomBookingById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid room id.");

            var isDeleted = _roomBookingRepository.Delete(id);
            if (!isDeleted)
                return NotFound();

            return NoContent();
        }
    }
}