// In TaskManager.Logic\Services\StatusService.cs
using TaskManager.Data.Entities;
using TaskManager.Data.Repositories;
using TaskManager.Infra.Repositories;

namespace TaskManager.Logic
{
    public class StatusService : IStatusService
    {
        private readonly ITaskStatusRepository _taskStatusRepository;
        public StatusService(ITaskStatusRepository taskStatusRepository)
        {
            _taskStatusRepository = taskStatusRepository;
        }

        public async Task<IEnumerable<StatusDto>> GetAllAsync()
        {
            var statuses = await _taskStatusRepository.GetAllAsync();
            return statuses.Select(s => new StatusDto
            {
                Id = s.Id,
                Name = s.Name,
                OrderId = s.Order,
                TaskTypeId = s.TaskTypeId,
            });
        }
    }
}