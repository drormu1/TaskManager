using Microsoft.AspNetCore.Mvc;
using TaskManager.Logic.Services;

namespace TaskManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

          [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok("API is up");
        }
        [HttpPost("init")]
        public async Task<IActionResult> Init()
        {
            var result = await _adminService.InitializeDatabaseAsync();
            if (result)
                return Ok("Database initialized");
            return StatusCode(500, "Database initialization failed");
        }
    }
}