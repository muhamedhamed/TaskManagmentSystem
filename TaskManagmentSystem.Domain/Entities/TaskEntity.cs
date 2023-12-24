namespace TaskManagmentSystem.Domain.Entities;

public class TaskEntity
{
    public string TaskId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsActive { get; set; } // Soft delete flag will be used in the future updates

    // Navigation properties
    public string TaskOwnerId { get; set; }
    public User User { get; set; }
}
