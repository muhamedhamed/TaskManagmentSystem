﻿namespace TaskManagmentSystem.Application.Dtos;

public class TaskDto
{
    public string TaskId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public string TaskOwnerId { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsActive { get; set; }
}

public class NewTaskDto
{
    public string Title { get; set; }
    public string Description { get; set; }
}
