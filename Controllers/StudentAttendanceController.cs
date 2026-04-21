using Hostel_2026.Models;
using Hostel_2026.Data;
using Microsoft.AspNetCore.Mvc;

namespace Hostel_2026.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAttendanceController : ControllerBase
    {
        private readonly StudentAttendanceRepository _attendanceRepository;

        public StudentAttendanceController(StudentAttendanceRepository attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
        }

        // 🔹 GET: api/studentattendance
        [HttpGet]
        public IActionResult GetAllAttendance()
        {
            var list = _attendanceRepository.SelectAll();
            return Ok(list);
        }

        // 🔹 GET: api/studentattendance/5
        [HttpGet("{id:int}")]
        public IActionResult GetAttendanceById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid attendance id.");

            var attendance = _attendanceRepository.SelectByPK(id);

            if (attendance == null)
                return NotFound();

            return Ok(attendance);
        }

        // 🔹 POST: api/studentattendance
        [HttpPost]
        public IActionResult InsertAttendance([FromBody] StudentAttendanceModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool isInserted = _attendanceRepository.Insert(model);

            if (!isInserted)
                return StatusCode(500, "Error inserting attendance.");

            return CreatedAtAction(
                nameof(GetAttendanceById),
                new { id = model.Attendance_id },
                model
            );
        }

        // 🔹 PUT: api/studentattendance/5
        [HttpPut("{id:int}")]
        public IActionResult UpdateAttendance(int id, [FromBody] StudentAttendanceModel model)
        {
            if (!ModelState.IsValid || id != model.Attendance_id)
                return BadRequest();

            var isUpdated = _attendanceRepository.Update(model);

            if (!isUpdated)
                return NotFound();

            return NoContent();
        }

        // 🔹 DELETE: api/studentattendance/5
        [HttpDelete("{id:int}")]
        public IActionResult DeleteAttendance(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid attendance id.");

            var isDeleted = _attendanceRepository.Delete(id);

            if (!isDeleted)
                return NotFound();

            return NoContent();
        }
    }
}