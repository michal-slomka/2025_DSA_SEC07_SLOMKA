namespace GetStartedApp.Models;

public class TaskItem
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Project { get; set; } = string.Empty;
    public string TimeSpent { get; set; } = string.Empty;
    public string Deadline { get; set; } = string.Empty;
    public string AssignedTo { get; set; } = string.Empty;

    // 🔵 Optional enhancements for future use:
    public string Status { get; set; } = string.Empty; // To Do, In Progress, Done
    public string Priority { get; set; } = string.Empty; // Low, Medium, High
    public string AvatarUrl { get; set; } = string.Empty; // URL or path to user icon
}
