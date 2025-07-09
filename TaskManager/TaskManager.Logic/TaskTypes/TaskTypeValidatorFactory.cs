using Microsoft.Extensions.DependencyInjection;
using System;

namespace TaskManager.Logic.TaskTypes
{
    public class TaskTypeValidatorFactory : ITaskTypeValidatorFactory
    {
        private readonly IServiceProvider _serviceProvider;
        
        public TaskTypeValidatorFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        public ITaskTypeValidator GetValidator(string taskTypeName)
        {
            return taskTypeName switch
            {
                "Procurement" => _serviceProvider.GetRequiredService<ProcurementTaskValidator>(),
                "Development" => _serviceProvider.GetRequiredService<DevelopmentTaskValidator>(),
                _ => new DefaultTaskValidator() // Fallback for unknown types
            };
        }
    }
}