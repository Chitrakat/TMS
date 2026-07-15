using TaskManagementAPI.Models;

namespace TaskManagementAPI.DTOs;

public class UpdateTaskRequestDto
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    public bool Completed { get; set; }
    public Priority Priority { get; set; } = Priority.Medium;
    public int CategoryId { get; set; }
}
