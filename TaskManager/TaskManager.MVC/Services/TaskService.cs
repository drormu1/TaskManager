using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TaskManager.Logic.DTOs;
using TaskManager.MVC.Models;

namespace TaskManager.MVC.Services
{
    public class TaskService : ITaskService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly ILogger<TaskService> _logger;

        public TaskService(HttpClient httpClient, ILogger<TaskService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _jsonOptions = new JsonSerializerOptions 
            { 
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve
            };
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
             
            var response = await _httpClient.GetAsync("api/user");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<UserDto>>() ?? Enumerable.Empty<UserDto>();
        }

        public async Task<IEnumerable<StatusDto>> GetAllStatusesAsync()
        {
            var response = await _httpClient.GetAsync("api/status");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<StatusDto>>() ?? Enumerable.Empty<StatusDto>();
        }

        public async Task<IEnumerable<TaskTypeDto>> GetAllTaskTypesAsync()
        {
            var response = await _httpClient.GetAsync("api/tasktype");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<TaskTypeDto>>() ?? Enumerable.Empty<TaskTypeDto>();
        }


        public async Task<IEnumerable<ManagedTaskDto>> GetTasksForUserAsync(int userId)
  {
            try
            {
                var response = await _httpClient.GetAsync($"api/task/user/{userId}");
                response.EnsureSuccessStatusCode();
               
                var result = await response.Content.ReadFromJsonAsync<IEnumerable<ManagedTaskDto>>(_jsonOptions);
                
                return result ?? Enumerable.Empty<ManagedTaskDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting tasks for user {UserId}", userId);
                throw;
            }
        }


        public async Task<ManagedTaskDto?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/task/{id}");
                
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return null;
                    
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<ManagedTaskDto>(_jsonOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting task {Id}", id);
                throw;
            }
        }

        public async Task<ManagedTaskDto> CreateAsync(ManagedTaskDto task)
        {
            var response = await _httpClient.PostAsJsonAsync("api/task", task);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ManagedTaskDto>();
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception(error); // This will be caught in your MVC controller
            }
        }

        public async Task<ManagedTaskDto?> ChangeStatusAsync(int taskId, int newStatusId, string? taskDataJson = null)
        {
            try
            {
                HttpContent? content = null;
                
                if (!string.IsNullOrEmpty(taskDataJson))
                {
                    var jsonObject = new TaskStatusUpdateModel { TaskDataJson = taskDataJson };
                    content = JsonContent.Create(jsonObject, options: _jsonOptions);
                }
                else
                {
                    content = new StringContent("{}", Encoding.UTF8, "application/json");
                }
                
                var response = await _httpClient.PostAsync($"api/task/{taskId}/status/{newStatusId}", content);
                
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    return null;
                    
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<ManagedTaskDto>(_jsonOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error changing task {TaskId} status to {NewStatusId}", taskId, newStatusId);
                throw;
            }
        }

        public async Task<ManagedTaskDto?> CloseTaskAsync(int taskId)
        {
            try
            {
                var content = new StringContent("{}", Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"api/task/{taskId}/close", content);
                
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    return null;
                    
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<ManagedTaskDto>(_jsonOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error closing task {TaskId}", taskId);
                throw;
            }
        }
    }
}


