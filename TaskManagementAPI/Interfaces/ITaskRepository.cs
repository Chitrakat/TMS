using TaskManagementAPI.Models;

namespace TaskManagementAPI.Interfaces;

public interface ITaskRepository : IRepository<TaskItem>
{
    Task<IEnumerable<TaskItem>> GetAllWithCategoryAsync();
    Task<TaskItem?> GetByIdWithCategoryAsync(int id);
    Task<IEnumerable<TaskItem>> SearchByTitleAsync(string searchText);
}
