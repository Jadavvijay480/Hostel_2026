using Hostel_2026.Models;
using Hostel_2026.Data;
using Microsoft.AspNetCore.Mvc;

namespace Hostel_2026.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintController : ControllerBase
    {
        private readonly ComplaintRepository _complaintRepository;

        public ComplaintController(ComplaintRepository complaintRepository)
        {
            _complaintRepository = complaintRepository;
        }

        // 🔹 GET: api/complaint
        [HttpGet]
        public IActionResult GetAllComplaints()
        {
            var complaints = _complaintRepository.SelectAll();
            return Ok(complaints);
        }

        // 🔹 GET: api/complaint/5
        [HttpGet("{id:int}")]
        public IActionResult GetComplaintById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid complaint id.");

            var complaint = _complaintRepository.SelectByPK(id);

            if (complaint == null)
                return NotFound();

            return Ok(complaint);
        }

        // 🔹 POST: api/complaint
        [HttpPost]
        public IActionResult InsertComplaint([FromBody] ComplaintModel complaint)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool isInserted = _complaintRepository.Insert(complaint);

            if (!isInserted)
                return StatusCode(500, "Error inserting complaint.");

            return CreatedAtAction(
                nameof(GetComplaintById),
                new { id = complaint.Complaint_id },
                complaint
            );
        }

        // 🔹 PUT: api/complaint/5
        [HttpPut("{id:int}")]
        public IActionResult UpdateComplaint(int id, [FromBody] ComplaintModel complaint)
        {
            if (!ModelState.IsValid || id != complaint.Complaint_id)
                return BadRequest();

            var isUpdated = _complaintRepository.Update(complaint);

            if (!isUpdated)
                return NotFound();

            return NoContent();
        }

        // 🔹 DELETE: api/complaint/5
        [HttpDelete("{id:int}")]
        public IActionResult DeleteComplaint(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid complaint id.");

            var isDeleted = _complaintRepository.Delete(id);

            if (!isDeleted)
                return NotFound();

            return NoContent();
        }
    }
}