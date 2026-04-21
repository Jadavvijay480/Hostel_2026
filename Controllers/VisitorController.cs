using Hostel_2026.Data;
using Hostel_2026.Models;
using Hostel_Consume_2026.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hostel_2026.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorController : ControllerBase
    {
        private readonly VisitorRepository _visitorRepository;

        public VisitorController(VisitorRepository visitorRepository)
        {
            _visitorRepository = visitorRepository;
        }

        // 🔹 GET: api/visitor
        [HttpGet]
        public IActionResult GetAllVisitors()
        {
            var visitors = _visitorRepository.SelectAll();
            return Ok(visitors);
        }

        // 🔹 GET: api/visitor/5
        [HttpGet("{id:int}")]
        public IActionResult GetVisitorById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid visitor id.");

            var visitor = _visitorRepository.SelectByPK(id);
            if (visitor == null)
                return NotFound();

            return Ok(visitor);
        }

        // 🔹 POST: api/visitor
        [HttpPost]
        public IActionResult InsertVisitor([FromBody] VisitorModel visitor)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool isInserted = _visitorRepository.Insert(visitor);

            if (!isInserted)
                return StatusCode(500, "Error inserting visitor.");

            return CreatedAtAction(nameof(GetVisitorById),
                new { id = visitor.Visitor_id },
                visitor);
        }

        // 🔹 PUT: api/visitor/5
        [HttpPut("{id:int}")]
        public IActionResult UpdateVisitor(int id, [FromBody] VisitorModel visitor)
        {
            if (!ModelState.IsValid || id != visitor.Visitor_id)
                return BadRequest();

            var isUpdated = _visitorRepository.Update(visitor);
            if (!isUpdated)
                return NotFound();

            return NoContent();
        }

        // 🔹 DELETE: api/visitor/5
        [HttpDelete("{id:int}")]
        public IActionResult DeleteVisitorById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid visitor id.");

            var isDeleted = _visitorRepository.Delete(id);
            if (!isDeleted)
                return NotFound();

            return NoContent();
        }
    }
}