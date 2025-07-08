namespace TaskManager.Data.Entities
{
    public class User  : AuditableEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public ICollection<ManagedTask> ManagedTasks  { get; set; } = new List<ManagedTask>();
    }
}