using Microsoft.EntityFrameworkCore;
using TaskManager.Data.Entities;
using System.Linq;
using System.Threading.Tasks;
using System;
using TaskStatus = TaskManager.Data.Entities.TaskStatus;

namespace TaskManager.Infra
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
     
        public DbSet<ManagedTask> ManagedTasks => Set<ManagedTask>();

      
        public DbSet<TaskStatusHistory> TaskStatusHistories => Set<TaskStatusHistory>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ManagedTask>()
                .HasOne(mt => mt.TaskType)
                .WithMany(tt => tt.ManagedTasks)
                .HasForeignKey(mt => mt.TaskTypeId)
                .OnDelete(DeleteBehavior.Restrict); // Prevents cascade

            modelBuilder.Entity<TaskStatus>()
                .HasOne(ts => ts.TaskType)
                .WithMany(tt => tt.Statuses)
                .HasForeignKey(ts => ts.TaskTypeId)
                .OnDelete(DeleteBehavior.Cascade); // Only one can be cascade


            modelBuilder.Entity<TaskStatusHistory>()
                .HasOne(h => h.OldStatus)
                .WithMany()
                .HasForeignKey(h => h.OldStatusId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TaskStatusHistory>()
                .HasOne(h => h.NewStatus)
                .WithMany()
                .HasForeignKey(h => h.NewStatusId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<Audit>();
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