using System.Text.Json;
using TaskManager.Data.Entities;
using TaskManager.Logic.TaskTypes;
using TaskStatus = TaskManager.Data.Entities.TaskStatus;

namespace TaskManager.Logic.TaskTypes
{
    public class DevelopmentTaskValidator : ITaskTypeValidator
    {
        public async Task<bool> ValidateTransitionAsync(ManagedTask managedTask, TaskStatus fromStatus, TaskStatus toStatus)
        {
            if (string.IsNullOrWhiteSpace(managedTask.TaskDataJson))
                return false;

            try
            {
                var jsonDoc = JsonDocument.Parse(managedTask.TaskDataJson);

                // 1 (Created) → 2 (Specification completed): needs specification
                if (fromStatus.Order == 1 && toStatus.Order == 2)
                    return jsonDoc.RootElement.TryGetProperty("specification", out var _);

                // 2 (Specification completed) → 3 (Development completed): needs branch
                if (fromStatus.Order == 2 && toStatus.Order == 3)
                    return jsonDoc.RootElement.TryGetProperty("branch", out var _);

                // 3 (Development completed) → 4 (Distribution completed): needs version
                if (fromStatus.Order == 3 && toStatus.Order == 4)
                    return jsonDoc.RootElement.TryGetProperty("version", out var _);
            }
            catch
            {
                return false;
            }

            return true; // Allow other transitions
        }
    }
}