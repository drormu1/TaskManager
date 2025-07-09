namespace TaskManager.Logic.TaskTypes
{
    public interface ITaskTypeValidatorFactory
    {
        ITaskTypeValidator GetValidator(string taskTypeName);
    }
}