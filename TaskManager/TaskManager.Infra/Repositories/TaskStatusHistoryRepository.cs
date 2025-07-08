using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaskManager.Data.Entities;
using TaskManager.Data.Repositories;

namespace TaskManager.Infra.Repositories
{
    public class TaskStatusHistoryRepository : ITaskStatusHistoryRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<TaskStatusHistoryRepository> _logger;

        public TaskStatusHistoryRepository(AppDbContext context, ILogger<TaskStatusHistoryRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<TaskStatusHistory?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.TaskStatusHistories
                    .Include(h => h.ManagedTask)
                    //.Include(h => h.ChangedByUser)
                    .Include(h => h.NextAssignedUser)
                    .FirstOrDefaultAsync(h => h.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching status history by id {Id}", id);
                throw;
            }
        }

        public async Task<IEnumerable<TaskStatusHistory>> GetAllAsync()
        {
            try
            {
                return await _context.TaskStatusHistories
                    .Include(h => h.ManagedTask)
                    //.Include(h => h.ChangedByUser)
                    .Include(h => h.NextAssignedUser)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all status histories");
                throw;
            }
        }

        public async Task AddAsync(TaskStatusHistory history)
        {
            try
            {
                _context.TaskStatusHistories.Add(history);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding status history");
                throw;
            }
        }

        public async Task UpdateAsync(TaskStatusHistory history)
        {
            try
            {
                _context.TaskStatusHistories.Update(history);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating status history");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var history = await _context.TaskStatusHistories.FindAsync(id);
                if (history != null)
                {
                    _context.TaskStatusHistories.Remove(history);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting status history {Id}", id);
                throw;
            }
        }
    }
}   