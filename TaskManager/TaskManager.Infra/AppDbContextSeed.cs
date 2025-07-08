using TaskManager.Data.Entities;
using Microsoft.Extensions.Logging;

namespace TaskManager.Infra
{
    public static class AppDbContextSeed
    {
        public static async Task SeedAsync(AppDbContext context, ILogger logger)
        {
            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User { UserName = "admin" },
                    new User { UserName = "user1" }
                );
                await context.SaveChangesAsync();
                logger.LogInformation("Seeded initial users.");
            }
        }
    }
}   