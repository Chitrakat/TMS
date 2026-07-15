using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Data;
using TaskManagementAPI.Interfaces;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Repositories;

public class TaskRepository : Repository<TaskItem>, ITaskRepository
{
    public TaskRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<TaskItem>> GetAllWithCategoryAsync()
    {
        return await Context.TaskItems
            .Include(task => task.Category)
            .ToListAsync();
    }

    public async Task<TaskItem?> GetByIdWithCategoryAsync(int id)
    {
        return await Context.TaskItems
            .Include(task => task.Category)
            .FirstOrDefaultAsync(task => task.Id == id);
    }

    public async Task<IEnumerable<TaskItem>> SearchByTitleAsync(string searchText)
    {
        return await Context.TaskItems
            .Include(task => task.Category)
            .Where(task => task.Title.Contains(searchText))
            .ToListAsync();
    }
}
