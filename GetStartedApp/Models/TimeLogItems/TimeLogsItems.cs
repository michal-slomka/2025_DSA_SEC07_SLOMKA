using System.Collections.Generic;

namespace GetStartedApp.Models.TimeLogItems;

public class TimeLogItem
{
    public required TimeLog TimeLog { get; init; }
    public required string EmployeeName { get; init; }
    public required string Color { get; init; }
}

public class TaskItem
{
    public required Task Task { get; init; }
    public required List<TimeLogItem> TimeLogItems { get; set; }
}

public class ProjectItem
{
    public required Project Project { get; init; }
    public required List<TaskItem> TaskItems { get; set; }
}
