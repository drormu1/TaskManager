namespace TaskManager.Data.Entities
{
    public class TaskStatus
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Order { get; set; }
        public int TaskTypeId { get; set; }
        public TaskType? TaskType { get; set; }


    }
}