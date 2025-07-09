using Microsoft.AspNetCore.Mvc;
using TaskManager.Data.Entities;
using TaskManager.Logic;
using TaskManager.Logic.DTOs;

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

        // Helper method to map entity to DTO
        private ManagedTaskDto MapToDto(ManagedTask task)
        {
            return new ManagedTaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                TaskTypeId = task.TaskTypeId,
                TaskTypeName = task.TaskType?.Name ?? "Unknown",
                TaskStatusId = task.TaskStatusId,
                TaskStatusName = task.TaskStatus?.Name ?? "Unknown",
                IsClosed = task.IsClosed,
                AssignedUserId = task.AssignedUserId,
                AssignedUserName = task.AssignedUser?.UserName ?? "Unknown",
                TaskDataJson = task.TaskDataJson,
                CreatedAt = task.CreatedAt,
                UpdatedAt = task.UpdatedAt,
                UpdatedById = task.UpdatedById,
                IsDeleted = task.IsDeleted
            };
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetTasksForUser(int userId)
        {
            var tasks = await _taskLogic.GetTasksForUserAsync(userId);
            var dtos = tasks.Select(MapToDto);
            return Ok(dtos);
        }

        // Create a new task
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ManagedTask task)
        {
            var created = await _taskLogic.CreateAsync(task);
            return StatusCode(201, MapToDto(created));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var task = await _taskLogic.GetByIdAsync(id);
            if (task == null) return NotFound();
            return Ok(MapToDto(task));
        }

        [HttpPost("{taskId}/status/{newStatusId}")]
        public async Task<IActionResult> ChangeStatus(int taskId, int newStatusId, [FromBody] TaskStatusUpdateDto updateDto)
        {
            var updated = await _taskLogic.ChangeStatusAsync(taskId, newStatusId, updateDto?.NextAssignedUserId);
            if (updated == null) return BadRequest("Invalid status transition or task is closed.");
            return Ok(MapToDto(updated));
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