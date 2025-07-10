using TaskManager.Data.Entities;

namespace TaskManager.Logic
{
    public interface ITaskLogic
    {
        Task<IEnumerable<ManagedTask>> GetTasksForUserAsync(int userId);
        Task<ManagedTask?> GetByIdAsync(int id);
        Task<ManagedTask> CreateAsync(ManagedTask task);
        Task<ManagedTask?> ChangeStatusAsync(int taskId, int newStatusId, int? nextAssignedUserId = null);
        Task<ManagedTask?> CloseTaskAsync(int taskId);
    }
}