using Microsoft.AspNetCore.Mvc;
using TaskManager.Data.Entities;
using TaskManager.Logic;
using TaskManager.Logic.DTOs;

namespace TaskManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskTypeController : ControllerBase
    {
        private readonly ITaskTypeService _taskTypeService;

        public TaskTypeController(ITaskTypeService taskTypeService)
        {
            _taskTypeService = taskTypeService;
        }

    

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatusDto>>> GetAll()
        {
            var dtos = await _taskTypeService.GetAllTaskTypesAsync();
            return Ok(dtos);
        }
    }
}