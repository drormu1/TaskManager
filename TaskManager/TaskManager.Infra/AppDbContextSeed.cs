using TaskManager.Data.Entities;
using Microsoft.Extensions.Logging;
using TaskStatus = TaskManager.Data.Entities.TaskStatus;

namespace TaskManager.Infra
{
    public static class AppDbContextSeed
    {
        public static async Task SeedAsync(AppDbContext context, ILogger logger)
        {
            // Seed TaskTypes and TaskStatuses
            if (!context.Set<TaskType>().Any())
            // Seed TaskTypes and TaskStatuses
            if (!context.Set<TaskType>().Any())
            {
                var procurementType = new TaskType { Name = "Procurement" };
                var developmentType = new TaskType { Name = "Development" };

                var procurementStatuses = new List<TaskStatus>
                {
                    new Data.Entities.TaskStatus { Name = "Open", Order = 1, TaskType = procurementType },
                    new TaskStatus { Name = "WaitingForApproval", Order = 2, TaskType = procurementType },
                    new TaskStatus { Name = "Closed", Order = 3, TaskType = procurementType }
                };

                var developmentStatuses = new List<TaskStatus>
                {
                    new TaskStatus { Name = "Open", Order = 1, TaskType = developmentType },
                    new TaskStatus { Name = "InProgress", Order = 2, TaskType = developmentType },
                    new TaskStatus { Name = "Review", Order = 3, TaskType = developmentType },
                    new TaskStatus { Name = "Closed", Order = 4, TaskType = developmentType }
                };

                context.Set<TaskType>().AddRange(procurementType, developmentType);
                context.Set<TaskStatus>().AddRange(procurementStatuses);
                context.Set<TaskStatus>().AddRange(developmentStatuses);

                await context.SaveChangesAsync();
                logger.LogInformation("Seeded TaskTypes and TaskStatuses.");
            }

            // Seed Users
            {
                context.Users.AddRange(
                    new User { UserName = "admin0" , CreatedAt = DateTime.Now.AddYears(-1) , UpdatedById = 1 },
                    new User { UserName = "user0", CreatedAt = DateTime.Now.AddYears(-1), UpdatedById = 1 },
                    new User { UserName = "developer0", CreatedAt = DateTime.Now.AddYears(-1), UpdatedById = 1 },
                    new User { UserName = "admin1", CreatedAt = DateTime.Now.AddYears(-1), UpdatedById = 1 },
                    new User { UserName = "user1", CreatedAt = DateTime.Now.AddYears(-1), UpdatedById = 1 },
                    new User { UserName = "developer1", CreatedAt = DateTime.Now.AddYears(-1), UpdatedById = 1 }
                );
                await context.SaveChangesAsync();
                logger.LogInformation("Seeded initial users.");
            }
        }
    }
}   