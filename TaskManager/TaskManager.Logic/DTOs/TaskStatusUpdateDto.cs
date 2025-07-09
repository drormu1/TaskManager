namespace TaskManager.Logic.DTOs
{
    public class TaskStatusUpdateDto
    {
        public string? TaskDataJson { get; set; }
        public int? NextAssignedUserId { get; set; }

        public int NewStatusId { get; set; }
    }

}