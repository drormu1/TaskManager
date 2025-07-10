using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Data.Repositories;
using TaskManager.Logic.DTOs;

using TaskManager.Data.Repositories;
using TaskManager.Infra.Repositories;


namespace TaskManager.Logic
{
    public class TaskTypeService  : ITaskTypeService
    {
        private readonly ITaskTypeRepository _taskTypeRepository;
        public TaskTypeService(ITaskTypeRepository taskTypeRepository)
        {
            _taskTypeRepository = taskTypeRepository;
        }   
    
        public async Task<IEnumerable<TaskTypeDto>> GetAllTaskTypesAsync()
        {
            var types = await _taskTypeRepository.GetAllAsync();
            return types.Select(t => new TaskTypeDto { Id = t.Id, Name = t.Name });
        }
    }
}

