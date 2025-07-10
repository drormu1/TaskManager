using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Logic.DTOs;

namespace TaskManager.Logic
{
    public interface ITaskTypeService
    {
        Task<IEnumerable<TaskTypeDto>> GetAllTaskTypesAsync();
    }
}
