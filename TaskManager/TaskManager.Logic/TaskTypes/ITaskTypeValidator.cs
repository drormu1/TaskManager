using TaskManager.Data.Entities;
using TaskStatus = TaskManager.Data.Entities.TaskStatus;

namespace TaskManager.Logic.TaskTypes
{
    public interface ITaskTypeValidator
    {
        Task<bool> ValidateTransitionAsync(ManagedTask managedTask, TaskStatus fromStatus, TaskStatus toStatus);
    }
}