// In TaskManager.Logic\Services\IStatusService.cs

using TaskManager.Data.Entities;

namespace TaskManager.Logic
{
    public interface IStatusService
    {
        Task<IEnumerable<StatusDto>> GetAllAsync();
    }
}