using Microsoft.AspNetCore.Mvc;
using TaskManager.Data.Entities;
using TaskManager.Logic;

namespace TaskManager.APIServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskLogic _taskLogic;

        public TaskController(ITaskLogic taskLogic)
        {
            _taskLogic = taskLogic;
        }

        // Get all tasks for a user
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetTasksForUser(int userId)
        {
            var tasks = await _taskLogic.GetTasksForUserAsync(userId);
            return Ok(tasks);
        }

        // Create a new task
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ManagedTask task)
        {
            var created = await _taskLogic.CreateAsync(task);
            return StatusCode(201, created);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var task = await _taskLogic.GetByIdAsync(id);
            if (task == null) return NotFound();
            return Ok(task);
        }

        // Change task status
        [HttpPost("{taskId}/status/{newStatusId}")]
        public async Task<IActionResult> ChangeStatus(int taskId, int newStatusId)
        {
            var updated = await _taskLogic.ChangeStatusAsync(taskId, newStatusId);
            if (updated == null) return BadRequest("Invalid status transition or task is closed.");
            return Ok(updated);
        }

        // Close a task
        [HttpPost("{taskId}/close")]
        public async Task<IActionResult> CloseTask(int taskId)
        {
            var closed = await _taskLogic.CloseTaskAsync(taskId);
            if (closed == null) return BadRequest("Task cannot be closed (not at final status or already closed).");
            return Ok(closed);
        }
    }
}