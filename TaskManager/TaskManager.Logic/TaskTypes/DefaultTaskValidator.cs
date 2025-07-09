using TaskManager.Data.Entities;
using TaskStatus = TaskManager.Data.Entities.TaskStatus;

namespace TaskManager.Logic.TaskTypes
{
    // Default validator that allows everything
    public class DefaultTaskValidator : ITaskTypeValidator
    {
        public Task<bool> ValidateTransitionAsync(ManagedTask managedTask, TaskStatus fromStatus, TaskStatus toStatus)
        {
            return Task.FromResult(true);
        }
    }
}