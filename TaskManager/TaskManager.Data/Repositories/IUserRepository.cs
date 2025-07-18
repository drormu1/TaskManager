using TaskManager.Data.Entities;

namespace TaskManager.Data.Repositories
{
}

namespace TaskManager.Data.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int id);
        Task<IEnumerable<User>> GetAllAsync();
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(int id);
    }
}