namespace TaskManager.Logic.DTOs
{
    public class ManagedTaskDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int TaskTypeId { get; set; }
        public string TaskTypeName { get; set; } = string.Empty;
        public int TaskStatusId { get; set; }
        public string TaskStatusName { get; set; } = string.Empty;
        public bool IsClosed { get; set; }
        public int AssignedUserId { get; set; }
        public string AssignedUserName { get; set; } = string.Empty;
        public string? TaskDataJson { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedById { get; set; }
        public bool IsDeleted { get; set; }


    }
}