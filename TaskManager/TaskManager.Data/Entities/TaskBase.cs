namespace TaskManager.Data.Entities
{
    public abstract class TaskBase
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TaskType Type { get; set; }
        public TaskStatus Status { get; set; }
        public bool IsClosed { get; set; }
        public int AssignedUserId { get; set; }
        public User? AssignedUser { get; set; }
        public ICollection<TaskStatusHistory> StatusHistory { get; set; } = new List<TaskStatusHistory>();
    }
}