namespace TaskManager.Data.Entities
{
    public class TaskStatusHistory  : AuditableEntity
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public ManagedTask? ManagedTask { get; set; }
        public TaskStatus OldStatus { get; set; }
        public int OldStatusId { get; set; }
        public TaskStatus NewStatus { get; set; }
        
        public int NewStatusId { get; set; }

        public int? NextAssignedUserId { get; set; }
        public User? NextAssignedUser { get; set; }
   
    }
}