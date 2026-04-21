using Hostel_2026.Data;
using Hostel_2026.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hostel_2026.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserViewController : ControllerBase
    {
        private readonly UserViewRepository _userViewRepository;

        public UserViewController(UserViewRepository userViewRepository)
        {
            _userViewRepository = userViewRepository;
        }

        // 🔹 GET: api/visitor
        [HttpGet]
        public IActionResult GetAllUserViews()
        {
            var userView = _userViewRepository.SelectAll();
            return Ok(userView);
        }

        // 🔹 GET: api/visitor/5
        [HttpGet("{id:int}")]
        public IActionResult GetUserViewById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid UserView id.");

            var userView = _userViewRepository.SelectByPK(id);
            if (userView == null)
                return NotFound();

            return Ok(userView);
        }

        // 🔹 POST: api/visitor
        [HttpPost]
        public IActionResult InsertUserView([FromBody] UserModel userView)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool isInserted = _userViewRepository.Insert(userView);

            if (!isInserted)
                return StatusCode(500, "Error inserting visitor.");

            return CreatedAtAction(nameof(GetUserViewById),
                new { id = userView.UserId },
                userView);
        }

        // 🔹 PUT: api/visitor/5
        [HttpPut("{id:int}")]
        public IActionResult UpdateUserView(int id, [FromBody] UserModel userView)
        {
            if (!ModelState.IsValid || id != userView.UserId)
                return BadRequest();

            var isUpdated = _userViewRepository.Update(userView);
            if (!isUpdated)
                return NotFound();

            return NoContent();
        }

        // 🔹 DELETE: api/visitor/5
        [HttpDelete("{id:int}")]
        public IActionResult DeleteUserViewById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid UserView id.");

            var isDeleted = _userViewRepository.Delete(id);
            if (!isDeleted)
                return NotFound();

            return NoContent();
        }
    }
}