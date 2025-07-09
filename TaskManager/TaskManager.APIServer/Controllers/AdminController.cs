using Microsoft.AspNetCore.Mvc;
using TaskManager.Logic;

namespace TaskManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //todo: add authorization and authentication guard
    public class AdminController : ControllerBase
    {
        private readonly IAdminLogic _adminService;

        public AdminController(IAdminLogic adminService)
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