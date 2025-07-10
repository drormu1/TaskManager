using Microsoft.AspNetCore.Mvc;
using TaskManager.Data.Entities;
using TaskManager.Logic;
using TaskManager.Logic.DTOs;

namespace TaskManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatusController : ControllerBase
    {
        private readonly IStatusService _statusService;

        public StatusController(IStatusService statusService)
        {
            _statusService = statusService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatusDto>>> GetAll()
        {
            var dtos = await _statusService.GetAllAsync();
            return Ok(dtos);
        }
    }
}