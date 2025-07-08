namespace TaskManager.Data.Entities
{
    public class TaskStatusHistory
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public TaskBase? Task { get; set; }
        public TaskStatus OldStatus { get; set; }
        public TaskStatus NewStatus { get; set; }
        public int ChangedByUserId { get; set; }
        public User? ChangedByUser { get; set; }
        public int? NextAssignedUserId { get; set; }
        public User? NextAssignedUser { get; set; }
        public DateTime ChangedAt { get; set; }
    }
}