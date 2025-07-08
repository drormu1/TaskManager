using TaskManager.Data.Entities;

namespace TaskManager.Data.Repositories
{
    public interface IManagedTaskRepository
    {
        Task<ManagedTask?> GetByIdAsync(int id);
        Task<IEnumerable<ManagedTask>> GetAllAsync();
        Task AddAsync(ManagedTask task);
        Task UpdateAsync(ManagedTask task);
        Task DeleteAsync(int id);
    }
}