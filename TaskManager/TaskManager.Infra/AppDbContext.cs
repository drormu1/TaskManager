using Microsoft.EntityFrameworkCore;
using TaskManager.Data.Entities;
using TaskManager.Data.Tasks;
using System.Linq;
using System.Threading.Tasks;

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
    }
}   