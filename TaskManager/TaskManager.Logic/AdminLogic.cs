using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaskManager.Infra;

namespace TaskManager.Logic
{
    public class AdminLogic : IAdminLogic
    {
        private readonly AppDbContext _context;
        private readonly ILogger<AdminLogic> _logger;

        public AdminLogic(AppDbContext context, ILogger<AdminLogic> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> InitializeDatabaseAsync()
        {
            try
            {
                await _context.Database.MigrateAsync();
                await AppDbContextSeed.SeedAsync(_context, _logger);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error initializing database");
                return false;
            }
        }
    }
}