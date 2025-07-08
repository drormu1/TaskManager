namespace TaskManager.Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public ICollection<TaskBase> Tasks { get; set; } = new List<TaskBase>();
    }
}