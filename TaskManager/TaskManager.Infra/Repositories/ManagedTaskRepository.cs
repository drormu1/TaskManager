using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaskManager.Data.Entities;
using TaskManager.Data.Repositories;

namespace TaskManager.Infra.Repositories
{
    public class ManagedTaskRepository : IManagedTaskRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ManagedTaskRepository> _logger;

        public ManagedTaskRepository(AppDbContext context, ILogger<ManagedTaskRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ManagedTask?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.ManagedTasks
                    .Include(t => t.AssignedUser)
                    .Include(t => t.StatusHistory)
                    .FirstOrDefaultAsync(t => t.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching managed task by id {TaskId}", id);
                throw;
            }
        }

        public async Task<IEnumerable<ManagedTask>> GetAllAsync()
        {
            try
            {
                return await _context.ManagedTasks
                    .Include(t => t.AssignedUser)
                    .Include(t => t.StatusHistory)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all managed tasks");
                throw;
            }
        }

        public async Task AddAsync(ManagedTask task)
        {
            try
            {
                _context.ManagedTasks.Add(task);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding managed task");
                throw;
            }
        }

        public async Task UpdateAsync(ManagedTask task)
        {
            try
            {
                _context.ManagedTasks.Update(task);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating managed task");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var task = await _context.ManagedTasks.FindAsync(id);
                if (task != null)
                {
                    _context.ManagedTasks.Remove(task);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting managed task {TaskId}", id);
                throw;
            }
        }
    }
}