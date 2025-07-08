using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaskManager.Infra;    

namespace TaskManager.Logic.Services
{
    public class AdminService : IAdminService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<AdminService> _logger;

        public AdminService(AppDbContext context, ILogger<AdminService> logger)
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