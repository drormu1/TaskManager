using Microsoft.EntityFrameworkCore;
using TaskManager.Data.Entities;
using TaskManager.Data.Tasks;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace TaskManager.Infra
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<TaskBase> Tasks => Set<TaskBase>();
        public DbSet<ProcurementTask> ProcurementTasks => Set<ProcurementTask>();
        public DbSet<DevelopmentTask> DevelopmentTasks => Set<DevelopmentTask>();
        public DbSet<TaskStatusHistory> TaskStatusHistories => Set<TaskStatusHistory>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskBase>()
                .HasDiscriminator<TaskType>("TaskType")
                .HasValue<ProcurementTask>(TaskType.Procurement)
                .HasValue<DevelopmentTask>(TaskType.Development);

            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<AuditableEntity>();
            var now = DateTime.UtcNow;

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = now;
                }
                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = now;
                    // UpdatedBy should be set by the service/controller before SaveChangesAsync
                }
            }
            //cancellation of tasks, such as database operations, HTTP requests, or long-running loops.
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}   