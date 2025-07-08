namespace TaskManager.Data.Entities
{
    public abstract class AuditableEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedById { get; set; } // UserId, nullable
    }
}