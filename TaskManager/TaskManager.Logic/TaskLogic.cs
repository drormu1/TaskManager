using TaskManager.Data.Entities;
using TaskManager.Data.Repositories;
using Microsoft.Extensions.Logging;
using TaskManager.Logic.TaskTypes;
using System.Collections.Generic;

namespace TaskManager.Logic
{
    public class TaskLogic : ITaskLogic
    {
        private readonly IManagedTaskRepository _taskRepository;
        private readonly ITaskStatusRepository _statusRepository;
        private readonly ILogger<TaskLogic> _logger;
        private readonly ITaskTypeValidatorFactory _validatorFactory;
        

    public TaskLogic(
        IManagedTaskRepository taskRepository,
        ITaskStatusRepository statusRepository,
        ILogger<TaskLogic> logger,
        ITaskTypeValidatorFactory validatorFactory) // Add this parameter
    {
        _taskRepository = taskRepository;
        _statusRepository = statusRepository;
        _logger = logger;
        _validatorFactory = validatorFactory;
    }

    public async Task<ManagedTask?> ChangeStatusAsync(int taskId, int newStatusId, int? nextAssignedUserId = null)
    {
        var managedTask = await _taskRepository.GetByIdAsync(taskId);
        if (managedTask == null || managedTask.IsClosed)
            return null;

        var statuses = (await _statusRepository.GetByTaskTypeIdAsync(managedTask.TaskTypeId))
            .OrderBy(s => s.Order).ToList();

        var currentStatus = statuses.FirstOrDefault(s => s.Id == managedTask.TaskStatusId);
        var newStatus = statuses.FirstOrDefault(s => s.Id == newStatusId);

        if (currentStatus == null || newStatus == null)
            return null;

        int currentOrder = currentStatus.Order;
        int newOrder = newStatus.Order;

        // Allow only one step forward or any step back
        if (newOrder == currentOrder + 1 || newOrder < currentOrder)
        {
            // Get the appropriate validator and validate the transition
            var validator = _validatorFactory.GetValidator(managedTask.TaskType?.Name ?? "");

            // Call your validator method - adjust the method name if different
            bool isValid = await validator.ValidateTransitionAsync(managedTask, currentStatus, newStatus);

            if (!isValid)
            {
                _logger.LogWarning("Task status transition validation failed for taskId {TaskId}", taskId);
                return null;
            }
        }
        else
        {
            // Invalid move (skipping forward)
            return null;
        }

        // If moving to last status, close the task
        int lastOrder = statuses.Max(s => s.Order);
        if (newOrder >= lastOrder)
        {
            managedTask.TaskStatusId = statuses.Last().Id;
            managedTask.IsClosed = true;
        }
        else
        {
            managedTask.TaskStatusId = newStatusId;
        }

        if (nextAssignedUserId.HasValue)
            managedTask.AssignedUserId = nextAssignedUserId.Value;

        await _taskRepository.UpdateAsync(managedTask);
        return managedTask;
    }

    public async Task<IEnumerable<ManagedTask>> GetTasksForUserAsync(int userId)
        {
            var tasks = await _taskRepository.GetAllAsync();
            return tasks.Where(t => t.AssignedUserId == userId);
        }

        public async Task<ManagedTask?> GetByIdAsync(int id)
        {
            return await _taskRepository.GetByIdAsync(id);
        }

        public async Task<ManagedTask> CreateAsync(ManagedTask task)
        {
            await _taskRepository.AddAsync(task);
            return task;
        }

       
        public async Task<ManagedTask?> CloseTaskAsync(int taskId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task == null || task.IsClosed)
                return null;

            var statuses = (await _statusRepository.GetByTaskTypeIdAsync(task.TaskTypeId))
                .OrderBy(s => s.Order).ToList();

            int lastStatusId = statuses.Last().Id;
            if (task.TaskStatusId != lastStatusId)
                return null; // Only allow close at last status

            task.IsClosed = true;
            await _taskRepository.UpdateAsync(task);
            return task;
        }
    }
}