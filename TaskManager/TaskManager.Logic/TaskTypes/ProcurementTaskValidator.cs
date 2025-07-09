using TaskManager.Data.Entities;
using System.Text.Json;
using TaskStatus = TaskManager.Data.Entities.TaskStatus;

namespace TaskManager.Logic.TaskTypes
{
    public class ProcurementTaskValidator : ITaskTypeValidator
    {
        public async Task<bool> ValidateTransitionAsync(ManagedTask managedTask, TaskStatus fromStatus, TaskStatus toStatus)
        {
            // 1 (Created) → 2 (Supplier offers received): needs two price quotes
            if (fromStatus.Order == 1 && toStatus.Order == 2)
            {
                if (string.IsNullOrWhiteSpace(managedTask.TaskDataJson))
                    return false;
                
                try
                {
                    var jsonDoc = JsonDocument.Parse(managedTask.TaskDataJson);
                    if (!jsonDoc.RootElement.TryGetProperty("priceQuotes", out var quotes))
                        return false;
                        
                    return quotes.GetArrayLength() >= 2;
                }
                catch
                {
                    return false;
                }
            }
            
            // 2 (Supplier offers received) → 3 (Purchase completed): needs receipt
            if (fromStatus.Order == 2 && toStatus.Order == 3)
            {
                if (string.IsNullOrWhiteSpace(managedTask.TaskDataJson))
                    return false;
                    
                try
                {
                    var jsonDoc = JsonDocument.Parse(managedTask.TaskDataJson);
                    return jsonDoc.RootElement.TryGetProperty("receipt", out var _);
                }
                catch
                {
                    return false;
                }
            }
            
            return true; // Allow other transitions
        }
    }
}