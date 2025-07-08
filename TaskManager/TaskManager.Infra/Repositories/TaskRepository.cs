using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaskManager.Data.Entities;
using TaskManager.Data.Repositories;

namespace TaskManager.Infra.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<TaskRepository> _logger;

        public TaskRepository(AppDbContext context, ILogger<TaskRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<TaskBase?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Tasks
                    .Include(t => t.AssignedUser)
                    .Include(t => t.StatusHistory)
                    .FirstOrDefaultAsync(t => t.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching task by id {TaskId}", id);
                throw;
            }
        }

        public async Task<IEnumerable<TaskBase>> GetAllAsync()
        {
            try
            {
                return await _context.Tasks
                    .Include(t => t.AssignedUser)
                    .Include(t => t.StatusHistory)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all tasks");
                throw;
            }
        }

        public async Task AddAsync(TaskBase task)
        {
            try
            {
                _context.Tasks.Add(task);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding task");
                throw;
            }
        }

        public async Task UpdateAsync(TaskBase task)
        {
            try
            {
                _context.Tasks.Update(task);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating task");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var task = await _context.Tasks.FindAsync(id);
                if (task != null)
                {
                    _context.Tasks.Remove(task);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting task {TaskId}", id);
                throw;
            }
        }
    }
}
