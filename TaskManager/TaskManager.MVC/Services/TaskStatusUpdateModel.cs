namespace TaskManager.MVC.Models
{
    public class TaskStatusUpdateModel
    {
        // The new status to move the task to
        public int NewStatusId { get; set; }

        // Any additional data for the task update (optional)
        public string? TaskDataJson { get; set; }
    }
}