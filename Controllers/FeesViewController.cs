using Hostel_2026.Data;
using Microsoft.AspNetCore.Mvc;

namespace Hostel_2026.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeesViewController : ControllerBase
    {
        private readonly FeesViewRepository _repository;

        public FeesViewController(FeesViewRepository repository)
        {
            _repository = repository;
        }

        // 🔹 GET: api/FeesView
        [HttpGet]
        public IActionResult Get()
        {
            var data = _repository.SelectAll();
            return Ok(data);
        }
    }
}