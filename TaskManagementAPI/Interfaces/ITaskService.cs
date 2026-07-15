using TaskManagementAPI.DTOs;

namespace TaskManagementAPI.Interfaces;

public interface ITaskService
{
    Task<IEnumerable<TaskResponseDto>> GetAllTasksAsync();
    Task<TaskResponseDto?> GetTaskByIdAsync(int id);
    Task<TaskResponseDto> CreateTaskAsync(TaskRequestDto taskItem);
    Task<bool> UpdateTaskAsync(int id, UpdateTaskRequestDto taskItem);
    Task<bool> DeleteTaskAsync(int id);
    Task<bool> CompleteTaskAsync(int id);
    Task<IEnumerable<TaskResponseDto>> SearchTasksAsync(string searchText);
}
