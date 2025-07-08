using TaskManager.Data.Entities;

namespace TaskManager.Data.Repositories
{
    public interface ITaskTypeRepository
    {
        Task<TaskType?> GetByIdAsync(int id);
        Task<IEnumerable<TaskType>> GetAllAsync();
        Task AddAsync(TaskType type);
        Task UpdateAsync(TaskType type);
        Task DeleteAsync(int id);
    }
}