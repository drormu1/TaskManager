// In TaskManager.Logic\Services\IStatusService.cs
public interface IStatusService
{
    Task<IEnumerable<StatusDto>> GetAllAsync();
}