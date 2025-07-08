using TaskManager.Data.Entities;

namespace TaskManager.Data.Repositories
{
    public interface ITaskRepository
    {
        Task<TaskBase?> GetByIdAsync(int id);
        Task<IEnumerable<TaskBase>> GetAllAsync();
        Task AddAsync(TaskBase task);
        Task UpdateAsync(TaskBase task);
        Task DeleteAsync(int id);
    }
}