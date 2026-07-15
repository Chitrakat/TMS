using TaskManagementAPI.Models;

namespace TaskManagementAPI.DTOs;

public static class EntityDtoMapper
{
    public static TaskResponseDto ToTaskResponseDto(TaskItem task)
    {
        return new TaskResponseDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            DueDate = task.DueDate,
            Completed = task.Completed,
            Priority = task.Priority,
            CategoryId = task.CategoryId,
            CategoryName = task.Category?.Name ?? string.Empty
        };
    }

    public static CategoryResponseDto ToCategoryResponseDto(Category category)
    {
        return new CategoryResponseDto
        {
            Id = category.Id,
            Name = category.Name
        };
    }

    public static TaskItem ToTaskItemEntity(TaskRequestDto dto)
    {
        return new TaskItem
        {
            Title = dto.Title,
            Description = dto.Description,
            DueDate = dto.DueDate,
            Priority = dto.Priority,
            CategoryId = dto.CategoryId
        };
    }

    public static Category ToCategoryEntity(CategoryRequestDto dto)
    {
        return new Category
        {
            Name = dto.Name
        };
    }
}
