using TaskManager.Logic.DTOs;
using TaskManager.MVC.Models;

namespace TaskManager.MVC.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<ManagedTaskDto>> GetTasksForUserAsync(int userId);
        Task<ManagedTaskDto?> GetByIdAsync(int id);
        Task<ManagedTaskDto> CreateAsync(ManagedTaskDto task);
        Task<ManagedTaskDto?> ChangeStatusAsync(int taskId, int newStatusId, string? taskDataJson = null);
        Task<ManagedTaskDto?> CloseTaskAsync(int taskId);

        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<IEnumerable<StatusDto>> GetAllStatusesAsync();

        Task<IEnumerable<TaskTypeDto>> GetAllTaskTypesAsync();
    }
}