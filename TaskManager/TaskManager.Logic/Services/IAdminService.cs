namespace TaskManager.Logic.Services
{
    public interface IAdminService
    {
        Task<bool> InitializeDatabaseAsync();
    }
}