using TaskManagementAPI.Interfaces;
using TaskManagementAPI.DTOs;

namespace TaskManagementAPI.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;
    private readonly ICategoryRepository _categoryRepository;

    public TaskService(ITaskRepository taskRepository, ICategoryRepository categoryRepository)
    {
        _taskRepository = taskRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<TaskResponseDto>> GetAllTasksAsync()
    {
        var tasks = await _taskRepository.GetAllWithCategoryAsync();
        return tasks.Select(EntityDtoMapper.ToTaskResponseDto);
    }

    public async Task<TaskResponseDto?> GetTaskByIdAsync(int id)
    {
        var task = await _taskRepository.GetByIdWithCategoryAsync(id);
        return task is null ? null : EntityDtoMapper.ToTaskResponseDto(task);
    }

    public async Task<TaskResponseDto> CreateTaskAsync(TaskRequestDto taskItem)
    {
        if (!await _categoryRepository.ExistsAsync(taskItem.CategoryId))
        {
            throw new InvalidOperationException("Category does not exist.");
        }

        var newTask = EntityDtoMapper.ToTaskItemEntity(taskItem);
        newTask.Title = newTask.Title.Trim();

        await _taskRepository.AddAsync(newTask);
        await _taskRepository.SaveChangesAsync();

        var createdTask = await _taskRepository.GetByIdWithCategoryAsync(newTask.Id);
        return EntityDtoMapper.ToTaskResponseDto(createdTask!);
    }

    public async Task<bool> UpdateTaskAsync(int id, UpdateTaskRequestDto taskItem)
    {
        var existingTask = await _taskRepository.GetByIdAsync(id);
        if (existingTask is null)
        {
            return false;
        }

        if (!await _categoryRepository.ExistsAsync(taskItem.CategoryId))
        {
            throw new InvalidOperationException("Category does not exist.");
        }

        existingTask.Title = taskItem.Title.Trim();
        existingTask.Description = taskItem.Description;
        existingTask.DueDate = taskItem.DueDate;
        existingTask.Completed = taskItem.Completed;
        existingTask.Priority = taskItem.Priority;
        existingTask.CategoryId = taskItem.CategoryId;

        _taskRepository.Update(existingTask);
        await _taskRepository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteTaskAsync(int id)
    {
        var task = await _taskRepository.GetByIdAsync(id);
        if (task is null)
        {
            return false;
        }

        _taskRepository.Delete(task);
        await _taskRepository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> CompleteTaskAsync(int id)
    {
        var task = await _taskRepository.GetByIdAsync(id);
        if (task is null)
        {
            return false;
        }

        if (task.Completed)
        {
            return true;
        }

        task.Completed = true;
        _taskRepository.Update(task);
        await _taskRepository.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<TaskResponseDto>> SearchTasksAsync(string searchText)
    {
        if (string.IsNullOrWhiteSpace(searchText))
        {
            return await GetAllTasksAsync();
        }

        var searchResults = await _taskRepository.SearchByTitleAsync(searchText.Trim());

        return searchResults.Select(EntityDtoMapper.ToTaskResponseDto);
    }
}
