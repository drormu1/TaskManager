using TaskManager.Data.Entities;

namespace TaskManager.Data.Tasks
{
    public class ProcurementTask : TaskBase
    {
        public string SupplierName { get; set; } = string.Empty;
        public string OrderNumber { get; set; } = string.Empty;
    }
}