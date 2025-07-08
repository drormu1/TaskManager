using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaskManager.Data.Entities;
using TaskManager.Data.Repositories;
using TaskStatus = TaskManager.Data.Entities.TaskStatus;

namespace TaskManager.Infra.Repositories
{
    public class TaskStatusRepository : ITaskStatusRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<TaskStatusRepository> _logger;

        public TaskStatusRepository(AppDbContext context, ILogger<TaskStatusRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<TaskStatus?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Set<TaskStatus>()
                    .Include(s => s.TaskType)
                    .FirstOrDefaultAsync(s => s.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching task status by id {Id}", id);
                throw;
            }
        }

        public async Task<IEnumerable<TaskStatus>> GetAllAsync()
        {
            try
            {
                return await _context.Set<TaskStatus>()
                    .Include(s => s.TaskType)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all task statuses");
                throw;
            }
        }

        public async Task<IEnumerable<TaskStatus>> GetByTaskTypeIdAsync(int taskTypeId)
        {
            try
            {
                return await _context.Set<TaskStatus>()
                    .Where(s => s.TaskTypeId == taskTypeId)
                    .OrderBy(s => s.Order)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching statuses for task type {TaskTypeId}", taskTypeId);
                throw;
            }
        }

        public async Task AddAsync(TaskStatus status)
        {
            try
            {
                _context.Set<TaskStatus>().Add(status);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding task status");
                throw;
            }
        }

        public async Task UpdateAsync(TaskStatus status)
        {
            try
            {
                _context.Set<TaskStatus>().Update(status);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating task status");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var status = await _context.Set<TaskStatus>().FindAsync(id);
                if (status != null)
                {
                    _context.Set<TaskStatus>().Remove(status);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting task status {Id}", id);
                throw;
            }
        }
    }
}