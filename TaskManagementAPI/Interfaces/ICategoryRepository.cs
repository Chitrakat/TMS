using TaskManagementAPI.Models;

namespace TaskManagementAPI.Interfaces;

public interface ICategoryRepository : IRepository<Category>
{
    Task<bool> HasTasksAsync(int categoryId);
}
