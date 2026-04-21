using Hostel_2026.Models;
using Hostel_2026.Data;
using Microsoft.AspNetCore.Mvc;

namespace Hostel_2026.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WardenController : ControllerBase
    {
        private readonly WardenRepository _wardenRepository;

        public WardenController(WardenRepository wardenRepository)
        {
            _wardenRepository = wardenRepository;
        }

        // 🔹 GET: api/warden
        [HttpGet]
        public IActionResult GetAllWardens()
        {
            var wardens = _wardenRepository.SelectAll();
            return Ok(wardens);
        }

        // 🔹 GET: api/warden/5
        [HttpGet("{id:int}")]
        public IActionResult GetWardensById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid warden id.");

            var warden = _wardenRepository.SelectByPK(id);
            if (warden == null)
                return NotFound();

            return Ok(warden);
        }

        // 🔹 POST: api/warden
        [HttpPost]
        public IActionResult InsertWardens([FromBody] Warden warden)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool isInserted = _wardenRepository.Insert(warden);

            if (!isInserted)
                return StatusCode(500, "Error inserting warden.");

            return CreatedAtAction(nameof(GetWardensById),
                new { id = warden.Warden_id },
                warden);
        }

        // 🔹 PUT: api/warden/5
        [HttpPut("{id:int}")]
        public IActionResult UpdateWardens(int id, [FromBody] Warden warden)
        {
            if (!ModelState.IsValid || id != warden.Warden_id)
                return BadRequest();

            var isUpdated = _wardenRepository.Update(warden);
            if (!isUpdated)
                return NotFound();

            return NoContent();
        }

        // 🔹 DELETE: api/warden/5
        [HttpDelete("{id:int}")]
        public IActionResult DeleteWardensById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid warden id.");

            var isDeleted = _wardenRepository.Delete(id);
            if (!isDeleted)
                return NotFound();

            return NoContent();
        }
    }
}
