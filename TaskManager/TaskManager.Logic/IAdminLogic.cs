namespace TaskManager.Logic
{
    public interface IAdminLogic
    {
        Task<bool> InitializeDatabaseAsync();
    }
}