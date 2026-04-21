using Hostel_2026.Data;
using Hostel_2026.Models;
using Hostel_Consume_2026.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hostel_2026.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly AdminRepository _adminRepository;

        public AdminController(AdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        // 🔹 GET: api/Admin
        [HttpGet]
        public IActionResult GetAllAdmins()
        {
            var admin = _adminRepository.SelectAll();
            return Ok(admin);
        }

        // 🔹 GET: api/Admin/5
        [HttpGet("{id:int}")]
        public IActionResult GetAdminById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid visitor id.");

            var admin = _adminRepository.SelectByPK(id);
            if (admin == null)
                return NotFound();

            return Ok(admin);
        }

        // 🔹 POST: api/Admin
        [HttpPost]
        public IActionResult InsertAdmin([FromBody] AdminModel admin)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool isInserted = _adminRepository.Insert(admin);

            if (!isInserted)
                return StatusCode(500, "Error inserting visitor.");

            return CreatedAtAction(nameof(GetAdminById),
                new { id = admin.Admin_id },
                admin);
        }

        // 🔹 PUT: api/Admin/5
        [HttpPut("{id:int}")]
        public IActionResult UpdateAdmin(int id, [FromBody] AdminModel admin)
        {
            if (!ModelState.IsValid || id != admin.Admin_id)
                return BadRequest();

            var isUpdated = _adminRepository.Update(admin);
            if (!isUpdated)
                return NotFound();

            return NoContent();
        }

        // 🔹 DELETE: api/Admin/5
        [HttpDelete("{id:int}")]
        public IActionResult DeleteAdminById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Admin id.");

            var isDeleted = _adminRepository.Delete(id);
            if (!isDeleted)
                return NotFound();

            return NoContent();
        }
    }
}