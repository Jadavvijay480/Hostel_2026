using Hostel_2026.Models;
using Hostel_2026.Data;
using Microsoft.AspNetCore.Mvc;

namespace Hostel_2026.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeesController : ControllerBase
    {
        private readonly FeesRepository _feesRepository;
        private readonly StudentRepository _studentRepository;
        private readonly RoomRepository _roomRepository;


        //public FeesController(FeesRepository feesRepository)
        //{
        //    _feesRepository = feesRepository;
        //}

        public FeesController(
           FeesRepository feesRepository,
           StudentRepository studentRepository,
           RoomRepository roomRepository)
        {
            _feesRepository = feesRepository;
            _studentRepository = studentRepository;
            _roomRepository = roomRepository;
        }



        // 🔹 GET: api/fees/students-dropdown
        [HttpGet("students-dropdown")]
        public IActionResult GetStudentsDropdown()
        {
            var students = _studentRepository.GetDropdown();

            if (students == null || students.Count == 0)
                return NotFound("No students found.");

            return Ok(students);
        }

        // 🔹 GET: api/fees/rooms-dropdown
        [HttpGet("rooms-dropdown")]
        public IActionResult GetRoomsDropdown()
        {
            var rooms = _roomRepository.GetDropdown();

            if (rooms == null || rooms.Count == 0)
                return NotFound("No rooms available.");

            return Ok(rooms);
        }






        // 🔹 GET: api/fees
        [HttpGet]
        public IActionResult GetAllFees()
        {
            var fees = _feesRepository.SelectAll();
            return Ok(fees);
        }
         

        // 🔹 GET: api/fees/5
        [HttpGet("{id:int}")]
        public IActionResult GetFeeById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid fee id.");

            var fee = _feesRepository.SelectByPK(id);
            if (fee == null)
                return NotFound();

            return Ok(fee);
        }

        // 🔹 POST: api/fees
        [HttpPost]
        public IActionResult InsertFee([FromBody] FeesModel fees)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool isInserted = _feesRepository.Insert(fees);

            if (!isInserted)
                return StatusCode(500, "Error inserting fee.");

            return CreatedAtAction(
                nameof(GetFeeById),
                new { id = fees.Fee_id },
                fees
            );
        }

        // 🔹 PUT: api/fees/5
        [HttpPut("{id:int}")]
        public IActionResult UpdateFee(int id, [FromBody] FeesModel fees)
        {
            if (!ModelState.IsValid || id != fees.Fee_id)
                return BadRequest();

            var isUpdated = _feesRepository.Update(fees);
            if (!isUpdated)
                return NotFound();

            return NoContent();
        }

        // 🔹 DELETE: api/fees/5
        [HttpDelete("{id:int}")]
        public IActionResult DeleteFee(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid fee id.");

            var isDeleted = _feesRepository.Delete(id);
            if (!isDeleted)
                return NotFound();

            return NoContent();
        }
    }
}