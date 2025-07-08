using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaskManager.Data.Entities;
using TaskManager.Data.Repositories;

namespace TaskManager.Infra.Repositories
{
    public class TaskTypeRepository : ITaskTypeRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<TaskTypeRepository> _logger;

        public TaskTypeRepository(AppDbContext context, ILogger<TaskTypeRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<TaskType?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Set<TaskType>()
                    .Include(t => t.Statuses)
                    .FirstOrDefaultAsync(t => t.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching task type by id {Id}", id);
                throw;
            }
        }

        public async Task<IEnumerable<TaskType>> GetAllAsync()
        {
            try
            {
                return await _context.Set<TaskType>()
                    .Include(t => t.Statuses)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all task types");
                throw;
            }
        }

        public async Task AddAsync(TaskType type)
        {
            try
            {
                _context.Set<TaskType>().Add(type);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding task type");
                throw;
            }
        }

        public async Task UpdateAsync(TaskType type)
        {
            try
            {
                _context.Set<TaskType>().Update(type);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating task type");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var type = await _context.Set<TaskType>().FindAsync(id);
                if (type != null)
                {
                    _context.Set<TaskType>().Remove(type);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting task type {Id}", id);
                throw;
            }
        }
    }
}