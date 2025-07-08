namespace TaskManager.Data.Entities
{
    public class TaskType
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<TaskStatus> Statuses { get; set; } = new List<TaskStatus>();
        public ICollection<ManagedTask> ManagedTasks { get; set; } = new List<ManagedTask>();
    }
}