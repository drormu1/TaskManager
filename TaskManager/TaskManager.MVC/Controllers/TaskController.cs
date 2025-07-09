using Microsoft.AspNetCore.Mvc;
using TaskManager.Logic.DTOs;
using TaskManager.MVC.Models;
using TaskManager.MVC.Services;

namespace TaskManager.MVC.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly ILogger<TaskController> _logger;

        public TaskController(ITaskService taskService, ILogger<TaskController> logger)
        {
            _taskService = taskService;
            _logger = logger;
        }

        // GET: Task
        public async Task<IActionResult> Index(int userId = 1)
        {
            try
            {
                ViewBag.UserId = userId;
                var tasks = await _taskService.GetTasksForUserAsync(userId);
                return View(tasks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving tasks for user {UserId}", userId);
                TempData["ErrorMessage"] = "An error occurred while retrieving tasks.";
                return View(Enumerable.Empty<ManagedTaskDto>());
            }
        }

        // GET: Task/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var task = await _taskService.GetByIdAsync(id);
                if (task == null)
                {
                    return NotFound();
                }
                return View(task);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving task {Id}", id);
                TempData["ErrorMessage"] = "An error occurred while retrieving task details.";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Task/Create
        public IActionResult Create()
        {
            return View(new ManagedTaskDto
            { 
                TaskStatusId = 1, 
                AssignedUserId = 1,
                TaskDataJson = "{}" 
            });
        }

        // POST: Task/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ManagedTaskDto task)
        {
            if (!ModelState.IsValid)
            {
                return View(task);
            }

            try
            {
                var createdTask = await _taskService.CreateAsync(task);
                TempData["SuccessMessage"] = "Task created successfully.";
                return RedirectToAction(nameof(Details), new { id = createdTask.Id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating task");
                ModelState.AddModelError("", "An error occurred while creating the task.");
                return View(task);
            }
        }

        // GET: Task/ChangeStatus/5
        public async Task<IActionResult> ChangeStatus(int id)
        {
            try
            {
                var task = await _taskService.GetByIdAsync(id);
                if (task == null)
                {
                    return NotFound();
                }
                
                ViewBag.Task = task;
                var model = new TaskStatusUpdateModel();
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving task {Id} for status change", id);
                TempData["ErrorMessage"] = "An error occurred while retrieving task.";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Task/ChangeStatus/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeStatus(int id, int newStatusId, TaskStatusUpdateModel model)
        {
            try
            {
                var result = await _taskService.ChangeStatusAsync(id, newStatusId, model.TaskDataJson);
                if (result == null)
                {
                    TempData["ErrorMessage"] = "Invalid status transition or task is closed.";
                    return RedirectToAction(nameof(Details), new { id });
                }

                TempData["SuccessMessage"] = "Task status updated successfully.";
                return RedirectToAction(nameof(Details), new { id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error changing task {Id} status to {NewStatusId}", id, newStatusId);
                TempData["ErrorMessage"] = "An error occurred while updating the task status.";
                return RedirectToAction(nameof(Details), new { id });
            }
        }

        // POST: Task/CloseTask/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CloseTask(int id)
        {
            try
            {
                var result = await _taskService.CloseTaskAsync(id);
                if (result == null)
                {
                    TempData["ErrorMessage"] = "Task cannot be closed (not at final status or already closed).";
                    return RedirectToAction(nameof(Details), new { id });
                }

                TempData["SuccessMessage"] = "Task closed successfully.";
                return RedirectToAction(nameof(Details), new { id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error closing task {Id}", id);
                TempData["ErrorMessage"] = "An error occurred while closing the task.";
                return RedirectToAction(nameof(Details), new { id });
            }
        }
    }
}