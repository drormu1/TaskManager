using TaskManager.Data.Entities;

namespace TaskManager.Data.Repositories
{
    public interface ITaskStatusHistoryRepository
    {
        Task<TaskStatusHistory?> GetByIdAsync(int id);
        Task<IEnumerable<TaskStatusHistory>> GetAllAsync();
        Task AddAsync(TaskStatusHistory history);
        Task UpdateAsync(TaskStatusHistory history);
        Task DeleteAsync(int id);
    }
}