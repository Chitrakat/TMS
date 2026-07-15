using System.ComponentModel.DataAnnotations;

namespace TaskManagementAPI.Models;

public class TaskItem
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public DateTime? DueDate { get; set; }

    public bool Completed { get; set; }

    public Priority Priority { get; set; } = Priority.Medium;

    public int CategoryId { get; set; }

    public Category Category { get; set; } = null!;
}
