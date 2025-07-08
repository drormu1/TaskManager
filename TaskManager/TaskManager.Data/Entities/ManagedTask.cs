using System.Collections.Generic;

namespace TaskManager.Data.Entities
{
    public class ManagedTask : AuditableEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int TaskTypeId { get; set; }
        public TaskType? TaskType { get; set; }
        public int TaskStatusId { get; set; }
        public TaskStatus? TaskStatus { get; set; }
        public bool IsClosed { get; set; }
        public int AssignedUserId { get; set; }
        public User? AssignedUser { get; set; }
        public string? TaskDataJson { get; set; }
        public ICollection<TaskStatusHistory> StatusHistory { get; set; } = new List<TaskStatusHistory>();
    }
}