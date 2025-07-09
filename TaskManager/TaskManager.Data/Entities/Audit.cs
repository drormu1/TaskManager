namespace TaskManager.Data.Entities
{
    public abstract class Audit
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedById { get; set; } // UserId
        public bool IsDeleted { get; set; }
    }
}