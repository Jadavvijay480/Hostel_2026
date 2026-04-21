using Hostel_2026.Models;
using Hostel_2026.Data;
using Microsoft.AspNetCore.Mvc;

namespace Hostel_2026.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentRepository _studentRepository;

        public StudentController(StudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        // 🔹 GET: api/student
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            var students = _studentRepository.SelectAll();
            return Ok(students);
        }

        // 🔹 GET: api/student/5
        [HttpGet("{id:int}")]
        public IActionResult GetStudentById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid student id.");

            var student = _studentRepository.SelectByPK(id);
            if (student == null)
                return NotFound();

            return Ok(student);
        }

        // 🔹 POST: api/student
        [HttpPost]
        public IActionResult InsertStudent([FromBody] StudentModel student)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool isInserted = _studentRepository.Insert(student);

            if (!isInserted)
                return StatusCode(500, "Error inserting student.");

            return CreatedAtAction(nameof(GetStudentById),
                new { id = student.Student_id },
                student);
        }

        // 🔹 PUT: api/student/5
        [HttpPut("{id:int}")]
        public IActionResult UpdateStudent(int id, [FromBody] StudentModel student)
        {
            if (!ModelState.IsValid || id != student.Student_id)
                return BadRequest();

            var isUpdated = _studentRepository.Update(student);
            if (!isUpdated)
                return NotFound();

            return NoContent();
        }

        // 🔹 DELETE: api/student/5
        [HttpDelete("{id:int}")]
        public IActionResult DeleteStudentById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid student id.");

            var isDeleted = _studentRepository.Delete(id);
            if (!isDeleted)
                return NotFound();

            return NoContent();
        }
    }
}


