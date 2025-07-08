using TaskManager.Data.Entities;
using TaskStatus = TaskManager.Data.Entities.TaskStatus;

namespace TaskManager.Data.Repositories
{
    public interface ITaskStatusRepository
    {
        Task<TaskStatus?> GetByIdAsync(int id);
        Task<IEnumerable<TaskStatus>> GetAllAsync();
        Task<IEnumerable<TaskStatus>> GetByTaskTypeIdAsync(int taskTypeId);
        Task AddAsync(TaskStatus status);
        Task UpdateAsync(TaskStatus status);
        Task DeleteAsync(int id);
    }
}   